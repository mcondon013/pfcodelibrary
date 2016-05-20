//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Net.Mail;
using PFTextObjects;
using PFListObjects;

namespace PFNetworkObjects
{
    /// <summary>
    /// Contains routines for both sending and resending emails.
    /// </summary>
    public class PFEmailManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //enEmailTasksStorageType _emailTaskStorageType = enEmailTasksStorageType.XMLFiles;     //enEmailTasksStorageType.Database is not implemented in this copy of PFNetworkObjects.
        private string _emailResendQueueFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "emailResendQueue.xml");

        //private variables for properties
        PFListEx<stEmailArchiveEntry> _emailResendQueue = new PFListEx<stEmailArchiveEntry>();
        private int _maxNumResendAttempts = 5;
        PFListEx<stEmailArchiveEntry> _emailLastResendList = new PFListEx<stEmailArchiveEntry>();

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFEmailManager()
        {
            InitInstance();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSaveConnectionString">Connection string pointing to resend queue file.</param>
        public PFEmailManager(string dataSaveConnectionString)
        {
            _emailResendQueueFile = dataSaveConnectionString;
            InitInstance();
        }

        private void InitInstance()
        {
            if (File.Exists(_emailResendQueueFile) == false)
                _emailResendQueue.SaveToXmlFile(_emailResendQueueFile);

            _emailResendQueue = PFListEx<stEmailArchiveEntry>.LoadFromXmlFile(_emailResendQueueFile);
        }

        //properties

        /// <summary>
        /// Contains list of emails that failed on send and are marked to be resent.
        /// </summary>
        public PFListEx<stEmailArchiveEntry> EmailResendQueue
        {
            get
            {
                return _emailResendQueue;
            }
            set
            {
                _emailResendQueue = value;
            }
        }

        /// <summary>
        /// Sets maximum number of send retries for an email that encounters an SMTP network or security error.
        /// </summary>
        public int MaxNumResendAttempts
        {
            get
            {
                return _maxNumResendAttempts;
            }
            set
            {
                _maxNumResendAttempts = value;
            }
        }

        /// <summary>
        /// Location of file containing entries for the email resend queue.
        /// </summary>
        public string EmailResendQueueFile
        {
            get
            {
                return _emailResendQueueFile;
            }
            set
            {
                _emailResendQueueFile = value;
            }
        }

        /// <summary>
        /// Contains any emails for which latest resend operation was the last one because the email has reached the MaxNumResendAttempts limit.
        /// </summary>
        public PFListEx<stEmailArchiveEntry> EmailLastResendList
        {
            get
            {
                return _emailLastResendList;
            }
            set
            {
                _emailLastResendList = value;
            }
        }


        //methods


        /// <summary>
        /// Sends the specified email.
        /// </summary>
        /// <param name="emailMsg">Email message object.</param>
        /// <returns>True if email sent successfully; otherwise false if an error occurred and email was not sent.</returns>
        public stEmailSendResult SendEmail(PFEmailMessage emailMsg)
        {
            return SendEmail(emailMsg, true);
        }

        /// <summary>
        /// Sends the specified email.
        /// </summary>
        /// <param name="emailMsg">Email message object.</param>
        /// <param name="resendOnError">If true, email manager will attempt to resend a message that fails due to an SMTP error.</param>
        /// <returns>True if email sent successfully; otherwise false if an error occurred and email was not sent.</returns>
        /// <remarks>Email manager will retry up to MaxNumResendAttempts times.</remarks>
        public stEmailSendResult SendEmail(PFEmailMessage emailMsg, bool resendOnError)
        {
            stEmailSendResult result = new stEmailSendResult(enEmailSendResult.Unknown,enEmailFailedReason.Unknown);
            bool queueForResend = false;

            try
            {
                emailMsg.Send();
                result.emailSendResult = enEmailSendResult.Success;
                result.emailFailedReason = enEmailFailedReason.NoError;
                result.failureMessages = string.Empty;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to send email to ");
                _msg.Append(emailMsg.ToAddress);
                _msg.Append(" failed. Error Message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                result.emailSendResult = enEmailSendResult.Failed;
                result.failureMessages = _msg.ToString();
                if (ex is SmtpException)
                {
                    result.emailFailedReason = enEmailFailedReason.SmtpException;
                    queueForResend = true;
                }
                else if (ex is SmtpFailedRecipientException)
                {
                    result.emailFailedReason = enEmailFailedReason.SmtpRecipientsException;
                    queueForResend = true;
                }
                else if (ex is InvalidOperationException)
                {
                    result.emailFailedReason = enEmailFailedReason.InvalidOperationsException;
                }
                else if (ex is ArgumentNullException)
                {
                    result.emailFailedReason = enEmailFailedReason.ArgumentNullException;
                }
                else
                {
                    result.emailFailedReason = enEmailFailedReason.GeneralError;
                }

            }
            finally
            {
                if (queueForResend && resendOnError)
                {
                    stEmailArchiveEntry newEntry = new stEmailArchiveEntry(result, emailMsg);
                    newEntry.firstSendAttempt = DateTime.Now;
                    newEntry.lastSendAttempt = DateTime.Now;
                    _emailResendQueue.Add(newEntry);
                    _emailResendQueue.SaveToXmlFile(_emailResendQueueFile);
                }
            }
                 
            return result;
        }

        /// <summary>
        /// Routine that attempts to resend emails that previously failed.
        /// </summary>
        /// <returns>List of emails that were successfully resent.</returns>
        /// <remarks>Uses internally maintained queue of emails that failed to send.</remarks>
        public PFListEx<stEmailArchiveEntry> ResendEmails()
        {
            PFListEx<stEmailArchiveEntry> successfulResends = new PFListEx<stEmailArchiveEntry>();
            PFListEx<stEmailArchiveEntry> failedResends = new PFListEx<stEmailArchiveEntry>();

            _emailResendQueue = PFListEx<stEmailArchiveEntry>.LoadFromXmlFile(_emailResendQueueFile);

            _emailLastResendList.Clear();

            foreach (stEmailArchiveEntry emailEntry in _emailResendQueue)
            {
                stEmailSendResult res = this.SendEmail(emailEntry.emailMessage, false);

                if (res.emailSendResult == enEmailSendResult.Success)
                {
                    stEmailArchiveEntry successEntry = new stEmailArchiveEntry(res, emailEntry.emailMessage);
                    successEntry.numRetries = emailEntry.numRetries + 1;
                    successEntry.firstSendAttempt = emailEntry.firstSendAttempt;
                    successEntry.lastSendAttempt = DateTime.Now;
                    successfulResends.Add(successEntry);
                }
                else
                {
                    stEmailArchiveEntry failedEntry = new stEmailArchiveEntry(res, emailEntry.emailMessage);
                    failedEntry.firstSendAttempt = emailEntry.firstSendAttempt;
                    failedEntry.lastSendAttempt = DateTime.Now;
                    failedEntry.numRetries = emailEntry.numRetries + 1;
                    failedResends.Add(failedEntry);
                }
            }

            _emailResendQueue.Clear();
            if (failedResends.Count > 0)
            {
                foreach (stEmailArchiveEntry entry in failedResends)
                {
                    if (entry.numRetries < _maxNumResendAttempts)
                    {
                        stEmailArchiveEntry newEntry = new stEmailArchiveEntry(entry.sendResult, entry.emailMessage);
                        newEntry.firstSendAttempt = entry.firstSendAttempt;
                        newEntry.lastSendAttempt = entry.lastSendAttempt;
                        newEntry.numRetries = entry.numRetries;
                        _emailResendQueue.Add(newEntry);
                    }
                    else
                    {
                        stEmailArchiveEntry lastEntry = new stEmailArchiveEntry(entry.sendResult, entry.emailMessage);
                        lastEntry.firstSendAttempt = entry.firstSendAttempt;
                        lastEntry.lastSendAttempt = entry.lastSendAttempt;
                        lastEntry.numRetries = entry.numRetries;
                        _emailLastResendList.Add(lastEntry);
                    }
                }
            }

            _emailResendQueue.SaveToXmlFile(_emailResendQueueFile);

            return successfulResends;
        }


        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFEmailManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFEmailManager.</returns>
        public static PFEmailManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFEmailManager));
            TextReader textReader = new StreamReader(filePath);
            PFEmailManager objectInstance;
            objectInstance = (PFEmailManager)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String containing results.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String containing names and values.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>Strings containing names and values.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFEmailManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFEmailManager.</returns>
        public static PFEmailManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFEmailManager));
            StringReader strReader = new StringReader(xmlString);
            PFEmailManager objectInstance;
            objectInstance = (PFEmailManager)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace

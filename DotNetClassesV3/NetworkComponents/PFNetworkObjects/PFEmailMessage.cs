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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PFNetworkObjects
{
    /// <summary>
    /// Class to send email messages via an SMTP server.
    /// </summary>
    public class PFEmailMessage
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private string _smtpServer = string.Empty;
        private string _senderAddress = string.Empty;
        private string _fromAddress = string.Empty;
        private string _toAddress = string.Empty;
        private string _ccAddress = string.Empty;
        private string _bccAddress = string.Empty;
        private string _messageSubject = string.Empty;
        private string _messageBody = string.Empty;
        private int _sendTimeout = 100000; //milliseconds
        private int _maxIdleTime = 1;
        private int _smtpPort = 25;
        private bool _enableSsl = false;

        SmtpClient _smtpClient = new SmtpClient();
        MailMessage _mailMessage = new MailMessage();

        Char[] _addressDelimiters = new Char[] { ',', ';' };

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFEmailMessage()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="smtpServer">Name or Tcp/Ip address of the SMTP host.</param>
        public PFEmailMessage(string smtpServer)
        {
            this.SMTPServer = smtpServer;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="smtpServer">Name or Tcp/Ip address of the SMTP host.</param>
        /// <param name="fromAddress">Address of sender.</param>
        /// <param name="toAddress">Address of one or more recipients. Separate multiple names with delimiter supported by the host.</param>
        /// <param name="messageSubject">Subject line for this message.</param>
        /// <param name="messageBody">The body of the message..</param>
        public PFEmailMessage(string smtpServer,
                              string fromAddress,
                              string toAddress,
                              string messageSubject,
                              string messageBody)
        {
            this.SMTPServer = smtpServer;
            this.SenderAddress = fromAddress;
            this.FromAddress = fromAddress;
            this.ToAddress = toAddress;
            this.MessageSubject = messageSubject;
            this.MessageBody = messageBody;

        }

        //Properties
        /// <summary>
        /// Gets or sets the name or IP address of the host used for SMTP transactions.
        /// </summary>
        public string SMTPServer
        {
            get
            {
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of delimiters that the SMTP server will recognize. By default, comma (,) and semi-colon (:) are the recognized delimiters.
        /// </summary>
        public Char[] AddressDelimiters
        {
            get
            {
                return _addressDelimiters;
            }
            set
            {
                _addressDelimiters = value;
            }
        }

        /// <summary>
        /// Sender's address.
        /// </summary>
        public string SenderAddress
        {
            get
            {
                return _senderAddress;
            }
            set
            {
                _senderAddress = value;
            }
        }

        /// <summary>
        /// Address of sender of the email.
        /// </summary>
        public string FromAddress
        {
            get
            {
                return _fromAddress;
            }
            set
            {
                _fromAddress = value;
            }
        }

        /// <summary>
        /// Recipient's address. Can include multiple names separated by a delimiter the email system will recognize. (, and ; are recognized by this class)
        /// </summary>
        public string ToAddress
        {
            get
            {
                return _toAddress;
            }
            set
            {
                _toAddress = value;
            }
        }

        /// <summary>
        /// CC's address. Can include multiple names separated by a delimiter the email system will recognize. (, and ; are recognized by this class)
        /// </summary>
        public string CCAddress
        {
            get
            {
                return _ccAddress;
            }
            set
            {
                _ccAddress = value;
            }
        }

        /// <summary>
        /// Bcc's address. Can include multiple names separated by a delimiter the email system will recognize. (, and ; are recognized by this class)
        /// </summary>
        public string BccAddress
        {
            get
            {
                return _bccAddress;
            }
            set
            {
                _bccAddress = value;
            }
        }

        /// <summary>
        /// Subject line for the message.
        /// </summary>
        public string MessageSubject
        {
            get
            {
                return _messageSubject;
            }
            set
            {
                _messageSubject = value;
            }
        }

        /// <summary>
        /// Text that makes up the body of the email.
        /// </summary>
        public string MessageBody
        {
            get
            {
                return _messageBody;
            }
            set
            {
                _messageBody = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies the amount of time after which a synchronous Send call times out.
        /// </summary>
        public int SendTimeout
        {
            get
            {
                return _sendTimeout;
            }
            set
            {
                _sendTimeout = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the amount of time a connection associated with the ServicePoint object can remain idle before the connection is closed.
        /// </summary>
        public int MaxIdleTime
        {
            get
            {
                return _maxIdleTime;
            }
            set
            {
                _maxIdleTime = value;
            }
        }

        /// <summary>
        /// Port used for SMTP transactions
        /// </summary>
        public int SmtpPort
        {
            get
            {
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
            }
        }

        /// <summary>
        /// Specifies whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection. 
        /// </summary>
        public bool EnableSsl
        {
            get
            {
                return _enableSsl;
            }
            set
            {
                _enableSsl = value;
            }
        }

        /// <summary>
        /// Property that allows caller to access the underlying .NET SmtpClient instance.
        /// </summary>
        public SmtpClient SmtpClient
        {
            get
            {
                return _smtpClient;
            }
        }

        
        //methods

        /// <summary>
        /// Sends email message defined by this instance to host identified in SMTPServer property.
        /// </summary>
        public void Send ()
        {
            try
            {
                VerifySendInformation();
                _smtpClient.Host = this.SMTPServer;
                _smtpClient.Timeout = this.SendTimeout;
                _smtpClient.ServicePoint.MaxIdleTime = this.MaxIdleTime;
                _smtpClient.Port = this._smtpPort;

                _mailMessage.From = new MailAddress(this.FromAddress);
                _mailMessage.Sender = new MailAddress(this.SenderAddress);
                GetAddressCollection(_mailMessage.To,this.ToAddress);
                GetAddressCollection(_mailMessage.CC,this.CCAddress);
                GetAddressCollection(_mailMessage.Bcc,this.BccAddress);
                _mailMessage.Subject = this.MessageSubject;
                _mailMessage.Body = this.MessageBody;

                _smtpClient.Send(_mailMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        private void VerifySendInformation()
        {
            StringBuilder errMsg = new StringBuilder();
            errMsg.Length = 0;

            //Assembly a = Assembly.GetExecutingAssembly();
            Type t = this.GetType();

            foreach (PropertyInfo prop in t.GetProperties())
            {
                object o;
                string s;
                o = prop.GetValue(this, null);
                if (o != null)
                {
                    if (o.GetType() == typeof(string))
                    {
                        s = (string)o;
                        s = s.Trim();
                        if (prop.Name == "FromAddress"
                            || prop.Name == "ToAddress"
                            || prop.Name == "MessageSubject"
                            || prop.Name == "MessageBody")
                        {
                            if (s.Length < 1)
                            {
                                errMsg.Append("You must specify ");
                                errMsg.Append(prop.Name);
                                errMsg.Append(".\n");
                            }
                        }
                    }
                }
                else
                {
                    if (prop.Name == "SenderAddress" || prop.Name == "CCAddress" || prop.Name == "BccAddress")
                    {
                        //empty string is allowed for these properties: they are optional for a send
                        errMsg.Append("Do not specify null for ");
                        errMsg.Append(prop.Name);
                        errMsg.Append(". Use empty string to leave blank.\n");
                    }
                    else
                    {
                        //all other string properties must be specified
                        errMsg.Append("Do not specify null or empty string for ");
                        errMsg.Append(prop.Name);
                        errMsg.Append(".\n");
                    }
                }
            }

            if (this.SendTimeout < 1)
            {
                errMsg.Append("Send timeout must be 1 or greater. SendTimeout is specified in milliseconds.");
                errMsg.Append("\n");
            }
            if (errMsg.Length > 0)
                throw new Exception(errMsg.ToString());
        }

        private void GetAddressCollection(MailAddressCollection addressCollection, string delimitedAddresses)
        {
            string[] addressArray=null;
            int inx = 0;
            int startInx = 0;
            int maxInx = 0;

            if (delimitedAddresses.Length>0)
            {
                //addressArray = delimitedAddresses.Split(',');
                addressArray = delimitedAddresses.Split(_addressDelimiters);
                startInx = addressArray.GetLowerBound(0);
                maxInx = addressArray.GetUpperBound(0);
                for (inx = startInx; inx <= maxInx; inx++)
                {
                    addressCollection.Add(new MailAddress(addressArray[inx]));
                }
            }
        }

        //class helpers

        /// <summary>
        /// Outputs the contents of the message in string format.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            //return base.ToString();
            _msg.Length = 0;
            _msg.Append("SMTP Server: <");
            _msg.Append(this.SMTPServer);
            _msg.Append(">\r\n");
            _msg.Append("To: ");
            _msg.Append(this.ToAddress);
            _msg.Append("\r\n");
            if (String.IsNullOrEmpty(this.CCAddress) == false)
            {
                _msg.Append("Cc: ");
                _msg.Append(this.CCAddress);
                _msg.Append("\r\n");
            }
            if (String.IsNullOrEmpty(this.BccAddress) == false)
            {
                _msg.Append("Bcc: ");
                _msg.Append(this.BccAddress);
                _msg.Append("\r\n");
            }
            _msg.Append("From: ");
            _msg.Append(this.FromAddress);
            _msg.Append("\r\n");
            _msg.Append("Subject: ");
            _msg.Append(this.MessageSubject);
            _msg.Append("\r\n");
            _msg.Append("Message:\r\n");
            _msg.Append(this.MessageBody);
            _msg.Append("\r\n");

            return _msg.ToString();
        }


        /// <summary>
        /// Routine outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String containing results.</returns>
        public string OutputObjectContents()
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
        /// <returns>String containing names and values.</returns>
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


        //routines to load and save instances of this class
        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFEmailMessage));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFEmailMessage.</returns>
        public static PFEmailMessage LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFEmailMessage));
            TextReader textReader = new StreamReader(filePath);
            PFEmailMessage objectInstance;
            objectInstance = (PFEmailMessage)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }

        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFEmailMessage));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFEmailMessage.</returns>
        public static PFEmailMessage LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFEmailMessage));
            StringReader strReader = new StringReader(xmlString);
            PFEmailMessage objectInstance;
            objectInstance = (PFEmailMessage)deserializer.Deserialize(strReader);
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

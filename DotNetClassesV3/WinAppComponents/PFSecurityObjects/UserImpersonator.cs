using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;
using System.Reflection;

namespace PFSecurityObjects
{
    /// <summary>
    /// Class implements code to impersonate another user while executing the current application.
    /// </summary>
    public class UserImpersonator
    {
#pragma warning disable 1591
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private unsafe static extern int FormatMessage(int dwFlags, ref IntPtr lpSource,
            int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, IntPtr* Arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        private const int LOGON32_PROVIDER_DEFAULT = 0;
        //This parameter causes LogonUser to create a primary token.
        private const int LOGON32_LOGON_INTERACTIVE = 2;

        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //class work variables (not displayed as properties)
        private WindowsIdentity _newId = null;
        private WindowsImpersonationContext _impersonatedUser = null;
        private WindowsIdentity _originalId = null;

        //class variables for properties
        private string _domain = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
#pragma warning restore 1591


        //properties

        /// <summary>
        /// Domain on which user id is defined.
        /// </summary>
        public string Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        /// <summary>
        /// Logon username.
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        /// <summary>
        /// Logon password.
        /// </summary>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        //methods

        /// <summary>
        /// Attempts to impersonate the user identified in the Domain, Username and Password properties.
        /// </summary>
        /// <returns></returns>

        public bool Impersonate()
        {
            return Impersonate(_domain, _username, _password);
        }

        /// <summary>
        /// Attempts to impersonate the user specified in the parameters passed to this method.
        /// </summary>
        /// <param name="domain">Domain user is defined in.</param>
        /// <param name="username">Logon username.</param>
        /// <param name="password">Logon password.</param>
        /// <returns>True if impersonate succeeded.</returns>
        public bool Impersonate(string domain, string username, string password)
        {
            bool impersonateSucceeded = false;
            bool logonSucceeded = false;
            IntPtr tokenHandle = new IntPtr(0);
            IntPtr dupeTokenHandle = new IntPtr(0);
            StringBuilder errorMessage = new StringBuilder();
            StringBuilder userToImpersonate = new StringBuilder();
            string newUser = string.Empty;

            try
            {
                tokenHandle = IntPtr.Zero;

                if (StringLength(domain)==0 || StringLength(password)==0)
                    throw new System.Exception("Domain and username must be specified for Impersonate method");



                _originalId = WindowsIdentity.GetCurrent();

                userToImpersonate.Length = 0;
                userToImpersonate.Append(domain);
                userToImpersonate.Append(@"\");
                userToImpersonate.Append(username);

                // Call LogonUser to obtain a handle to an access token.
                logonSucceeded = LogonUser(username, domain, password,
                                           LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                                           ref tokenHandle);

                if (!logonSucceeded)
                {
                    int ret = Marshal.GetLastWin32Error();
                    errorMessage.Length = 0;
                    errorMessage.Append("Logon failed with error code: ");
                    errorMessage.Append(ret.ToString());
                    throw new System.ComponentModel.Win32Exception(ret, errorMessage.ToString());
                }

                //WindowsIdentity newId = new WindowsIdentity(tokenHandle);
                //WindowsImpersonationContext impersonatedUser = newId.Impersonate();

                _newId = new WindowsIdentity(tokenHandle);
                _impersonatedUser = _newId.Impersonate();

                newUser = WindowsIdentity.GetCurrent().Name;

                if (newUser.ToUpper() == userToImpersonate.ToString().ToUpper())
                    impersonateSucceeded = true;

                this.Domain = domain;
                this.Username = username;
                this.Password = password;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return impersonateSucceeded;
        }


        /// <summary>
        /// Reverts current user to user credentials that were active before impersonate was done.
        /// </summary>
        /// <returns>True if user credentials successfully reverted.</returns>
        public bool Revert()
        {
            bool revertSucceeded = false;
            WindowsIdentity currentId = null;

            try
            {
                if (_impersonatedUser != null)
                {
                    _impersonatedUser.Undo();
                    _impersonatedUser = null;
                    currentId = WindowsIdentity.GetCurrent();
                    if (currentId.Name == _originalId.Name)
                        revertSucceeded = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return revertSucceeded;
        }

        /// <summary>
        /// Returns name of the current user.
        /// </summary>
        /// <returns></returns>
        public static string CurrentUser()
        {
            return WindowsIdentity.GetCurrent().Name;
        }

        private int StringLength(string stringValue)
        {
            int len = 0;
            if (stringValue == null)
                len = 0;
            else
                len = stringValue.Length;
            return len;
        }

        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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




    }//end class
}//end namespace

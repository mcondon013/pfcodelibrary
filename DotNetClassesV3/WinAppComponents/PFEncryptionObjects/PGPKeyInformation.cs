//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using DidiSoft.Pgp;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PGPKeyInformation
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _algorithm; 
        private DateTime _creationTime; 
        private bool _encryptionKey; 
        private string _fingerprint; 
        private bool _hasPrivateKey; 
        private bool _isExpired; 
        private bool _isLegacyRSAKey; 
        private long _keyId; 
        private string _keyIdHex; 
        private int _keySize; 
        private pfPGPCompressionAlgorithm[] _preferredCompressions; 
        private pfPGPCypherAlgorithm[] _preferredCyphers; 
        private pfPGPHashAlgorithm[] _preferredHashes;
        private bool _hasPrivateKeyRing;
        private bool _hasPrivateSubKeys;
        private bool _hasPublicKeyRing;
        private bool _hasPublicSubKeys;
        private bool _revoked; 
        private long[] _signedWithKeyIds; 
        private bool _signingKey; 
        private pfPGPTrustLevel _trust; 
        private string _userId; 
        private string[] _userIds; 
        private int _validDays;

        //private PgpSecretKeyRing _privateRing;
        //private KeyPairInformation.SubKey[] _privateSubKeys;
        //private PgpPublicKeyRing _publicRing;
        //private KeyPairInformation.SubKey[] _publicSubKeys; 

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        internal PGPKeyInformation()
        {
            ;
        }

        //properties

        /// <summary>
        /// Algorithm Property.
        /// </summary>
        public string Algorithm
        {
            get
            {
                return _algorithm;
            }
            set
            {
                _algorithm = value;
            }
        }

        /// <summary>
        /// CreationTime Property.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return _creationTime;
            }
            set
            {
                _creationTime = value;
            }
        }

        /// <summary>
        /// EncryptionKey Property.
        /// </summary>
        public bool EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
            set
            {
                _encryptionKey = value;
            }
        }

        /// <summary>
        /// Fingerprint Property.
        /// </summary>
        public string Fingerprint
        {
            get
            {
                return _fingerprint;
            }
            set
            {
                _fingerprint = value;
            }
        }

        /// <summary>
        /// HasPrivateKey Property.
        /// </summary>
        public bool HasPrivateKey
        {
            get
            {
                return _hasPrivateKey;
            }
            set
            {
                _hasPrivateKey = value;
            }
        }

        /// <summary>
        /// IsExpired Property.
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return _isExpired;
            }
            set
            {
                _isExpired = value;
            }
        }

        /// <summary>
        /// IsLegacyRSAKey Property.
        /// </summary>
        public bool IsLegacyRSAKey
        {
            get
            {
                return _isLegacyRSAKey;
            }
            set
            {
                _isLegacyRSAKey = value;
            }
        }

        /// <summary>
        /// KeyId Property.
        /// </summary>
        public long KeyId
        {
            get
            {
                return _keyId;
            }
            set
            {
                _keyId = value;
            }
        }

        /// <summary>
        /// KeyIdHex Property.
        /// </summary>
        public string KeyIdHex
        {
            get
            {
                return _keyIdHex;
            }
            set
            {
                _keyIdHex = value;
            }
        }

        /// <summary>
        /// KeySize Property.
        /// </summary>
        public int KeySize
        {
            get
            {
                return _keySize;
            }
            set
            {
                _keySize = value;
            }
        }

        /// <summary>
        /// PreferredCompressions Property.
        /// </summary>
        public pfPGPCompressionAlgorithm[] PreferredCompressions
        {
            get
            {
                return _preferredCompressions;
            }
            set
            {
                _preferredCompressions = value;
            }
        }

        /// <summary>
        /// PreferredCyphers Property.
        /// </summary>
        public pfPGPCypherAlgorithm[] PreferredCyphers
        {
            get
            {
                return _preferredCyphers;
            }
            set
            {
                _preferredCyphers = value;
            }
        }

        /// <summary>
        /// PreferredHashes Property.
        /// </summary>
        public pfPGPHashAlgorithm[] PreferredHashes
        {
            get
            {
                return _preferredHashes;
            }
            set
            {
                _preferredHashes = value;
            }
        }

        /// <summary>
        /// HasPrivateKeyRing Property.
        /// </summary>
        public bool HasPrivateKeyRing
        {
            get
            {
                return _hasPrivateKeyRing;
            }
            set
            {
                _hasPrivateKeyRing = value;
            }
        }

        /// <summary>
        /// HasPrivateSubKeys Property.
        /// </summary>
        public bool HasPrivateSubKeys
        {
            get
            {
                return _hasPrivateSubKeys;
            }
            set
            {
                _hasPrivateSubKeys = value;
            }
        }

        /// <summary>
        /// HasPublicKeyRing Property.
        /// </summary>
        public bool HasPublicKeyRing
        {
            get
            {
                return _hasPublicKeyRing;
            }
            set
            {
                _hasPublicKeyRing = value;
            }
        }

        /// <summary>
        /// HasPublicSubKeys Property.
        /// </summary>
        public bool HasPublicSubKeys
        {
            get
            {
                return _hasPublicSubKeys;
            }
            set
            {
                _hasPublicSubKeys = value;
            }
        }

        /// <summary>
        /// Revoked Property.
        /// </summary>
        public bool Revoked
        {
            get
            {
                return _revoked;
            }
            set
            {
                _revoked = value;
            }
        }

        /// <summary>
        /// SignedWithKeyIds Property.
        /// </summary>
        public long[] SignedWithKeyIds
        {
            get
            {
                return _signedWithKeyIds;
            }
            set
            {
                _signedWithKeyIds = value;
            }
        }

        /// <summary>
        /// SigningKey Property.
        /// </summary>
        public bool SigningKey
        {
            get
            {
                return _signingKey;
            }
            set
            {
                _signingKey = value;
            }
        }

        /// <summary>
        /// Trust Property.
        /// </summary>
        public pfPGPTrustLevel Trust
        {
            get
            {
                return _trust;
            }
            set
            {
                _trust = value;
            }
        }

        /// <summary>
        /// UserId Property.
        /// </summary>
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        /// <summary>
        /// UserIds Property.
        /// </summary>
        public string[] UserIds
        {
            get
            {
                return _userIds;
            }
            set
            {
                _userIds = value;
            }
        }

        /// <summary>
        /// ValidDays Property.
        /// </summary>
        public int ValidDays
        {
            get
            {
                return _validDays;
            }
            set
            {
                _validDays = value;
            }
        }




        //methods

        internal void LoadPropertyValuesFromKeyPairInformation(KeyPairInformation kpi)
        {
        }

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PGPKeyInformation));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PGPKeyInformation.</returns>
        public static PGPKeyInformation LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PGPKeyInformation));
            TextReader textReader = new StreamReader(filePath);
            PGPKeyInformation columnDefinitions;
            columnDefinitions = (PGPKeyInformation)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name and value for all public properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("  ");
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                //if (prop.Name == "TestStringArray")
                //{
                //    for (int i = 0; i < this.TestStringArray.Length; i++)
                //    {
                //        data.Append("Index ");
                //        data.Append(i.ToString("#,##0"));
                //        data.Append(": ");
                //        data.Append(this.TestStringArray[i].ToString());
                //        data.Append("  ");
                //    }
                //}

            }

            return data.ToString();
        }

        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PGPKeyInformation));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
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

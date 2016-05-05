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
using System.IO;
using DidiSoft.Pgp;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class implements PGP encryption functionality.
    /// </summary>
    /// 
    public class PGPEncryptor
    {

        
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties
        private PGPLib _pgp = new PGPLib();
        private CypherAlgorithm _cypher = CypherAlgorithm.CAST5;
        private HashAlgorithm _hash = HashAlgorithm.SHA1;
        private CompressionAlgorithm _compression = CompressionAlgorithm.ZIP;
        private bool _asciiArmor = true;
        private bool _withIntegrityCheck = false;

        private string[] _availableCypherAlgorithms;
        private string[] _availableHashAlgorithms;
        private string[] _availableCompressionAlgorithms;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PGPEncryptor()
        {
            InitializeInstance();
        }

        private void InitializeInstance()
        {
            // Load available Cypher algorithms
            List<string>items = new List<string>();
            foreach (CypherAlgorithm alg in Enum.GetValues(typeof(CypherAlgorithm)))
            {
                items.Add(alg.ToString());
            }
            _availableCypherAlgorithms = items.ToArray();

            // Load available Hash algorithms
            items.Clear();
            foreach (HashAlgorithm alg in Enum.GetValues(typeof(HashAlgorithm)))
            {
                items.Add(alg.ToString());
            }
            _availableHashAlgorithms = items.ToArray();

            // Load available Compression algorithms
            items.Clear();
            foreach (CompressionAlgorithm alg in Enum.GetValues(typeof(CompressionAlgorithm)))
            {
                items.Add(alg.ToString());
            }
            _availableCompressionAlgorithms = items.ToArray();
        
        }

        //properties
        /// <summary>
        /// Pgp engines used for encryption. Provides access to an expanded set of encryption and decryption methods.
        /// </summary>
        /// <remarks>To use this object, the caller must have a reference to the DidiSoft.Pgp namespace.</remarks>
        public DidiSoft.Pgp.PGPLib Pgp
        {
            get
            {
                return _pgp;
            }
        }

        /// <summary>
        /// The encryption algorithm to use.
        /// </summary>
        public pfPGPCypherAlgorithm Cypher
        {
            get
            {
                return (pfPGPCypherAlgorithm)_cypher;
            }
            set
            {
                _cypher = (CypherAlgorithm)value;
                _pgp.Cypher = _cypher;
            }
        }

        /// <summary>
        /// Hash algorithm to use.
        /// </summary>
        public pfPGPHashAlgorithm Hash
        {
            get
            {
                return (pfPGPHashAlgorithm)_hash;
            }
            set
            {
                _hash = (HashAlgorithm)value;
                _pgp.Hash = _hash;
            }
        }

        /// <summary>
        /// Type of Compression to use.
        /// </summary>
        public pfPGPCompressionAlgorithm Compression
        {
            get
            {
                return (pfPGPCompressionAlgorithm)_compression;
            }
            set
            {
                _compression = (CompressionAlgorithm)value;
                _pgp.Compression = _compression;
            }
        }

        /// <summary>
        /// AsciiArmor Property. Determines whether or not to encode binary data as a sequence of characters. Useful if transmission channel does not allow binary data.
        /// </summary>
        public bool AsciiArmor
        {
            get
            {
                return _asciiArmor;
            }
            set
            {
                _asciiArmor = value;
            }
        }

        /// <summary>
        /// WithIntegrityCheck Property. If true, integrity check information is added to the output.
        /// </summary>
        public bool WithIntegrityCheck
        {
            get
            {
                return _withIntegrityCheck;
            }
            set
            {
                _withIntegrityCheck = value;
            }
        }

        /// <summary>
        /// Returns an array of strings representing the available encryption cypher algorithms for use with this class.
        /// </summary>
        public string[] AvailableCypherAlgorithms
        {
            get
            {
                return _availableCypherAlgorithms;
            }
            set
            {
                _availableCypherAlgorithms = value;
            }
        }

        /// <summary>
        /// Returns an array of strings representing the available hash algorithms for use with this class.
        /// </summary>
        public string[] AvailableHashAlgorithms
        {
            get
            {
                return _availableHashAlgorithms;
            }
            set
            {
                _availableHashAlgorithms = value;
            }
        }

        /// <summary>
        /// Returns an array of strings representing the available compression algorithms for use with this class.
        /// </summary>
        public string[] AvailableCompressionAlgorithms
        {
            get
            {
                return _availableCompressionAlgorithms;
            }
            set
            {
                _availableCompressionAlgorithms = value;
            }
        }



        //methods

        //NOTE: Methods with PBE at the end of the name: Password Based Encryption.
        //      Recipient will use the same password used for encryption for decryption.
        //      This is considered less secure than standard PGP methods.

        /// <summary>
        /// Encrypts string using PGP.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <param name="publicKeyFile">Path to public key file. Usually a .asc file.</param>
        /// <returns>Encrypted string. It is always ascii armored.</returns>
        public string EncryptString(string stringToEncrypt, string publicKeyFile)
        {
            string encryptedString = string.Empty;

            encryptedString = _pgp.EncryptString(stringToEncrypt, new FileInfo(publicKeyFile));

            return encryptedString;
        }

        /// <summary>
        /// Encrypts string using PGP.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="userid">UserId in the Key Store to use for this encryption.</param>
        /// <returns></returns>
        public string EncryptString(string stringToEncrypt, string keyStoreFile, string keyStorePassword, string userid)
        {
            string encryptedString = string.Empty;
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword); 

            encryptedString = _pgp.EncryptString(stringToEncrypt, ks, userid);

            return encryptedString;
        }

        /// <summary>
        /// Decrypts string using PGP.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <param name="privateKeyFile">Path to the private key file. Usually a .asc file.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <returns>Decrypted string.</returns>
        /// <remarks>Encrypted string must be ascii armored.</remarks>
        public string DecryptString(string stringToDecrypt, string privateKeyFile, string privateKeyPassword)
        {
            string decryptedString = string.Empty;

            decryptedString = _pgp.DecryptString(stringToDecrypt, new FileInfo(privateKeyFile), privateKeyPassword);

            return decryptedString;
        }

        /// <summary>
        /// Decrypts string using PGP.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this decryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <returns></returns>
        public string DecryptString(string stringToDecrypt, string keyStoreFile, string keyStorePassword, string privateKeyPassword)
        {
            string decryptedString = string.Empty;
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword); 

            decryptedString = _pgp.DecryptString(stringToDecrypt, ks, privateKeyPassword);

            return decryptedString;
        }

        /// <summary>
        /// Encrypts a MemoryStream using PGP.
        /// </summary>
        /// <param name="inputStream">Stream containing data to be encrypted.</param>
        /// <param name="publicKeyFile">Path to the private key file. Usually a .asc file.</param>
        /// <returns>MemoryStream with encrypted data. It is always ascii armored.</returns>
        public MemoryStream EncryptStream(MemoryStream inputStream, string publicKeyFile)
        {
            MemoryStream encryptedStream = new MemoryStream();

            _pgp.EncryptStream(inputStream, "input.txt", new FileInfo(publicKeyFile), encryptedStream, true);

            return encryptedStream;
        }

        /// <summary>
        /// Encrypts a MemoryStream using PGP.
        /// </summary>
        /// <param name="inputStream">Stream containing data to be encrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="userid">UserId in the Key Store to use for this encryption.</param>
        /// <returns>MemoryStream with encrypted data. It is always ascii armored.</returns>
        public MemoryStream EncryptStream(MemoryStream inputStream, string keyStoreFile, string keyStorePassword, string userid)
        {
            MemoryStream encryptedStream = new MemoryStream();
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword); 

            _pgp.EncryptStream(inputStream, "input.txt", ks, userid, encryptedStream, true);

            return encryptedStream;
        }

        /// <summary>
        /// Decrypts MemoryStream using PGP.
        /// </summary>
        /// <param name="encryptedStream">MemoryStream containing data to be decrypted</param>
        /// <param name="privateKeyFile">Path to the private key file. Usually a .asc file.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <returns>MemoryStream containing decrypted data.</returns>
        /// <remarks>MemoryStream containing encrypted data must be ascii armored.</remarks>
        public MemoryStream DecryptStream(MemoryStream encryptedStream, string privateKeyFile, string privateKeyPassword)
        {
            MemoryStream decryptedStream = new MemoryStream();
            FileStream privateKeyStream = File.OpenRead(privateKeyFile);

            _pgp.DecryptStream(encryptedStream, privateKeyStream, privateKeyPassword, decryptedStream);

            return decryptedStream;
        }

        /// <summary>
        /// Decrypts MemoryStream using PGP.
        /// </summary>
        /// <param name="encryptedStream">MemoryStream containing data to be decrypted</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <returns>MemoryStream containing decrypted data.</returns>
        /// <remarks>MemoryStream containing encrypted data must be ascii armored.</remarks>
        public MemoryStream DecryptStream(MemoryStream encryptedStream, string keyStoreFile, string keyStorePassword, string privateKeyPassword)
        {
            MemoryStream decryptedStream = new MemoryStream();
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword); 

            _pgp.DecryptStream(encryptedStream, ks, privateKeyPassword, decryptedStream);

            return decryptedStream;
        }

        /// <summary>
        /// Method to encrypt a file.
        /// </summary>
        /// <param name="fileToEncrypt">File containing the data to be encrypted.</param>
        /// <param name="publicKeyFile">File containing the public key.</param>
        /// <param name="encryptedFile">Output file containing encrypted data.</param>
        /// <param name="asciiArmor">Should the encrypted file be in ASCII Armored format. If false the encrypted file is in binary format.</param>
        public void EncryptFile(string fileToEncrypt, string publicKeyFile, string encryptedFile, bool asciiArmor)
        {
            this.EncryptFile(fileToEncrypt, publicKeyFile, encryptedFile, asciiArmor, false);
        }

        /// <summary>
        /// Method to encrypt a file.
        /// </summary>
        /// <param name="fileToEncrypt">File containing the data to be encrypted.</param>
        /// <param name="publicKeyFile">File containing the public key.</param>
        /// <param name="encryptedFile">Output file containing encrypted data.</param>
        /// <param name="asciiArmor">Should the encrypted file be in ASCII Armored format. If false the encrypted file is in binary format.</param>
        /// <param name="withIntegrityCheck">Should integrity check information be added to the encrypted file.</param>
        public void EncryptFile(string fileToEncrypt, string publicKeyFile, string encryptedFile, bool asciiArmor, bool withIntegrityCheck)
        {
            _pgp.EncryptFile(new FileInfo(fileToEncrypt), new FileInfo(publicKeyFile), new FileInfo(encryptedFile), asciiArmor, withIntegrityCheck);
        }

        /// <summary>
        /// Method to encrypt a file.
        /// </summary>
        /// <param name="fileToEncrypt">File containing the data to be encrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="userid">UserId in the Key Store to use for this encryption.</param>
        /// <param name="encryptedFile">Output file containing encrypted data.</param>
        /// <param name="asciiArmor">Should the encrypted file be in ASCII Armored format. If false the encrypted file is in binary format.</param>
        public void EncryptFile(string fileToEncrypt, string keyStoreFile, string keyStorePassword, string userid, string encryptedFile, bool asciiArmor)
        {
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword);

            this.EncryptFile(fileToEncrypt, keyStoreFile, keyStorePassword, userid, encryptedFile, asciiArmor, false);
        }

        /// <summary>
        /// Method to encrypt a file.
        /// </summary>
        /// <param name="fileToEncrypt">File containing the data to be encrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="userid">UserId in the Key Store to use for this encryption.</param>
        /// <param name="encryptedFile">Output file containing encrypted data.</param>
        /// <param name="asciiArmor">Should the encrypted file be in ASCII Armored format. If false the encrypted file is in binary format.</param>
        /// <param name="withIntegrityCheck">Should integrity check information be added to the encrypted file.</param>
        public void EncryptFile(string fileToEncrypt, string keyStoreFile, string keyStorePassword, string userid, string encryptedFile, bool asciiArmor, bool withIntegrityCheck)
        {
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword); 

            _pgp.EncryptFile(fileToEncrypt, ks, userid,  encryptedFile, asciiArmor, withIntegrityCheck);
        
        }

        
        /// <summary>
        /// Method to decrypt a file.
        /// </summary>
        /// <param name="encryptedFile">File containing data to be decrypted.</param>
        /// <param name="privateKeyFile">File containing private key.</param>
        /// <param name="privateKeyPassword">Private Key password.</param>
        /// <param name="decryptedFile">Output file containing the decrypted data.</param>
        public void DecryptFile(string encryptedFile, string privateKeyFile, string privateKeyPassword, string decryptedFile)
        {
            _pgp.DecryptFile(new FileInfo(encryptedFile), new FileInfo(privateKeyFile), privateKeyPassword, new FileInfo(decryptedFile));
        }

        /// <summary>
        /// Method to decrypt a file.
        /// </summary>
        /// <param name="encryptedFile">File containing data to be decrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="privateKeyPassword">Private Key password.</param>
        /// <param name="decryptedFile">Output file containing the decrypted data.</param>
        public void DecryptFile(string encryptedFile, string keyStoreFile, string keyStorePassword, string privateKeyPassword, string decryptedFile)
        {
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword);

            _pgp.DecryptFile(encryptedFile, ks, privateKeyPassword, decryptedFile);
        }


        /// <summary>
        /// Encrypts a FileStream using PGP.
        /// </summary>
        /// <param name="inputStream">Stream containing data to be encrypted.</param>
        /// <param name="publicKeyFile">Path to the private key file. Usually a .asc file.</param>
        /// <param name="encryptedFile">Path to file encapsulated by the FileStream.</param>
        /// <param name="asciiArmor">Specifies whether or not encrypted data is to be ascii armored.</param>
        /// <returns>FileStream with encrypted data.</returns>
        public FileStream EncryptStream(FileStream inputStream, string publicKeyFile, string encryptedFile, bool asciiArmor)
        {
            FileStream encryptedStream = new FileStream(encryptedFile, FileMode.Create);

            _pgp.EncryptStream(inputStream, "input.txt", new FileInfo(publicKeyFile), encryptedStream, asciiArmor);

            return encryptedStream;
        }

        /// <summary>
        /// Encrypts a FileStream using PGP.
        /// </summary>
        /// <param name="inputStream">Stream containing data to be encrypted.</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="userid">UserId in the Key Store to use for this encryption.</param>
        /// <param name="encryptedFile">Path to file encapsulated by the FileStream.</param>
        /// <param name="asciiArmor">Specifies whether or not encrypted data is to be ascii armored.</param>
        /// <returns>FileStream with encrypted data.</returns>
        public FileStream EncryptStream(FileStream inputStream, string keyStoreFile, string keyStorePassword, string userid, string encryptedFile, bool asciiArmor)
        {
            FileStream encryptedStream = new FileStream(encryptedFile, FileMode.Create);
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword);

            _pgp.EncryptStream(inputStream, "input.txt", ks, userid, encryptedStream, asciiArmor);

            return encryptedStream;
        }

        /// <summary>
        /// Decrypts FileStream using PGP.
        /// </summary>
        /// <param name="encryptedStream">FileStream containing data to be decrypted</param>
        /// <param name="privateKeyFile">Path to the private key file. Usually a .asc file.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <param name="decryptedFile">Path to file encapsulated by the FileStream.</param>
        /// <returns>FileStream containing decrypted data.</returns>
        public FileStream DecryptStream(FileStream encryptedStream, string privateKeyFile, string privateKeyPassword, string decryptedFile)
        {
            FileStream decryptedStream = new FileStream(decryptedFile, FileMode.Create);
            FileStream privateKeyStream = File.OpenRead(privateKeyFile);

            _pgp.DecryptStream(encryptedStream, privateKeyStream, privateKeyPassword, decryptedStream);

            return decryptedStream;
        }

        /// <summary>
        /// Decrypts FileStream using PGP.
        /// </summary>
        /// <param name="encryptedStream">FileStream containing data to be decrypted</param>
        /// <param name="keyStoreFile">Path to Key Store to be used for this encryption.</param>
        /// <param name="keyStorePassword">Password for the Key Store.</param>
        /// <param name="privateKeyPassword">Password to use.</param>
        /// <param name="decryptedFile">Path to file encapsulated by the FileStream.</param>
        /// <returns>FileStream containing decrypted data.</returns>
        public FileStream DecryptStream(FileStream encryptedStream, string keyStoreFile, string keyStorePassword, string privateKeyPassword, string decryptedFile)
        {
            FileStream decryptedStream = new FileStream(decryptedFile, FileMode.Create);
            KeyStore ks = new KeyStore(keyStoreFile, keyStorePassword);

            _pgp.DecryptStream(encryptedStream, ks, privateKeyPassword, decryptedStream);

            return decryptedStream;
        }


        //*******************************************
        //class helpers
        //*******************************************

        /// <summary>
        /// Converts text name of algorithm to a pfPGPCypherAlgorithm object.
        /// </summary>
        /// <param name="cypherDescription">Text of algorithm name.</param>
        /// <returns>pfPGPCypherAlgorithm object corresponding to algorithm name.</returns>
        public pfPGPCypherAlgorithm ToCypherAlgorithm(string cypherDescription)
        {
            pfPGPCypherAlgorithm alg = (pfPGPCypherAlgorithm)Enum.Parse(typeof(pfPGPCypherAlgorithm), cypherDescription);
            return alg;
        }

        /// <summary>
        /// Converts text name of algorithm to a pfPGPHashAlgorithm object.
        /// </summary>
        /// <param name="hashDescription">Text of algorithm name.</param>
        /// <returns>pfPGPHashAlgorithm object corresponding to algorithm name.</returns>
        public pfPGPHashAlgorithm ToHashAlgorithm(string hashDescription)
        {
            pfPGPHashAlgorithm alg = (pfPGPHashAlgorithm)Enum.Parse(typeof(pfPGPHashAlgorithm), hashDescription);
            return alg;
        }

        /// <summary>
        /// Converts text name of algorithm to a pfPGPCompressionAlgorithm object.
        /// </summary>
        /// <param name="compressionDescription">Text of algorithm name.</param>
        /// <returns>pfPGPCompressionAlgorithm object corresponding to algorithm name.</returns>
        public pfPGPCompressionAlgorithm ToCompressionAlgorithm(string compressionDescription)
        {
            pfPGPCompressionAlgorithm alg = (pfPGPCompressionAlgorithm)Enum.Parse(typeof(pfPGPCompressionAlgorithm), compressionDescription);
            return alg;
        }




        /// <summary>
        /// Routine overrides default ToString method and outputs name and value for all public properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();

            data.Append("Class Type: ");
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
            }

            return data.ToString();
        }


    }//end class
}//end namespace

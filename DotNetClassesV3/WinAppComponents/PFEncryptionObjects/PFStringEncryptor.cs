//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using AppGlobals;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Alternative class for encryption of strings using symmetric algorithms (AES, DES, TripleDES).
    /// Does not use interface approach. 
    /// Allows logic to be concentrated in one module.cle
    /// </summary>
    public class PFStringEncryptor
    {

        //AesCryptoServiceProvider
        //        Legal min key size = 128
        //        Legal max key size = 256
        //        Legal min block size = 128
        //        Legal max block size = 128

        //DESCryptoServiceProvider 
        //        Legal min key size = 64 
        //        Legal max key size = 64 
        //        Legal min block size = 64 
        //        Legal max block size = 64 

        //TripleDESCryptoServiceProvider 
        //        Legal min key size = 128 
        //        Legal max key size = 192 
        //        Legal min block size = 64 
        //        Legal max block size = 64

        //IV = Initialization Vector (randomizes the encryption)

        pfEncryptionAlgorithm _encryptionAlgorithm = pfEncryptionAlgorithm.NotSpecified;
        private byte[] _defaultKey = ASCIIEncoding.ASCII.GetBytes(" ");
        private byte[] _defaultIv = ASCIIEncoding.ASCII.GetBytes(" ");
        private byte[] _key;
        private byte[] _iv;
        private SymmetricAlgorithm _cryptoProvider;
        private KeySizes[] _validKeySizes;
        private KeySizes[] _validIVSizes;
        private List<pfLegalKeySize> _legalKeySizes = new List<pfLegalKeySize>();
        private List<pfLegalBlockSize> _legalBlockSizes = new List<pfLegalBlockSize>();
        private StringBuilder _str = new StringBuilder();
        private StringBuilder _msg = new StringBuilder();

        //constructor
        /// <summary>
        /// Constructor initializes the Key and Iv values to defaults upon object creation. 
        /// Use Key and Iv properties to customize values to your application's requirements.
        /// </summary>
        /// <param name="alg">Encryption algorithm to use for this instance.</param>
        public PFStringEncryptor(pfEncryptionAlgorithm alg)
        {
            switch (alg)
            {
                case pfEncryptionAlgorithm.AES:
                    _cryptoProvider = new AesCryptoServiceProvider();
                    break;
                case pfEncryptionAlgorithm.DES:
                    _cryptoProvider = new DESCryptoServiceProvider();
                    break;
                case pfEncryptionAlgorithm.TripleDES:
                    _cryptoProvider = new TripleDESCryptoServiceProvider();
                    break;
                default:
                    _msg.Length = 0;
                    _msg.Append("Invalid or unexpected encryption algorithm: ");
                    _msg.Append(alg.ToString());
                    throw new System.Exception(_msg.ToString());
            }
            _encryptionAlgorithm = alg;
            Initialize();
        }

        private void Initialize()
        {
            _validKeySizes = _cryptoProvider.LegalKeySizes;
            _validIVSizes = _cryptoProvider.LegalBlockSizes;

            _key = _defaultKey;
            _iv = _defaultIv;

            KeySizes[] ks = _cryptoProvider.LegalKeySizes;
            foreach (KeySizes k in ks)
            {
                _legalKeySizes.Add(new pfLegalKeySize(k.MinSize, k.MaxSize));
            }

            ks = _cryptoProvider.LegalBlockSizes;
            foreach (KeySizes k in ks)
            {
                _legalBlockSizes.Add(new pfLegalBlockSize(k.MinSize, k.MaxSize));
            }

        }

        //properties

        /// <summary>
        /// Encryption algorithm used by this instance of the class.
        /// </summary>
        public pfEncryptionAlgorithm EncryptionAlgorithm
        {
            get
            {
                return _encryptionAlgorithm;
            }
            set
            {
                _encryptionAlgorithm = value;
            }
        }

        /// <summary>
        /// Gets or sets the secret key for the symmetric algorithm. 
        /// </summary>
        public string Key
        {
            get
            {
                string retValue = string.Empty;
                if (_key != null)
                    if (_key.Length > 0)
                        retValue = ASCIIEncoding.ASCII.GetString(_key);
                return retValue;
            }
            set
            {
                string newValue = string.Empty;
                if (value != null)
                    if (value.Length > 0)
                        newValue = value;
                _key = ASCIIEncoding.ASCII.GetBytes(newValue);
            }
        }

        /// <summary>
        /// Gets or sets the initialization vector (IV) for the symmetric algorithm.
        /// </summary>
        public string IV
        {
            get
            {
                string retValue = string.Empty;
                if (_iv != null)
                    if (_iv.Length > 0)
                        retValue = ASCIIEncoding.ASCII.GetString(_iv);
                return retValue;
            }
            set
            {
                string newValue = string.Empty;
                if (value != null)
                    if (value.Length > 0)
                        newValue = value;
                _iv = ASCIIEncoding.ASCII.GetBytes(newValue);
            }
        }

        /// <summary>
        /// List of valid key sizes for the algorithm.
        /// This is a read-only property.
        /// </summary>
        public List<pfLegalKeySize> LegalKeySizes
        {
            get
            {
                return _legalKeySizes;
            }
        }

        /// <summary>
        /// List of valid block sizes for the algorithm.
        /// This is a read-only property.
        /// </summary>
        public List<pfLegalBlockSize> LegalBlockSizes
        {
            get
            {
                return _legalBlockSizes;
            }
        }

        //methods
        /// <summary>
        /// Verifies the key is a valid size.
        /// </summary>
        /// <param name="keyString">
        /// String containing the Key to be validated.
        /// </param>
        /// <returns>
        /// True if key is a valid size; otherwise False.
        /// </returns>
        public bool KeyIsValid(string keyString)
        {
            bool retVal = false;
            if (keyString != null)
            {
                int keylen = keyString.Length * 8;  //_validKeySizes stores sizes in bits
                int inx = 0;
                int maxInx = _validKeySizes.Length - 1;
                for (inx = 0; inx <= maxInx; inx++)
                {
                    if (keylen == _validKeySizes[inx].MaxSize
                       || keylen == _validKeySizes[inx].MinSize)
                    {
                        retVal = true;
                        break;
                    }
                }
            }


            return retVal;
        }


        /// <summary>
        /// Verifies the IV (initialization vector) is a valid block size.
        /// </summary>
        /// <param name="ivString">
        /// String containing the IV to be validated.
        /// </param>
        /// <returns>
        /// True if IV is a valid size; otherwise False.
        /// </returns>
        public bool IVIsValid(string ivString)
        {
            bool retVal = false;
            if (ivString != null)
            {
                int ivlen = ivString.Length * 8;  //_validIVSizes stores sizes in bits
                int inx = 0;
                int maxInx = _validIVSizes.Length - 1;
                for (inx = 0; inx <= maxInx; inx++)
                {
                    if (ivlen == _validIVSizes[inx].MaxSize
                       || ivlen == _validIVSizes[inx].MinSize)
                    {
                        retVal = true;
                        break;
                    }
                }
            }


            return retVal;
        }

        /// <summary>
        /// Converts byte array to a string.
        /// </summary>
        /// <param name="bytes">
        /// Array of bytes to be converted.
        /// </param>
        /// <returns>
        /// String value containing converted byte array.
        /// </returns>
        private string GetStringFromByteArray(byte[] bytes)
        {
            string retString = string.Empty;
            System.Text.ASCIIEncoding enc = new ASCIIEncoding();

            retString = enc.GetString(bytes);


            return retString;
        }

        /// <summary>
        /// Encrypts string using one of the .NET symmetric crypto providers.
        /// </summary>
        /// <param name="stringToEncrypt">
        /// String to be encrypted.
        /// </param>
        /// <returns>
        /// Returns the encrypted string.
        /// </returns>
        /// <remarks>
        /// Throws exception if string to be encryted is null.
        /// Throws exception if Key or Iv values are invalid.
        /// </remarks>
        public string Encrypt(string stringToEncrypt)
        {
            if (String.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null or empty.");
            }

            if (KeyIsValid(GetStringFromByteArray(_key)) == false)
            {
                throw new System.Exception("Invalid length for Key.");
            }
            if (IVIsValid(GetStringFromByteArray(_iv)) == false)
            {
                throw new System.Exception("Invalid length for IV.");
            }


            //_cryptoProvider.Padding = PaddingMode.None;
            //_cryptoProvider.Mode = CipherMode.CBC;
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, _cryptoProvider.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(stringToEncrypt);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();


            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Encrypts string using one of the .NET symmetric crypto providers and then saves the encrypted string to a file.
        /// </summary>
        /// <param name="stringToEncrypt">
        /// String to be encrypted.
        /// </param>
        /// <param name="encryptedOutputFile">
        /// Full path of the file to which the encrypted string will be saved.
        /// </param>
        /// <returns>
        /// Returns the encrypted string.
        /// </returns>
        /// <remarks>
        /// Throws exception if string to be encryted is null.
        /// Throws exception if Key or Iv values are invalid.
        /// </remarks>
        public string Encrypt(string stringToEncrypt, string encryptedOutputFile)
        {
            string encryptedString = string.Empty;

            if (String.IsNullOrEmpty(stringToEncrypt) || String.IsNullOrEmpty(encryptedOutputFile))
            {
                throw new ArgumentNullException("Paths to both string and encrypted output file need to specified.");
            }

            if (KeyIsValid(GetStringFromByteArray(_key)) == false)
            {
                throw new System.Exception("Invalid length for Key.");
            }
            if (IVIsValid(GetStringFromByteArray(_iv)) == false)
            {
                throw new System.Exception("Invalid length for IV.");
            }

            try
            {
                encryptedString = this.Encrypt(stringToEncrypt);
                File.WriteAllText(encryptedOutputFile, encryptedString);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to encrypt and save to file failed: \r\n");
                _msg.Append("String to encryp: ");
                _msg.Append(stringToEncrypt);
                _msg.Append("\r\n");
                _msg.Append("Encrypted output file: ");
                _msg.Append(encryptedOutputFile);
                _msg.Append("\r\n");
                _msg.Append("Error message: ");
                _msg.Append("\r\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }




            return encryptedOutputFile;
        }

        /// <summary>
        /// Decrypts string that was encrypted using one of the .NET crypto providers.
        /// </summary>
        /// <param name="encryptedString">
        /// String to be decrypted.
        /// </param>
        /// <returns>
        /// Returns the decrypted string.
        /// </returns>
        /// <remarks>
        /// Throws exception if string to be decryted is null.
        /// Throws exception if Key or Iv values are invalid.
        /// </remarks>

        public string Decrypt(string encryptedString)
        {
            string decryptedString = string.Empty;

            if (String.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null or empty.");
            }

            if (KeyIsValid(GetStringFromByteArray(_key)) == false)
            {
                throw new System.Exception("Invalid length for Key.");
            }
            if (IVIsValid(GetStringFromByteArray(_iv)) == false)
            {
                throw new System.Exception("Invalid length for IV.");
            }


            try
            {
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, _cryptoProvider.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                decryptedString = reader.ReadToEnd();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("ERROR: Verify you are using same key and iv to decrypt that were used to encrypt. Error message: ");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
                decryptedString = string.Empty;
            }
            finally
            {
                ;
            }
                 
        

            return decryptedString;
        }


        /// <summary>
        /// Decrypts string that was encrypted using one of the .NET crypto providers.
        /// </summary>
        /// <param name="encryptedString">
        /// String to be decrypted.
        /// </param>
        /// <param name="decryptedOutputFile">Path to file that will hold the decrypted data.</param>
        /// <returns>
        /// Returns decrypted string.
        /// </returns>
        /// <remarks>
        /// Throws exception if string to be decryted is null.
        /// Throws exception if Key or Iv values are invalid.
        /// </remarks>

        public string Decrypt(string encryptedString, string decryptedOutputFile)
        {
            string decryptedString = string.Empty;

            if (String.IsNullOrEmpty(encryptedString) || String.IsNullOrEmpty(decryptedOutputFile))
            {
                throw new ArgumentNullException("Paths to both string and decrypted output file need to specified.");
            }

            if (KeyIsValid(GetStringFromByteArray(_key)) == false)
            {
                throw new System.Exception("Invalid length for Key.");
            }
            if (IVIsValid(GetStringFromByteArray(_iv)) == false)
            {
                throw new System.Exception("Invalid length for IV.");
            }


            try
            {
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, _cryptoProvider.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                decryptedString = reader.ReadToEnd();
                File.WriteAllText(decryptedOutputFile, decryptedString);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("ERROR: Verify you are using same key and iv to decrypt that were used to encrypt. Error message: ");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
                decryptedString = string.Empty;
            }
            finally
            {
                ;
            }



            return decryptedOutputFile;
        }


        
        /// <summary>
        /// Resets Key and Iv properties to their default values.
        /// </summary>
        public void ResetToDefaults()
        {
            _key = _defaultKey;
            _iv = _defaultIv;

        }





    }//end class
}//end namespace

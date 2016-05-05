﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using AppGlobals;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class to encrypt and decrypt data in files using the Triple Data Encryption Standard (TripleDES) algorithm.
    /// </summary>
    public class FileEncryptorTripleDES : PFEncryptionObjects.IFileEncryptor
    {

        //TripleDESCryptoServiceProvider 
        //        Legal min key size = 128 
        //        Legal max key size = 192 
        //        Legal min block size = 64 
        //        Legal max block size = 64

        //IV = Initialization Vector (randomizes the encryption)


        private byte[] _defaultKey = ASCIIEncoding.ASCII.GetBytes("xnczzpfckkrrewvc");
        private byte[] _defaultIv = ASCIIEncoding.ASCII.GetBytes("20100318");
        private byte[] _key;
        private byte[] _iv;
        private TripleDESCryptoServiceProvider _cryptoProvider = new TripleDESCryptoServiceProvider();
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
        public FileEncryptorTripleDES()
        {
            Initialize();
        }

        private void Initialize()
        {
            _validKeySizes = _cryptoProvider.LegalKeySizes; ;
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
        /// Encrypts using a byte array. The input file is loaded into a byte array and then encrypted and saved to the output file.
        /// </summary>
        /// <param name="inputFile">Full path to file that will be encrypted.</param>
        /// <param name="encryptedOutputFile">Full path to file that will contain encrypted data.</param>
        /// <returns>Returns path to encrypted output file.</returns>
        public string EncryptBinary(string inputFile, string encryptedOutputFile)
        {
            if (String.IsNullOrEmpty(inputFile) || String.IsNullOrEmpty(encryptedOutputFile))
            {
                throw new ArgumentNullException("Paths to both input and encrypted output files need to specified.");
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
                FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
                FileStream fsEncrypted = new FileStream(encryptedOutputFile, FileMode.Create, FileAccess.Write);
                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, _cryptoProvider.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);

                byte[] bytearrayinput = new byte[fsInput.Length];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();
                fsInput.Close();
                fsEncrypted.Close();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to encrypt file ");
                _msg.Append(inputFile);
                _msg.Append(" failed with following message: ");
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
        /// Decrypts using a byte array. The encrypted input file is loaded into a stream and then decrypted and saved as unencrypted data to the output file.
        /// </summary>
        /// <param name="encryptedInputFile">Full path to file containing the encrypted data.</param>
        /// <param name="outputFile">Full path to file that will contain decrypted data.</param>
        /// <returns>Returns path to output file.</returns>
        public string DecryptBinary(string encryptedInputFile, string outputFile)
        {
            if (String.IsNullOrEmpty(encryptedInputFile) || String.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentNullException("Paths to both the encrypted input file and the output file need to be specified.");
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
                FileStream fsread = new FileStream(encryptedInputFile, FileMode.Open, FileAccess.Read);
                CryptoStream cryptoStream = new CryptoStream(fsread, _cryptoProvider.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
                StreamWriter fsDecrypted = new StreamWriter(outputFile);
                fsDecrypted.Write(new StreamReader(cryptoStream).ReadToEnd());
                fsDecrypted.Flush();
                fsDecrypted.Close();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to decrypt file ");
                _msg.Append(encryptedInputFile);
                _msg.Append(" failed. Verify you are using same key/iv pair used to encrypt.  Error message: ");

                _msg.Append("\r\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return outputFile;

        }

        /// <summary>
        /// Encrypts by loading inputFile to a string and then encrypting the string. String is then written out as text to the encryptedOutputFile.
        /// </summary>
        /// <param name="inputFile">Full path to file that will be encrypted.</param>
        /// <param name="encryptedOutputFile">Full path to file that will contain encrypted data.</param>
        /// <returns>Returns path to encrypted output file.</returns>
        public string Encrypt(string inputFile, string encryptedOutputFile)
        {
            IStringEncryptor encryptor = new StringEncryptorTripleDES();

            if (String.IsNullOrEmpty(inputFile) || String.IsNullOrEmpty(encryptedOutputFile))
            {
                throw new ArgumentNullException("Paths to both input and encrypted output file need to specified.");
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
                string data = File.ReadAllText(inputFile);
                encryptor.Key = this.Key;
                encryptor.IV = this.IV;
                string encryptedData = encryptor.Encrypt(data);
                File.WriteAllText(encryptedOutputFile, encryptedData);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to encrypt file ");
                _msg.Append(inputFile);
                _msg.Append(" failed with following message: ");
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
        /// Decrypts by loading encryptedInputFile to a string and then decrypting the string. Decrypted string is then written out as text to the outputFile.
        /// </summary>
        /// <param name="encryptedInputFile">Full path to file containing the encrypted data.</param>
        /// <param name="outputFile">Full path to file that will contain decrypted data.</param>
        /// <returns>Returns path to output file.</returns>
        public string Decrypt(string encryptedInputFile, string outputFile)
        {
            IStringEncryptor encryptor = new StringEncryptorTripleDES();

            if (String.IsNullOrEmpty(encryptedInputFile) || String.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentNullException("Paths to both the encrypted input file and the output file need to be specified.");
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
                string encryptedData = File.ReadAllText(encryptedInputFile);
                encryptor.Key = this.Key;
                encryptor.IV = this.IV;
                string decryptedData = encryptor.Decrypt(encryptedData);
                File.WriteAllText(outputFile, decryptedData);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to decrypt file ");
                _msg.Append(encryptedInputFile);
                _msg.Append(" failed. Verify you are using same key/iv pair used to encrypt.  Error message: ");
                _msg.Append("\r\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return outputFile;

        }


        /// <summary>
        /// Loads contents of encryptedInputFile to string, decrypts the string and returns decrypted string to caller.
        /// </summary>
        /// <param name="encryptedInputFile">Path to file containing encrypted data.</param>
        /// <returns>Decrypted string.</returns>
        public string Decrypt(string encryptedInputFile)
        {
            string encryptedString = string.Empty;
            string decryptedString = string.Empty;
            IStringEncryptor encryptor = new StringEncryptorTripleDES();

            if (String.IsNullOrEmpty(encryptedInputFile))
            {
                throw new ArgumentNullException("Path to both the encrypted input file needs to be specified.");
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
                encryptedString = File.ReadAllText(encryptedInputFile);
                encryptor.Key = this.Key;
                encryptor.IV = this.IV;
                decryptedString = encryptor.Decrypt(encryptedString);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to decrypt file to string failed: Make sure you are using the same key and IV used to encryp ");
                _msg.Append(encryptedInputFile);
                _msg.Append("\r\n");
                _msg.Append("Error Message:");
                _msg.Append("\r\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }


            return decryptedString;

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

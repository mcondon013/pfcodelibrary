//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class for encrypting and decrypting files using Windows NTFS encryption.
    /// Files encrypted with this method can only be read and decrypted by sessions using the logon account in effect when the file was encrypted.
    /// </summary>
    public class WinNtfsEncryptor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Cosntructor.
        /// </summary>
        public WinNtfsEncryptor()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Method to encrypt files using Windows NTFS encryption.
        /// </summary>
        /// <param name="fileToEncrypt">Path to file to be encrypted.</param>
        /// <remarks>Encrypts a file so that only the account used to encrypt the file can decrypt it.</remarks>
        public void Encrypt(string fileToEncrypt)
        {
            if (File.Exists(fileToEncrypt) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find file: ");
                _msg.Append(fileToEncrypt);
                throw new FileNotFoundException(_msg.ToString());
            }
            FileInfo fi = new FileInfo(fileToEncrypt);
            fi.Encrypt();
        }

        /// <summary>
        /// Method to decrypt files that were encrypted using Windows NTFS encryption.
        /// </summary>
        /// <param name="fileToDecrypt">Path to file to be decrypted.</param>
        /// <remarks>Decrypts a file that was encrypted by the current account using the Encrypt method.</remarks>
        public void Decrypt(string fileToDecrypt)
        {
            if (File.Exists(fileToDecrypt) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find file: ");
                _msg.Append(fileToDecrypt);
                throw new FileNotFoundException(_msg.ToString());
            }
            FileInfo fi = new FileInfo(fileToDecrypt);
            fi.Decrypt();
        }

    }//end class
}//end namespace

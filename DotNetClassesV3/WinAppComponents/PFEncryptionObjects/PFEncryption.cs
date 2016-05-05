using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFRandomData;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class for creating and managing encryption objects.
    /// </summary>
    public class PFEncryption
    {
        /// <summary>
        /// Generate randomized Key and Iv values for symmetric encryption processing.
        /// </summary>
        /// <param name="typeEncryption">See <see cref="pfEncryptionAlgorithm"/> for list of valid values.</param>
        /// <returns>pfKeyIvPair value.</returns>
        
        public static pfKeyIvPair GenerateKeyIvPair(pfEncryptionAlgorithm typeEncryption)
        {
            int keyLength = 8;
            int ivLength = 8;
            pfKeyIvPair keyIvPair = new pfKeyIvPair();
            RandomString randstr = new RandomString();

            switch (typeEncryption)
            {
                case pfEncryptionAlgorithm.AES:
                    keyLength = 16;
                    ivLength = 16;
                    break;
                case pfEncryptionAlgorithm.DES:
                    keyLength = 8;
                    ivLength = 8;
                    break;
                case pfEncryptionAlgorithm.TripleDES:
                    keyLength = 16;
                    ivLength = 8;
                    break;
                default:
                    //default to AES
                    keyLength = 16;
                    ivLength = 16;
                    break;

            }
            keyIvPair.key = randstr.GetStringNoSpaces(keyLength);
            keyIvPair.IV = randstr.GetStringNoSpaces(ivLength);

            return keyIvPair;
        }

        /// <summary>
        /// Translates text description to a pfEncryptionAlgorithm enum value.
        /// </summary>
        /// <param name="typeDescription">String specifying the name of the algorithm.</param>
        /// <returns>pfEncryptionAlgorithm value.</returns>
        public static pfEncryptionAlgorithm GetEncryptionAlgorithm(string typeDescription)
        {
            pfEncryptionAlgorithm ret = pfEncryptionAlgorithm.NotSpecified;

            switch (typeDescription)
            {
                case "AES":
                    ret = pfEncryptionAlgorithm.AES;
                    break;
                case "DES":
                    ret = pfEncryptionAlgorithm.DES;
                    break;
                case "TripleDES":
                    ret = pfEncryptionAlgorithm.TripleDES;
                    break;
                default:
                    ret = pfEncryptionAlgorithm.Invalid;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Gets the length that an Iv value must have for the encryption algorithm specified by the method's argument.
        /// </summary>
        /// <param name="alg">Type of encryption algorithm.</param>
        /// <returns>Int value representing string length.</returns>
        public static int GetEncryptionKeyIvStringLength(pfEncryptionAlgorithm alg)
        {
            int stringLength = 16;

            switch (alg)
            {
                case pfEncryptionAlgorithm.AES:
                    stringLength = 16;
                    break;
                case pfEncryptionAlgorithm.DES:
                    stringLength = 8;
                    break;
                case pfEncryptionAlgorithm.TripleDES:
                    stringLength = 16;
                    break;
                default:
                    //default to AES
                    stringLength = 16;
                    break;

            }

            return stringLength;
        }

        /// <summary>
        /// Factory method for creating instances of the string encryptor classes.
        /// </summary>
        /// <param name="alg">Type of encryption algorithm.</param>
        /// <returns>IStringEncryptor interface representing the chosen string encryptor.</returns>
        public static IStringEncryptor GetStringEncryptor(pfEncryptionAlgorithm alg)
        {
            IStringEncryptor enc;

            switch (alg)
            {
                case pfEncryptionAlgorithm.AES:
                    enc = new StringEncryptorAES();
                    break;
                case pfEncryptionAlgorithm.DES:
                    enc = new StringEncryptorDES();
                    break;
                case pfEncryptionAlgorithm.TripleDES:
                    enc = new StringEncryptorTripleDES();
                    break;
                default:
                    enc = new StringEncryptorAES();
                    break;

            }

            return enc;

        }

        /// <summary>
        /// Factory method for creating instances of file encryption objects.
        /// </summary>
        /// <param name="alg">Type of encryption algorithm.</param>
        /// <returns>IFileEncryptor interface for the chosen file encryptor.</returns>
        public static IFileEncryptor GetFileEncryptor(pfEncryptionAlgorithm alg)
        {
            IFileEncryptor enc;

            switch (alg)
            {
                case pfEncryptionAlgorithm.AES:
                    enc = new FileEncryptorAES();
                    break;
                case pfEncryptionAlgorithm.DES:
                    enc = new FileEncryptorDES();
                    break;
                case pfEncryptionAlgorithm.TripleDES:
                    enc = new FileEncryptorTripleDES();
                    break;
                default:
                    enc = new FileEncryptorAES();
                    break;

            }

            return enc;

        }


    }//end class
}//end namespace

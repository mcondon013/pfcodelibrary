using System;
namespace PFEncryptionObjects
{
    /// <summary>
    /// Interface for string encryption classes that implement Microsoft symmetric encryption algorithms.
    /// </summary>
    public interface IStringEncryptor
    {
        /// <summary>
        /// Decrypts string that was encrypted using the Advanced Encryption Standard (AES) algorithm.
        /// </summary>
        /// <param name="encryptedString">
        /// String to be decrypted.
        /// </param>
        /// <returns>
        /// Returns the decrypted string.
        /// </returns>
        string Decrypt(string encryptedString);
        /// <summary>
        /// Encrypts string using a symmetric algorithm.
        /// </summary>
        /// <param name="stringToEncrypt">
        /// String to be encrypted.
        /// </param>
        /// <returns>
        /// Returns the encrypted string.
        /// </returns>
        string Encrypt(string stringToEncrypt);
        /// <summary>
        /// Encrypts string using a symmetric algorithm and then saves the encrypted string to a file.
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
        string Encrypt(string stringToEncrypt, string encryptedOutputFile);
        /// <summary>
        /// Gets or sets the initialization vector (IV) for the symmetric algorithm.
        /// </summary>
        string IV { get; set; }
        /// <summary>
        /// Gets or sets the secret key for the symmetric algorithm. 
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// List of valid block sizes for the algorithm.
        /// This is a read-only property.
        /// </summary>
        System.Collections.Generic.List<pfLegalBlockSize> LegalBlockSizes { get; }
        /// <summary>
        /// List of valid key sizes for the algorithm.
        /// This is a read-only property.
        /// </summary>
        System.Collections.Generic.List<pfLegalKeySize> LegalKeySizes { get; }
        /// <summary>
        /// Resets Key and Iv properties to their default values.
        /// </summary>
        void ResetToDefaults();
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
        bool KeyIsValid(string keyString);
        /// <summary>
        /// Verifies the IV (initialization vector) is a valid block size.
        /// </summary>
        /// <param name="ivString">
        /// String containing the IV to be validated.
        /// </param>
        /// <returns>
        /// True if IV is a valid size; otherwise False.
        /// </returns>
        bool IVIsValid(string ivString);

    }
}

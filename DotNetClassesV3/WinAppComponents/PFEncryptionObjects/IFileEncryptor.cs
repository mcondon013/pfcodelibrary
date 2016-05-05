using System;
namespace PFEncryptionObjects
{
    /// <summary>
    /// Interface for file encryption classes that implement Microsoft symmetric encryption algorithms.
    /// </summary>
    public interface IFileEncryptor
    {
        /// <summary>
        /// Decrypts file by processing a data as a stream of bytes.
        /// </summary>
        /// <param name="encryptedInputFile">Full path to file containing the encrypted data.</param>
        /// <param name="outputFile">Full path to file that will contain decrypted data.</param>
        /// <returns>Returns path to output file.</returns>
        string DecryptBinary(string encryptedInputFile, string outputFile);
        /// <summary>
        /// Decrypts by processing data as a string.
        /// </summary>
        /// <param name="encryptedInputFile">Full path to file containing the encrypted data.</param>
        /// <param name="outputFile">Full path to file that will contain decrypted data.</param>
        /// <returns>Returns path to output file.</returns>
        string Decrypt(string encryptedInputFile, string outputFile);
        /// <summary>
        /// Loads contents of encryptedInputFile to string, decrypts the string and returns decrypted string to caller.
        /// </summary>
        /// <param name="encryptedInputFile">Path to file containing encrypted data.</param>
        /// <returns>Decrypted string.</returns>
        string Decrypt(string encryptedInputFile);
        /// <summary>
        /// Encrypts the file by processing data as a stream of bytes.
        /// </summary>
        /// <param name="inputFile">Full path to file that will be encrypted.</param>
        /// <param name="encryptedOutputFile">Full path to file that will contain encrypted data.</param>
        /// <returns>Returns path to encrypted output file.</returns>
        string EncryptBinary(string inputFile, string encryptedOutputFile);
        /// <summary>
        /// Encrypts by processing the data to be encrypted as a string.
        /// </summary>
        /// <param name="inputFile">Full path to file that will be encrypted.</param>
        /// <param name="encryptedOutputFile">Full path to file that will contain encrypted data.</param>
        /// <returns>Returns path to encrypted output file.</returns>
        string Encrypt(string inputFile, string encryptedOutputFile);
        /// <summary>
        /// Gets or sets the initialization vector (IV) for the symmetric algorithm.
        /// </summary>
        string IV { get; set; }
        //properties
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

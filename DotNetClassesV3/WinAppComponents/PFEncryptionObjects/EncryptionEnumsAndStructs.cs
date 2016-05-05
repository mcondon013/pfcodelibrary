using System;
namespace PFEncryptionObjects
{
    /// <summary>
    /// Supported encryption algorithms.
    /// </summary>
    public enum pfEncryptionAlgorithm
    {
        /// <summary>
        /// Encryption algorithm has not been set.
        /// </summary>
        NotSpecified=0,
        /// <summary>
        /// Data Encryption Standard.
        /// </summary>
        DES=1,
        /// <summary>
        /// Triple Data Encryption Standard.
        /// </summary>
        TripleDES=2,
        /// <summary>
        /// Advanced Encryption Standard.
        /// </summary>
        AES=3,
        /// <summary>
        /// Invalid or not supported encryption standard.
        /// </summary>
        Invalid = 9
    }

    /// <summary>
    /// Defines a legal key size.
    /// </summary>
    public struct pfLegalKeySize
    {
        /// <summary>
        /// Legal key size struct constructor.
        /// </summary>
        /// <param name="min">Smallest key size allowed.</param>
        /// <param name="max">Largest key size allowed.</param>
        public pfLegalKeySize(int min, int max)
        {
            MinKeySize = min;
            MaxKeySize = max;
        }
        /// <summary>
        /// Smallest key size allowed.
        /// </summary>
        public int MinKeySize;
        /// <summary>
        /// Largest key size allowed.
        /// </summary>
        public int MaxKeySize;
    }

    /// <summary>
    /// Legal block size struct
    /// </summary>
    public struct pfLegalBlockSize
    {
        /// <summary>
        /// Legal block size struct constructor.
        /// </summary>
        /// <param name="min">Smallest block size allowed.</param>
        /// <param name="max">Largest block size allowed.</param>
        public pfLegalBlockSize(int min, int max)
        {
            MinBlockSize = min;
            MaxBlockSize = max;
        }
        /// <summary>
        /// Smallest block size allowed.
        /// </summary>
        public int MinBlockSize;
        /// <summary>
        /// Largest block size allowed.
        /// </summary>
        public int MaxBlockSize;
    }

    /// <summary>
    /// Struct for defining key and IV combination.
    /// </summary>
    public struct pfKeyIvPair
    {
        /// <summary>
        /// Secret Key value.
        /// </summary>
        public string key;
        /// <summary>
        /// Initialization Vector value.
        /// </summary>
        public string IV;
    }




}//end namespace

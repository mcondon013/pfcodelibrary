using System;
namespace PFEncryptionObjects
{

    //enums for PGP
    /// <summary>
    /// Algorithms supported by the PGP encryption class.
    /// </summary>
    public enum pfPGPCypherAlgorithm
    {
        /// <summary>
        /// Triple DES. 168 bit cipher involving three applications of the DES algorithm
        /// </summary>
        TRIPLE_DES = DidiSoft.Pgp.CypherAlgorithm.TRIPLE_DES,
        /// <summary>
        /// CAST 5 (Default). 128 bit cipher used since PGP 5.0
        /// </summary>
        CAST5 = DidiSoft.Pgp.CypherAlgorithm.CAST5,
        /// <summary>
        ///  Blowfish
        /// </summary>
        BLOWFISH = DidiSoft.Pgp.CypherAlgorithm.BLOWFISH,
        /// <summary>
        ///  128 bit AES (Rijndael)
        /// </summary>
        AES_128 = DidiSoft.Pgp.CypherAlgorithm.AES_128,
        /// <summary>
        ///  192 bit AES (Rijndael)
        /// </summary>
        AES_192 = DidiSoft.Pgp.CypherAlgorithm.AES_192,
        /// <summary>
        ///  256 bit AES (Rijndael)
        /// </summary>
        AES_256 = DidiSoft.Pgp.CypherAlgorithm.AES_256,
        /// <summary>
        ///  Twofish. 256-bit variant of former AES version
        /// </summary>
        TWOFISH = DidiSoft.Pgp.CypherAlgorithm.TWOFISH,
        /// <summary>
        /// DES
        /// </summary>
        DES = DidiSoft.Pgp.CypherAlgorithm.DES,
        /// <summary>
        /// SAFER
        /// </summary>
        SAFER = DidiSoft.Pgp.CypherAlgorithm.SAFER,
        /// <summary>
        /// IDEA
        /// </summary>
        IDEA = DidiSoft.Pgp.CypherAlgorithm.IDEA,

    }

    /// <summary>
    /// Specifies a hash algorithm.
    /// </summary>
    public enum pfPGPHashAlgorithm
    {
        /// <summary>
        /// Secure Hash Algorithm (SHA-1)
        /// </summary>
        SHA1 = DidiSoft.Pgp.HashAlgorithm.SHA1,
        /// <summary>
        /// Secure Hash Algorithm 256 bit (SHA-2 256)
        /// </summary>
        SHA256 = DidiSoft.Pgp.HashAlgorithm.SHA256,
        /// <summary>
        /// Secure Hash Algorithm 384 bit (SHA-2 384)
        /// </summary>
        SHA384 = DidiSoft.Pgp.HashAlgorithm.SHA384,
        /// <summary>
        /// Secure Hash Algorithm 512 bit (SHA-2 512)
        /// </summary>
        SHA512 = DidiSoft.Pgp.HashAlgorithm.SHA512,
        /// <summary>
        /// Secure Hash Algorithm 224 bit (SHA-2 224)
        /// </summary>
        SHA224 = DidiSoft.Pgp.HashAlgorithm.SHA224,
        /// <summary>
        /// Message Digest 5
        /// </summary>
        MD5 = DidiSoft.Pgp.HashAlgorithm.MD5,
        /// <summary>
        ///  RIPEMD-160, 160-bit message digest algorithm (RACE Integrity Primitives Evaluation Message Digest)
        /// </summary>
        RIPEMD160 = DidiSoft.Pgp.HashAlgorithm.RIPEMD160,
        /// <summary>
        /// Message Digest 2
        /// </summary>
        MD2 = DidiSoft.Pgp.HashAlgorithm.MD2,
    }

   /// <summary>
    /// Specifies a compression algorithm.
    /// </summary>
    public enum pfPGPCompressionAlgorithm
    {
        /// <summary>
        /// ZLib compression algorithm
        /// </summary>
        ZLIB = DidiSoft.Pgp.CompressionAlgorithm.ZLIB,
        /// <summary>
        /// ZIP compression algorithm
        /// </summary>
        ZIP = DidiSoft.Pgp.CompressionAlgorithm.ZIP,
        /// <summary>
        /// BZip 2 compression algorithm
        /// </summary>
        BZIP2 = DidiSoft.Pgp.CompressionAlgorithm.BZIP2,
        /// <summary>
        /// Data is not compressed
        /// </summary>
        UNCOMPRESSED = DidiSoft.Pgp.CompressionAlgorithm.UNCOMPRESSED,
    }


    
    /// <summary>
    /// Defines the supported asymmetric encryption algorithms.
    /// </summary>
    public enum pfKeyAlgorithm
    {
        /// <summary>
        /// RSA (Rivest, Shamir and Adleman algorithm)
        /// </summary>
        RSA = DidiSoft.Pgp.KeyAlgorithm.RSA,
        /// <summary>
        /// Implementation of DH/DSS
        /// </summary>
        ELGAMAL = DidiSoft.Pgp.KeyAlgorithm.ELGAMAL,
    }

    /// <summary>
    ///  Holds common PGP trust values
    /// </summary>
    public enum pfPGPTrustLevel
    {
        /// <summary>
        /// Not trusted
        /// </summary>
        None = DidiSoft.Pgp.TrustLevel.None,
        /// <summary>
        /// Partially trusted.
        /// </summary>
        Marginal = DidiSoft.Pgp.TrustLevel.Marginal,
        /// <summary>
        /// Maximum trust
        /// </summary>
        Trusted = DidiSoft.Pgp.TrustLevel.Trusted,
    }



}//end namespace
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
using DidiSoft.Pgp;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class provides functionality for adding, modifying, deleting, importing and exporting PGP keys.
    /// </summary>
    public class PGPKeyManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private KeyPairInformation[] _keys =null;
        private KeyStore _keyStore = null;
        private int _minValidKeySize = 1024;
        private int _maxValidKeySize = 4096;

        //private varialbles for properties
        private string _keyStorePath;
        private string _keyStorePassword;

        //constructors

        //public PGPKeyManager()
        //{
        //    ;
        //}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="keyStorePath">Full path to the key store file.</param>
        /// <param name="keyStorePassword">Password used to open the key store file.</param>
        public PGPKeyManager(string keyStorePath, string keyStorePassword)
        {
            _keyStore = new KeyStore(keyStorePath, keyStorePassword);
            this.KeyStorePath = keyStorePath;
            this.KeyStorePassword = keyStorePassword;
            GetKeyList();
        }

        private void GetKeyList()
        {
            _keys = null;
            _keys = _keyStore.GetKeys();
        }


        //properties
        /// <summary>
        /// KeyStorePath Property.
        /// </summary>
        public string KeyStorePath
        {
            get
            {
                return _keyStorePath;
            }
            private set
            {
                _keyStorePath = value;
            }
        }

        /// <summary>
        /// KeyStorePassword Property.
        /// </summary>
        public string KeyStorePassword
        {
            get
            {
                return _keyStorePassword;
            }
            private set
            {
                _keyStorePassword = value;
            }
        }

        /// <summary>
        /// MinValidKeySize Property.
        /// </summary>
        public int MinValidKeySize
        {
            get
            {
                return _minValidKeySize;
            }
            internal set
            {
                _minValidKeySize = value;
            }
        }

        /// <summary>
        /// MaxValidKeySize Property.
        /// </summary>
        public int MaxValidKeySize
        {
            get
            {
                return _maxValidKeySize;
            }
            internal set
            {
                _maxValidKeySize = value;
            }
        }

        /// <summary>
        /// Resuts array containing the key information for all keys in the Key Store.
        /// </summary>
        public PGPKeyInformation[] Keys
        {
            get
            {
                return ConvertKeyPairInformationArray(_keys);
            }
        }



        //methods

        /// <summary>
        /// Saves contents of Key Store to disk at location specified when this instance created.
        /// </summary>
        public void SaveKeyStoreToDisk()
        {
            _keyStore.Save();
        }

        private bool IsValidKeySize(int keySize)
        {
            bool isValid = false;

            if (keySize >= _minValidKeySize)
                if (keySize <= _maxValidKeySize)
                    isValid = true;

            return isValid;
        }

        /// <summary>
        /// True if the specified userId exists in the KeyStore.
        /// </summary>
        /// <param name="userId">UserId to search for.</param>
        /// <returns>True if found; otherwise false.</returns>
        public bool KeyExists(string userId)
        {
            return _keyStore.ContainsKey(userId);
        }

        /// <summary>
        /// Creates a key pair in  the key store.
        /// </summary>
        /// <param name="keySize">Size for key. Valid values from 1024 to 4096.</param>
        /// <param name="userId">User id for the key.</param>
        /// <param name="keyAlgorithm">Algorithm used for key storage.</param>
        /// <param name="password">Private key password.</param>
        /// <param name="compressionTypes">Types of compression supported in comma delimited format.</param>
        /// <param name="hashingAlgorithmTypes">Types of hashing supported in comma delimited format.</param>
        /// <param name="cypherTypes">Types of cyphers supported in comma delimited format.</param>
        public PGPKeyInformation CreateKey(int keySize,
                                           string userId,
                                           string keyAlgorithm,
                                           string password,
                                           string compressionTypes,
                                           string hashingAlgorithmTypes,
                                           string cypherTypes)
        {
            return CreateKey(keySize, userId, keyAlgorithm, password, compressionTypes, hashingAlgorithmTypes, cypherTypes, DateTime.MaxValue);
        }
        
        /// <summary>
        /// Creates a key pair in  the key store.
        /// </summary>
        /// <param name="keySize">Size for key. Valid values from 1024 to 4096.</param>
        /// <param name="userId">User id for the key.</param>
        /// <param name="keyAlgorithm">Algorithm used for key storage.</param>
        /// <param name="password">Private key password.</param>
        /// <param name="compressionTypes">Types of compression supported in comma delimited format.</param>
        /// <param name="hashingAlgorithmTypes">Types of hashing supported in comma delimited format.</param>
        /// <param name="cypherTypes">Types of cyphers supported in comma delimited format.</param>
        /// <param name="expirationDate">Date at which the key will no longer be valid.</param>
        /// <returns>Returns object with PGP Key information.</returns>
        public PGPKeyInformation CreateKey(int keySize,
                                           string userId,
                                           string keyAlgorithm,
                                           string password,
                                           string compressionTypes,
                                           string hashingAlgorithmTypes,
                                           string cypherTypes,
                                           DateTime expirationDate)
        {
            KeyPairInformation kpi = null;

            if (IsValidKeySize(keySize) == false)
            {
                _msg.Length = 0;
                _msg.Append("Invalid key size: ");
                _msg.Append(keySize.ToString());
                _msg.Append("  Valid key sizes: Min: ");
                _msg.Append(_minValidKeySize.ToString());
                _msg.Append(", Max: ");
                _msg.Append(_maxValidKeySize.ToString());
                _msg.Append(".");
                throw new System.Exception(_msg.ToString());
            }

            if (expirationDate == DateTime.MinValue || expirationDate == DateTime.MaxValue)
            {
                kpi = _keyStore.GenerateKeyPair(keySize,
                                                userId,
                                                ConvertToKeyAlgorithm(keyAlgorithm),
                                                password,
                                                ConvertToCompressionAlgorithmArray(compressionTypes),
                                                ConvertToHashAlgorithmArray(hashingAlgorithmTypes),
                                                ConvertToCypherAlgorithmArray(cypherTypes));
            }
            else
            {
                kpi = _keyStore.GenerateKeyPair(keySize,
                                                userId,
                                                ConvertToKeyAlgorithm(keyAlgorithm),
                                                password,
                                                ConvertToCompressionAlgorithmArray(compressionTypes),
                                                ConvertToHashAlgorithmArray(hashingAlgorithmTypes),
                                                ConvertToCypherAlgorithmArray(cypherTypes),
                                                expirationDate);
            }

            GetKeyList();

            return ConvertKeyPairInformation(kpi);

        }

        /// <summary>
        /// Creates a key pair in  the key store.
        /// </summary>
        /// <param name="keySize">Size for key. Valid values from 1024 to 4096.</param>
        /// <param name="userId">User id for the key.</param>
        /// <param name="keyAlgorithm">Algorithm used for key storage.</param>
        /// <param name="password">Private key password.</param>
        /// <param name="compressionTypes">Array containing types of compression supported.</param>
        /// <param name="hashingAlgorithmTypes">Array containing types of hashing supported.</param>
        /// <param name="cypherTypes">Array containing types of cyphers supported.</param>
        /// <returns>Returns object with PGP Key information.</returns>
        public PGPKeyInformation CreateKey(int keySize,
                                           string userId,
                                           pfKeyAlgorithm keyAlgorithm,
                                           string password,
                                           pfPGPCompressionAlgorithm[] compressionTypes,
                                           pfPGPHashAlgorithm[] hashingAlgorithmTypes,
                                           pfPGPCypherAlgorithm[] cypherTypes)
        {
            return CreateKey(keySize, userId, keyAlgorithm, password, compressionTypes, hashingAlgorithmTypes, cypherTypes, DateTime.MaxValue);
        }

        /// <summary>
        /// Creates a key pair in  the key store.
        /// </summary>
        /// <param name="keySize">Size for key. Valid values from 1024 to 4096.</param>
        /// <param name="userId">User id for the key.</param>
        /// <param name="keyAlgorithm">Algorithm used for key storage.</param>
        /// <param name="password">Private key password.</param>
        /// <param name="compressionTypes">Array containing types of compression supported.</param>
        /// <param name="hashingAlgorithmTypes">Array containing types of hashing supported.</param>
        /// <param name="cypherTypes">Array containing types of cyphers supported.</param>
        /// <param name="expirationDate">Date at which the key will no longer be valid.</param>
        /// <returns>Returns object with PGP Key information.</returns>
        public PGPKeyInformation CreateKey(int keySize,
                                           string userId,
                                           pfKeyAlgorithm keyAlgorithm,
                                           string password,
                                           pfPGPCompressionAlgorithm[] compressionTypes,
                                           pfPGPHashAlgorithm[] hashingAlgorithmTypes,
                                           pfPGPCypherAlgorithm[] cypherTypes,
                                           DateTime expirationDate)
        {
            KeyPairInformation kpi = null;

            if (IsValidKeySize(keySize) == false)
            {
                _msg.Length = 0;
                _msg.Append("Invalid key size: ");
                _msg.Append(keySize.ToString());
                _msg.Append("  Valid key sizes: Min: ");
                _msg.Append(_minValidKeySize.ToString());
                _msg.Append(", Max: ");
                _msg.Append(_maxValidKeySize.ToString());
                _msg.Append(".");
                throw new System.Exception(_msg.ToString());
            }

            if (_keyStore.ContainsKey(userId))
            {
                _msg.Length = 0;
                _msg.Append("User ID (");
                _msg.Append(userId);
                _msg.Append(") already exists in the Key Store (");
                _msg.Append(this.KeyStorePath);
                _msg.Append(").");
                throw new System.Exception(_msg.ToString());
            }

            if (expirationDate == DateTime.MinValue || expirationDate == DateTime.MaxValue)
            {
                kpi=_keyStore.GenerateKeyPair(keySize,
                                          userId,
                                          (KeyAlgorithm)keyAlgorithm,
                                          password,
                                          ConvertToCompressionAlgorithmArray(compressionTypes),
                                          ConvertToHashAlgorithmArray(hashingAlgorithmTypes),
                                          ConvertToCypherAlgorithmArray(cypherTypes));
            }
            else
            {
                kpi=_keyStore.GenerateKeyPair(keySize,
                                          userId,
                                          (KeyAlgorithm)keyAlgorithm,
                                          password,
                                          ConvertToCompressionAlgorithmArray(compressionTypes),
                                          ConvertToHashAlgorithmArray(hashingAlgorithmTypes),
                                          ConvertToCypherAlgorithmArray(cypherTypes),
                                          expirationDate);
            }

            GetKeyList();

            return ConvertKeyPairInformation(kpi);
        }

        /// <summary>
        /// Imports key (public, private or both) from the specified file to the Key Store.
        /// </summary>
        /// <param name="keyFilePath">Path to file containing the key to import. Usually as .asc file.</param>
        /// <returns>Array of PFPKeyInformation objects. Null if no keys found in the Key Store.</returns>
        public PGPKeyInformation[] ImportKey(string keyFilePath)
        {
            KeyPairInformation[] kpi = _keyStore.ImportKeyRing(keyFilePath);
            GetKeyList();
            return ConvertKeyPairInformationArray(kpi);
        }

        private PGPKeyInformation ConvertKeyPairInformation(KeyPairInformation kpi)
        {
            PGPKeyInformation ki = null;
            if (kpi != null)
            {
                ki = new PGPKeyInformation();
                ki.Algorithm = kpi.Algorithm;
                ki.CreationTime = kpi.CreationTime;
                ki.EncryptionKey = kpi.EncryptionKey;
                ki.Fingerprint = kpi.Fingerprint;
                ki.HasPrivateKey = kpi.HasPrivateKey;
                ki.IsExpired = kpi.IsExpired;
                ki.IsLegacyRSAKey = kpi.IsLegacyRSAKey;
                ki.KeyId = kpi.KeyId;
                ki.KeyIdHex = kpi.KeyIdHex;
                ki.KeySize = kpi.KeySize;
                if (kpi.PreferredCompressions != null)
                {
                    ki.PreferredCompressions = new pfPGPCompressionAlgorithm[kpi.PreferredCompressions.Length];
                    for (int j = 0; j < kpi.PreferredCompressions.Length; j++)
                    {
                        ki.PreferredCompressions[j] = (pfPGPCompressionAlgorithm)kpi.PreferredCompressions[j];
                    }
                }
                if (kpi.PreferredCyphers != null)
                {
                    ki.PreferredCyphers = new pfPGPCypherAlgorithm[kpi.PreferredCyphers.Length];
                    for (int j = 0; j < kpi.PreferredCyphers.Length; j++)
                    {
                        ki.PreferredCyphers[j] = (pfPGPCypherAlgorithm)kpi.PreferredCyphers[j];
                    }
                }
                if (kpi.PreferredHashes != null)
                {
                    ki.PreferredHashes = new pfPGPHashAlgorithm[kpi.PreferredHashes.Length];
                    for (int j = 0; j < kpi.PreferredHashes.Length; j++)
                    {
                        ki.PreferredHashes[j] = (pfPGPHashAlgorithm)kpi.PreferredHashes[j];
                    }
                }
                ki.HasPrivateKeyRing = kpi.PrivateRing != null ? true : false;
                ki.HasPrivateSubKeys = kpi.PrivateSubKeys != null ? true : false;
                ki.HasPublicKeyRing = kpi.PublicRing != null ? true : false;
                ki.HasPublicSubKeys = kpi.PublicSubKeys != null ? true : false;
                ki.Revoked = kpi.Revoked;
                ki.SignedWithKeyIds = kpi.SignedWithKeyIds;
                ki.SigningKey = kpi.SigningKey;
                ki.Trust = (pfPGPTrustLevel)kpi.Trust;
                ki.UserId = kpi.UserId;
                ki.UserIds = kpi.UserIds;
                ki.ValidDays = kpi.ValidDays;
            }
            return ki;
        }



        /// <summary>
        /// Converts internal format of key information to external format available to calling routines.
        /// </summary>
        /// <param name="kpi">Object containing key/pair information in internal format.</param>
        /// <returns>Key/pair object in external format.</returns>
        private PGPKeyInformation[] ConvertKeyPairInformationArray(KeyPairInformation[] kpi)
        {
            PGPKeyInformation[] ki = null;
            if (kpi != null)
            {
                if (kpi.Length > 0)
                {
                    ki = new PGPKeyInformation[kpi.Length];
                    for (int i = 0; i < kpi.Length; i++)
                    {
                        ki[i] = new PGPKeyInformation();
                        ki[i].Algorithm = kpi[i].Algorithm;
                        ki[i].CreationTime = kpi[i].CreationTime;
                        ki[i].EncryptionKey = kpi[i].EncryptionKey;
                        ki[i].Fingerprint = kpi[i].Fingerprint;
                        ki[i].HasPrivateKey = kpi[i].HasPrivateKey;
                        ki[i].IsExpired = kpi[i].IsExpired;
                        ki[i].IsLegacyRSAKey = kpi[i].IsLegacyRSAKey;
                        ki[i].KeyId = kpi[i].KeyId;
                        ki[i].KeyIdHex = kpi[i].KeyIdHex;
                        ki[i].KeySize = kpi[i].KeySize;
                        if (kpi[i].PreferredCompressions != null)
                        {
                            ki[i].PreferredCompressions = new pfPGPCompressionAlgorithm[kpi[i].PreferredCompressions.Length];
                            for (int j = 0; j < kpi[i].PreferredCompressions.Length; j++)
                            {
                                ki[i].PreferredCompressions[j] = (pfPGPCompressionAlgorithm)kpi[i].PreferredCompressions[j];
                            }
                        }
                        if (kpi[i].PreferredCyphers != null)
                        {
                            ki[i].PreferredCyphers = new pfPGPCypherAlgorithm[kpi[i].PreferredCyphers.Length];
                            for (int j = 0; j < kpi[i].PreferredCyphers.Length; j++)
                            {
                                ki[i].PreferredCyphers[j] = (pfPGPCypherAlgorithm)kpi[i].PreferredCyphers[j];
                            }
                        }
                        if (kpi[i].PreferredHashes != null)
                        {
                            ki[i].PreferredHashes = new pfPGPHashAlgorithm[kpi[i].PreferredHashes.Length];
                            for (int j = 0; j < kpi[i].PreferredHashes.Length; j++)
                            {
                                ki[i].PreferredHashes[j] = (pfPGPHashAlgorithm)kpi[i].PreferredHashes[j];
                            }
                        }
                        ki[i].HasPrivateKeyRing = kpi[i].PrivateRing != null ? true : false;
                        ki[i].HasPrivateSubKeys = kpi[i].PrivateSubKeys != null ? true : false;
                        ki[i].HasPublicKeyRing = kpi[i].PublicRing != null ? true : false;
                        ki[i].HasPublicSubKeys = kpi[i].PublicSubKeys != null ? true : false;
                        ki[i].Revoked = kpi[i].Revoked;
                        ki[i].SignedWithKeyIds = kpi[i].SignedWithKeyIds;
                        ki[i].SigningKey = kpi[i].SigningKey;
                        ki[i].Trust = (pfPGPTrustLevel)kpi[i].Trust;
                        ki[i].UserId = kpi[i].UserId;
                        ki[i].UserIds = kpi[i].UserIds;
                        ki[i].ValidDays = kpi[i].ValidDays;

                    }
                }
            }
            return ki;
        }

        /// <summary>
        /// Method to change the private key password for a key.
        /// </summary>
        /// <param name="userId">User id of the key to be modified.</param>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="newPassword">New password.</param>
        /// <returns>True if password changed; otherwise false.</returns>
        public bool ChangePrivateKeyPassword(string userId, string oldPassword, string newPassword)
        {
            return _keyStore.ChangePrivateKeyPassword(userId, oldPassword, newPassword);
        }

        /// <summary>
        /// Method to change the private key password for a key.
        /// </summary>
        /// <param name="keyID">Long integer representing the KeyID of the key whose password is to be changed.</param>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="newPassword">New password.</param>
        /// <returns>True if password changed; otherwise false.</returns>
        public bool ChangePrivateKeyPassword(long keyID, string oldPassword, string newPassword)
        {
            bool passwordChanged = false;

            try
            {
                _keyStore.ChangePrivateKeyPassword(keyID, oldPassword, newPassword);
                passwordChanged = true;
            }
            catch
            {
                passwordChanged = false;
            }

            return passwordChanged;
        }

        /// <summary>
        /// Copies the public key for the specified user id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="userId">String representing the user id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportPublicKey(string fileName, string userId, bool asciiArmored)
        {
            _keyStore.ExportPublicKey(fileName, userId, asciiArmored);
        }

        /// <summary>
        /// Copies the private key for the specified user id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="userId">String representing the user id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportPrivateKey(string fileName, string userId, bool asciiArmored)
        {
            _keyStore.ExportPrivateKey(fileName, userId, asciiArmored);
        }

        /// <summary>
        /// Copies both the public and private keys for the specified user id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="userId">String representing the user id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportKeyRing(string fileName, string userId, bool asciiArmored)
        {
            _keyStore.ExportKeyRing(fileName, userId, asciiArmored);
        }

        /// <summary>
        /// Copies the public key for the specified key id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="keyID">String representing the key id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportPublicKey(string fileName, long keyID, bool asciiArmored)
        {
            _keyStore.ExportPublicKey(fileName, keyID, asciiArmored);
        }

        /// <summary>
        /// Copies the private key for the specified key id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="keyID">String representing the key id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportPrivateKey(string fileName, long keyID, bool asciiArmored)
        {
            _keyStore.ExportPrivateKey(fileName, keyID, asciiArmored);
        }

        /// <summary>
        /// Copies both the public and private keys for the specified user id to the specified file.
        /// </summary>
        /// <param name="fileName">Full path to output file.</param>
        /// <param name="keyID">String representing the key id for the key to export.</param>
        /// <param name="asciiArmored">If true, output will be ascii armored; otherwise output will be in binary.</param>
        public void ExportKeyRing(string fileName, long keyID, bool asciiArmored)
        {
            _keyStore.ExportKeyRing(fileName, keyID, asciiArmored);
        }

        /// <summary>
        /// Deletes key from Key Store for the specified user id.
        /// </summary>
        /// <param name="userID">String containing the user id.</param>
        /// <returns>True if delete successful; otherwise false.</returns>
        public bool DeleteKey(string userID)
        {
            bool isDeleted = false;
            isDeleted = _keyStore.DeleteKeyPair(userID);
            GetKeyList();
            return isDeleted;
        }

        /// <summary>
        /// Deletes key from Key Store for the specified key id.
        /// </summary>
        /// <param name="keyID">Long integer representing the key id to be deleted.</param>
        /// <returns>True if delete successful; otherwise false.</returns>
        public bool DeleteKey(long keyID)
        {
            bool isDeleted = false;
            isDeleted = _keyStore.DeleteKeyPair(keyID);
            GetKeyList();
            return isDeleted;
        }
        
        //class helpers

        private KeyAlgorithm ConvertToKeyAlgorithm(string pKeyAlgorithm)
        {
            KeyAlgorithm keyAlgorithm = (KeyAlgorithm)(pfKeyAlgorithm)Enum.Parse(typeof(pfKeyAlgorithm), pKeyAlgorithm);
            return keyAlgorithm;
        }

        private CompressionAlgorithm[] ConvertToCompressionAlgorithmArray(string compressionAlgorithmsCsv)
        {
            pfPGPCompressionAlgorithm[] compressionAlgorithms = null;

            if (String.IsNullOrEmpty(compressionAlgorithmsCsv))
            {
                compressionAlgorithms = new pfPGPCompressionAlgorithm[1];
                compressionAlgorithms[0] = pfPGPCompressionAlgorithm.ZIP;
                return ConvertToCompressionAlgorithmArray(compressionAlgorithms);
            }

            string[] sCompressionAlgorithms = compressionAlgorithmsCsv.Split(',');
            compressionAlgorithms = new pfPGPCompressionAlgorithm[sCompressionAlgorithms.Length];
            for (int i = 0; i < sCompressionAlgorithms.Length; i++)
            {
                compressionAlgorithms[i] = ToCompressionAlgorithm(sCompressionAlgorithms[i]);
            }


            return ConvertToCompressionAlgorithmArray(compressionAlgorithms);
        }

        private HashAlgorithm[] ConvertToHashAlgorithmArray(string hashAlgorithmsCsv)
        {
            pfPGPHashAlgorithm[] hashAlgorithms = null;

            if (String.IsNullOrEmpty(hashAlgorithmsCsv))
            {
                hashAlgorithms = new pfPGPHashAlgorithm[1];
                hashAlgorithms[0] = pfPGPHashAlgorithm.SHA1;
                return ConvertToHashAlgorithmArray(hashAlgorithms);
            }

            string[] sHashAlgorithms = hashAlgorithmsCsv.Split(',');
            hashAlgorithms = new pfPGPHashAlgorithm[sHashAlgorithms.Length];
            for (int i = 0; i < sHashAlgorithms.Length; i++)
            {
                hashAlgorithms[i] = ToHashAlgorithm(sHashAlgorithms[i]);
            }


            return ConvertToHashAlgorithmArray(hashAlgorithms);
        }

        private CypherAlgorithm[] ConvertToCypherAlgorithmArray(string cypherAlgorithmsCsv)
        {
            pfPGPCypherAlgorithm[] cypherAlgorithms = null;

            if (String.IsNullOrEmpty(cypherAlgorithmsCsv))
            {
                cypherAlgorithms = new pfPGPCypherAlgorithm[1];
                cypherAlgorithms[0] = pfPGPCypherAlgorithm.CAST5;
                return ConvertToCypherAlgorithmArray(cypherAlgorithms);
            }

            string[] sCypherAlgorithms = cypherAlgorithmsCsv.Split(',');
            cypherAlgorithms = new pfPGPCypherAlgorithm[sCypherAlgorithms.Length];
            for (int i = 0; i < sCypherAlgorithms.Length; i++)
            {
                cypherAlgorithms[i] = ToCypherAlgorithm(sCypherAlgorithms[i]);
            }


            return ConvertToCypherAlgorithmArray(cypherAlgorithms);
        }

        private CompressionAlgorithm[] ConvertToCompressionAlgorithmArray(pfPGPCompressionAlgorithm[] compressionAlgorithms)
        {
            CompressionAlgorithm[] converted = null;
            if (compressionAlgorithms.Length == 0)
            {
                converted = new CompressionAlgorithm[1];
                converted[0] = CompressionAlgorithm.ZIP;
                return converted;
            }

            converted = new CompressionAlgorithm[compressionAlgorithms.Length];

            for (int i = 0; i < compressionAlgorithms.Length; i++)
            {
                converted[i] = (CompressionAlgorithm)compressionAlgorithms[i];
            }

            return converted;
        }

        private HashAlgorithm[] ConvertToHashAlgorithmArray(pfPGPHashAlgorithm[] hashTypes)
        {
            HashAlgorithm[] converted = null;
            if (hashTypes.Length == 0)
            {
                converted = new HashAlgorithm[1];
                converted[0] = HashAlgorithm.SHA1;
                return converted;
            }

            converted = new HashAlgorithm[hashTypes.Length];

            for (int i = 0; i < hashTypes.Length; i++)
            {
                converted[i] = (HashAlgorithm)hashTypes[i];
            }

            return converted;
        }

        private CypherAlgorithm[] ConvertToCypherAlgorithmArray(pfPGPCypherAlgorithm[] cypherTypes)
        {
            CypherAlgorithm[] converted = null;
            if (cypherTypes.Length == 0)
            {
                converted = new CypherAlgorithm[1];
                converted[0] = CypherAlgorithm.CAST5;
                return converted;
            }

            converted = new CypherAlgorithm[cypherTypes.Length];

            for (int i = 0; i < cypherTypes.Length; i++)
            {
                converted[i] = (CypherAlgorithm)cypherTypes[i];
            }

            return converted;
        }


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
        /// Overrides the default ToString processing.
        /// </summary>
        /// <returns>Name of class type plus values for all public properties.</returns>
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
            }

            return data.ToString();
        }


    }//end class

}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFProcessObjects;
using PFEncryptionObjects;
using PFFileSystemObjects;
using PFTimers;

namespace pfEncryptorObjects
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private bool _batchMode = false;

        //app encryption key 17, 16   IV 49, 16
        private string _zenkPath = @"D332C5A049462DC8" 
                                 + @"DDAAA35879F4CA6D"
                                 + @"62A6E05C8CEBD834"
                                 + @"182CF2F834B6B0AF"
                                 + @"47C2E4463A06C896"
                                 + @"BE096709C258EA58";


        /// <summary>
        /// Event delegate used for setting up callbacks that report on number of bytes and elapsed time from encryption and decryption methods.
        /// </summary>
        /// <param name="operationType">Encryption or Decryption.</param>
        /// <param name="operationState">In Progress, UserCancel, ErrorCancel or Completed.</param>
        /// <param name="totalBytesProcessed">Total number of bytes encrypted or decrypted as of current status report.</param>
        /// <param name="totalSeconds">Elapsed seconds since start of the encrypt or decrypt operation.</param>
        /// <param name="formattedElapsedTime">Elapsed time in displayable output format.</param>
        public delegate void StatusReportDelegate(string operationType, string operationState, long totalBytesProcessed, long totalSeconds, string formattedElapsedTime);
        /// <summary>
        /// Event that returns status information for encrypt or decrypt operation to the calling program.
        /// </summary>
        public event StatusReportDelegate currentStatusReport;
        private int _statusReportIntervalSeconds = 5;  //in seconds


        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Set to true if running in batch mode (i.e. no UI).
        /// </summary>
        public bool BatchMode
        {
            get
            {
                return _batchMode;
            }
            set
            {
                _batchMode = value;
            }
        }


        //application routines

        private void DisplayErrorMessage(string errorMessage, bool saveErrorMessagesToAppLog)
        {
            if (_batchMode == false)
                AppMessages.DisplayErrorMessage(errorMessage, saveErrorMessagesToAppLog);
            else
                ConsoleMessages.DisplayErrorMessage(errorMessage, saveErrorMessagesToAppLog);
        }

        //encryption Eoutines

        public string Encrypt(pfEncryptorRequest encryptorRequest, ref bool encryptionSuccessful)
        {
            string result = string.Empty;

            try
            {
                if (encryptorRequest.SourceObjectType == pfEncryptorObjectType.File)
                {
                    result = EncryptFile(encryptorRequest, ref encryptionSuccessful);
                }
                else if (encryptorRequest.SourceObjectType == pfEncryptorObjectType.String)
                {
                    result = EncryptString(encryptorRequest, ref encryptionSuccessful);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unexpected or invalid encryptor object type: ");
                    _msg.Append(encryptorRequest.SourceObjectType.ToString());
                    throw new System.Exception(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
                 
            return result;
        }

        public pfKeyIvPair GenerateKeyIVPair(pfEncryptionAlgorithm alg)
        {
            pfKeyIvPair ki = PFEncryption.GenerateKeyIvPair(alg);
            return ki;
        
        }

        public pfKeyIvPair LoadKeyIVPair(string filePath)
        {
            pfKeyIvPair kvp = new pfKeyIvPair();
            try
            {
                PFKeyIVValues kvv = PFKeyIVValues.LoadFromXmlFile(filePath);
                kvp.key = kvv.Key;
                kvp.IV = kvv.IV;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
            return kvp;
        }

        public void SaveKeyIVPair(pfEncryptionAlgorithm alg, pfKeyIvPair kvp, string filePath)
        {
            try
            {
                PFKeyIVValues kvv = new PFKeyIVValues(kvp.key, kvp.IV);
                kvv.Algorithm = alg;
                kvv.SaveToXmlFile(filePath);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
        }


        public string EncryptString(pfEncryptorRequest encryptorRequest, ref bool encryptionSuccessful)
        {
            PFStringEncryptor encryptor = null;
            string encryptedText = string.Empty;

            try
            {
                if (String.IsNullOrEmpty(encryptorRequest.SourceObject))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a string to encrypt.");
                    throw new System.Exception(_msg.ToString());
                }

                if (string.IsNullOrEmpty(encryptorRequest.EncryptionKey)
                    || string.IsNullOrEmpty(encryptorRequest.EncryptionIV))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both Key and IV values.");
                    throw new System.Exception(_msg.ToString());
                }


                encryptor = new PFStringEncryptor(encryptorRequest.EncryptionAlgorithm);

                encryptor.Key = encryptorRequest.EncryptionKey;
                encryptor.IV = encryptorRequest.EncryptionIV;

                if (encryptorRequest.DestinationObjectType == pfEncryptorObjectType.String)
                {
                    encryptedText = encryptor.Encrypt(encryptorRequest.SourceObject);
                    encryptorRequest.DestinationObject = encryptedText;
                }
                else
                {
                    encryptedText = encryptor.Encrypt(encryptorRequest.SourceObject, encryptorRequest.DestinationObject);
                }

                encryptionSuccessful = true;
            }
            catch (System.Exception ex)
            {
                encryptionSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }



            return encryptedText;
        }

        public string EncryptFile(pfEncryptorRequest encryptorRequest, ref bool encryptionSuccessful)
        {
            PFFileEncryptor encryptor = null;
            string encryptedResult = string.Empty;
            StatusTimer st = new StatusTimer();

            try
            {
                if (String.IsNullOrEmpty(encryptorRequest.SourceObject))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to a file to decrypt.");
                    throw new System.Exception(_msg.ToString());
                }

                if (String.IsNullOrEmpty(encryptorRequest.DestinationObject))
                {
                    encryptorRequest.DestinationObject = encryptorRequest.SourceObject + ".encrypted";
                }

                if (string.IsNullOrEmpty(encryptorRequest.EncryptionKey)
                    || string.IsNullOrEmpty(encryptorRequest.EncryptionIV))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both Key and IV values.");
                    throw new System.Exception(_msg.ToString());
                }

                if (encryptorRequest.SourceObject == encryptorRequest.DestinationObject)
                {
                    _msg.Length = 0;
                    _msg.Append("Source and destination file paths are the same. They must be different for encryption routine to work.");
                    throw new System.Exception(_msg.ToString());
                }


                encryptor = new PFFileEncryptor(encryptorRequest.EncryptionAlgorithm);
                encryptor.currentStatusReport += UpdateStatus;
                encryptor.StatusReportIntervalSeconds = _statusReportIntervalSeconds;

                encryptor.Key = encryptorRequest.EncryptionKey;
                encryptor.IV = encryptorRequest.EncryptionIV;

                st.ShowElapsedTimeMilliseconds = false;
                st.Start();

                if (encryptorRequest.UseBinaryEncryption == false)
                {
                    if (encryptorRequest.DestinationObjectType == pfEncryptorObjectType.File)
                    {
                        encryptedResult = encryptor.Encrypt(encryptorRequest.SourceObject, encryptorRequest.DestinationObject, st);
                    }
                    else
                    {
                        encryptedResult = encryptor.Encrypt(encryptorRequest.SourceObject);
                    }
                }
                else
                {
                    encryptedResult = encryptor.EncryptBinary(encryptorRequest.SourceObject, encryptorRequest.DestinationObject, st);
                }

                encryptorRequest.DestinationObject = encryptedResult;

                encryptionSuccessful = true;

            }
            catch (System.Exception ex)
            {
                encryptionSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (encryptor != null)
                {
                    encryptor.currentStatusReport -= UpdateStatus;
                    encryptor = null;
                }
                if (st != null)
                {
                    if (st.StatusTimerIsRunning)
                        st.Stop();
                    st = null;
                }

            }


            return encryptedResult;
        }

        public void UpdateStatus(string operationType, string operationState, long totalBytesProcessed, long totalSeconds, string formattedElapsedTime)
        {
            if (currentStatusReport != null)
            {
                currentStatusReport(operationType, operationState, totalBytesProcessed, totalSeconds, formattedElapsedTime);
            }
        }

        public bool SaveEncryptionRequestToFile(pfEncryptorRequest er, string outputFilePath)
        {
            bool saveSuccessful = false;
            try
            {
                EncryptKeyAndIV(ref er);
                er.SaveToXmlFile(outputFilePath);
                saveSuccessful = true;
            }
            catch (System.Exception ex)
            {
                saveSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

            return saveSuccessful;
        }

        private void EncryptKeyAndIV (ref pfEncryptorRequest er)
        {
            pfEncryptorRequest enc = new pfEncryptorRequest();
            string newValue = string.Empty;
            bool encryptionSuccessful = false;
            
            //encrypt the key value
            enc.EncryptionAlgorithm = pfEncryptionAlgorithm.AES;
            enc.OperationType = pfEncryptorOperationType.Encryption;
            enc.SourceObjectType = pfEncryptorObjectType.String;
            enc.DestinationObjectType = pfEncryptorObjectType.String;
            enc.EncryptionKey = _zenkPath.Substring(17, 16);
            enc.EncryptionIV = _zenkPath.Substring(49, 16);
            enc.SourceObject = er.EncryptionKey;
            enc.DestinationObject = string.Empty;
            enc.UseBinaryEncryption = false;
            newValue = EncryptString(enc, ref encryptionSuccessful);
            if (encryptionSuccessful)
                er.EncryptionKey = newValue;
            else
                EncryptKeysError();

            //encrypt the IV value
            enc.SourceObject = er.EncryptionIV;
            enc.DestinationObject = string.Empty;
            newValue = EncryptString(enc, ref encryptionSuccessful);
            if (encryptionSuccessful)
                er.EncryptionIV = newValue;
            else
                EncryptKeysError();
        
        }

        private void EncryptKeysError()
        {
            throw new System.Exception("Attenpt to encrypt key and IV values for save operation failed.");
        }

        public bool LoadEncryptionRequestFromFile(string filePath, ref pfEncryptorRequest er)
        {
            bool loadSuccessful = false;

            try
            {
                er = pfEncryptorRequest.LoadFromXmlFile(filePath);
                DecryptKeyAndIV(ref er);

                loadSuccessful = true;

            }
            catch (System.Exception ex)
            {
                loadSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

            return loadSuccessful;
        }

        private void DecryptKeyAndIV(ref pfEncryptorRequest er)
        {
            pfEncryptorRequest enc = new pfEncryptorRequest();
            string newValue = string.Empty;
            bool decryptionSuccessful = false;

            //encrypt the key value
            enc.EncryptionAlgorithm = pfEncryptionAlgorithm.AES;
            enc.OperationType = pfEncryptorOperationType.Encryption;
            enc.SourceObjectType = pfEncryptorObjectType.String;
            enc.DestinationObjectType = pfEncryptorObjectType.String;
            enc.EncryptionKey = _zenkPath.Substring(17, 16);
            enc.EncryptionIV = _zenkPath.Substring(49, 16);
            enc.SourceObject = er.EncryptionKey;
            enc.DestinationObject = string.Empty;
            enc.UseBinaryEncryption = false;
            newValue = DecryptString(enc, ref decryptionSuccessful);
            if (decryptionSuccessful)
                er.EncryptionKey = newValue;
            else
                DecryptKeysError();

            //encrypt the IV value
            enc.SourceObject = er.EncryptionIV;
            enc.DestinationObject = string.Empty;
            newValue = DecryptString(enc, ref decryptionSuccessful);
            if (decryptionSuccessful)
                er.EncryptionIV = newValue;
            else
                DecryptKeysError();

        }

        private void DecryptKeysError()
        {
            throw new System.Exception("Attenpt to decrypt key and IV values for save operation failed.");
        }


        //decryption Eoutines

        public string Decrypt(pfEncryptorRequest encryptorRequest, ref bool decryptionSuccessful)
        {
            string result = string.Empty;

            try
            {
                if (encryptorRequest.SourceObjectType == pfEncryptorObjectType.File)
                {
                    result = DecryptFile(encryptorRequest, ref decryptionSuccessful);
                }
                else if (encryptorRequest.SourceObjectType == pfEncryptorObjectType.String)
                {
                    result = DecryptString(encryptorRequest, ref decryptionSuccessful);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unexpected or invalid encryptor object type: ");
                    _msg.Append(encryptorRequest.SourceObjectType.ToString());
                    throw new System.Exception(_msg.ToString());
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

            return result;
        }

        public string DecryptString(pfEncryptorRequest decryptorRequest, ref bool decryptionSuccessful)
        {
            PFStringEncryptor decryptor = null;
            string decryptedText = string.Empty;

            try
            {
                if (String.IsNullOrEmpty(decryptorRequest.SourceObject))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a string to decrypt.");
                    throw new System.Exception(_msg.ToString());
                }

                if (string.IsNullOrEmpty(decryptorRequest.EncryptionKey)
                    || string.IsNullOrEmpty(decryptorRequest.EncryptionIV))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both Key and IV values.");
                    throw new System.Exception(_msg.ToString());
                }

                decryptor = new PFStringEncryptor(decryptorRequest.EncryptionAlgorithm);

                decryptor.Key = decryptorRequest.EncryptionKey;
                decryptor.IV = decryptorRequest.EncryptionIV;

                if (decryptorRequest.DestinationObjectType == pfEncryptorObjectType.String)
                {
                    decryptedText = decryptor.Decrypt(decryptorRequest.SourceObject);
                    decryptorRequest.DestinationObject = decryptedText;
                }
                else
                {
                    decryptedText = decryptor.Decrypt(decryptorRequest.SourceObject, decryptorRequest.DestinationObject);
                }

                decryptionSuccessful = true;
            }
            catch (System.Exception ex)
            {
                decryptionSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }



            return decryptedText;
        }

        public string DecryptFile(pfEncryptorRequest decryptorRequest, ref bool decryptionSuccessful)
        {
            PFFileEncryptor decryptor = null;
            string decryptedResult = string.Empty;
            StatusTimer st = new StatusTimer();

            try
            {
                if (String.IsNullOrEmpty(decryptorRequest.SourceObject))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to a file to decrypt.");
                    throw new System.Exception(_msg.ToString());
                }

                if (String.IsNullOrEmpty(decryptorRequest.DestinationObject))
                {
                    decryptorRequest.DestinationObject = decryptorRequest.SourceObject + ".decrypted";
                }

                if (string.IsNullOrEmpty(decryptorRequest.EncryptionKey)
                    || string.IsNullOrEmpty(decryptorRequest.EncryptionIV))
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both Key and IV values.");
                    throw new System.Exception(_msg.ToString());
                }

                if (decryptorRequest.SourceObject == decryptorRequest.DestinationObject)
                {
                    _msg.Length = 0;
                    _msg.Append("Source and destination file paths are the same. They must be different for decryption routine to work.");
                    throw new System.Exception(_msg.ToString());
                }


                decryptor = new PFFileEncryptor(decryptorRequest.EncryptionAlgorithm);
                decryptor.currentStatusReport += UpdateStatus;
                decryptor.StatusReportIntervalSeconds = _statusReportIntervalSeconds;

                decryptor.Key = decryptorRequest.EncryptionKey;
                decryptor.IV = decryptorRequest.EncryptionIV;

                st.ShowElapsedTimeMilliseconds = false;
                st.Start();

                if (decryptorRequest.UseBinaryEncryption == false)
                {
                    if (decryptorRequest.DestinationObjectType == pfEncryptorObjectType.File)
                    {
                        decryptedResult = decryptor.Decrypt(decryptorRequest.SourceObject, decryptorRequest.DestinationObject, st);
                    }
                    else
                    {
                        decryptedResult = decryptor.Decrypt(decryptorRequest.SourceObject);
                    }
                }
                else
                {
                    decryptedResult = decryptor.DecryptBinary(decryptorRequest.SourceObject, decryptorRequest.DestinationObject, st);
                }

                decryptorRequest.DestinationObject = decryptedResult;

                decryptionSuccessful = true;

            }
            catch (System.Exception ex)
            {
                decryptionSuccessful = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (decryptor != null)
                {
                    decryptor.currentStatusReport -= UpdateStatus;
                    decryptor = null;
                }
                if (st != null)
                {
                    if (st.StatusTimerIsRunning)
                        st.Stop();
                    st = null;
                }
            }


            return decryptedResult;
        }



    }//end class
}//end namespace

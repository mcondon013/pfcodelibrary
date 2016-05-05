//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Class was created using code retrieved from the MSDN Developer website.
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Cryptography;
using AppGlobals;
using PFTimers;

namespace PFEncryptionObjects
{
    /// <summary>
    /// Class for changing the encoding of a file. 
    /// </summary>
    public class PFFileEncoder
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private const int _defaultSize = 1024000;
        private const int _defaultSize = 4096;

        private int BufferSize = _defaultSize;
        private int CharsToRead = _defaultSize / 2;

        // ********************************************************************************************************************************************
        // *NOTE: Testing in Feb. 2014 showed use of the callbacks from PFFileEncoder slowed down the copying severely.
        //        e.g. Encrypting and encoding a 1gb file, takes 3 minutes without the callbacks and about 9 minutes with the callbacks.
        // ********************************************************************************************************************************************

        /// <summary>
        /// Event delegate used for setting up callbacks that report on number of bytes and elapsed time from encoding and decoding methods.
        /// </summary>
        /// <param name="operationType">Encode or Decode.</param>
        /// <param name="operationState">In Progress, UserCancel, ErrorCancel or Completed.</param>
        /// <param name="totalBytesProcessed">Total number of bytes encoded or decoded as of current status report.</param>
        /// <param name="totalSeconds">Elapsed seconds since start of the encode or decode operation.</param>
        /// <param name="formattedElapsedTime">Elapsed time in displayable output format.</param>
        /// <remarks>CAUTION: Using callback status reporter from PFFileEncoder can slow down the encoding process by up to three times slower.</remarks>
        public delegate void StatusReportDelegate(string operationType, string operationState, long totalBytesProcessed, long totalSeconds, string formattedElapsedTime);
        /// <summary>
        /// Event that returns status information for encrypt or decrypt operation to the calling program.
        /// </summary>
        public event StatusReportDelegate currentStatusReport;
        private int _statusReportIntervalSeconds = 5;  //in seconds


        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFileEncoder()
        {
            ;
        }

        //properties

        /// <summary>
        /// StatusReportIntervalSeconds property
        /// </summary>
        public int StatusReportIntervalSeconds
        {
            get
            {
                return _statusReportIntervalSeconds;
            }
            set
            {
                _statusReportIntervalSeconds = value;
            }
        }

        //methods
        /// <summary>
        /// Converts file to base64 encoding.
        /// </summary>
        /// <param name="srcFile">File containing data to be encoded.</param>
        /// <param name="targetFile">Output file that will contain the base64 encoded data.</param>
        /// <param name="st">A StatusTimer object to use for timing the encode operation.</param>
        public void EncodeFileToBase64(string srcFile, string targetFile, StatusTimer st)
        {
            byte[] bytes = new byte[BufferSize];
            FileStream fs = null;
            StreamWriter sw = null;
            FileStream fsOut = null;
            int bytesRead = 0;
            long totalBytesRead = 0;
            bool errorOccurred = false;

            // Open srcFile in read-only mode.
            try
            {
                if (st == null)
                {
                    throw new ArgumentNullException("StatusTime must be specified for this routine.");
                }
                st.NumSecondsInterval = _statusReportIntervalSeconds;

                fs = new FileStream(srcFile, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
                long sourceSize = new FileInfo(srcFile).Length;

                if (sourceSize <= BufferSize)
                {
                    // Open stream writer
                    sw = new StreamWriter(targetFile, false, Encoding.ASCII);
                    bytesRead = fs.Read(bytes, 0, BufferSize);

                    if (bytesRead > 0)
                    {
                        if (currentStatusReport != null)
                        {
                            if (st.StatusReportDue())
                            {
                                currentStatusReport("EncodeToBase64", "In Progress", totalBytesRead, (int)st.GetElapsedTime().TotalSeconds, st.GetFormattedElapsedTime());
                            }
                        }
                        string base64String = Convert.ToBase64String(bytes, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        sw.Write(base64String);
                        if (currentStatusReport != null)
                        {
                            if (st.StatusReportDue())
                            {
                                currentStatusReport("EncodeToBase64", "Completed", totalBytesRead, (int)st.GetElapsedTime().TotalSeconds, st.GetFormattedElapsedTime());
                            }
                        }
                    }
                }
                else
                {
                    // Instantiate a ToBase64Transform object.
                    ToBase64Transform transf = new ToBase64Transform();
                    // Arrays to hold input and output bytes.
                    byte[] inputBytes = new byte[transf.InputBlockSize];
                    byte[] outputBytes = new byte[transf.OutputBlockSize];
                    int bytesWritten;

                    fsOut = new FileStream(targetFile, FileMode.Create, FileAccess.Write);

                    do
                    {
                        if (currentStatusReport != null)
                        {
                            if(st.StatusReportDue())
                            {
                                currentStatusReport("EncodeToBase64", "In Progress", totalBytesRead, (int)st.GetElapsedTime().TotalSeconds, st.GetFormattedElapsedTime());
                            }
                        }
                        bytesRead = fs.Read(inputBytes, 0, inputBytes.Length);
                        totalBytesRead += bytesRead;
                        bytesWritten = transf.TransformBlock(inputBytes, 0, bytesRead, outputBytes, 0);
                        fsOut.Write(outputBytes, 0, bytesWritten);
                    } while (sourceSize - totalBytesRead > transf.InputBlockSize);

                    // Transform the final block of data.
                    bytesRead = fs.Read(inputBytes, 0, inputBytes.Length);
                    totalBytesRead += bytesRead;
                    byte[] finalOutputBytes = transf.TransformFinalBlock(inputBytes, 0, bytesRead);
                    fsOut.Write(finalOutputBytes, 0, finalOutputBytes.Length);

                    if (currentStatusReport != null)
                    {
                        currentStatusReport("EncodeToBase64", "Completed", totalBytesRead, (int)st.GetElapsedTime().TotalSeconds, st.GetFormattedElapsedTime());
                    }

                    // Clear Base64Transform object.
                    transf.Clear();
                }

            }
            catch (IOException ex)
            {
                errorOccurred = true;
                _msg.Length = 0;
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new IOException(_msg.ToString());
            }
            catch (SecurityException ex)
            {
                errorOccurred = true;
                _msg.Length = 0;
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new SecurityException(_msg.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                errorOccurred = true;
                _msg.Length = 0;
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new UnauthorizedAccessException(_msg.ToString());
            }
            finally
            {
                if (errorOccurred)
                {
                    if (currentStatusReport != null)
                    {
                        currentStatusReport("EncodeToBase64", "ErrorCancel", totalBytesRead, (int)st.GetElapsedTime().TotalSeconds, st.GetFormattedElapsedTime());
                    }
                }

                if (sw != null) sw.Close();
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
                if (fsOut != null)
                {
                    fsOut.Dispose();
                    fsOut.Close();
                }
            }
        }

        /// <summary>
        /// Converts a file encoded in base64 back to its original encoding.
        /// </summary>
        /// <param name="srcFile">Input from Base64 encoded data file..</param>
        /// <param name="targetFile">Output containing decoded data.</param>
        public void DecodeFileFromBase64(string srcFile, string targetFile)
        {

            TextReader reader = null;
            FileStream fs = null;
            FileStream fsIn = null;
            long sourceSize = new FileInfo(srcFile).Length;

            try
            {
                fs = new FileStream(targetFile, FileMode.Create, FileAccess.Write);

                // Read entire file in a single operation.
                if (sourceSize <= CharsToRead)
                {
                    reader = new StreamReader(srcFile, Encoding.ASCII);

                    string encodedChars = reader.ReadToEnd();

                    try
                    {
                        byte[] bytes = Convert.FromBase64String(encodedChars);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    catch (FormatException ex)
                    {
                        _msg.Length = 0;
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        throw new FormatException(_msg.ToString());
                    }

                }
                else
                {
                    // Read file as a stream into a buffer.

                    // Instantiate a FromBase64Transform object.
                    FromBase64Transform transf = new FromBase64Transform();
                    // Arrays to hold input and output bytes.
                    byte[] inputBytes = new byte[transf.InputBlockSize];
                    byte[] outputBytes = new byte[transf.OutputBlockSize];
                    int bytesRead = 0;
                    int bytesWritten = 0;
                    long totalBytesRead = 0;

                    fsIn = new FileStream(srcFile, FileMode.Open, FileAccess.Read);

                    do
                    {
                        bytesRead = fsIn.Read(inputBytes, 0, inputBytes.Length);
                        totalBytesRead += bytesRead;
                        bytesWritten = transf.TransformBlock(inputBytes, 0, bytesRead, outputBytes, 0);
                        fs.Write(outputBytes, 0, bytesWritten);
                    } while (sourceSize - totalBytesRead > transf.InputBlockSize);

                    // Transform the final block of data.
                    bytesRead = fsIn.Read(inputBytes, 0, inputBytes.Length);
                    byte[] finalOutputBytes = transf.TransformFinalBlock(inputBytes, 0, bytesRead);
                    fs.Write(finalOutputBytes, 0, finalOutputBytes.Length);

                    // Clear Base64Transform object.
                    transf.Clear();
                }

            }
            catch (IOException ex)
            {
                _msg.Length = 0;
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new IOException(_msg.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                    fs.Dispose();
                }
            }
        }


    }//end class
}//end namespace

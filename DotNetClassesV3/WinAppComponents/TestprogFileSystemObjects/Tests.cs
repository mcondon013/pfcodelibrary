using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFFileSystemObjects;
using PFTimers;
using PFCollectionsObjects;
using PFSecurityObjects;

namespace TestprogFileSystemObjects
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        //properties
        public static bool SaveErrorMessagesToAppLog
        {
            get
            {
                return Tests._saveErrorMessagesToAppLog;
            }
            set
            {
                Tests._saveErrorMessagesToAppLog = value;
            }
        }

        //tests
        public static void PFDirectoryExTest(MainForm frm)
        {
            Stopwatch sw = new Stopwatch();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("PFDirectoryExTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                if (Directory.Exists(frm.txtDirectoryToProcess.Text) == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find directory: ");
                    _msg.Append(frm.txtDirectoryToProcess.Text);
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Directoy to process: ");
                _msg.Append(frm.txtDirectoryToProcess.Text);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.ShowMilliseconds = true;
                sw.Start();

                PFDirectoryEx dirEx = new PFDirectoryEx(frm.txtDirectoryToProcess.Text,frm.chkGetDirectoryTree.Checked);


                //_msg.Length = 0;
                //_msg.Append(dirEx.ToString());
                //Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("\r\n\r\n");
                _msg.Append("Name = ");
                _msg.Append(dirEx.Name);
                _msg.Append("\r\n");
                _msg.Append("FullName = ");
                _msg.Append(dirEx.FullName);
                _msg.Append("\r\n");
                _msg.Append("NumBytes = ");
                _msg.Append(dirEx.NumBytesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("NumFiles = ");
                _msg.Append(dirEx.NumFilesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("NumSubdirs = ");
                _msg.Append(dirEx.NumSubdirectoriesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                if (frm.chkGetDirectoryTree.Checked)
                {
                    _msg.Append("Total NumBytes = ");
                    _msg.Append(dirEx.NumBytesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                    _msg.Append("Total NumFiles = ");
                    _msg.Append(dirEx.TotalNumFilesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                    _msg.Append("Total Subdirectories = ");
                    _msg.Append(dirEx.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                }
                _msg.Append("Number of errors = ");
                _msg.Append(dirEx.NumErrors.ToString("#,##0"));
                _msg.Append("\r\n");
                if (dirEx.ErrorMessages.Length>0)
                {
                    _msg.Append("Error messages:\r\n");
                    _msg.Append(dirEx.ErrorMessages);
                    _msg.Append("\r\n");
                }

                Program._messageLog.WriteLine(_msg.ToString());

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("\r\nTime to create new PFDirectoryEx instance:\r\n");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                
                _msg.Length = 0;
                _msg.Append("\r\nDirEx ToString:\r\n");
                _msg.Append(dirEx.ToString());
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nDirEx ToXmlString:\r\n");
                _msg.Append(dirEx.ToXmlString(true));
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nDirEx ToXmlDocument:\r\n");
                _msg.Append(dirEx.ToXmlDocument(true).OuterXml);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                

                _msg.Length = 0;
                _msg.Append("\r\nDirEx SaveToXmlFile:\r\n");
                dirEx.SaveToXmlFile(@"c:\temp\direx.xml", true);
                _msg.Append("\r\nDirEx LoadFromXmlFile:\r\n");
                PFDirectoryEx direx2 = PFDirectoryEx.LoadFromXmlFile(@"c:\temp\direx.xml");
                if (direx2 != null)
                {
                    _msg.Length = 0;
                    _msg.Append("\r\nDIREX2 ToString:\r\n--------------\r\n");
                    _msg.Append(direx2.ToString());
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());

                    _msg.Length = 0;
                    _msg.Append("\r\nDIREX2 ToXmlString:\r\n--------------\r\n");
                    _msg.Append(direx2.ToXmlString(true));
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("\r\n");
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Append("\r\nERROR: Unable to load DIREX2 from Xml file");
                }
           


                _msg.Length = 0;
                _msg.Append("\r\nFile information:\r\n");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFFileEx filex = dirEx.Files.FirstItem;
                PFFileSpec fileSpec = new PFFileSpec("TestAttr*.*");
                while (filex != null)
                {
                    if(fileSpec.IsMatch(filex.Name))
                    {
                        _msg.Length = 0;
                        _msg.Append("File name: ");
                        _msg.Append(filex.Name);
                        _msg.Append("\r\n");
                        _msg.Append("File path: ");
                        _msg.Append(filex.FullName);
                        _msg.Append("\r\n");
                        _msg.Append("File size: ");
                        _msg.Append(filex.Size.ToString("#,##0"));
                        _msg.Append("\r\n");
                        _msg.Append("IsReadOnly: ");
                        _msg.Append(filex.IsReadOnly.ToString());
                        _msg.Append("\r\n");
                        _msg.Append("IsHidden: ");
                        _msg.Append(filex.IsHidden.ToString());
                        _msg.Append("\r\n");
                        _msg.Append("IsCompressed: ");
                        _msg.Append(filex.IsCompressed.ToString());
                        _msg.Append("\r\n");
                        _msg.Append("Ready to Archive: ");
                        _msg.Append(filex.IsReadyToArchive.ToString());
                        _msg.Append("\r\n");
                        _msg.Append("\r\n");

                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                    if (filex.Name == "testout8gb.txt" || filex.Name == "Database1.accdb")
                    {
                        _msg.Length = 0;
                        _msg.Append("ToString:\r\n--------------\r\n");
                        _msg.Append(filex.ToString());
                        _msg.Append("\r\n");

                        Program._messageLog.WriteLine(_msg.ToString());

                        _msg.Length = 0;
                        _msg.Append("ToXmlString:\r\n--------------\r\n");
                        _msg.Append(filex.ToXmlString());
                        _msg.Append("\r\n");

                        Program._messageLog.WriteLine(_msg.ToString());

                        filex.SaveToXmlFile(@"c:\temp\testfilex.xml");

                        PFFileEx filex2 = PFFileEx.LoadFromXmlFile(@"c:\temp\testfilex.xml");
                        if (filex2 != null)
                        {
                            _msg.Length = 0;
                            _msg.Append("\r\nFILEX2 ToString:\r\n--------------\r\n");
                            _msg.Append(filex2.ToString());
                            _msg.Append("\r\n");
                            Program._messageLog.WriteLine(_msg.ToString());

                            _msg.Length = 0;
                            _msg.Append("\r\nFILEX2 ToXmlString:\r\n--------------\r\n");
                            _msg.Append(filex2.ToXmlString());
                            _msg.Append("\r\n");
                            Program._messageLog.WriteLine(_msg.ToString());
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("\r\n");
                            Program._messageLog.WriteLine(_msg.ToString());
                            _msg.Append("\r\nERROR: Unable to load FILEX2 from Xml file");
                        }
                    }
                    filex = dirEx.Files.NextItem;
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... PFDirectoryExTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ReadListsTest(MainForm frm)
        {
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("ReadListsTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                if (Directory.Exists(frm.txtDirectoryToProcess.Text) == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find directory: ");
                    _msg.Append(frm.txtDirectoryToProcess.Text);
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Directoy to process: ");
                _msg.Append(frm.txtDirectoryToProcess.Text);
                Program._messageLog.WriteLine(_msg.ToString());

                sw.ShowMilliseconds = true;
                sw.Start();

                PFDirectoryEx dirEx = new PFDirectoryEx(frm.txtDirectoryToProcess.Text, frm.chkGetDirectoryTree.Checked);


                _msg.Length = 0;
                _msg.Append("\r\n\r\n");
                _msg.Append("Name = ");
                _msg.Append(dirEx.Name);
                _msg.Append("\r\n");
                _msg.Append("FullName = ");
                _msg.Append(dirEx.FullName);
                _msg.Append("\r\n");
                _msg.Append("NumBytes = ");
                _msg.Append(dirEx.NumBytesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("NumFiles = ");
                _msg.Append(dirEx.NumFilesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("NumSubdirs = ");
                _msg.Append(dirEx.NumSubdirectoriesInDirectory.ToString("#,##0"));
                _msg.Append("\r\n");
                if (frm.chkGetDirectoryTree.Checked)
                {
                    _msg.Append("Total NumBytes = ");
                    _msg.Append(dirEx.NumBytesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                    _msg.Append("Total NumFiles = ");
                    _msg.Append(dirEx.TotalNumFilesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                    _msg.Append("Total Subdirectories = ");
                    _msg.Append(dirEx.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("\r\n");
                }
                _msg.Append("Number of errors = ");
                _msg.Append(dirEx.NumErrors.ToString("#,##0"));
                _msg.Append("\r\n");
                if (dirEx.ErrorMessages.Length > 0)
                {
                    _msg.Append("Error messages:\r\n");
                    _msg.Append(dirEx.ErrorMessages);
                    _msg.Append("\r\n");
                }

                Program._messageLog.WriteLine(_msg.ToString());

                sw.Stop();


                _msg.Length = 0;
                _msg.Append("\r\nTime to create new PFDirectoryEx instance:\r\n");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                OutputDirectoryTree(dirEx);

                //PFFileEx fileEx = null;
                //if (dirEx.Files.Count > 0)
                //{
                //    fileEx = dirEx.Files.FirstItem;
                //    while (fileEx != null)
                //    {
                //        _msg.Length = 0;
                //        _msg.Append("FILE: ");
                //        _msg.Append(fileEx.Name);
                //        _msg.Append(" (");
                //        _msg.Append(fileEx.FullName);
                //        _msg.Append(")\r\n");
                //        _msg.Append("\tSize: ");
                //        _msg.Append(fileEx.Size.ToString("#,##0"));
                //        _msg.Append("  Last Modified: ");
                //        _msg.Append(fileEx.LastModifiedTime.ToString("yyyy/MM/dd HH:mm:ss"));
                //        _msg.Append("  Archive: ");
                //        _msg.Append(fileEx.IsReadyToArchive.ToString());
                //        _msg.Append("  ReadOnly: ");
                //        _msg.Append(fileEx.IsReadOnly.ToString());
                //        _msg.Append("  Compressed: ");
                //        _msg.Append(fileEx.IsCompressed.ToString());
                //        _msg.Append("  Encrypted: ");
                //        _msg.Append(fileEx.IsEncrypted.ToString());

                //        Program._messageLog.WriteLine(_msg.ToString());

                //        fileEx = dirEx.Files.NextItem;
                //    }
                //}//end if

                //PFDirectoryEx dirEx2 = null;
                //if (dirEx.Subdirectories.Count > 0)
                //{
                //    dirEx2 = dirEx.Subdirectories.FirstItem;
                //    while(dirEx2 != null)
                //    {
                //        _msg.Length = 0;
                //        _msg.Append("DIR: ");
                //        _msg.Append(dirEx2.Name);
                //        _msg.Append(" (");
                //        _msg.Append(dirEx2.FullName);
                //        _msg.Append(")\r\n");
                //        _msg.Append("\tNumBytesInDir:  ");
                //        _msg.Append(dirEx2.NumBytesInDirectory.ToString("#,##0"));
                //        _msg.Append("  NumFilesInDir:  ");
                //        _msg.Append(dirEx2.NumFilesInDirectory.ToString("#,##0"));
                //        _msg.Append("  NumSubdirsInDir:  ");
                //        _msg.Append(dirEx2.NumSubdirectoriesInDirectory.ToString("#,##0"));
                //        _msg.Append("  NumBytesInDirTree:  ");
                //        _msg.Append(dirEx2.NumBytesInDirectoryTree.ToString("#,##0"));
                //        _msg.Append("  TotNumFilesInDirTree:  ");
                //        _msg.Append(dirEx2.TotalNumFilesInDirectoryTree.ToString("#,##0"));
                //        _msg.Append("  TotNumSubdirsInDirTree:  ");
                //        _msg.Append(dirEx2.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));

                //        Program._messageLog.WriteLine(_msg.ToString());

                //        dirEx2 = dirEx.Subdirectories.NextItem;
                //    }
                //}

            
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... ReadListsTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void OutputDirectoryTree(PFDirectoryEx dirEx)
        {

            _msg.Length = 0;
            _msg.Append("\r\n**** Outputting information for ****\r\n    ");
            _msg.Append(dirEx.FullName);
            _msg.Append("\r\n");
            Program._messageLog.WriteLine(_msg.ToString());

            PFFileEx fileEx = null;
            if (dirEx.Files.Count > 0)
            {
                fileEx = dirEx.Files.FirstItem;
                while (fileEx != null)
                {
                    _msg.Length = 0;
                    _msg.Append("FILE: ");
                    _msg.Append(fileEx.Name);
                    _msg.Append(" (");
                    _msg.Append(fileEx.FullName);
                    _msg.Append(")\r\n");
                    _msg.Append("\tSize: ");
                    _msg.Append(fileEx.Size.ToString("#,##0"));
                    _msg.Append("  Last Modified: ");
                    _msg.Append(fileEx.LastModifiedTime.ToString("yyyy/MM/dd HH:mm:ss"));
                    _msg.Append("  Archive: ");
                    _msg.Append(fileEx.IsReadyToArchive.ToString());
                    _msg.Append("  ReadOnly: ");
                    _msg.Append(fileEx.IsReadOnly.ToString());
                    _msg.Append("  Compressed: ");
                    _msg.Append(fileEx.IsCompressed.ToString());
                    _msg.Append("  Encrypted: ");
                    _msg.Append(fileEx.IsEncrypted.ToString());

                    Program._messageLog.WriteLine(_msg.ToString());

                    fileEx = dirEx.Files.NextItem;
                }
            }//end if

            PFDirectoryEx dirEx2 = null;
            if (dirEx.Subdirectories.Count > 0)
            {
                dirEx2 = dirEx.Subdirectories.FirstItem;
                while (dirEx2 != null)
                {
                    _msg.Length = 0;
                    _msg.Append("DIR: ");
                    _msg.Append(dirEx2.Name);
                    _msg.Append(" (");
                    _msg.Append(dirEx2.FullName);
                    _msg.Append(")\r\n");
                    _msg.Append("\tNumBytesInDir:  ");
                    _msg.Append(dirEx2.NumBytesInDirectory.ToString("#,##0"));
                    _msg.Append("  NumFilesInDir:  ");
                    _msg.Append(dirEx2.NumFilesInDirectory.ToString("#,##0"));
                    _msg.Append("  NumSubdirsInDir:  ");
                    _msg.Append(dirEx2.NumSubdirectoriesInDirectory.ToString("#,##0"));
                    _msg.Append("  NumBytesInDirTree:  ");
                    _msg.Append(dirEx2.NumBytesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("  TotNumFilesInDirTree:  ");
                    _msg.Append(dirEx2.TotalNumFilesInDirectoryTree.ToString("#,##0"));
                    _msg.Append("  TotNumSubdirsInDirTree:  ");
                    _msg.Append(dirEx2.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));
                    if (dirEx2.NumErrors > 0)
                    {
                        _msg.Append("\r\n");
                        _msg.Append("\tNumErrors: ");
                        _msg.Append(dirEx2.NumErrors.ToString("#,##0"));
                        _msg.Append("  Messages: ");
                        _msg.Append(dirEx2.ErrorMessages);
                    }

                    Program._messageLog.WriteLine(_msg.ToString());

                    dirEx2 = dirEx.Subdirectories.NextItem;
                }

                //recurse through tree to output all information on subdirectories and their files
                dirEx2 = dirEx.Subdirectories.FirstItem;
                while (dirEx2 != null)
                {
                    OutputDirectoryTree(dirEx2);
                    dirEx2 = dirEx.Subdirectories.NextItem;
                }
            }

        
        }


        public static void FileNtfsEncryptDecryptTest(MainForm frm)
        {
            UserImpersonator imp = new UserImpersonator();
            bool ret = false;
            string fileName = @"c:\temp\testfileNtfsEncryption.txt";
            PFFileEx fileEx = new PFFileEx(fileName);

            try
            {
                _msg.Length = 0;
                _msg.Append("FileNtfsEncryptDecryptTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                fileEx.Encrypt();

                _msg.Length = 0;
                _msg.Append("File has been encrypted by ");
                _msg.Append(Environment.UserName);
                _msg.Append(" (IsEncrypted = ");
                _msg.Append(fileEx.IsEncrypted.ToString());
                _msg.Append(")");
                Program._messageLog.WriteLine(_msg.ToString());

                try
                {
                    imp.Username = "Miketest";
                    imp.Password = "miketest";
                    imp.Domain = "PROFASTWS5";

                    ret = imp.Impersonate();

                    if (ret == true)
                    {
                        //impersonation worked
                        _msg.Length = 0;
                        _msg.Append("Impersonate succeeded: Now logged on as ");
                        _msg.Append(Environment.UserName);
                        _msg.Append("\r\n");
                        Program._messageLog.WriteLine(_msg.ToString());

                        try
                        {
                            fileEx.Decrypt();

                            _msg.Length = 0;
                            _msg.Append("File has been decrypted by ");
                            _msg.Append(Environment.UserName);
                            _msg.Append(" (IsEncrypted = ");
                            _msg.Append(fileEx.IsEncrypted.ToString());
                            _msg.Append(")");
                            Program._messageLog.WriteLine(_msg.ToString());

                        }
                        catch (System.Exception)
                        {
                            _msg.Length = 0;
                            _msg.Append(Environment.UserName);
                            _msg.Append(" is unable to decrypt the file.");
                            Program._messageLog.WriteLine(_msg.ToString());
                        }

                        if (imp.Revert() == true)
                        {
                            _msg.Length = 0;
                            _msg.Append("\r\nImpersonate revert succeeded. Back to original logon: ");
                            _msg.Append(Environment.UserName);
                            _msg.Append("\r\n");
                            Program._messageLog.WriteLine(_msg.ToString());

                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("\r\nImpersonate revert failed: \r\n");
                            Program._messageLog.WriteLine(_msg.ToString());
                        }
                    }
                    else
                    {
                        //impersonation failed
                        _msg.Length = 0;
                        _msg.Append("\r\nImpersonate failed: \r\n");
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }
                catch (System.Exception)
                {
                    _msg.Length = 0;
                    _msg.Append(Environment.UserName);
                    _msg.Append(" is unable to decrypt the file.");
                    Program._messageLog.WriteLine(_msg.ToString());
                }


                fileEx.Decrypt();
                _msg.Length = 0;
                _msg.Append("File has been decrypted by ");
                _msg.Append(Environment.UserName);
                _msg.Append(" (IsEncrypted = ");
                _msg.Append(fileEx.IsEncrypted.ToString());
                _msg.Append(")");
                Program._messageLog.WriteLine(_msg.ToString());



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... FileNtfsEncryptDecryptTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void ValidPathAndFileTest(MainForm frm)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("ValidPathAndFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                bool isValid = false;
                string testPath = @"c:\abc\efg\file1.dat";
                string testFile = @"my file_now.txt.zip";

                _msg.Length = 0;
                _msg.Append("GetInvalidPathChars: ");
                _msg.Append(new String(Path.GetInvalidPathChars()));
                _msg.Append("\r\n\r\n");
                _msg.Append("GetInvalidFileNameChars: ");
                _msg.Append(new String(Path.GetInvalidFileNameChars()));
                _msg.Append("\r\n\r\n");
                Program._messageLog.WriteLine(_msg.ToString());


                isValid = PFFile.IsValidFileName(testFile);
                OutputValidityCheckResult(testFile, isValid);
                testFile = @"fil&<|.x";
                isValid = PFFile.IsValidFileName(testFile);
                OutputValidityCheckResult(testFile, isValid);
                isValid = PFFile.IsValidPath(testPath);
                OutputValidityCheckResult(testPath, isValid);
                testPath = @"d:\ab+c\ggg\t?st.xxx";
                isValid = PFFile.IsValidPath(testPath);
                OutputValidityCheckResult(testPath, isValid);
                testPath = @"d:\abc\ggg\t&st.xxx";
                isValid = PFFile.IsValidPath(testPath);
                OutputValidityCheckResult(testPath, isValid);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... ValidPathAndFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void OutputValidityCheckResult(string name, bool res)
        {
            _msg.Length = 0;
            _msg.Append(name);
            _msg.Append(": ");
            _msg.Append(res.ToString());
            Program._messageLog.WriteLine(_msg.ToString());
        }


        public static void PFFileExAttributesTest(MainForm frm)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("PFFileExAttributesTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFFile.FileDelete(@"C:\Testfiles\AttributesTestfiles\test1.txt");
                PFFile.FileDelete(@"C:\Testfiles\AttributesTestfiles\test2.txt");
                PFFile.FileDelete(@"C:\Testfiles\AttributesTestfiles\test3.txt");

                File.Copy(@"C:\Testfiles\AttributesTestfiles\InitFiles\test1.txt", @"C:\Testfiles\AttributesTestfiles\test1.txt", true);
                File.Copy(@"C:\Testfiles\AttributesTestfiles\InitFiles\test2.txt", @"C:\Testfiles\AttributesTestfiles\test2.txt", true);
                File.Copy(@"C:\Testfiles\AttributesTestfiles\InitFiles\test3.txt", @"C:\Testfiles\AttributesTestfiles\test3.txt", true);


                PFFileEx f1 = new PFFileEx(@"C:\Testfiles\AttributesTestfiles\test1.txt");
                f1.SetReadOnlyAttribute();
                f1.SetHiddenAttribute();

                PFFileEx f2 = new PFFileEx(@"C:\Testfiles\AttributesTestfiles\test2.txt");
                f2.RemoveReadOnlyAttribute();
                f2.RemoveHiddenAttribute();

                PFFileEx f3 = new PFFileEx(@"C:\Testfiles\AttributesTestfiles\test3.txt");
                f3.SetReadOnlyAttribute();
                f3.SetHiddenAttribute();
                f3.RemoveReadOnlyAttribute();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... PFFileExAttributesTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void PFDirectoryStatsTest(MainForm frm)
        {
            PFDirectoryStats dirStats = null;
            string folderPath = string.Empty;
            try
            {
                _msg.Length = 0;
                _msg.Append("PFDirectoryStatsTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                folderPath = frm.txtDirectoryToProcess.Text;

                //dirStats = new PFDirectoryStats(@"c:\temp\mikesave\", true);
                //dirStats = new PFDirectoryStats(@"c:\temp\mikesave\test1", true);
                dirStats = new PFDirectoryStats(folderPath, true);

                _msg.Length = 0;
                _msg.Append(dirStats.FullName);
                _msg.Append(Environment.NewLine);
                _msg.Append("Size: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.NumBytesInDirectory.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.NumBytesInDirectoryTree.ToString("#,##0"));

                _msg.Append(Environment.NewLine);
                _msg.Append("NumFolders: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.NumSubdirectoriesInDirectory.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));

                _msg.Append(Environment.NewLine);
                _msg.Append("NumFiles: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.NumFilesInDirectory.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append(dirStats.TotalNumFilesInDirectoryTree.ToString("#,##0"));
                
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                dirStats = null;

                _msg.Length = 0;
                _msg.Append("\r\n... PFDirectoryStatsTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void PFTempFileTest(MainForm frm)
        {
            PFTempFile tempfile0 = null;
            try
            {
                _msg.Length = 0;
                _msg.Append("PFTempFileTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                //dispose will be called automatically at the end of this block
                using(PFTempFile tempfile = new PFTempFile())
                {
                    _msg.Length = 0;
                    _msg.Append("Tempfile name: ");
                    _msg.Append(tempfile.TempFileName);
                    Program._messageLog.WriteLine(_msg.ToString());
                }


                tempfile0 = new PFTempFile();

                _msg.Length = 0;
                _msg.Append("Tempfile0 name: ");
                _msg.Append(tempfile0.TempFileName);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                tempfile0.Dispose();
                tempfile0 = null;
                _msg.Length = 0;
                _msg.Append("\r\n... PFTempFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void PFTempFileCollectionTest(MainForm frm)
        {
            PFTempFileCollection tempFiles = new PFTempFileCollection(@"c:\temp\", false);
            try
            {
                _msg.Length = 0;
                _msg.Append("PFTempFileCollectionTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                //.tmp file first created
                //.txt file created on first use (write to)
                string filename1 = tempFiles.AddExtension("txt", false);
                _msg.Length = 0;
                _msg.Append("filename1 = ");
                _msg.Append(filename1);
                _msg.Append(" Exists = ");
                _msg.Append(File.Exists(filename1).ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                File.WriteAllText(filename1, "This is the test.");

                string contents = File.ReadAllText(filename1);

                Program._messageLog.WriteLine(contents);

                string filename2 = @"c:\temp\test1temp.xxx";
                tempFiles.AddFile(filename2, false);

                _msg.Length = 0;
                _msg.Append("filename2 = ");
                _msg.Append(filename2);
                _msg.Append(" Exists = ");
                _msg.Append(File.Exists(filename2).ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                File.WriteAllText(filename2, "This is the second test.");

                string contents2 = File.ReadAllText(filename2);

                Program._messageLog.WriteLine(contents2);

                _msg.Length = 0;
                _msg.Append("Num temp files: ");
                _msg.Append(tempFiles.Count.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                tempFiles.Dispose();
                tempFiles = null;
                _msg.Length = 0;
                _msg.Append("\r\n... PFTempFileCollectionTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void CopyDirectoryTest(MainForm frm)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("CopyDirectoryTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtSourceDirectory.Text.Trim().Length == 0
                    || frm.txtDestinationDirectory.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both source and destination directories for the CopyDirectoryTest");
                    throw new System.Exception(_msg.ToString());
                }

                _numXCopyResultMessages = 0;

                if (frm.chkDeleteDestinationBeforeCopy.Checked)
                {
                    if (Directory.Exists(frm.txtDestinationDirectory.Text))
                    {
                        PFDirectory.returnXDeleteResult += new PFDirectory.XDeleteResultDelegate(OutputXDeleteResultMessages);
                        PFDirectory.returnErrorMessage += new PFDirectory.ErrorMessageDelegate(OutputErrorMessages);
                        XDeleteInfo deleteInfo = PFDirectory.XDelete(frm.txtDestinationDirectory.Text, frm.txtSearchPattern.Text, true, true, 15);
                        PFDirectory.returnXDeleteResult -= new PFDirectory.XDeleteResultDelegate(OutputXDeleteResultMessages);
                        PFDirectory.returnErrorMessage -= new PFDirectory.ErrorMessageDelegate(OutputErrorMessages);
                        _msg.Length = 0;
                        _msg.Append("Destination directory already existed. ");
                        _msg.Append(frm.txtDestinationDirectory.Text);
                        _msg.Append(" was deleted before xcopy operation was processed.");
                        Program._messageLog.WriteLine(_msg.ToString());

                        _msg.Length = 0;
                        if (Directory.Exists(frm.txtDestinationDirectory.Text)==false)
                        {
                            _msg.Append(frm.txtSourceDirectory.Text);
                            _msg.Append(" deleted.");
                        }
                        else
                        {
                            if (frm.txtSearchPattern.Text.ToLower().Trim() == "*.*"
                                || String.IsNullOrEmpty(frm.txtSearchPattern.Text.ToLower().Trim()))
                            {
                                _msg.Append("XDelete failed for ");
                                _msg.Append(frm.txtSourceDirectory.Text);
                                _msg.Append(". Folder still exists.");
                            }
                            else
                            {
                                _msg.Append("XDelete did partial delete based on search pattern for ");
                                _msg.Append(frm.txtSourceDirectory.Text);
                                _msg.Append(". Folder still exists.");
                            }
                        }
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num folders deleted:       ");
                        _msg.Append(deleteInfo.NumFolders.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num files deleted:         ");
                        _msg.Append(deleteInfo.NumFiles.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num bytes deleted:         ");
                        _msg.Append(deleteInfo.NumBytes.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num Folders not deleted:   ");
                        _msg.Append(deleteInfo.NumFoldersNotDeleted.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num Files not deleted:     ");
                        _msg.Append(deleteInfo.NumFilesNotDeleted.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Num errors:                ");
                        _msg.Append(deleteInfo.NumErrors.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        if (deleteInfo.NumErrors > 0)
                        {
                            _msg.Append("Error messages:           ");
                            _msg.Append(Environment.NewLine);
                            _msg.Append(deleteInfo.errorMessages);
                            _msg.Append(Environment.NewLine);
                        }
                        Program._messageLog.WriteLine(_msg.ToString());

                    }//end if directory exists
                }//end delete destination first if

                PFDirectory.returnXCopyResult += new PFDirectory.XCopyResultDelegate(OutputXCopyResultMessages);
                PFDirectory.returnErrorMessage += new PFDirectory.ErrorMessageDelegate(OutputErrorMessages);
                XCopyInfo sizeInfo = PFDirectory.XCopy(frm.txtSourceDirectory.Text, frm.txtDestinationDirectory.Text, frm.txtSearchPattern.Text, true, true, frm.chkPreserveTimestamps.Checked, true, 15);
                PFDirectory.returnXCopyResult -= new PFDirectory.XCopyResultDelegate(OutputXCopyResultMessages);
                PFDirectory.returnErrorMessage -= new PFDirectory.ErrorMessageDelegate(OutputErrorMessages);

                _msg.Length = 0;
                if (Directory.Exists(frm.txtDestinationDirectory.Text))
                {
                    _msg.Append(frm.txtDestinationDirectory.Text);
                    _msg.Append(" copied.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num folders copied:       ");
                    _msg.Append(sizeInfo.NumFolders.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num files copied:         ");
                    _msg.Append(sizeInfo.NumFiles.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num bytes copied:         ");
                    _msg.Append(sizeInfo.NumBytes.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num Directory Overwrites: ");
                    _msg.Append(sizeInfo.NumFolderOverwrites.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num File Overwrites:      ");
                    _msg.Append(sizeInfo.NumFileOverwrites.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Num errors:               ");
                    _msg.Append(sizeInfo.NumErrors.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    if (sizeInfo.NumErrors > 0)
                    {
                        _msg.Append("Error messages:           ");
                        _msg.Append(Environment.NewLine);
                        _msg.Append(sizeInfo.errorMessages);
                        _msg.Append(Environment.NewLine);
                    }
                }
                else
                {
                    _msg.Append("XCopy failed for ");
                    _msg.Append(frm.txtSourceDirectory.Text);
                    _msg.Append(". Destination ");
                    _msg.Append(frm.txtDestinationDirectory.Text);
                    _msg.Append(" not found.");
                }
                Program._messageLog.WriteLine(_msg.ToString());


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... CopyDirectoryTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }//end method

        private static long _numXCopyResultMessages = 0;
        private static long _outputXCopyResultsInterval = 1;
        private static void OutputXCopyResultMessages(SourceAndDestinationInfo objectInfo, XCopyInfo runningTotals)
        {
            _numXCopyResultMessages++;

            if ((_numXCopyResultMessages % _outputXCopyResultsInterval) == 0)
            {
                _msg.Length = 0;
                if(_outputXCopyResultsInterval == 1)
                {
                    _msg.Append(objectInfo.objectType.ToString());
                    _msg.Append(" copy: ");
                    _msg.Append(objectInfo.sourcePath);
                    _msg.Append(" to ");
                    _msg.Append(objectInfo.destinationPath);
                    _msg.Append("  Bytes: ");
                    _msg.Append(objectInfo.destinationBytes.ToString("#,##0"));
                    _msg.Append(".");
                    _msg.Append(Environment.NewLine);
                }
                _msg.Append("Total Num Folders Copied: ");
                _msg.Append(runningTotals.NumFolders.ToString("#,##0"));
                _msg.Append("  Files: ");
                _msg.Append(runningTotals.NumFiles.ToString("#,##0"));
                _msg.Append("  Bytes: ");
                _msg.Append(runningTotals.NumBytes.ToString("#,##0"));

                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        private static long _numXDeleteResultMessages = 0;
        private static long _outputXDeleteResultsInterval = 1;
        private static void OutputXDeleteResultMessages(SourceInfo objectInfo, XDeleteInfo runningTotals)
        {
            _numXDeleteResultMessages++;

            if ((_numXDeleteResultMessages % _outputXDeleteResultsInterval) == 0)
            {
                _msg.Length = 0;
                if (_outputXDeleteResultsInterval == 1)
                {
                    _msg.Append(objectInfo.objectType.ToString());
                    _msg.Append(" delete: ");
                    _msg.Append(objectInfo.sourcePath);
                    _msg.Append("  Bytes: ");
                    _msg.Append(objectInfo.sourceBytes.ToString("#,##0"));
                    _msg.Append(".");
                    _msg.Append(Environment.NewLine);
                }
                _msg.Append("Total Num Folders Deleted: ");
                _msg.Append(runningTotals.NumFolders.ToString("#,##0"));
                _msg.Append("  Files: ");
                _msg.Append(runningTotals.NumFiles.ToString("#,##0"));
                _msg.Append("  Bytes: ");
                _msg.Append(runningTotals.NumBytes.ToString("#,##0"));
                if (runningTotals.NumFoldersNotDeleted > 0)
                {
                    _msg.Append("  Folders Not Deleted: ");
                    _msg.Append(runningTotals.NumFoldersNotDeleted.ToString("#,##0"));
                }
                if (runningTotals.NumFilesNotDeleted > 0)
                {
                    _msg.Append("  Files Not Deleted: ");
                    _msg.Append(runningTotals.NumFilesNotDeleted.ToString("#,##0"));
                }

                Program._messageLog.WriteLine(_msg.ToString());
            }

        }



        public static void DeleteFilesTest(MainForm frm)
        {
            string folderPath = frm.txtDeletePath.Text;
            int numFilesDeleted = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("DeleteFilesTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                    _msg.Length = 0;
                    _msg.Append(folderPath);
                    _msg.Append(" deleted.");
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                string origFolderPath = folderPath + "Orig";
                PFDirectory.XCopy(origFolderPath, folderPath, true);
                _msg.Length = 0;
                _msg.Append(folderPath);
                _msg.Append(" created.");
                Program._messageLog.WriteLine(_msg.ToString());

                PFDirectory.returnDeleteFilesResult += new PFDirectory.DeleteFilesResultDelegate(OutputDeleteFileResults);
                numFilesDeleted = PFDirectory.DeleteFiles(folderPath, frm.txtDeleteFilesFileSpec.Text, frm.chkIncludeFilesInSubfolders.Checked);
                PFDirectory.returnDeleteFilesResult -= new PFDirectory.DeleteFilesResultDelegate(OutputDeleteFileResults);

                _msg.Length = 0;
                _msg.Append("Num files deleted: ");
                _msg.Append(numFilesDeleted);
                Program._messageLog.WriteLine(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... DeleteFilesTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void OutputDeleteFileResults(string deletedFilePath)
        {
            _msg.Length = 0;
            _msg.Append("Deleted file: ");
            _msg.Append(deletedFilePath);
            Program._messageLog.WriteLine(_msg.ToString());
        }



        public static void DeleteSubfoldersTest(MainForm frm)
        {
            string folderPath = frm.txtDeletePath.Text;
            int numEntriesDeleted = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("DeleteSubfoldersTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                    _msg.Length = 0;
                    _msg.Append(folderPath);
                    _msg.Append(" deleted.");
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                string origFolderPath = folderPath + "Orig";
                PFDirectory.XCopy(origFolderPath, folderPath, true);
                _msg.Length = 0;
                _msg.Append(folderPath);
                _msg.Append(" created.");
                Program._messageLog.WriteLine(_msg.ToString());

                PFDirectory.returnDeleteSubfolderResult += new PFDirectory.DeleteSubfolderResultDelegate(OutputDeleteSubfolderResults);
                numEntriesDeleted = PFDirectory.DeleteSubFolders(folderPath, frm.txtDeleteFilesFileSpec.Text, frm.chkIncludeFilesInSubfolders.Checked);
                PFDirectory.returnDeleteSubfolderResult -= new PFDirectory.DeleteSubfolderResultDelegate(OutputDeleteSubfolderResults);

                _msg.Length = 0;
                _msg.Append("Num entries (folder and files) deleted: ");
                _msg.Append(numEntriesDeleted);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... DeleteSubfoldersTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void OutputDeleteSubfolderResults(FileSystemObjectType deletedObjectType, string deletedObjectPath)
        {
            _msg.Length = 0;
            _msg.Append("Deleted ");
            _msg.Append(deletedObjectType.ToString());
            _msg.Append(": ");
            _msg.Append(deletedObjectPath);
            Program._messageLog.WriteLine(_msg.ToString());
        }

        private static void OutputErrorMessages(string errorMessage)
        {
            _msg.Length = 0;
            _msg.Append("ERROR: ");
            _msg.Append(errorMessage);
            Program._messageLog.WriteLine(_msg.ToString());
        }


        private static string _folderName = @"C:\Testfiles\QuickTest";
        private static bool _includeSubfolders = true;

        public static void CompressDirectoryTest()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("CompressDirectoryTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                PFDirectory.Compress(_folderName, _includeSubfolders);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... CompressDirectoryTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void UncompressDirectoryTest()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("UncompressDirectoryTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                PFDirectory.Uncompress(_folderName, _includeSubfolders);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... UncompressDirectoryTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        private static string _fileName = @"C:\Testfiles\QuickTest\FirstNames.txt";

        public static void CompressFileTest()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("CompressFileTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                PFFile.Compress(_fileName);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... CompressFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void UncompressFileTest()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("UncompressFileTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());


                PFFile.Uncompress(_fileName);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... UncompressFileTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }




    }//end class
}//end namespace

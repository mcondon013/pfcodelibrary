using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFAppUtils;
using PFFileSystemObjects;
using System.Windows.Forms;
using System.Xml;

namespace TestprogAppUtils
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
        public static void RunShowDateTimeTest()
        {
            int testCounter = 0;
            try
            {
                testCounter++;
                _msg.Length = 0;
                _msg.Append(testCounter.ToString("#,##0"));
                _msg.Append(": ");
                _msg.Append("Current date/time is ");
                _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
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
                ;

            }
        }


        public static void ShowNamelistPrompt()
        {
            PFNameListPrompt nlp = new PFNameListPrompt();
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowNamelistPrompt started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                nlp.Text = "Test of NameListPrompt";
                nlp.lblSelect.Text = "Select an item from the list below:";

                for (int i = 1; i <= 10; i++)
                {
                    nlp.lstNames.Items.Add("TestName_" + i.ToString("000"));
                }

                DialogResult res = nlp.ShowDialog();

                _msg.Length = 0;
                _msg.Append("DiaglogResult: ");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append("Selected item: ");
                    _msg.Append(nlp.lstNames.SelectedItem.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                

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
                nlp.Dispose();
                _msg.Length = 0;
                _msg.Append("\r\n... ShowNamelistPrompt finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void ShowTreeViewFolderBrowser(MainForm frm)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowTreeViewFolderBrowser started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Show folder tree for ");
                _msg.Append(frm.txtRootFolderPath.Text.Trim());
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                PFTreeViewFolderBrowserForm folderBrowserForm = new PFTreeViewFolderBrowserForm(frm.txtRootFolderPath.Text.Trim());
                folderBrowserForm.ShowDialog();
                if (folderBrowserForm.DialogResult == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append("Number of selected folders: ");
                    _msg.Append(folderBrowserForm.SelectedFolders.Count.ToString());
                    _msg.Append(Environment.NewLine);
                    Program._messageLog.WriteLine(_msg.ToString());

                    foreach (string str in folderBrowserForm.SelectedFolders)
                    {
                        _msg.Length = 0;
                        _msg.Append(str);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }

                    Program._messageLog.WriteLine(Environment.NewLine);

                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("DialogResult is ");
                    _msg.Append(folderBrowserForm.DialogResult.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }
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
                _msg.Append("\r\n... ShowTreeViewFolderBrowser finished.");
                Program._messageLog.WriteLine(_msg.ToString());

                frm.Focus();

            }
        }



        public static void ShowTreeViewFolderBrowserExt(MainForm frm)
        {
            List<string> rootFolderPaths = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowTreeViewFolderBrowserExt started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                rootFolderPaths = PFDiskDrive.GetDiskDrivesRootDirectories();

                PFTreeViewFolderBrowserFormExt folderBrowserForm = new PFTreeViewFolderBrowserFormExt(rootFolderPaths);
                folderBrowserForm.ShowDialog();
                if (folderBrowserForm.DialogResult == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append("Number of selected folders: ");
                    _msg.Append(folderBrowserForm.SelectedFolders.Count.ToString());
                    _msg.Append(Environment.NewLine);
                    Program._messageLog.WriteLine(_msg.ToString());

                    foreach (string str in folderBrowserForm.SelectedFolders)
                    {
                        _msg.Length = 0;
                        _msg.Append(str);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }

                    Program._messageLog.WriteLine(Environment.NewLine);

                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("DialogResult is ");
                    _msg.Append(folderBrowserForm.DialogResult.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

            
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
                _msg.Append("\r\n... ShowTreeViewFolderBrowserExt finished.");
                Program._messageLog.WriteLine(_msg.ToString());

                frm.Focus();

            }
        }


        public static void PFClassWriterTest(MainForm frm)
        {
            PFClassExtended1 cls = new PFClassExtended1();

            try
            {
                _msg.Length = 0;
                _msg.Append("PFClassWriterTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFClassWriter<PFClassExtended1> classWriter = new PFClassWriter<PFClassExtended1>(cls);

                string toStringOutput = classWriter.ToString();
                _msg.Length = 0;
                _msg.Append("toStringOutput:\r\n");
                _msg.Append(toStringOutput);
                Program._messageLog.WriteLine(_msg.ToString());

                string toXmlString = classWriter.ToXmlString();
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("toXmlString:\r\n");
                _msg.Append(toXmlString);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("SaveToXmlFile:\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                classWriter.SaveToXmlFile(@"c:\temp\pfclasswritertest.xml");

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("LoadFromXmlFile:\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                PFClassExtended1 newClass = PFClassWriter<PFClassExtended1>.LoadFromXmlFile(@"c:\temp\pfclasswritertest.xml");
                newClass.TestLOng = 666777;

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("LoadFromXmlString:\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                PFClassExtended1 newClass2 = PFClassWriter<PFClassExtended1>.LoadFromXmlString(toXmlString);
                newClass2.TestLOng = 666777;
                Program._messageLog.WriteLine(newClass2.TestLOng.ToString());

                PFClassWriter<PFClassExtended1> classWriterNew = new PFClassWriter<PFClassExtended1>(newClass);

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("outputToXmlstring\r\n");
                Program._messageLog.WriteLine(classWriterNew.ToXmlString());

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("ToXmlDocument:\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                XmlDocument xmlDoc =  classWriter.ToXmlDocument(); ;
                _msg.Length = 0;
                _msg.Append(xmlDoc.OuterXml);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("Copy()\r\n");
                PFClassExtended1 copiedClass = classWriter.Copy();
                _msg.Append("TestString value = ");
                _msg.Append(copiedClass.TestString);
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
                _msg.Append("\r\n... PFClassWriterTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
               


    }//end class
}//end namespace

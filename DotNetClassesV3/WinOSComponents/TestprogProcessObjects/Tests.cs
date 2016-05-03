using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFProcessObjects;

namespace TestprogProcessObjects
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;
        private static string _consoleOutputFile = @"c:\temp\testout.txt";

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

        public static string ConsoleOutputFile
        {
            get
            {
                return Tests._consoleOutputFile;
            }
            set
            {
                Tests._consoleOutputFile = value;
            }
        }

        //tests

        public static void RunProcessTest(MainForm frm)
        {
            PFProcess proc = new PFProcess();
            string[] args = frm.txtArguments.Lines;
            StringBuilder clArgs = new StringBuilder();
            string fileToRun = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("RunProcessTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (args != null)
                {
                    if (args.Length >= 1)
                    {
                        clArgs.Append(args[0]);
                        if(args.Length > 1)
                            clArgs.Append(" ");
                    }
                    if (args.Length >= 2)
                    {
                        clArgs.Append(args[1]);
                    }
                }
                proc.Arguments = clArgs.ToString();
                proc.ExecutableToRun = frm.txtFileToRun.Text;
                proc.WorkingDirectory = frm.txtWorkingDirectory.Text;
                proc.CreateNoWindow = frm.chkCreateNoWindow.Checked;
                proc.UseShellExecute = frm.chkUseShellExecute.Checked;
                proc.WindowStyle = PFAppUtils.PFEnumProcessor.StringToEnum<PFProcessWindowStyle>(frm.cboWindowStyle.Text);
                proc.RedirectStandardOutput = frm.chkRedirectStandardOutput.Checked;
                proc.RedirectStandardError = frm.chkRedirectStandardError.Checked;
                proc.RedirectStandardInput = frm.chkRedirectStandardInput.Checked;
                proc.CheckIfProcessWaitingForInput = frm.chkCheckIfWaitingForInput.Checked;
                proc.MaxProcessRunSeconds = Convert.ToInt32(frm.txtMaxRunsecs.Text);

                proc.Run();

                _msg.Length = 0;
                _msg.Append("Process finished with exit code: ");
                _msg.Append(proc.ProcessExitCode.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time: ");
                _msg.Append(proc.ElapsedTime.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                if (proc.ProcessMessages.Length > 0)
                {
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Process messages:");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    _msg.Append(proc.ProcessMessages);
                    _msg.Append(Environment.NewLine);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                //get messages from the text file where messages were written to by this external app
                string fileContents = File.ReadAllText(_consoleOutputFile);
                Program._messageLog.WriteLine(Environment.NewLine);
                Program._messageLog.WriteLine(fileContents);

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
                //can delete console output file here, if needed
                _msg.Length = 0;
                _msg.Append("\r\n... RunProcessTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace

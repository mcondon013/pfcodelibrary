using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AppGlobals;

namespace TestprogExternalApp
{
    class Program
    {
        private static StringBuilder _msg = new StringBuilder();
        private static int _exitCode = 0;
        private static string _mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string _messageLogFile = AppConfig.GetStringValueFromConfigFile("ApplicationLogFileName", "InitConsoleAppLog.txt");
        private static bool _saveErrorMessagesToAppLog = true;
        private static string _outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\Temp\TextExtAppOutput.txt");

        static void Main(string[] args)
        {
            int numSecondsToRun = -1;
            bool pauseAtEndOfTest = false;
            string outputFolder = Path.GetDirectoryName(_outputFile);

            try
            {
                _msg.Length = 0;
                if (args != null)
                {
                    if (args.Length >= 1)
                    {
                        _msg.Append("args[0]=");
                        _msg.Append(args[0]);
                        _msg.Append(Environment.NewLine);
                        numSecondsToRun = Convert.ToInt32(args[0]);
                    }
                    if (args.Length >= 2)
                    {
                        _msg.Append("args[1]=");
                        _msg.Append(args[1]);
                        _msg.Append(Environment.NewLine);
                        _outputFile = args[1];
                        outputFolder = Path.GetDirectoryName(_outputFile);
                    }
                    if (args.Length >= 3)
                    {
                        _msg.Append("args[2]=");
                        _msg.Append(args[2]);
                        _msg.Append(Environment.NewLine);
                        string temp = args[2];
                        if (temp.ToUpper() == "TRUE" || temp.ToUpper() == "YES")
                            pauseAtEndOfTest = true;
                    }
                }

                if (Directory.Exists(outputFolder) == false)
                    Directory.CreateDirectory(outputFolder);

                StreamWriter sw = new StreamWriter(_outputFile);
                sw.AutoFlush = true;
                Console.SetOut(sw);

                if (_msg.Length > 0)
                    Console.Out.WriteLine(_msg.ToString());
                else
                    Console.Out.WriteLine("No command line arguments found.");

                
                
                if (System.IO.Path.GetDirectoryName(_messageLogFile).Length == 0)
                    _messageLogFile = _mydocumentsFolder + @"\" + _messageLogFile;
                ConsoleMessages.AppLogFilename = _messageLogFile;


                _msg.Length = 0;
                _msg.Append("AppLogFilename = ");
                _msg.Append(_messageLogFile);
                Console.Out.WriteLine(_msg.ToString());


                if (numSecondsToRun > 0)
                {
                    _exitCode = 2;
                    System.Threading.Thread.Sleep(numSecondsToRun * 1000);
                    _exitCode = 0;
                }

                _exitCode = RunTests(args);

            }
            catch (System.Exception ex)
            {
                _exitCode = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.ConsoleMessages.FormatErrorMessage(ex));
                Console.Out.WriteLine(_msg.ToString());
                ConsoleMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("Exit code is ");
                _msg.Append(_exitCode.ToString());
                Console.Out.WriteLine(_msg.ToString());
            }

            Console.Out.Write("PauseAtEndOfTest is " + pauseAtEndOfTest.ToString() + Environment.NewLine);

            if (pauseAtEndOfTest)
            {
                Console.WriteLine("Press Enter Key to exit the program:");
                Console.ReadLine();
            }

            Environment.ExitCode = _exitCode;


        }//end Main

        private static int RunTests(string[] args)
        {
            int ret = 0;


            try
            {
                _msg.Length = 0;
                _msg.Append("RunTests started ...");
                Console.Out.WriteLine(_msg.ToString());

                Tests.Test1();

                ret = 0;
            }
            catch (System.Exception ex)
            {
                ret = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Console.Out.WriteLine(_msg.ToString());
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... RunTests finished.");
                Console.Out.WriteLine(_msg.ToString());
            }

            return ret;
        }


        /* Example from MSDN:
         * 
        using System;
        using System.IO;

        public class Example
        {
           public static void Main()
           {
              // Get all files in the current directory.
              string[] files = Directory.GetFiles(".");
              Array.Sort(files);

              // Display the files to the current output source to the console.
              Console.WriteLine("First display of filenames to the console:");
              Array.ForEach(files, s => Console.Out.WriteLine(s));   
              Console.Out.WriteLine();

              // Redirect output to a file named Files.txt and write file list.
              StreamWriter sw = new StreamWriter(@".\Files.txt");
              sw.AutoFlush = true;
              Console.SetOut(sw);
              Console.Out.WriteLine("Display filenames to a file:");
              Array.ForEach(files, s => Console.Out.WriteLine(s));   
              Console.Out.WriteLine();

              // Close previous output stream and redirect output to standard output.
              Console.Out.Close();
              sw = new StreamWriter(Console.OpenStandardOutput());
              sw.AutoFlush = true;
              Console.SetOut(sw);

              // Display the files to the current output source to the console.
              Console.Out.WriteLine("Second display of filenames to the console:");
              Array.ForEach(files, s => Console.Out.WriteLine(s));   
           }   
        }


         * */


    }//end class



}//end namespace

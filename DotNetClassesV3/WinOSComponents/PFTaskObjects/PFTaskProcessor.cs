//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFThreadObjects;
using System.Threading;
using System.Diagnostics;

namespace PFTaskObjects
{
    /// <summary>
    /// Class for running the application specified task.
    /// </summary>
    public class PFTaskProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private string _progressMessage = string.Empty;
        private string _logMessage = string.Empty;
        private PFThread _t = null;

        //private variables for properties
        PFTask _taskToRun = null;
        string _outputMessages = string.Empty;
        string _errorMessages = string.Empty;
        int _processExitCode = -1;
        int _threadID = -1;

        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTaskProcessor()
        {
            InitializeInstance();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="taskToRun">Task object containing task to run.</param>
        public PFTaskProcessor(PFTask taskToRun)
        {
            _taskToRun = taskToRun;
            InitializeInstance();
        }


        private void InitializeInstance()
        {
            ;
        }


        //properties
        /// <summary>
        /// True if the the processing is finished.
        /// </summary>
        public bool HasFinished
        {
            get
            {
                return _t.HasFinished;
            }
        }

        /// <summary>
        /// Time the the was started.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return _t.StartTime;
            }
        }

        /// <summary>
        /// Time the processing finished.
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                return _t.FinishTime;
            }
        }

        /// <summary>
        /// Elapsed time for the the processing.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                return _t.ElapsedTime;
            }
        }

        /// <summary>
        /// Elapsed time returned as a formatted string.
        /// </summary>
        public string ElapsedTimeFormatted
        {
            get
            {
                return _t.ElapsedTimeFormatted;
            }
        }

        /// <summary>
        /// Object that defines the the to be run.
        /// </summary>
        public PFTask TaskToRun
        {
            get
            {
                return _taskToRun;
            }
            set
            {
                _taskToRun = value;
            }
        }

         /// <summary>
        /// Output messages produced by the the.
        /// </summary>
        public string OutputMessages
        {
            get
            {
                return _outputMessages;
            }
            set
            {
                _outputMessages = value;
            }
        }

        /// <summary>
        /// Error messages produced by the the.
        /// </summary>
        public string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
            set
            {
                _errorMessages = value;
            }
        }

        /// <summary>
        /// Exit code produced by the the.
        /// </summary>
        public int ProcessExitCode
        {
            get
            {
                return _processExitCode;
            }
            set
            {
                _processExitCode = value;
            }
        }

        /// <summary>
        ///  .NET managed thread id for the thread spawned by this instance of PFTaskProcessor.
        /// </summary>
        public int ThreadID
        {
            get
            {
                return _threadID;
            }
            set
            {
                _threadID = value;
            }
        }




        //methods

        private void InitializeThread(object parameter)
        {
            if (_taskToRun == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a Task object for the the processor to use. TaskToRun property is null.");
                throw new System.Exception(_msg.ToString());
            }
            if (String.IsNullOrEmpty(_taskToRun.FileToRun.Trim()))
            {
                _msg.Length = 0;
                _msg.Append("You must specify a file to run. FileToRun property is empty.");
                throw new System.Exception(_msg.ToString());
            }

            string threadName = _taskToRun.TaskName;
            string threadDescription = "Thread for " + _taskToRun.TaskName;
            if (parameter == null)
                _t = new PFThread(new ThreadStart(TaskEntryPoint), threadName);
            else
                _t = new PFThread(new ParameterizedThreadStart(TaskEntryPointWithParameter), threadName);
            _t.ThreadDescription = threadDescription;

 
            _t.HasFinished = false;
            _t.StartTime = DateTime.Now;
        }


        private void FinishThread()
        {
            _t.FinishTime = DateTime.Now;

            _t.HasFinished = true;

        }

        /// <summary>
        /// Starts the the on its own thread.
        /// </summary>
        public void Start()
        {
            InitializeThread(null);
            _t.Start();
        }

        /// <summary>
        /// Starts the the on its own thread and passed the specified parameters to the thread.
        /// </summary>
        /// <param name="parameter">Parameter object used to send expected parameters.</param>
        /// <remarks>Use the parameter object to send in an array of strings representing the parameters expected by the application the the thread will run.</remarks>
        public void Start(object parameter)
        {
            InitializeThread(parameter);
            _t.Start(parameter);
        }

        private void TaskEntryPoint()
        {
            TaskWork(null);
            FinishThread();

        }

        private void TaskEntryPointWithParameter(object parameter)
        {
            TaskWork(parameter);
            FinishThread();
        }

        private void TaskWork(object parameter)
        {
            StringBuilder msg = new StringBuilder();
            Process oProcess = new Process();


            _threadID = Thread.CurrentThread.ManagedThreadId;

            //test
            //Console.WriteLine("Doing the work at " + DateTime.Now.ToString("HH:mm:ss"));
            //Console.WriteLine("Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            //System.Threading.Thread.Sleep(3000);
            //Thread.Yield();
            //end test

            oProcess.StartInfo.WorkingDirectory = this.TaskToRun.WorkingDirectory;
            oProcess.StartInfo.FileName = this.TaskToRun.FileToRun;
            oProcess.StartInfo.Arguments = BuildArguments(this.TaskToRun.Arguments);
            oProcess.StartInfo.CreateNoWindow = this.TaskToRun.CreateNoWindow;
            oProcess.StartInfo.WindowStyle = (ProcessWindowStyle)this.TaskToRun.WindowStyle;
            oProcess.StartInfo.UseShellExecute = this.TaskToRun.UseShellExecute;
            oProcess.StartInfo.RedirectStandardOutput = this.TaskToRun.RedirectStandardOutput;
            oProcess.StartInfo.RedirectStandardError = this.TaskToRun.RedirectStandardError;

            oProcess.Start();
            //Check to see if the process is stalled waiting for input.
            System.Threading.Thread.Sleep(1000);
            while (oProcess.HasExited == false)
            {
                foreach (ProcessThread thread in oProcess.Threads)
                {
                    if (oProcess.HasExited == false)
                    {
                        if (thread.ThreadState == System.Diagnostics.ThreadState.Wait
                            && thread.WaitReason == System.Diagnostics.ThreadWaitReason.LpcReply)
                            oProcess.Kill();
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
            //end check for stall
            msg.Length = 0;
            msg.Append(oProcess.StandardOutput.ReadToEnd());
            _outputMessages = msg.ToString();

            msg.Length = 0;
            msg.Append(oProcess.StandardError.ReadToEnd());
            _errorMessages = msg.ToString();

            oProcess.WaitForExit();
            _processExitCode = oProcess.ExitCode;

        }//end TaskWork method

        private string BuildArguments(string[] arguments)
        {
            StringBuilder args = new StringBuilder();


            if (arguments != null)
            {
                for (int i = 0; i < arguments.Length; i++)
                {
                    args.Append(arguments[i]);
                    if (i != (arguments.Length - 1))
                    {
                        args.Append(" ");
                    }
                }
            }
            else
            {
                args.Append(string.Empty);
            }

            return args.ToString();
        }


    }//end class
}//end namespace

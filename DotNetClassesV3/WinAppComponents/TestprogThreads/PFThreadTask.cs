//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFTextFiles;
using PFThreadObjects;
using System.Threading;

namespace TestprogThreads
{
    public class PFThreadTask
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private string _progressMessage = string.Empty;
        private string _logMessage = string.Empty;
        private PFTextFile _log = new PFTextFile();
        private PFThread _t = null;

        //private variables for properties
        private string _taskName = string.Empty;
        private string _taskDescription = string.Empty;
        //private bool _hasFinished = false;
        private string _logfolder = string.Empty;
        private bool _useSharedLogFile = false;
        private bool _writeMessagesToLog = true;

        int _testMultiplier = 10;
        int _numWorkLoops = 10;

        //constructors

        public PFThreadTask()
        {
            InitializeInstance(null, null);
        }

        public PFThreadTask(string taskName)
        {
            _taskName = taskName;
            InitializeInstance(taskName, null);
        }

        public PFThreadTask(string taskName, string taskDescription)
        {
            InitializeInstance(taskName, taskDescription);
        }

        private void InitializeInstance(string taskName, string taskDescription)
        {
            if (taskName != null)
                _taskName = taskName;
            else
                _taskName = BuildTaskName();
            if (taskDescription != null)
                _taskDescription = taskDescription;
            else
                _taskDescription = BuildTaskDescription();
            this.LogFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private string BuildTaskName()
        {
            _str.Length = 0;
            _str.Append("Task_");
            _str.Append(DateTime.Now.ToString("yyyyMMdd_"));
            _str.Append(DateTime.Now.ToString("HHmmss"));
            return _str.ToString();
        }

        private string BuildTaskDescription()
        {
            if(_taskName == null)
                _taskName = "Task";
            _str.Length = 0;
            _str.Append("Work task ");
            _str.Append(_taskName);
            _str.Append(".");
            return _str.ToString();
        }

        private string FormatLogFilePath(string logFolder, string logFile)
        {
            string res = string.Empty;

            _str.Length = 0;
            _str.Append(LogFolder);
            if (_str.ToString().EndsWith(@"\") == false)
                _str.Append(@"\");
            _str.Append(logFile);

            res = _str.ToString();

            return res;
        }



        //properties

        /// <summary>
        /// TaskName Property.
        /// </summary>
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
            }
        }

        /// <summary>
        /// TaskDescription Property.
        /// </summary>
        public string TaskDescription
        {
            get
            {
                return _taskDescription;
            }
            set
            {
                _taskDescription = value;
            }
        }

        public bool HasFinished
        {
            get
            {
                return _t.HasFinished;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _t.StartTime;
            }
        }

        public DateTime FinishTime
        {
            get
            {
                return _t.FinishTime;
            }
        }

        public TimeSpan ElapsedTime
        {
            get
            {
                return _t.ElapsedTime;
            }
        }

        public string ElapsedTimeFormatted
        {
            get
            {
                return _t.ElapsedTimeFormatted;
            }
        }

        public string LogFolder
        {
            get
            {
                return _logfolder;
            }
            set
            {
                _logfolder = value;
            }
        }

        public bool UseSharedLogFile
        {
            get
            {
                return _useSharedLogFile;
            }
            set
            {
                _useSharedLogFile = value;
            }
        }

        public bool WriteMessagesToLog
        {
            get
            {
                return _writeMessagesToLog;
            }
            set
            {
                _writeMessagesToLog = value;
            }
        }


        //following properties are for testing

        /// <summary>
        /// TestMultiplier Property.
        /// </summary>
        public int TestMultiplier
        {
            get
            {
                return _testMultiplier;
            }
            set
            {
                _testMultiplier = value;
            }
        }

        /// <summary>
        /// NumWorkLoops Property.
        /// </summary>
        public int NumWorkLoops
        {
            get
            {
                return _numWorkLoops;
            }
            set
            {
                _numWorkLoops = value;
            }
        }


        //methods

        private void InitializeThread(object parameter)
        {
            string threadName = "Thread_" + _taskName;
            string threadDescription = "Thread for " + _taskDescription;
            if (parameter == null)
                _t = new PFThread(new ThreadStart(TaskEntryPoint), threadName);
            else
                _t = new PFThread(new ParameterizedThreadStart(TaskEntryPointWithParameter), threadName);
            _t.ThreadDescription = threadDescription;

            string logfile = FormatLogFilePath(this.LogFolder, this.TaskName + ".log");
            if (this.UseSharedLogFile)
                _log.OpenFile(logfile, PFFileOpenOperation.OpenFileForAppend);
            else
                _log.OpenFile(logfile, PFFileOpenOperation.OpenFileForWrite);

            _t.HasFinished = false;
            _t.StartTime = DateTime.Now;
        }

        private void FinishThread()
        {
            _t.FinishTime = DateTime.Now;
            
            _msg.Length = 0;
            _msg.Append(_t.ThreadName);
            _msg.Append(" in FinishThread at ");
            _msg.Append(_t.FinishTime.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());

            _t.HasFinished = true;

        }

        public void Start()
        {
            InitializeThread(null);
            _t.Start();
        }

        public void Start(object parameter)
        {
            InitializeThread(parameter);
            _t.Start(parameter);
        }

        private void TaskEntryPoint()
        {
            _msg.Length = 0;
            _msg.Append("TaskEntryPoint on thread ");
            _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());
            TaskWork(null);
            FinishThread();

        }

        private void TaskEntryPointWithParameter(object parameter)
        {
            _msg.Length = 0;
            _msg.Append("TaskEntryPointWithParameter on thread ");
            _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());
            TaskWork(parameter);
            FinishThread();
        }

        private void TaskWork(object parameter)
        {
            int result = 0;
            int pct = 0;
 
            _msg.Length = 0;
            _msg.Append("TaskWork on thread ");
            _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());

            _msg.Length = 0;
            _msg.Append("Parameter for thread ");
            _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            _msg.Append(": ");
            _msg.Append(parameter==null ? "<null>" : parameter.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());


            _msg.Length = 0;
            _msg.Append(_t.ThreadName);
            _msg.Append(" started at ");
            _msg.Append(_t.StartTime.ToString());
            Console.WriteLine(_msg.ToString());

            _msg.Length = 0;
            _msg.Append("ThreadName,Method,Inx,Result,Progress,Thread,StartTime,CurrTime,ElapsedTime,ElapsedTimeFormatted");
            _log.WriteLine(_msg.ToString());

            for (int inx = 1; inx <= this.NumWorkLoops; inx++)
            {
                result = inx * this.TestMultiplier;
                _msg.Length = 0;
                _msg.Append(_t.ThreadName);
                _msg.Append(": inx ");
                _msg.Append(inx.ToString());
                _msg.Append(" goes to ");
                _msg.Append(result.ToString());
                _msg.Append(" (Thread = ");
                _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                _msg.Append(")");
                _progressMessage = _msg.ToString();
                Console.WriteLine(_progressMessage);
                pct = Convert.ToInt32(((double)inx / (double)this._numWorkLoops) * (double)100.0);

                if (this.WriteMessagesToLog)
                {
                    //output to log
                    _msg.Length = 0;
                    _msg.Append(_t.ThreadName);
                    _msg.Append(",");
                    _msg.Append("TaskWork");
                    _msg.Append(",");
                    _msg.Append(inx.ToString());
                    _msg.Append(",");
                    _msg.Append(result.ToString());
                    _msg.Append(",");
                    _msg.Append(pct.ToString("0") + " pct");
                    _msg.Append(",");
                    _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                    _msg.Append(",");
                    _msg.Append(_t.StartTime.ToString("HH:mm:ss"));
                    _msg.Append(",");
                    _msg.Append(DateTime.Now.ToString("HH:mm:ss"));
                    _msg.Append(",");
                    _msg.Append(this.ElapsedTime);
                    _msg.Append(",");
                    _msg.Append(this.ElapsedTimeFormatted);
                    _logMessage = _msg.ToString();
                    if (_log != null)
                        _log.WriteLine(_msg.ToString());
                }

                System.Threading.Thread.Sleep(1000);
                Thread.Yield();
            }

            _msg.Length = 0;
            _msg.Append(_t.ThreadName);
            _msg.Append(" finished at ");
            _msg.Append(_t.FinishTime.ToString());
            Console.WriteLine(_msg.ToString());
            if (this.WriteMessagesToLog)
                _log.WriteLine(_msg.ToString());


        }//end TaskWork method

    }//end class
}//end namespace

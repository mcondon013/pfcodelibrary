using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFSchedulerObjects;
using PFTaskObjects;
using PFCollectionsObjects;
using PFTimers;
using System.Diagnostics;

namespace TestprogTaskObjects
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
        public static void TaskManagerSaveAndLoadTest(MainForm frm)
        {
            string filename = @"c:\tempObjectInstance\TestMgr1.xml";
            PFTaskManager taskManager = new PFTaskManager(PFTaskObjects.enTaskStorageType.XMLFiles,@"c:\tempObjectInstance\");

            try
            {
                _msg.Length = 0;
                _msg.Append("TaskManagerSaveAndLoadTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if(File.Exists(filename))
                    File.Delete(filename);

                for (int t = 1; t <= 5; t++)
                {
                    string taskName = "Task" + t.ToString("000");
                    PFTask task = new PFTask(taskName);
                    task.MaxTaskHistoryEntries = 25;
                    task.TaskType = enTaskType.WindowsExecutable;
                    task.FileToRun = @"c:\rundir\" + taskName + @".exe";
                    taskManager.TaskList.Add(task);
                }
                
 
                taskManager.SaveToXmlFile(filename);
                
                PFTaskManager taskMgr2 = PFTaskManager.LoadFromXmlFile(filename);

                _msg.Length = 0;
                _msg.Append(taskMgr2.ToXmlString());
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
                _msg.Append("\r\n... TaskManagerSaveAndLoadTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void InitTaskProcessorTest(MainForm frm)
        {
            PFTask task = new PFTask();
            PFTaskHistoryEntry the = new PFTaskHistoryEntry();
            PFTimers.Stopwatch sw = new PFTimers.Stopwatch();
            
            try
            {
                _msg.Length = 0;
                _msg.Append("InitTaskProcessorTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());


                sw.Start();

                _msg.Length = 0;
                _msg.Append("Before taskProcessor.Start");
                Program._messageLog.WriteLine(_msg.ToString());

                System.Diagnostics.Process p = Process.GetCurrentProcess();
                foreach (ProcessThread th in p.Threads)
                {
                    _msg.Length = 0;
                    _msg.Append("Thread ID: ");
                    _msg.Append(th.Id.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                task.TaskName = "TestTask01";
                task.TaskDescription = "Test the task for Testprog";
                task.TaskType = enTaskType.WindowsExecutable;
                task.FileToRun = @"C:\Temp\TestAppFolders\FilesToRun\testbat01.bat";
                task.WorkingDirectory = @"C:\Temp\TestAppFolders\FilesToRun";
                task.MaxTaskHistoryEntries = 15;
                task.Schedule = PFSchedule.LoadFromXmlFile(@"C:\ProFast\Projects\ScheduleFiles\TestWeeklyOccurs.xml");

                the.TaskID = task.ID;
                the.TaskName = task.TaskName;
                the.ScheduledStartTime = task.Schedule.GetCurrentScheduledDateTime(DateTime.Now, DateTime.Now.AddHours(1));

                PFTaskProcessor taskProcessor = new PFTaskProcessor(task);
                taskProcessor.Start();

                _msg.Length = 0;
                _msg.Append("After taskProcessor.Start");
                Program._messageLog.WriteLine(_msg.ToString());

                System.Diagnostics.Process p1 = Process.GetCurrentProcess();
                foreach (ProcessThread th in p1.Threads)
                {
                    _msg.Length = 0;
                    _msg.Append("Thread ID: ");
                    _msg.Append(th.Id.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                _msg.Length = 0;
                _msg.Append("Main thread id:    ");
                _msg.Append(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append("Spawned thread id: ");
                _msg.Append(taskProcessor.ThreadID.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                while (taskProcessor.HasFinished == false)
                {
                    System.Threading.Thread.Sleep(1000);
                    System.Threading.Thread.Yield();
                }


                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("\r\nTaskProcessor property values: \r\n");
                _msg.Append("StartTime:       ");
                _msg.Append(taskProcessor.StartTime.ToString("MM/dd/yyyy HH:mm:ss"));
                _msg.Append("\r\n");
                _msg.Append("FinishTime       ");
                _msg.Append(taskProcessor.FinishTime.ToString("MM/dd/yyyy HH:mm:ss"));
                _msg.Append("\r\n");
                _msg.Append("Elapsed Time:    ");
                _msg.Append(taskProcessor.ElapsedTimeFormatted);
                _msg.Append("\r\n");
                _msg.Append("Exit Code:       ");
                _msg.Append(taskProcessor.ProcessExitCode.ToString());
                _msg.Append("\r\n");
                _msg.Append("OutputMessages:  ");
                _msg.Append(taskProcessor.OutputMessages);
                _msg.Append("\r\n");
                _msg.Append("ErrorMessages:   ");
                _msg.Append(taskProcessor.ErrorMessages);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                //fill up and save the history entry
                the.ActualStartTime = taskProcessor.StartTime;
                the.ActualEndTime = taskProcessor.FinishTime;
                the.TaskOutputMessages = taskProcessor.OutputMessages;
                the.TaskErrorMessages = taskProcessor.ErrorMessages;
                the.TaskReturnCode = taskProcessor.ProcessExitCode;
                if (taskProcessor.ProcessExitCode == 0)
                    the.TaskRunResult = enTaskRunResult.Success;
                else
                    the.TaskRunResult = enTaskRunResult.Failure;
                //string theFilename = @"c:\tempObjectInstance\" + the.TaskName + "_" + the.TaskID.ToString() + "_" + the.ActualEndTime.ToString("_yyyyMMdd_HHmmss") + ".txt";
                string theFilename = @"c:\tempObjectInstance\" + the.TaskName + "_" + the.ActualEndTime.ToString("_yyyyMMdd_HHmmss") + ".txt";
                if (File.Exists(theFilename))
                    File.Delete(theFilename);
                the.SaveToXmlFile(theFilename);
                                

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
                _msg.Append("\r\n... InitTaskProcessorTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        



    }//end class
}//end namespace

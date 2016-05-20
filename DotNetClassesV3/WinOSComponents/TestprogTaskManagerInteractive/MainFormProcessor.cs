//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using PFAppUtils;
using PFTaskObjects;
using PFSchedulerObjects;
using PFCollectionsObjects;
using PFTimers;
using PFTextObjects;

namespace TestprogTaskManagerInteractive
{
    public class MainFormProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        private string _taskDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\");
        private string _taskHistoryFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\TaskHistory\");
        private string _scheduleDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Schedules\");
        private string _defaultFilesToRunFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\FilesToRun\");

        private string _taskDefsDbConnectionString = "DataSource='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Database\TaskAndSchedule.sdf") + "'";

        //private variables for properties
        private MainForm _frm = null;

        //constructors

        public MainFormProcessor(MainForm frm)
        {
            _frm = frm;
            InitInstance();
        }

        private void InitInstance()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsFolder", string.Empty);
            if (configValue.Trim().Length > 0)
                _taskDefinitionsFolder = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryFolder", string.Empty);
            if (configValue.Trim().Length > 0)
                _taskHistoryFolder = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsFolder", string.Empty);
            if (configValue.Trim().Length > 0)
                _scheduleDefinitionsFolder = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("DefaultFilesToRunFolder", string.Empty);
            if (configValue.Trim().Length > 0)
                _defaultFilesToRunFolder = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefsDbConnectionString", string.Empty);
            if (configValue.Trim().Length > 0)
                _taskDefsDbConnectionString = configValue;

        }

        //properties

        //methods

        public void GetTask()
        {
            if (_frm.optUseXmlFiles.Checked)
                GetTaskFromXml();
            else
                GetTaskFromDatabase();
        }

        public void GetTaskFromXml()
        {
            string taskName = _frm.txtTaskName.Text.Trim();
            PFTask task = null;
            string inputFileName = string.Empty;

            try
            {
                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                _frm._saveFilter = "XML Files|*.xml|All Files|*.*";
                _frm._saveSelectionsFolder = _taskDefinitionsFolder;
                if (taskName.Length > 0)
                    _frm._saveSelectionsFile = taskName + ".xml";
                else
                    _frm._saveSelectionsFile = string.Empty;

                DialogResult res = _frm.ShowOpenFileDialog();
                if (res == DialogResult.OK)
                {
                    _frm.InitForm();
                    taskName = Path.GetFileNameWithoutExtension(_frm._saveSelectionsFile);
                    if (taskName.Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify a task name");
                        throw new System.Exception(_msg.ToString());
                    }

                    inputFileName = Path.Combine(_taskDefinitionsFolder, taskName + ".xml");

                    if (File.Exists(inputFileName) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find file for specified task name: ");
                        _msg.Append(inputFileName);
                        throw new System.Exception(_msg.ToString());
                    }

                    task = PFTask.LoadFromXmlFile(inputFileName);

                    _frm.txtTaskName.Text = task.TaskName;
                    _frm.txtTaskDescription.Text = task.TaskDescription;
                    _frm.cboTaskType.Text = task.TaskType.ToString();
                    _frm.txtMaxHistoryEntries.Text = task.MaxTaskHistoryEntries.ToString();
                    _frm.txtTaskSchedule.Text = task.ScheduleName;
                    _frm.txtFileToRun.Text = task.FileToRun;
                    _frm.txtWorkingDirectory.Text = task.WorkingDirectory;
                    if (task.Arguments != null)
                    {
                        foreach (string s in task.Arguments)
                        {
                            if(s.Trim().Length > 0)
                                _frm.txtArguments.Text += s + Environment.NewLine;
                        }
                    }
                    _frm.cboWindowStyle.Text = task.WindowStyle.ToString();
                    _frm.chkCreateNoWindow.Checked = task.CreateNoWindow;
                    _frm.chkUseShellExecute.Checked = task.UseShellExecute;
                    _frm.chkRedirectStandardOutput.Checked = task.RedirectStandardOutput;
                    _frm.chkRedirectStandardError.Checked = task.RedirectStandardError;
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
                ;
            }
                 
        
        }

        public void GetTaskFromDatabase()
        {
            string taskName = _frm.txtTaskName.Text.Trim();
            PFList<PFTask> taskList = null;
            PFTask task = null;
            PFTaskManager taskMgr = new PFTaskManager(enTaskStorageType.Database, _taskDefsDbConnectionString);
            string inputFileName = string.Empty;

            try
            {
                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                taskList = taskMgr.GetTaskList();
                NameListPrompt namesPrompt = new NameListPrompt();
                namesPrompt.lblSelect.Text = "Select the name from list below:";
                for (int i = 0; i < taskList.Count; i++)
                {
                    namesPrompt.lstNames.Items.Add(taskList[i].TaskName);
                }
                if (taskList.Count > 0)
                {
                    namesPrompt.lstNames.SelectedIndex = 0;
                }
                DialogResult res = namesPrompt.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (namesPrompt.lstNames.SelectedIndex == -1)
                    {
                        _msg.Length = 0;
                        _msg.Append("You did not select any task to load");
                        throw new System.Exception(_msg.ToString());
                    }
                    _frm.InitForm();
                    taskName = namesPrompt.lstNames.SelectedItem.ToString();
                    if (taskName.Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify a task name");
                        throw new System.Exception(_msg.ToString());
                    }

                    task = taskMgr.GetTaskByName(taskName);

                     if (task == null)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find task ");
                        _msg.Append(taskName);
                        _msg.Append(" in the database at ");
                        _msg.Append(_taskDefsDbConnectionString);
                        throw new System.Exception(_msg.ToString());
                    }

                    _frm.txtTaskName.Text = task.TaskName;
                    _frm.txtTaskDescription.Text = task.TaskDescription;
                    _frm.cboTaskType.Text = task.TaskType.ToString();
                    _frm.txtMaxHistoryEntries.Text = task.MaxTaskHistoryEntries.ToString();
                    _frm.txtTaskSchedule.Text = task.ScheduleName;
                    _frm.txtFileToRun.Text = task.FileToRun;
                    _frm.txtWorkingDirectory.Text = task.WorkingDirectory;
                    if (task.Arguments != null)
                    {
                        foreach (string s in task.Arguments)
                        {
                            if (s.Trim().Length > 0)
                                _frm.txtArguments.Text += s + Environment.NewLine;
                        }
                    }
                    _frm.cboWindowStyle.Text = task.WindowStyle.ToString();
                    _frm.chkCreateNoWindow.Checked = task.CreateNoWindow;
                    _frm.chkUseShellExecute.Checked = task.UseShellExecute;
                    _frm.chkRedirectStandardOutput.Checked = task.RedirectStandardOutput;
                    _frm.chkRedirectStandardError.Checked = task.RedirectStandardError;
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
                ;
            }

        }

        public void SaveTask()
        {
            string taskName = _frm.txtTaskName.Text.Trim();
            PFTask task = null;
            string outputFileName = string.Empty;


            try
            {
                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (taskName.Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a the name");
                    throw new System.Exception(_msg.ToString());
                }
                task = new PFTask(taskName);
                task.TaskDescription = _frm.txtTaskDescription.Text;
                task.TaskType = PFAppUtils.PFEnumProcessor.StringToEnum<enTaskType>(_frm.cboTaskType.Text);
                task.MaxTaskHistoryEntries = Convert.ToInt32(_frm.txtMaxHistoryEntries.Text);
                task.ScheduleName = _frm.txtTaskSchedule.Text;
                task.FileToRun = _frm.txtFileToRun.Text;
                task.WorkingDirectory = _frm.txtWorkingDirectory.Text;
                string[] lines = _frm.txtArguments.Lines;

                task.Arguments = lines;
                task.WindowStyle = PFAppUtils.PFEnumProcessor.StringToEnum<enWindowStyle>(_frm.cboWindowStyle.Text);
                task.CreateNoWindow = _frm.chkCreateNoWindow.Checked;
                task.UseShellExecute = _frm.chkUseShellExecute.Checked;
                task.RedirectStandardOutput = _frm.chkRedirectStandardOutput.Checked;
                task.RedirectStandardError = _frm.chkRedirectStandardError.Checked;


                outputFileName = Path.Combine(_taskDefinitionsFolder, taskName + ".xml");

                if (Directory.Exists(_taskDefinitionsFolder) == false)
                {
                    Directory.CreateDirectory(_taskDefinitionsFolder);
                }

                _msg.Length = 0;
                _msg.Append("Task ");
                _msg.Append(taskName);
                _msg.Append(" saved to ");
                if (_frm.optUseXmlFiles.Checked)
                {
                    task.SaveToXmlFile(outputFileName);
                    _msg.Append(outputFileName);
                }
                else
                {
                    task.SaveToDatabase(_frm._taskDefsDbConnectionString);
                    _msg.Append(_frm._taskDefsDbConnectionString);
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
                ;
            }
                 
        


        }

        public void RunTask()
        {
            PFTask task = new PFTask();
            PFTaskHistoryEntry the = new PFTaskHistoryEntry();
            PFTimers.Stopwatch sw = new PFTimers.Stopwatch();
            string connectionString = string.Empty;
            string scheduleFolder = string.Empty;
            string scheduleName = string.Empty;

            try
            {

                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (_frm.txtTaskName.Text.Trim().Length == 0
                   || _frm.txtFileToRun.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a the name and a file to run for RunTest");
                    throw new System.Exception(_msg.ToString());

                }

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

                scheduleFolder = _scheduleDefinitionsFolder;
                scheduleName = Path.GetFileNameWithoutExtension(_frm.txtTaskSchedule.Text.Trim()) + ".xml";
                if (_frm.optUseXmlFiles.Checked)
                {
                    connectionString = Path.Combine(scheduleFolder, scheduleName);
                }
                else if (_frm.optUseDatabase.Checked) 
                {
                    connectionString = _taskDefsDbConnectionString;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("You must select storage type: XML Files or Database.");
                    throw new System.Exception(_msg.ToString());
                }


                task.TaskName = _frm.txtTaskName.Text;
                task.TaskDescription = _frm.txtTaskDescription.Text;
                task.TaskType = PFAppUtils.PFEnumProcessor.StringToEnum<enTaskType>(_frm.cboTaskType.Text);
                task.MaxTaskHistoryEntries = Convert.ToInt32(_frm.txtMaxHistoryEntries.Text);
                if (_frm.optUseXmlFiles.Checked)
                    task.Schedule = PFSchedule.LoadFromXmlFile(connectionString);
                else
                    task.Schedule = PFSchedule.LoadFromDatabase(connectionString, _frm.txtTaskSchedule.Text.Trim());
                task.FileToRun = _frm.txtFileToRun.Text;
                task.WorkingDirectory = _frm.txtWorkingDirectory.Text;
                string[] lines = _frm.txtArguments.Lines;
                task.Arguments = lines;
                task.WindowStyle = PFAppUtils.PFEnumProcessor.StringToEnum<enWindowStyle>(_frm.cboWindowStyle.Text);
                task.CreateNoWindow = _frm.chkCreateNoWindow.Checked;
                task.UseShellExecute = _frm.chkUseShellExecute.Checked;
                task.RedirectStandardOutput = _frm.chkRedirectStandardOutput.Checked;
                task.RedirectStandardError = _frm.chkRedirectStandardError.Checked;

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
                _msg.Append("Curr Directory:  ");
                _msg.Append(task.WorkingDirectory);
                _msg.Append("\r\n");
                _msg.Append("Command Line:    ");
                _msg.Append(task.FileToRun);
                _msg.Append(" ");
                _msg.Append(PFTextProcessor.ConvertStringArrayToString(task.Arguments));
                _msg.Append("\r\n");
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
                if (_frm.optUseXmlFiles.Checked)
                {
                    string theFilename = _taskHistoryFolder + the.TaskName + "_" + the.ActualEndTime.ToString("_yyyyMMdd_HHmmss") + ".xml";
                    if (File.Exists(theFilename))
                        File.Delete(theFilename);
                    the.SaveToXmlFile(theFilename);
                }
                else if (_frm.optUseDatabase.Checked)
                {
                    the.SaveToDatabase(_taskDefsDbConnectionString);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify whether to store the history in XML files or Database.");
                    throw new System.Exception(_msg.ToString());
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
                ;
            }
        
        }


        public void GetSchedule()
        {
            
            if (_frm.optUseXmlFiles.Checked)
                GetScheduleXml();
            else if (_frm.optUseDatabase.Checked)
                GetScheduleDatabase();
            else
            {
                _msg.Length = 0;
                _msg.Append("You must select either Xml Files or Database for storage location.");
                throw new System.Exception(_msg.ToString());
            }

        }

        public void GetScheduleXml()
        {
            string scheduleFile = _frm.txtTaskSchedule.Text;

            try
            {
                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                _frm._saveSelectionsFolder = _scheduleDefinitionsFolder;
                _frm._saveFilter = "XML Files|*.xml|All Files|*.*";
                if (scheduleFile.Length > 0)
                    _frm._saveSelectionsFile = scheduleFile + ".xml";
                else
                    _frm._saveSelectionsFile = string.Empty;

                DialogResult res = _frm.ShowOpenFileDialog();
                if (res == DialogResult.OK)
                {
                    _frm.txtTaskSchedule.Text = Path.GetFileNameWithoutExtension(_frm._saveSelectionsFile);
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
                ;

            }
        }

        public void GetScheduleDatabase()
        {
            string scheduleName = _frm.txtTaskSchedule.Text.Trim();
            PFList<PFSchedule> scheduleList = null;
            PFSchedule sked = null;
            PFScheduleManager skedMgr = new PFScheduleManager(enScheduleStorageType.Database, _taskDefsDbConnectionString);

            try
            {
                if (_frm.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                scheduleList = skedMgr.GetScheduleList();
                if (scheduleList.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("No schedules found in database: ");
                    _msg.Append(_taskDefsDbConnectionString);
                    throw new System.Exception(_msg.ToString());
                }
                NameListPrompt namesPrompt = new NameListPrompt();
                namesPrompt.lblSelect.Text = "Select the name from list below:";
                for (int i = 0; i < scheduleList.Count; i++)
                {
                    namesPrompt.lstNames.Items.Add(scheduleList[i].Name);
                }
                if (scheduleList.Count > 0)
                {
                    namesPrompt.lstNames.SelectedIndex = 0;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("No schedules found in database: ");
                    _msg.Append(_taskDefsDbConnectionString);
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                DialogResult res = namesPrompt.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (namesPrompt.lstNames.SelectedIndex == -1)
                    {
                        _msg.Length = 0;
                        _msg.Append("You did not select any schedule.");
                        throw new System.Exception(_msg.ToString());
                    }
                    scheduleName = namesPrompt.lstNames.SelectedItem.ToString();
                    if (scheduleName.Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify a the name");
                        throw new System.Exception(_msg.ToString());
                    }

                    sked = skedMgr.GetScheduleByName(scheduleName);

                    if (sked == null)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find the ");
                        _msg.Append(scheduleName);
                        _msg.Append(" in the database at ");
                        _msg.Append(_taskDefsDbConnectionString);
                        throw new System.Exception(_msg.ToString());
                    }

                    _frm.txtTaskSchedule.Text = sked.Name;

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
                ;

            }
        }

        public void GetFileToRun()
        {
            string fileToRun = _frm.txtFileToRun.Text;
            try
            {
                if(_frm._saveSelectionsFolder.Trim().Length == 0)
                    _frm._saveSelectionsFolder = _defaultFilesToRunFolder;
                _frm._saveFilter = "EXE Files|*.exe|Batch Files|*.bat;*.cmd|All Files|*.*";
                if (fileToRun.Length > 0)
                    _frm._saveSelectionsFile = fileToRun;
                else
                    _frm._saveSelectionsFile = string.Empty;

                DialogResult res = _frm.ShowOpenFileDialog();
                if (res == DialogResult.OK)
                {
                    _frm.txtFileToRun.Text = _frm._saveSelectionsFile;
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
                ;
            }
                 
        }

        public void GetWorkingDirectory()
        {
            string workingDirectory = _frm.txtWorkingDirectory.Text;

            try
            {
                if (_frm._saveSelectionsFolder.Trim().Length == 0)
                    _frm._saveSelectionsFolder = _defaultFilesToRunFolder;

                DialogResult res = _frm.ShowFolderBrowserDialog();
                if (res == DialogResult.OK)
                {
                    _frm.txtWorkingDirectory.Text = _frm._saveSelectionsFolder;
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
                ;
            }
                 
        
        }


    }//end class
}//end namespace

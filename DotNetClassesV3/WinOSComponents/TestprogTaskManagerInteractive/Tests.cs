using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFTaskObjects;
using PFSchedulerObjects;
using PFCollectionsObjects;

namespace TestprogTaskManagerInteractive
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        private static string _taskDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\");
        private static string _taskHistoryFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\TaskHistory\");
        private static string _scheduleDefinitionsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Schedules\");
        private static string _defaultFilesToRunFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\FilesToRun\");

        private static string _taskDefsDbConnectionString = "DataSource='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Database\TaskAndSchedule.sdf") + "'";


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

        public static void GetTaskHistory(MainForm frm)
        {
            if (frm.optUseXmlFiles.Checked)
                GetTaskHistoryXml(frm);
            else if (frm.optUseDatabase.Checked)
               GetTaskHistoryDatabase(frm) ;
            else
            {
                _msg.Length = 0;
                _msg.Append("You must specify storage type of XML or database.");
                throw new System.Exception(_msg.ToString());
            }
        }

        
        public static void GetTaskHistoryXml(MainForm frm)
        {
            string taskHistoryFolder = string.Empty;
            List<PFTaskHistoryEntry> taskHistoryEntries = null;
            PFTaskHistoryManager thm = new PFTaskHistoryManager();
            string taskName = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetTaskHistoryXml started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtTaskName.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the the name for GetTaskHistory.");
                    throw new System.Exception(_msg.ToString());
                }

                InitFolderPaths();

                taskName = frm.txtTaskName.Text;
                taskHistoryFolder = _taskHistoryFolder;

                thm.TaskStorageType = enTaskStorageType.XMLFiles;
                thm.ConnectionString = taskHistoryFolder;
                
                taskHistoryEntries = thm.GetTaskHistoryList(taskName);

                if(taskHistoryEntries == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No the history found for ");
                    _msg.Append(taskName);
                }
                

                _msg.Length = 0;
                _msg.Append("Number of the history files found for ");
                _msg.Append(taskName);
                _msg.Append(": ");
                _msg.Append(taskHistoryEntries.Count.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
                for (int i = 0; i < taskHistoryEntries.Count; i++)
                {
                    _msg.Length = 0;
                    string filePath = Path.Combine(thm.ConnectionString, taskHistoryEntries[i].TaskName + "__" + taskHistoryEntries[i].ActualStartTime.ToString("yyyyMMdd_HHmmss") + ".xml");
                    _msg.Append(filePath);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                for (int i = 0; i < taskHistoryEntries.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append("\r\nDetails for ");
                    _msg.Append(taskHistoryEntries[i].TaskName);
                    _msg.Append(" run at ");
                    _msg.Append(taskHistoryEntries[i].ActualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
                    _msg.Append(":\r\n");
                    _msg.Append(taskHistoryEntries[i].ToXmlString());
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
                _msg.Append("\r\n... GetTaskHistoryXml finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetTaskHistoryDatabase(MainForm frm)
        {
            string taskHistoryDbConnectionString = string.Empty;
            List<PFTaskHistoryEntry> taskHistoryEntries = null;
            PFTaskHistoryManager thm = new PFTaskHistoryManager();
            string taskName = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetTaskHistoryDatabase started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtTaskName.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the the name for GetTaskHistory.");
                    throw new System.Exception(_msg.ToString());
                }

                InitFolderPaths();

                taskName = frm.txtTaskName.Text;
                taskHistoryDbConnectionString = _taskDefsDbConnectionString;

                thm.TaskStorageType = enTaskStorageType.Database;
                thm.ConnectionString = taskHistoryDbConnectionString;

                taskHistoryEntries = thm.GetTaskHistoryList(taskName);

                if (taskHistoryEntries == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No the history found for ");
                    _msg.Append(taskName);
                }


                _msg.Length = 0;
                _msg.Append("Number of the history entries found for ");
                _msg.Append(taskName);
                _msg.Append(": ");
                _msg.Append(taskHistoryEntries.Count.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());
                for (int i = 0; i < taskHistoryEntries.Count; i++)
                {
                    _msg.Length = 0;
                    string filePath = Path.Combine(thm.ConnectionString, taskHistoryEntries[i].TaskName + "__" + taskHistoryEntries[i].ActualStartTime.ToString("yyyyMMdd_HHmmss"));
                    _msg.Append(filePath);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                for (int i = 0; i < taskHistoryEntries.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append("\r\nDetails for ");
                    _msg.Append(taskHistoryEntries[i].TaskName);
                    _msg.Append(" run at ");
                    _msg.Append(taskHistoryEntries[i].ActualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
                    _msg.Append(":\r\n");
                    _msg.Append(taskHistoryEntries[i].ToXmlString());
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
                _msg.Append("\r\n... GetTaskHistory finishedDatabase.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void ThrowTaskHistoryNotFoundError(string taskNameSearchPattern, string taskHistoryFolder)
        {
            _msg.Length = 0;
            _msg.Append("No the history files found for ");
            _msg.Append(taskNameSearchPattern);
            _msg.Append(" in ");
            _msg.Append(taskHistoryFolder);
            _msg.Append(".");
            throw new System.Exception(_msg.ToString());
        }


        public static void GetTaskList(MainForm frm)
        {
            if (frm.optUseXmlFiles.Checked)
                GetTaskListXml(frm);
            else if (frm.optUseDatabase.Checked)
                GetTaskListDatabase(frm);
            else
            {
                _msg.Length = 0;
                _msg.Append("You must specify storage type of XML or database.");
                throw new System.Exception(_msg.ToString());
            }
        }

        public static void GetTaskListXml(MainForm frm)
        {
            PFList<PFTask> taskList = null;
            PFTaskManager tm = null;
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetTaskListXml started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitFolderPaths();

                connectionString = _taskDefinitionsFolder;

                tm = new PFTaskManager(enTaskStorageType.XMLFiles, connectionString);

                taskList = tm.GetTaskList();

                if (taskList == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No tasks found in ");
                    _msg.Append(connectionString);
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Number of sked definitions found: ");
                _msg.Append(taskList.Count.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Folder: ");
                _msg.Append(connectionString);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < taskList.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(taskList[i].TaskName + ".xml");
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
                _msg.Append("\r\n... GetTaskListXml finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetTaskListDatabase(MainForm frm)
        {
            PFList<PFTask> taskList = null;
            PFTaskManager tm = null;
            string connectionString = string.Empty;
            try
            {
                _msg.Length = 0;
                _msg.Append("GetTaskListDatabase started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitFolderPaths();

                connectionString = _taskDefsDbConnectionString;

                tm = new PFTaskManager(enTaskStorageType.Database, connectionString);

                taskList = tm.GetTaskList();

                if (taskList == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No tasks found in ");
                    _msg.Append(connectionString);
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Number of sked definitions found: ");
                _msg.Append(taskList.Count.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Database: ");
                _msg.Append(connectionString);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < taskList.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(taskList[i].TaskName);
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
                _msg.Append("\r\n... GetTaskListDatabase finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void ThrowTaskListNotFoundError(string taskDefinitionsFolder)
        {
            _msg.Length = 0;
            _msg.Append("No the files found in ");
            _msg.Append(taskDefinitionsFolder);
            _msg.Append(".");
            throw new System.Exception(_msg.ToString());
        }

        public static void ShowScheduleDetail(MainForm frm)
        {
            if (frm.optUseXmlFiles.Checked)
                ShowScheduleDetailXml(frm);
            else if (frm.optUseDatabase.Checked)
                ShowScheduleDetailDatabase(frm);
            else
            {
                _msg.Length = 0;
                _msg.Append("You must select storage type: XML Files or Database.");
                throw new System.Exception(_msg.ToString());
            }
        }

        public static void ShowScheduleDetailXml(MainForm frm)
        {
            PFSchedule sked = null;
            string connectionString = string.Empty;
            string scheduleDefinitionsFolder = string.Empty;
            string scheduleFileName = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowScheduleDetail started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtTaskSchedule.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the name of the schedule for ShowScheduleDetail.");
                    throw new System.Exception(_msg.ToString());
                }

                InitFolderPaths();

                scheduleDefinitionsFolder = _scheduleDefinitionsFolder;
                scheduleFileName = Path.GetFileNameWithoutExtension(frm.txtTaskSchedule.Text) + ".xml";

                connectionString = Path.Combine(scheduleDefinitionsFolder, scheduleFileName);

                sked = PFSchedule.LoadFromXmlFile(connectionString);

                _msg.Length = 0;
                _msg.Append(sked.ToXmlString());
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
                _msg.Append("\r\n... ShowScheduleDetail finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ShowScheduleDetailDatabase(MainForm frm)
        {
            PFSchedule sked = null;
            string connectionString = string.Empty;
            string scheduleName = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowScheduleDetailDatabase started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (frm.txtTaskSchedule.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the name of the schedule for ShowScheduleDetail.");
                    throw new System.Exception(_msg.ToString());
                }

                InitFolderPaths();

                connectionString = _taskDefsDbConnectionString;
                scheduleName = Path.GetFileNameWithoutExtension(frm.txtTaskSchedule.Text);

                sked = PFSchedule.LoadFromDatabase(connectionString, scheduleName);

                _msg.Length = 0;
                _msg.Append(sked.ToXmlString());
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
                _msg.Append("\r\n... ShowScheduleDetailDatabase finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


        public static void GetScheduleList(MainForm frm)
        {
            if (frm.optUseXmlFiles.Checked)
                GetScheduleListXml(frm);
            else if (frm.optUseDatabase.Checked)
                GetScheduleListDatabase(frm);
            else
            {
                _msg.Length = 0;
                _msg.Append("You must specify storage type of XML or database.");
                throw new System.Exception(_msg.ToString());
            }
        }

        public static void GetScheduleListXml(MainForm frm)
        {
            PFList<PFSchedule> scheduleList = null;
            PFScheduleManager sm = null;
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetScheduleListXml started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitFolderPaths();

                connectionString = _scheduleDefinitionsFolder;

                sm = new PFScheduleManager(enScheduleStorageType.XMLFiles, connectionString);

                scheduleList = sm.GetScheduleList();

                if (scheduleList == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No tasks found in ");
                    _msg.Append(connectionString);
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Number of schedule definitions found: ");
                _msg.Append(scheduleList.Count.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Folder: ");
                _msg.Append(connectionString);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < scheduleList.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(scheduleList[i].Name + ".xml");
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
                _msg.Append("\r\n... GetScheduleListXml finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void GetScheduleListDatabase(MainForm frm)
        {
            PFList<PFSchedule> scheduleList = null;
            PFScheduleManager sm = null;
            string connectionString = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetScheduleListDatabase started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                InitFolderPaths();

                connectionString = _taskDefsDbConnectionString;

                sm = new PFScheduleManager(enScheduleStorageType.Database, connectionString);

                scheduleList = sm.GetScheduleList();

                if (scheduleList == null)
                {
                    _msg.Length = 0;
                    _msg.Append("No schedules found in ");
                    _msg.Append(connectionString);
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Number of schedule definitions found: ");
                _msg.Append(scheduleList.Count.ToString("#,##0"));
                _msg.Append("\r\n");
                _msg.Append("Database: ");
                _msg.Append(connectionString);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < scheduleList.Count; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(scheduleList[i].Name);
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
                _msg.Append("\r\n... GetScheduleListDatabase finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private static void ThrowScheduleListNotFoundError(string scheduleDefinitionsFolder)
        {
            _msg.Length = 0;
            _msg.Append("No schedule files found in ");
            _msg.Append(scheduleDefinitionsFolder);
            _msg.Append(".");
            throw new System.Exception(_msg.ToString());
        }


        private static void InitFolderPaths()
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



    }//end class
}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFTimers;
using PFThreadObjects;
using System.Threading;

namespace TestprogThreads
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
        public static void WaitTest()
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                _msg.Length = 0;
                _msg.Append("WaitTest started. Waiting several seconds ...");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                PFCurrentThread.Wait(3);

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total wait time: \r\n");
                _msg.Append(sw.FormattedElapsedTime);
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
                _msg.Append("... WaitTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void PFThreadTest()
        {
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("PFThreadTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Calling thread is ");
                _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                _msg.Append(".");
                Program._messageLog.WriteLine(_msg.ToString());
                Console.WriteLine(_msg.ToString());

                //PFThread t = new PFThread(new ThreadStart(EntryPointForThreadTest), "TestThread1");
                PFThread t = new PFThread(new ThreadStart(EntryPointForThreadTest));

                t.ThreadDescription="Thread run by PFThreadTest method.";
                t.ShowElapsedMilliseconds=true;

                t.StartTime = DateTime.Now;
                t.Start();

                //t.ThreadObject.Join();   //blocking

                //Thread.Sleep(100);  //give it time to make sure thread has started
                //while (t.IsAlive == true)
                //{
                //    Thread.Sleep(1000);
                //}

                while (t.ThreadObject.Join(TimeSpan.Zero) == false)
                {
                    Thread.Sleep(1000);
                }

                t.FinishTime = DateTime.Now;

                _msg.Length=0;
                _msg.Append("Thread run time: ");
                _msg.Append(t.ElapsedTimeFormatted);
                Program._messageLog.WriteLine(_msg.ToString());
                Console.WriteLine(_msg.ToString());

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
                _msg.Append("... PFThreadTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Stop();
                _msg.Length = 0;
                _msg.Append("Total run time: \r\n");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        private static void EntryPointForThreadTest()
        {
            StringBuilder tmsg = new StringBuilder();

            tmsg.Length = 0;
            tmsg.Append(Thread.CurrentThread.Name);
            tmsg.Append(" is doing work in thread id ");
            tmsg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(tmsg.ToString());

            Thread.SpinWait(100000000);
        }



        public static void PFThreadTaskTest()
        {
            PFThreadTask task = new PFThreadTask("TestTask01", "Unit testing task.");
            object parameter = "Test parameter";
            try
            {
                _msg.Length = 0;
                _msg.Append("PFThreadTaskTest started at ");
                _msg.Append(DateTime.Now.ToString("HH:mm:ss.ms"));
                _msg.Append(" on thread ");
                _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                _msg.Append(" ...");
                Program._messageLog.WriteLine(_msg.ToString());
                Console.WriteLine(_msg.ToString());

                task.WriteMessagesToLog = true;
                task.UseSharedLogFile = false;

                task.Start();

                while (task.HasFinished == false)
                {
                    Thread.Yield();
                }

                _msg.Length = 0;
                _msg.Append("PFThreadTaskTest finished at ");
                _msg.Append(DateTime.Now.ToString("HH:mm:ss.ms"));
                _msg.Append("\r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(task.ElapsedTimeFormatted);
                Console.WriteLine(_msg.ToString());
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
                _msg.Append("... PFThreadTaskTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void MultiplePFThreadTaskTest()
        {
            Stopwatch sw = new Stopwatch();
            int maxNumTasks = 10;
            bool allTasksCompleted = false;
            List<PFThreadTask> tasks = new List<PFThreadTask>();
            object parameter = "Test parameter";
            try
            {
                sw.Start();

                _msg.Length = 0;
                _msg.Append("MultiplePFThreadTaskTest started at ");
                _msg.Append(sw.StartTime.ToString("HH:mm:ss.ms"));
                _msg.Append(" on thread ");
                _msg.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                _msg.Append(" ...");
                Program._messageLog.WriteLine(_msg.ToString());
                Console.WriteLine(_msg.ToString());

                for (int inx = 0; inx < maxNumTasks; inx++)
                {
                    tasks.Add(new PFThreadTask());
                }

                for (int inx = 0; inx < maxNumTasks; inx++)
                {
                    tasks[inx].WriteMessagesToLog = true;
                    tasks[inx].UseSharedLogFile = false;
                    tasks[inx].TaskName = "PFThreadTask" + inx.ToString();
                    tasks[inx].TaskDescription = "Task to work in " + tasks[inx].TaskName + ".";
                }

                for (int inx = 0; inx < maxNumTasks; inx++)
                {
                    if (inx == 2 || inx == 5 || inx == 7)
                    {
                        tasks[inx].Start(parameter + inx.ToString());
                    }
                    else
                    {
                        tasks[inx].Start();
                    }
                }

                allTasksCompleted = false;
                while (allTasksCompleted == false)
                {
                    allTasksCompleted = true;
                    for (int inx = 0; inx < maxNumTasks; inx++)
                    {
                        if(tasks[inx].HasFinished == false)
                        {
                            allTasksCompleted=false;
                            break;
                        }
                        Thread.Yield();
                    }
                }


                sw.Stop();

                _msg.Length = 0;
                _msg.Append("MultiplePFThreadTaskTest finished at ");
                _msg.Append(sw.StopTime.ToString("HH:mm:ss.ms"));
                _msg.Append("\r\n");
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());
                Console.WriteLine(_msg.ToString());

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
                _msg.Append("... MultiplePFThreadTaskTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


                 
        

    }//end class
}//end namespace

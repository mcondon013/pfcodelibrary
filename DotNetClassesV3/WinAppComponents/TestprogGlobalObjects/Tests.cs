using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;

namespace TestprogGlobalObjects
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

        public static void TimedDialogBoxTest(MainForm frm)
        {
            ModelessPopupDialog diag = null;
            int maxSeconds = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("TestTimedDialogBox started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                maxSeconds = AppTextGlobals.ConvertStringToInt(frm.txtNumSecs.Text, 0);

                if (maxSeconds == 0)
                    maxSeconds = 5;

                diag = new ModelessPopupDialog(maxSeconds);
                diag.Message = "Processing will be done for " + maxSeconds.ToString() + " seconds.";
                diag.Show();

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
                _msg.Append("\r\n... TestTimedDialogBox finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ModelessDialogBoxTest(MainForm frm)
        {
            ModelessPopupDialog diag = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ModelessDialogBoxTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                diag = new ModelessPopupDialog();

                diag.ButtonLabel = "Cancel";
                diag.Message = "This is the first message.\r\nPlus the second line which goes here and not there...I don't really know.\r\n"
                             + "Let this be the third line.\r\n"
                             + "Let this be the yet another line.\r\n"
                             + "More, More, More ...\r\n"
                             + "Let this be the yet another line.\r\n"
                             + "More, More, More ...\r\n"
                             + "Done, done, done ... that's all";
                diag.ActionButtonPressed = false;
                diag.Show();

                int cnt = 0;
                int maxCnt = 100000000;
                while (diag.ActionButtonPressed == false)
                {
                    cnt++;
                    if ((cnt % 1000000) == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("cnt = ");
                        _msg.Append(cnt.ToString("#,##0"));
                        Program._messageLog.RetainFocus = false;  //can cause screen flicker in this context; normally this should be true
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                    if (cnt > maxCnt)
                    {
                        break;
                    }
                    Application.DoEvents();
                }

                diag.Close();

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
                if (diag != null)
                    if (diag.IsVisible)
                        diag.Close();
                diag = null;
                _msg.Length = 0;
                _msg.Append("\r\n... ModelessDialogBoxTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void InputBoxTest(MainForm frm)
        {
            InputBox box = new InputBox();
            box.Caption = "Testprog Find Pop Up";
            box.InputTextLabel = "Text to find: ";
            box.OkButtonText = "Find";
            box.CancelButtonText = "Cancel";
            box.InputText = "find this";

            DialogResult res = box.ShowDialog((Control)frm);
            //DialogResult defaultPrinterName = box.ShowDialog();
            //frm.Focus();

            _msg.Length = 0;
            if (res == DialogResult.OK)
            {
                _msg.Append("InputBox text is:\r\n");
                _msg.Append(box.InputText);
            }
            else
            {
                _msg.Append("Cancel button chosen.");
            }

            //box.Close();

            Program._messageLog.WriteLine(_msg.ToString());

        }




    }//end class
}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using PFProcessObjects;
using PFMessageLogs;
using PFProviderForms;

namespace TestprogProviderForms
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }


        //application routines

        public void RunShowDateTimeTest()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("RunShowDateTimeTest started ...");
                _messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Current date/time is ");
                _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                _messageLog.WriteLine(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... RunShowDateTimeTest finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void ShowProviderSelectorForm()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("ShowProviderSelectorForm started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                ProviderSelectorForm frm = new ProviderSelectorForm();
                DialogResult res = frm.ShowDialog();

                _msg.Length = 0;
                _msg.Append("DialogResult is ");
                _msg.Append(res.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("Time to do something with changes.");
                }
                else
                {
                    _msg.Append("Any changes have been discarded.");
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
                _msg.Length = 0;
                _msg.Append("\r\n... ShowProviderSelectorForm finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace

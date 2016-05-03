using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFNetworkObjects;
using PFListObjects;

namespace TestprogEmail
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;
        private static string _emailResendQueueDatabaseConnectionString = "data source ='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\Database\Lists.sdf") + "'";

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
        public static void SMTPQuickTest()
        {
            
            try
            {
                _msg.Length = 0;
                _msg.Append("SMTPQuickTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                PFEmailMessage emMsg = new PFEmailMessage("PROFASTWS5", "testprog@ws5", "Mike@WS9", "Test Message", "Send date/time: " + DateTime.Now.ToString());
                emMsg.SmtpPort = 25;

                _msg.Length = 0;
                _msg.Append("SMTP Port = ");
                _msg.Append(emMsg.SmtpPort);
                _msg.Append("\r\n");
                _msg.Append("SMTP Server = ");
                _msg.Append(emMsg.SMTPServer);
                _msg.Append("\r\n");
                _msg.Append("From Address = ");
                _msg.Append(emMsg.FromAddress);
                _msg.Append("\r\n");
                _msg.Append("To Address = ");
                _msg.Append(emMsg.ToAddress);
                _msg.Append("\r\n");
                _msg.Append("Message Subject = ");
                _msg.Append(emMsg.MessageSubject);
                _msg.Append("\r\n");
                _msg.Append("Message Body = ");
                _msg.Append(emMsg.MessageBody);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                emMsg.Send();

               
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
                _msg.Append("\r\n... SMTPQuickTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void SmtpTest(MainForm frm)
        {
            PFEmailMessage emMsg = new PFEmailMessage();
            PFEmailManager emMgr;
            string connectionString = string.Empty;
            string configValue = string.Empty;
            
            try
            {
                _msg.Length = 0;
                _msg.Append("SmtpTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("EmailResendQueueDatabaseConnectionString", string.Empty);
                if (configValue.Length > 0)
                    _emailResendQueueDatabaseConnectionString = configValue;
                connectionString = _emailResendQueueDatabaseConnectionString;

                emMgr = new PFEmailManager(@"c:\temp\EmailResendQueue.xml");

                emMsg.SMTPServer = frm.txtSmtpServer.Text;
                emMsg.SmtpPort = AppTextGlobals.ConvertStringToInt(frm.txtSmtpPort.Text, 25);
                if (frm.txtSendTimeout.Text.Length > 0)
                    emMsg.SendTimeout = AppTextGlobals.ConvertStringToInt(frm.txtSendTimeout.Text, 100000);
                if (frm.txtMaxIdleTime.Text.Length > 0)
                    emMsg.MaxIdleTime = AppTextGlobals.ConvertStringToInt(frm.txtMaxIdleTime.Text, 1);
                emMsg.EnableSsl = frm.chkUseSsl.Checked;
                emMsg.SenderAddress = frm.txtFromAddress.Text;
                emMsg.FromAddress = frm.txtFromAddress.Text;
                emMsg.ToAddress = frm.txtToAddress.Text;
                if (frm.txtCCAddress.Text.Length > 0)
                    emMsg.CCAddress = frm.txtCCAddress.Text;
                if (frm.txtBCCAddress.Text.Length > 0)
                    emMsg.BccAddress = frm.txtBCCAddress.Text;
                emMsg.MessageSubject = frm.txtSubjectLine.Text;
                emMsg.MessageBody = frm.txtMessageBody.Text;


                _msg.Length = 0;
                _msg.Append("SMTP Port = ");
                _msg.Append(emMsg.SmtpPort);
                _msg.Append("\r\n");
                _msg.Append("Send Timeout = ");
                _msg.Append(emMsg.SendTimeout);
                _msg.Append("\r\n");
                _msg.Append("Max Idle Time = ");
                _msg.Append(emMsg.MaxIdleTime);
                _msg.Append("\r\n");
                _msg.Append("SMTP Server = ");
                _msg.Append(emMsg.SMTPServer);
                _msg.Append("\r\n");
                _msg.Append("From Address = ");
                _msg.Append(emMsg.FromAddress);
                _msg.Append("\r\n");
                _msg.Append("To Address = ");
                _msg.Append(emMsg.ToAddress);
                _msg.Append("\r\n");
                _msg.Append("CC Address = ");
                _msg.Append(emMsg.CCAddress);
                _msg.Append("\r\n");
                _msg.Append("BCC Address = ");
                _msg.Append(emMsg.BccAddress);
                _msg.Append("\r\n");
                _msg.Append("Message Subject = ");
                _msg.Append(emMsg.MessageSubject);
                _msg.Append("\r\n");
                _msg.Append("Message Body = ");
                _msg.Append(emMsg.MessageBody);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                
                //emMsg.Send();
                stEmailSendResult res = emMgr.SendEmail(emMsg);

                
                _msg.Length = 0;
                _msg.Append("Email send result: ");
                _msg.Append(res.emailSendResult.ToString());
                if (res.emailSendResult != enEmailSendResult.Success)
                {
                    _msg.Append("\r\n");
                    _msg.Append("Failure reason: ");
                    _msg.Append(res.emailFailedReason.ToString());
                    _msg.Append("\r\nError Messages:\r\n");
                    _msg.Append(res.failureMessages);
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
                _msg.Append("\r\n... SmtpTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void ResendEmailsTest(MainForm frm)
        {
            PFEmailManager emMgr;
            string connectionString = string.Empty;
            string configValue = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ResendEmailsTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("EmailResendQueueDatabaseConnectionString", string.Empty);
                if (configValue.Length > 0)
                    _emailResendQueueDatabaseConnectionString = configValue;

                emMgr = new PFEmailManager(@"c:\temp\EmailResendQueue.xml");

                PFListEx<stEmailArchiveEntry> successfulResends = emMgr.ResendEmails();

                _msg.Length = 0;
                _msg.Append("Number of successful resends: ");
                _msg.Append(successfulResends.Count());
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("Number of failed resends:     ");
                _msg.Append(emMgr.EmailResendQueue.Count());
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < emMgr.EmailResendQueue.Count; i++)
                {
                    stEmailArchiveEntry em = emMgr.EmailResendQueue[i];
                    _msg.Length = 0;
                    _msg.Append("Attempt to send email to server ");
                    _msg.Append(em.emailMessage.SMTPServer);
                    _msg.Append(" failed. Retry count = ");
                    _msg.Append(em.numRetries.ToString());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Number of last resends:       ");
                _msg.Append(emMgr.EmailLastResendList.Count());
                Program._messageLog.WriteLine(_msg.ToString());

                for (int i = 0; i < emMgr.EmailLastResendList.Count; i++)
                {
                    stEmailArchiveEntry em = emMgr.EmailLastResendList[i];
                    _msg.Length = 0;
                    _msg.Append("Final attempt to send email to server ");
                    _msg.Append(em.emailMessage.SMTPServer);
                    _msg.Append(" failed. Retry count = ");
                    _msg.Append(em.numRetries.ToString());
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
                _msg.Append("\r\n... ResendEmailsTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace

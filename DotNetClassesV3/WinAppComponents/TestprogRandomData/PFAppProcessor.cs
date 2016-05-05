using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFProcessObjects;
using PFMessageLogs;
using PFRandomData;

namespace TestprogRandomData
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


        public void RandomNamesTest(string pNameLocation, string pNumOutputValues)
        {
            enNameLocation nameLocation = enNameLocation.enUnknown;
            int numOutputValues = -1;
            RandomName rname = null;
            try
            {
                _msg.Length = 0;
                _msg.Append("RandomNamesTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                nameLocation = (enNameLocation)Enum.Parse(typeof(enNameLocation), pNameLocation);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rname = new RandomName(nameLocation); 

                _msg.Length = 0;
                _msg.Append("Name Location: ");
                _msg.Append(nameLocation.ToString());
                WriteMessageToLog(_msg.ToString());

                RunRandomLastNameTest(nameLocation, numOutputValues, rname);
                RunRandomFirstNameFemaleTest(nameLocation, numOutputValues, rname);
                RunRandomFirstNameMaleTest(nameLocation, numOutputValues, rname);
                RunRandomFullNameFemaleTest(nameLocation, numOutputValues, rname);
                RunRandomFullNameMaleTest(nameLocation, numOutputValues, rname);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomNamesTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        private void RunRandomLastNameTest(enNameLocation nameLocation, int numOutputValues, RandomName rname)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomLastNameTest for ");
            _msg.Append(nameLocation.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rname.GetLastName());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomFirstNameFemaleTest(enNameLocation nameLocation, int numOutputValues, RandomName rname)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomFirstNameFemaleTest for ");
            _msg.Append(nameLocation.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rname.GetFirstNameFemale());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomFirstNameMaleTest(enNameLocation nameLocation, int numOutputValues, RandomName rname)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomFirstNameMaleTest for ");
            _msg.Append(nameLocation.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rname.GetFirstNameMale());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomFullNameFemaleTest(enNameLocation nameLocation, int numOutputValues, RandomName rname)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomFullNameFemaleTest for ");
            _msg.Append(nameLocation.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rname.GetFullNameFemale());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomFullNameMaleTest(enNameLocation nameLocation, int numOutputValues, RandomName rname)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomFullNameMaleTest for ");
            _msg.Append(nameLocation.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rname.GetFullNameMale());
                WriteMessageToLog(_msg.ToString());
            }
        }



        public void CustomRandomNamesTest(string pCustomFirstNamesFemaleFile,
                                          string pCustomFirstNamesMaleFile,
                                          string pCustomLastNamesFile,
                                          string pNumOutputValues)
        {
            enNameLocation nameLocation = enNameLocation.enUnknown;
            string customFirstNamesFemaleFile = string.Empty;
            string customFirstNamesMaleFile = string.Empty;
            string customLastNamesFile = string.Empty;
            int numOutputValues = -1;
            RandomName rname = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("CustomRandomNamesTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                nameLocation = enNameLocation.enCustom;
                customFirstNamesFemaleFile = pCustomFirstNamesFemaleFile;
                customFirstNamesMaleFile = pCustomFirstNamesMaleFile;
                customLastNamesFile = pCustomLastNamesFile;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rname = new RandomName(customFirstNamesFemaleFile, customFirstNamesMaleFile, customLastNamesFile, Encoding.UTF7);

                RunRandomLastNameTest(nameLocation, numOutputValues, rname);
                RunRandomFirstNameFemaleTest(nameLocation, numOutputValues, rname);
                RunRandomFirstNameMaleTest(nameLocation, numOutputValues, rname);
                RunRandomFullNameFemaleTest(nameLocation, numOutputValues, rname);
                RunRandomFullNameMaleTest(nameLocation, numOutputValues, rname);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... CustomRandomNamesTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void CustomRandomValuesTest(string pCustomValuesFile,
                                          string pNumOutputValues)
        {
            string customValuesFile = string.Empty;
            int numOutputValues = -1;
            RandomValues rvals = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("CustomRandomValuesTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                customValuesFile = pCustomValuesFile;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rvals = new RandomValues(pCustomValuesFile, System.Text.Encoding.UTF7);

                for (int i = 0; i < numOutputValues; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(rvals.GetRandomValue().ToString());
                    WriteMessageToLog(_msg.ToString());
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... CustomRandomValuesTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        public void RandomBusinessNamesTest(string pNameLocation, string pNumOutputValues)
        {
            enNameLocation nameLocation = enNameLocation.enUnknown;
            int numOutputValues = -1;
            RandomName rname = null;
            
            try
            {
                _msg.Length = 0;
                _msg.Append("RandomBusinessNamesTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                nameLocation = (enNameLocation)Enum.Parse(typeof(enNameLocation), pNameLocation);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rname = new RandomName(nameLocation);

                _msg.Length = 0;
                _msg.Append("Name Location: ");
                _msg.Append(nameLocation.ToString());
                WriteMessageToLog(_msg.ToString());

                for (int i = 0; i < numOutputValues; i++)
                {
                    _msg.Length = 0;
                    _msg.Append(rname.GetBusinessName());
                    WriteMessageToLog(_msg.ToString());
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomBusinessNamesTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void RandomDateTimeTest(string pFromDate, string pToDate, string pFromTime, string pToTime, bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            TimeSpan fromTime = TimeSpan.MinValue;
            TimeSpan toTime = TimeSpan.MinValue;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDateTimeTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                fromDate = Convert.ToDateTime(pFromDate);
                toDate = Convert.ToDateTime(pToDate);
                fromTime = TimeSpan.Parse(pFromTime);
                toTime = TimeSpan.Parse(pToTime);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (generateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            _msg.Append(rdt.GenerateRandomDateTime(fromDate, toDate, fromTime, toTime).ToString("MM/dd/yyyy HH:mm:ss"));
                        }
                        else
                        {
                            long dwRandomDateTime = rdt.GenerateRandomDateTimeAsInteger(fromDate, toDate, fromTime, toTime);
                            _msg.Append(dwRandomDateTime.ToString("0"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        DateTime[] randomDateTimes = rdt.GenerateRandomDateTime(fromDate, toDate, fromTime, toTime, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDateTimes[i].ToString("MM/dd/yyyy HH:mm:ss"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        long[] randomDateTimes = rdt.GenerateRandomDateTimeAsInteger(fromDate, toDate, fromTime, toTime, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDateTimes[i].ToString("0"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDateTimeTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        public void RandomDateTest(string pFromDate, string pToDate, bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDateTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                fromDate = Convert.ToDateTime(pFromDate);
                toDate = Convert.ToDateTime(pToDate);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (generateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            _msg.Append(rdt.GenerateRandomDate(fromDate, toDate));
                        }
                        else
                        {
                            int dwRandomDate = rdt.GenerateRandomDateAsInteger(fromDate, toDate);
                            _msg.Append(dwRandomDate.ToString("0"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        DateTime[] randomDates = rdt.GenerateRandomDate(fromDate, toDate, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDates[i].ToString("MM/dd/yyyy HH:mm:ss"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        int[] randomDateTimes = rdt.GenerateRandomDateAsInteger(fromDate, toDate, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDateTimes[i].ToString("0"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }




            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDateTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        public void RandomTimeTest(string pFromTime, string pToTime, bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            TimeSpan fromTime = TimeSpan.MinValue;
            TimeSpan toTime = TimeSpan.MinValue;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();
            TimeSpan res = default(TimeSpan);
            TimeSpan tsOut = default(TimeSpan);
            
            try
            {
                _msg.Length = 0;
                _msg.Append("RandomTimeTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                fromTime = TimeSpan.Parse(pFromTime);
                toTime = TimeSpan.Parse(pToTime);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (generateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            res = rdt.GenerateRandomTime(fromTime, toTime);
                            tsOut = new TimeSpan(0, Math.Abs(res.Hours), Math.Abs(res.Minutes), Math.Abs(res.Seconds), 0);
                            _msg.Append(tsOut.ToString(""));
                        }
                        else
                        {
                            int dwRandomTime = 0;
                            dwRandomTime = rdt.GenerateRandomTimeAsInteger(fromTime, toTime);
                            _msg.Append(dwRandomTime.ToString("000000"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        TimeSpan[] randomTimes = rdt.GenerateRandomTime(fromTime, toTime, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            tsOut = new TimeSpan(0, Math.Abs(randomTimes[i].Hours), Math.Abs(randomTimes[i].Minutes), Math.Abs(randomTimes[i].Seconds), 0);
                            _msg.Append(tsOut.ToString(""));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        int[] randomTimes = rdt.GenerateRandomTimeAsInteger(fromTime, toTime, numOutputValues);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomTimes[i].ToString("000000"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomTimeTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }



        public void DateTimeOffsetTest(string pDateTimeToModify, System.Windows.Forms.RadioButton pOffsetSelection, string pMinOffset, string pMaxOffset, 
                                       bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            DateTime dateTimeToModify = DateTime.MinValue;
            DateTime dtm = DateTime.MinValue;
            enRandomOffsetType randomOffsetInterval = enRandomOffsetType.enUnknown;
            string offsetText = "en";
            int minOffset = 0;
            int maxOffset = 0;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();

            try
            {
                _msg.Length = 0;
                _msg.Append("DateTimeOffsetTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                dateTimeToModify = Convert.ToDateTime(pDateTimeToModify);
                offsetText += pOffsetSelection.Text.Trim();
                randomOffsetInterval = (enRandomOffsetType)Enum.Parse(typeof(enRandomOffsetType), offsetText);
                minOffset = Convert.ToInt32(pMinOffset);
                maxOffset = Convert.ToInt32(pMaxOffset);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (pGenerateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            dtm = rdt.OffsetDateTime(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dtm.ToString("MM/dd/yyyy HH:mm:ss"));
                        }
                        else
                        {
                            
                            long dwDateTime = 0;
                            dwDateTime = rdt.OffsetDateTimeAsInteger(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dwDateTime.ToString("0"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //create set if date times for the test: same datetime repeated in each element of the array
                    DateTime[] dateTimesToModify = new DateTime[numOutputValues];
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        dateTimesToModify[i] = dateTimeToModify;
                    }
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        DateTime[] modifiedDateTimes = rdt.OffsetDateTime(dateTimesToModify,randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(modifiedDateTimes[i].ToString("MM/dd/yyyy HH:mm:ss"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        ;
                        long[] randomDateTimes = rdt.OffsetDateTimeAsInteger(dateTimesToModify, randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDateTimes[i].ToString("0"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... DateTimeOffsetTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        public void DateOffsetTest(string pDateTimeToModify, System.Windows.Forms.RadioButton pOffsetSelection, string pMinOffset, string pMaxOffset,
                                   bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            DateTime dateTimeToModify = DateTime.MinValue;
            DateTime dtm = DateTime.MinValue;
            enRandomOffsetType randomOffsetInterval = enRandomOffsetType.enUnknown;
            string offsetText = "en";
            int minOffset = 0;
            int maxOffset = 0;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();

            try
            {
                _msg.Length = 0;
                _msg.Append("DateOffsetTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                dateTimeToModify = Convert.ToDateTime(pDateTimeToModify);
                offsetText += pOffsetSelection.Text.Trim();
                randomOffsetInterval = (enRandomOffsetType)Enum.Parse(typeof(enRandomOffsetType), offsetText);
                minOffset = Convert.ToInt32(pMinOffset);
                maxOffset = Convert.ToInt32(pMaxOffset);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (pGenerateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            dtm = rdt.OffsetDate(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dtm.ToString("MM/dd/yyyy"));
                        }
                        else
                        {

                            int dwDate = 0;
                            dwDate = rdt.OffsetDateAsInteger(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dwDate.ToString("0"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //create set if date times for the test: same datetime repeated in each element of the array
                    DateTime[] dateTimesToModify = new DateTime[numOutputValues];
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        dateTimesToModify[i] = dateTimeToModify;
                    }
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        DateTime[] modifiedDates = rdt.OffsetDate(dateTimesToModify, randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(modifiedDates[i].ToString("MM/dd/yyyy"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        ;
                        int[] randomDates = rdt.OffsetDateAsInteger(dateTimesToModify, randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomDates[i].ToString("0"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... DateOffsetTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void TimeOffsetTest(string pDateTimeToModify, System.Windows.Forms.RadioButton pOffsetSelection, string pMinOffset, string pMaxOffset,
                                   bool pShowResultAsInteger, string pNumOutputValues, bool pGenerateResultsArray)
        {
            DateTime dateTimeToModify = DateTime.MinValue;
            DateTime dtm = DateTime.MinValue;
            enRandomOffsetType randomOffsetInterval = enRandomOffsetType.enUnknown;
            string offsetText = "en";
            int minOffset = 0;
            int maxOffset = 0;
            bool showResultAsInteger = false;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomDateTime rdt = new RandomDateTime();

            try
            {
                _msg.Length = 0;
                _msg.Append("TimeOffsetTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                dateTimeToModify = Convert.ToDateTime(pDateTimeToModify);
                offsetText += pOffsetSelection.Text.Trim();
                randomOffsetInterval = (enRandomOffsetType)Enum.Parse(typeof(enRandomOffsetType), offsetText);
                minOffset = Convert.ToInt32(pMinOffset);
                maxOffset = Convert.ToInt32(pMaxOffset);
                showResultAsInteger = pShowResultAsInteger;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (pGenerateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        if (showResultAsInteger == false)
                        {
                            dtm = rdt.OffsetTime(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dtm.ToString("HH:mm:ss"));
                        }
                        else
                        {

                            int dwTime = 0;
                            dwTime = rdt.OffsetTimeAsInteger(dateTimeToModify, randomOffsetInterval, minOffset, maxOffset);
                            _msg.Append(dwTime.ToString("000000"));
                        }
                        WriteMessageToLog(_msg.ToString());
                    }
                }
                else
                {
                    //create set if date times for the test: same datetime repeated in each element of the array
                    DateTime[] dateTimesToModify = new DateTime[numOutputValues];
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        dateTimesToModify[i] = dateTimeToModify;
                    }
                    //generate a results array
                    if (showResultAsInteger == false)
                    {
                        DateTime[] modifiedTimes = rdt.OffsetTime(dateTimesToModify, randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(modifiedTimes[i].ToString("HH:mm:ss"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                    else
                    {
                        int[] randomTimes = rdt.OffsetTimeAsInteger(dateTimesToModify, randomOffsetInterval, minOffset, maxOffset);
                        for (int i = 0; i < numOutputValues; i++)
                        {
                            _msg.Length = 0;
                            _msg.Append(randomTimes[i].ToString("000000"));
                            WriteMessageToLog(_msg.ToString());
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... TimeOffsetTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void RandomNumberTest(enRandomNumberType numberType, string pMinNum, string pMaxNum, string pNumOutputValues, bool pGenerateResultsArray)
        {
            double minNum = 0;
            double maxNum = 0;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomNumber rn = new RandomNumber();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomNumberTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                minNum = Convert.ToDouble(pMinNum);
                maxNum = Convert.ToDouble(pMaxNum);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;

                if (generateResultsArray == false)
                {
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        _msg.Append(numberType.ToString());
                        _msg.Append(":  ");
                        switch (numberType)
                        {
                            case enRandomNumberType.enInt:
                                _msg.Append(rn.GenerateRandomInt((int)minNum, (int)maxNum).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enUInt:
                                _msg.Append(rn.GenerateRandomNumber((uint)minNum, (uint)maxNum).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enLong:
                                _msg.Append(rn.GenerateRandomNumber((long)minNum, (long)maxNum).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enULong:
                                _msg.Append(rn.GenerateRandomNumber((ulong)minNum, (ulong)maxNum).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enShort:
                                _msg.Append(rn.GenerateRandomNumber((short)minNum, (short)maxNum).ToString());
                                break;
                            case enRandomNumberType.enUShort:
                                _msg.Append(rn.GenerateRandomNumber((ushort)minNum, (ushort)maxNum).ToString());
                                break;
                            case enRandomNumberType.enSByte:
                                _msg.Append(rn.GenerateRandomNumber((sbyte)minNum, (sbyte)maxNum).ToString());
                                break;
                            case enRandomNumberType.enByte:
                                _msg.Append(rn.GenerateRandomNumber((byte)minNum, (byte)maxNum).ToString());
                                break;
                            case enRandomNumberType.enDouble:
                                _msg.Append(rn.GenerateRandomNumber((double)minNum, (double)maxNum).ToString("#,##0.0###########"));
                                break;
                            case enRandomNumberType.enFloat:
                                _msg.Append(rn.GenerateRandomNumber((float)minNum, (float)maxNum).ToString("#,##0.0###########"));
                                break;
                            case enRandomNumberType.enDecimal:
                                _msg.Append(rn.GenerateRandomNumber((decimal)minNum, (decimal)maxNum).ToString("#,##0.0###########"));
                                break;
                            default:
                                _msg.Append("???");
                                break;
                        }
                        WriteMessageToLog(_msg.ToString());

                    }
                }
                else
                {
                    string formattedMsg = string.Empty;
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        _msg.Append(numberType.ToString());
                        _msg.Append(":  ");
                        switch (numberType)
                        {
                            case enRandomNumberType.enInt:
                                int[] res = rn.GenerateRandomInt((int)minNum, (int)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = res[n].ToString("#,##0") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0, 15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enUInt:
                                uint[] resUint = rn.GenerateRandomNumber((uint)minNum, (uint)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resUint[n].ToString("#,##0") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0, 15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enLong:
                                long[] resLong = rn.GenerateRandomNumber((long)minNum, (long)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resLong[n].ToString("#,##0") + new String(' ', 20);
                                    _msg.Append(formattedMsg.Substring(0, 20));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enULong:
                                ulong[] resUlong = rn.GenerateRandomNumber((ulong)minNum, (ulong)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resUlong[n].ToString("#,##0") + new String(' ', 20);
                                    _msg.Append(formattedMsg.Substring(0, 20));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enShort:
                                short[] resShort = rn.GenerateRandomNumber((short)minNum, (short)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resShort[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enUShort:
                                ushort[] resUshort = rn.GenerateRandomNumber((ushort)minNum, (ushort)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resUshort[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enSByte:
                                sbyte[] resSbyte = rn.GenerateRandomNumber((sbyte)minNum, (sbyte)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resSbyte[n].ToString() + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enByte:
                                byte[] resByte = rn.GenerateRandomNumber((byte)minNum, (byte)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resByte[n].ToString() + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enDouble:
                                double[] resDouble = rn.GenerateRandomNumber((double)minNum, (double)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resDouble[n].ToString("#,##0.0###########") + new String(' ', 30);
                                    _msg.Append(formattedMsg.Substring(0, 30));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enFloat:
                                float[] resFloat = rn.GenerateRandomNumber((float)minNum, (float)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resFloat[n].ToString("#,##0.0###########") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0, 15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enDecimal:
                                decimal[] resDecimal = rn.GenerateRandomNumber((decimal)minNum, (decimal)maxNum, numOutputValues);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resDecimal[n].ToString("#,##0.0###########") + new String(' ', 30);
                                    _msg.Append(formattedMsg.Substring(0, 30));
                                    _msg.Append(" ");
                                }
                                break;
                            default:
                                _msg.Append("???");
                                break;
                        }
                        WriteMessageToLog(_msg.ToString());

                    }
                }

           
            
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomNumberTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void RandomOffsetTest(enRandomNumberType numberType, string pNumToModify, string pMinOffsetPercent, string pMaxOffsetPercent, string pNumOutputValues, bool pGenerateResultsArray)
        
        {
            double numToModify = 0.0;
            double minOffsetPercent = 0.0;
            double maxOffsetPercent = 0.0;
            int numOutputValues = -1;
            bool generateResultsArray = false;
            RandomNumber rn = new RandomNumber();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomOffsetTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                numToModify = Convert.ToDouble(pNumToModify);
                minOffsetPercent = Convert.ToDouble(pMinOffsetPercent);
                maxOffsetPercent = Convert.ToDouble(pMaxOffsetPercent);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                generateResultsArray = pGenerateResultsArray;


                if (generateResultsArray == false)
                {
                    _msg.Length = 0;
                    _msg.Append("NumToModify: ");
                    _msg.Append(pNumToModify);
                    WriteMessageToLog(_msg.ToString());
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        _msg.Append(numberType.ToString());
                        _msg.Append(":  ");
                        switch (numberType)
                        {
                            case enRandomNumberType.enInt:
                                _msg.Append(rn.OffsetInt((int)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enUInt:
                                _msg.Append(rn.OffsetNumber((uint)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enLong:
                                _msg.Append(rn.OffsetNumber((long)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enULong:
                                _msg.Append(rn.OffsetNumber((ulong)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enShort:
                                _msg.Append(rn.OffsetNumber((short)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enUShort:
                                _msg.Append(rn.OffsetNumber((ushort)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enSByte:
                                _msg.Append(rn.OffsetNumber((sbyte)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enByte:
                                _msg.Append(rn.OffsetNumber((byte)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0"));
                                break;
                            case enRandomNumberType.enDouble:
                                _msg.Append(rn.OffsetNumber((double)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0.0###########"));
                                break;
                            case enRandomNumberType.enFloat:
                                _msg.Append(rn.OffsetNumber((float)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0.0###########"));
                                break;
                            case enRandomNumberType.enDecimal:
                                _msg.Append(rn.OffsetNumber((decimal)numToModify, minOffsetPercent, maxOffsetPercent).ToString("#,##0.0###########"));
                                break;
                            default:
                                _msg.Append("???");
                                break;
                        }
                        WriteMessageToLog(_msg.ToString());

                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("NumToModify: ");
                    _msg.Append(pNumToModify);
                    WriteMessageToLog(_msg.ToString());
                    //generate results array
                    string formattedMsg = string.Empty;
                    for (int i = 0; i < numOutputValues; i++)
                    {
                        _msg.Length = 0;
                        _msg.Append(numberType.ToString());
                        _msg.Append(":  ");
                        switch (numberType)
                        {
                            case enRandomNumberType.enInt:
                                int[] inp = new int[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inp[n] = Convert.ToInt32(pNumToModify);
                                }
                                int[] res = rn.OffsetInt(inp, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = res[n].ToString("#,##0") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0,15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enUInt:
                                uint[] inpUint = new uint[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpUint[n] = Convert.ToUInt32(pNumToModify);
                                }
                                uint[] resUint = rn.OffsetNumber(inpUint, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resUint[n].ToString("#,##0") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0, 15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enLong:
                                long[] inpLong = new long[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpLong[n] = Convert.ToInt64(pNumToModify);
                                }
                                long[] resLong = rn.OffsetNumber(inpLong, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resLong[n].ToString("#,##0") + new String(' ', 20);
                                    _msg.Append(formattedMsg.Substring(0, 20));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enULong:
                                ulong[] inpULong = new ulong[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpULong[n] = Convert.ToUInt64(pNumToModify);
                                }
                                ulong[] resULong = rn.OffsetNumber(inpULong, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resULong[n].ToString("#,##0") + new String(' ', 20);
                                    _msg.Append(formattedMsg.Substring(0, 20));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enShort:
                                short[] inpShort = new short[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpShort[n] = Convert.ToInt16(pNumToModify);
                                }
                                short[] resShort = rn.OffsetNumber(inpShort, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resShort[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enUShort:
                                ushort[] inpUShort = new ushort[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpUShort[n] = Convert.ToUInt16(pNumToModify);
                                }
                                ushort[] resUShort = rn.OffsetNumber(inpUShort, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resUShort[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enSByte:
                                sbyte[] inpSByte = new sbyte[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpSByte[n] = Convert.ToSByte(pNumToModify);
                                }
                                sbyte[] resSByte = rn.OffsetNumber(inpSByte, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resSByte[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enByte:
                                byte[] inpByte = new byte[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpByte[n] = Convert.ToByte(pNumToModify);
                                }
                                byte[] resByte = rn.OffsetNumber(inpByte, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resByte[n].ToString("#,##0") + new String(' ', 10);
                                    _msg.Append(formattedMsg.Substring(0, 10));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enDouble:
                                double[] inpDouble = new double[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpDouble[n] = Convert.ToDouble(pNumToModify);
                                }
                                double[] resDouble = rn.OffsetNumber(inpDouble, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resDouble[n].ToString("#,##0.0###########") + new String(' ', 30);
                                    _msg.Append(formattedMsg.Substring(0, 30));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enFloat:
                                float[] inpFloat = new float[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpFloat[n] = Convert.ToSingle(pNumToModify);
                                }
                                float[] resFloat = rn.OffsetNumber(inpFloat, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resFloat[n].ToString("#,##0.0###########") + new String(' ', 15);
                                    _msg.Append(formattedMsg.Substring(0, 15));
                                    _msg.Append(" ");
                                }
                                break;
                            case enRandomNumberType.enDecimal:
                                decimal[] inpDecimal = new decimal[numOutputValues];
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    inpDecimal[n] = Convert.ToDecimal(pNumToModify);
                                }
                                decimal[] resDecimal = rn.OffsetNumber(inpDecimal, minOffsetPercent, maxOffsetPercent);
                                for (int n = 0; n < numOutputValues; n++)
                                {
                                    formattedMsg = resDecimal[n].ToString("#,##0.0###########") + new String(' ', 30);
                                    _msg.Append(formattedMsg.Substring(0, 30));
                                    _msg.Append(" ");
                                }
                                break;
                            default:
                                _msg.Append("???");
                                break;
                        }
                        WriteMessageToLog(_msg.ToString());

                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomOffsetTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }



        public void RandomLocationTest(string pLocation, string pNumOutputValues)
        {
            enLocation location = enLocation.enUnknown;
            int numOutputValues = -1;
            RandomLocation rloc = null;
            try
            {
                _msg.Length = 0;
                _msg.Append("RandomLocationTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                location = (enLocation)Enum.Parse(typeof(enLocation), "en" + pLocation);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rloc = new RandomLocation(location);

                _msg.Length = 0;
                _msg.Append("Location: ");
                _msg.Append(location.ToString());
                WriteMessageToLog(_msg.ToString());

                RunRandomCityNameTest(location, numOutputValues, rloc);
                RunRandomStateProvinceCodeTest(location, numOutputValues, rloc);
                RunRandomStateProvinceNameTest(location, numOutputValues, rloc);
                RunRandomStreetNameTest(location, numOutputValues, rloc);
                RunRandomNeighborhoodNameTest(location, numOutputValues, rloc);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomLocationTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }

        private void RunRandomCityNameTest(enLocation location, int numOutputValues, RandomLocation rloc)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomCityNameTest for ");
            _msg.Append(location.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rloc.GetCityName());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomStateProvinceCodeTest(enLocation location, int numOutputValues, RandomLocation rloc)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomStateProvinceCodeTest for ");
            _msg.Append(location.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rloc.GetStateProvinceCode());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomStateProvinceNameTest(enLocation location, int numOutputValues, RandomLocation rloc)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomStateProvinceNameTest for ");
            _msg.Append(location.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rloc.GetStateProvinceName());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomStreetNameTest(enLocation location, int numOutputValues, RandomLocation rloc)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomStreetNameTest for ");
            _msg.Append(location.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rloc.GetStreetName());
                WriteMessageToLog(_msg.ToString());
            }
        }

        private void RunRandomNeighborhoodNameTest(enLocation location, int numOutputValues, RandomLocation rloc)
        {
            _msg.Length = 0;
            _msg.Append(Environment.NewLine);
            _msg.Append("RandomNeighborhoodNameTest for ");
            _msg.Append(location.ToString());
            _msg.Append(":");
            _msg.Append(Environment.NewLine);
            WriteMessageToLog(_msg.ToString());

            for (int i = 0; i < numOutputValues; i++)
            {
                _msg.Length = 0;
                _msg.Append(rloc.GetNeighborhoodName());
                WriteMessageToLog(_msg.ToString());
            }
        }


        public void CustomLocationTest(string pLocation, string pLocType, string pCustomLocationFile, string pNumOutputValues)
        {
            enLocation location = enLocation.enUnknown;
            enLocationNameType locType = enLocationNameType.enUnknown;
            string customLocationFile = string.Empty;
            int numOutputValues = -1;
            RandomLocation rloc = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("CustomLocationTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                if (pCustomLocationFile.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a custom file name for the CustomLocationTest.");
                    throw new ArgumentException(_msg.ToString());
                }

                location = (enLocation)Enum.Parse(typeof(enLocation), "en" + pLocation);
                locType = (enLocationNameType)Enum.Parse(typeof(enLocationNameType), "en" + pLocType);
                customLocationFile = pCustomLocationFile;
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                rloc = new RandomLocation(location);

                _msg.Length = 0;
                _msg.Append("Location: ");
                _msg.Append(location.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("CustomLocationFile: ");
                _msg.Append(pCustomLocationFile);
                WriteMessageToLog(_msg.ToString());

                rloc.LoadCustomLocation(customLocationFile, locType);
                RunRandomCityNameTest(location, numOutputValues, rloc);
                RunRandomStateProvinceCodeTest(location, numOutputValues, rloc);
                RunRandomStateProvinceNameTest(location, numOutputValues, rloc);
                RunRandomStreetNameTest(location, numOutputValues, rloc);
                RunRandomNeighborhoodNameTest(location, numOutputValues, rloc);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... CustomLocationTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }


        public void RandomBooleanTest(string pNumTrue, string pNumFalse, System.Windows.Forms.RadioButton pOutputType, string pTrueVal, string pFalseVal, string pNumOutputValues)
        {
            enBooleanOutputType outputType = enBooleanOutputType.enUnknown;
            double numTrue = 50.0;
            double numFalse = 50.0;
            bool bTrueVal = true;
            bool bFalseVal = false;
            int nTrueVal = 1;
            int nFalseVal = 0;
            string sTrueVal = "true";
            string sFalseVal = "false";
            int numOutputValues = -1;
            RandomBoolean rb = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomBooleanTest started ...\r\n");
                WriteMessageToLog(_msg.ToString());

                outputType = (enBooleanOutputType)Enum.Parse(typeof(enBooleanOutputType), "en" + pOutputType.Text);
                numTrue = Convert.ToDouble(pNumTrue);
                numFalse = Convert.ToDouble(pNumFalse);
                rb = new RandomBoolean(numTrue, numFalse);
                numOutputValues = Convert.ToInt32(pNumOutputValues);
                switch (pOutputType.Text)
                {
                    case "Boolean":
                        bTrueVal = true;
                        bFalseVal = false;
                        break;
                    case "Integer":
                        nTrueVal = Convert.ToInt32(pTrueVal);
                        nFalseVal = Convert.ToInt32(pFalseVal);
                        break;
                    case "String":
                        sTrueVal = pTrueVal;
                        sFalseVal = pFalseVal;
                        break;
                    default:
                        _msg.Length = 0;
                        _msg.Append("Unexpected or invalid output type: ");
                        _msg.Append(pOutputType.Text);
                        throw new ArgumentException(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("OutputType: ");
                _msg.Append(outputType.ToString());
                _msg.Append(Environment.NewLine);
                _msg.Append("NumTrue: ");
                _msg.Append(numTrue.ToString("#,##0.0######"));
                _msg.Append("  NumFalse: ");
                _msg.Append(numFalse.ToString("#,##0.0######"));
                _msg.Append(Environment.NewLine);
                _msg.Append("TrueVal: ");
                _msg.Append(outputType == enBooleanOutputType.enBoolean ? bTrueVal.ToString() : outputType == enBooleanOutputType.enInteger ? nTrueVal.ToString() : sTrueVal);
                _msg.Append("  FalseVal: ");
                _msg.Append(outputType == enBooleanOutputType.enBoolean ? bFalseVal.ToString() : outputType == enBooleanOutputType.enInteger ? nFalseVal.ToString() : sFalseVal);
                WriteMessageToLog(_msg.ToString());

                for (int i = 0; i < numOutputValues; i++)
                {
                    _msg.Length = 0;
                    switch (outputType)
                    {
                        case enBooleanOutputType.enBoolean:
                            _msg.Append(rb.GetRandomBoolean().ToString());
                            break;
                        case enBooleanOutputType.enInteger:
                            _msg.Append(rb.GetRandomInt(nTrueVal, nFalseVal).ToString());
                            break;
                        case enBooleanOutputType.enString:
                            _msg.Append(rb.GetRandomString(sTrueVal, sFalseVal));
                            break;
                        default:
                            _msg.Append("?????");
                            break;
                    }
                    WriteMessageToLog(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("Second test: True/False Count using current specs.");
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());

                int numRandomTrue = 0;
                int numRandomFalse = 0;
                for (int i = 0; i < 100; i++)
                {
                    _msg.Length = 0;
                    switch (outputType)
                    {
                        case enBooleanOutputType.enBoolean:
                            bool resBoolean = rb.GetRandomBoolean();
                            if (resBoolean == true)
                                numRandomTrue++;
                            else
                                numRandomFalse++;
                            break;
                        case enBooleanOutputType.enInteger:
                            int resInteger = rb.GetRandomInt(nTrueVal, nFalseVal);
                            if(resInteger == nTrueVal)
                                numRandomTrue++;
                            else
                                numRandomFalse++;
                            break;
                        case enBooleanOutputType.enString:
                            string resString = rb.GetRandomString(sTrueVal, sFalseVal);
                            if(resString == sTrueVal)
                                numRandomTrue++;
                            else
                                numRandomFalse++;
                            break;
                        default:
                            break;
                    }
                }
                _msg.Length = 0;
                _msg.Append("NumRandomTrue =  ");
                _msg.Append(numRandomTrue.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("NumRandomFalse = ");
                _msg.Append(numRandomFalse.ToString("#,##0"));
                WriteMessageToLog(_msg.ToString());




            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomBooleanTest finished.");
                WriteMessageToLog(_msg.ToString());

            }
        }
                 
        
        
        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }


    }//end class
}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFTextObjects;

namespace TestprogTextObjects
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

        public static void FormatTimeSpanTest()
        {
            DateTime startTime = Convert.ToDateTime("7/1/2013 08:55:11.100");
            DateTime endTime = Convert.ToDateTime("7/18/2013 23:45:25.321");
            TimeSpan ts = new TimeSpan(0, 15, 33, 21, 150);
            string elapsedTime = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("FormatTimeSpanTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                elapsedTime = PFTextProcessor.FormatElapsedTime(startTime, endTime);

                Program._messageLog.WriteLine(elapsedTime);

                elapsedTime = PFTextProcessor.FormatElapsedTime(startTime, endTime, true);

                Program._messageLog.WriteLine(elapsedTime);

                elapsedTime = PFTextProcessor.FormatTimeSpan(ts);

                Program._messageLog.WriteLine(elapsedTime);

                elapsedTime = PFTextProcessor.FormatTimeSpan(ts, true);

                Program._messageLog.WriteLine(elapsedTime);

                ts = new TimeSpan(0, 0, 14, 0, 101);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts);
                Program._messageLog.WriteLine(elapsedTime);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts, true);
                Program._messageLog.WriteLine(elapsedTime);

                ts = new TimeSpan(0, 0, 0, 0, 101);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts);
                Program._messageLog.WriteLine(elapsedTime);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts, true);
                Program._messageLog.WriteLine(elapsedTime);

                ts = new TimeSpan(0, 3, 0, 50, 101);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts);
                Program._messageLog.WriteLine(elapsedTime);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts, true);
                Program._messageLog.WriteLine(elapsedTime);

                ts = new TimeSpan(0, 3, 0, 0, 707);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts);
                Program._messageLog.WriteLine(elapsedTime);
                elapsedTime = PFTextProcessor.FormatTimeSpan(ts, true);
                Program._messageLog.WriteLine(elapsedTime);

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
                _msg.Append("\r\n... FormatTimeSpanTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }




        public static void RunCommandLineParseTest(MainForm frm)
        {
            PFParseString parse = new PFParseString();

            try
            {
                _msg.Length = 0;
                _msg.Append("RunCommandLineParseTest running ...");
                Program._messageLog.WriteLine(_msg.ToString());

                parse.QuotedValues = frm.chkQuotedValues.Checked;
                parse.KeyType = frm.cboKeyType.Text == "NoKey" ? PFParseString.PFArgumentKeyType.NoKey
                                : frm.cboKeyType.Text == "CharKey" ? PFParseString.PFArgumentKeyType.CharKey
                                : PFParseString.PFArgumentKeyType.NamedKey;
                parse.Delimiters = frm.txtDelimiters.Text;
                parse.StringToParse = frm.txtCommandLineToParse.Text;

                _msg.Length = 0;
                _msg.Append("Key type: ");
                _msg.Append(parse.KeyType.ToString());
                _msg.Append("\r\n");
                _msg.Append("Delimiters: ");
                _msg.Append(parse.Delimiters);
                _msg.Append("\r\n");
                _msg.Append("QuotedValues: ");
                _msg.Append(parse.QuotedValues.ToString());
                _msg.Append("\r\n");
                _msg.Append("String to parse: ");
                _msg.Append(parse.StringToParse);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;

                if (parse.KeyType == PFParseString.PFArgumentKeyType.NoKey)
                {
                    string val = string.Empty;
                    _msg.Append("Show first to last:\r\n");
                    val = parse.GetFirst();
                    while (parse.EOL == false)
                    {
                        _msg.Append(val);
                        _msg.Append("\r\n");
                        val = parse.GetNext();
                    }
                    _msg.Append("Show last to first:\r\n");
                    val = parse.GetLast();
                    while (parse.EOL == false)
                    {
                        _msg.Append(val);
                        _msg.Append("\r\n");
                        val = parse.GetPrev();
                    }
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    //is either CharKey or NamedKey
                    PFParseString.PFKeyValuePair kv = new PFParseString.PFKeyValuePair();
                    _msg.Append("Show first to last:\r\n");
                    kv = parse.GetFirstKeyValue();
                    while (kv.Key != string.Empty)
                    {
                        _msg.Append("Key = ");
                        _msg.Append(kv.Key);
                        _msg.Append(" Val = ");
                        _msg.Append(kv.Value);
                        _msg.Append("\r\n");
                        kv = parse.GetNextKeyValue();
                    }
                    _msg.Append("Show last to first:\r\n");
                    kv = parse.GetLastKeyValue();
                    while (kv.Key != string.Empty)
                    {
                        _msg.Append("Key = ");
                        _msg.Append(kv.Key);
                        _msg.Append(" Val = ");
                        _msg.Append(kv.Value);
                        _msg.Append("\r\n");
                        kv = parse.GetPrevKeyValue();
                    }
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
                ;

            }
        }

        public static void ParseStringAutomaticTest()
        {

            try
            {
                Program._messageLog.WriteLine("\r\n\r\nRunning ParseStringAutomaticTest ...");

                PFParseString parse = new PFParseString();
                string stringToParse = "";

                Program._messageLog.WriteLine("\r\nNoKey");
                parse.KeyType = PFParseString.PFArgumentKeyType.NoKey;
                parse.Delimiters = " ,";
                parse.QuotedValues = true;
                stringToParse = (string)("parm1 parm2 third_parm \"this is fourth param\"");
                Program._messageLog.WriteLine("StringToParse = " + stringToParse);
                parse.StringToParse = stringToParse;

                DisplayParsedItems(parse);
                GetParseKey(parse, 2);

                Program._messageLog.WriteLine("\r\n\r\n");
                Program._messageLog.WriteLine("CharKey");
                parse.KeyType = PFParseString.PFArgumentKeyType.CharKey;
                parse.Delimiters = "-/";
                parse.QuotedValues = false;
                stringToParse = "/Usa -Ume -ppassword -sSERV01 /Iineturl /Ierror";
                //sStringToParse = "/Usa -ppassword -sSERV01 /Iineturl"
                //sStringToParse = "/Usa -ppassword -s""SERV01 """"-moi-"""""" /Iineturl"
                Program._messageLog.WriteLine("StringToParse = " + stringToParse);
                parse.StringToParse = stringToParse;
                DisplayParsedItems(parse);
                GetParseKey(parse, "U");
                GetParseKey(parse, "s");
                GetParseKey(parse, "i");
                GetParseKey(parse, "/x");

                Program._messageLog.WriteLine("\r\n\r\n");
                Program._messageLog.WriteLine("NamedKey");
                parse.KeyType = PFParseString.PFArgumentKeyType.NamedKey;
                //parse.Delimiters = ",";
                parse.Delimiters = " ,";
                parse.QuotedValues = true;
                stringToParse = " Application=MineVer01, Username=\"Me ,And You\", Password=moi1999, Server=DOWN24HRADAY, InternetConnection=\"On The Fritz\"";
                Program._messageLog.WriteLine("StringToParse = " + stringToParse);
                //sStringToParse = "Mojo, Application=MineVer01, MidLife and cries, Username=moi, Password=moi1999, Server=DOWN24HRADAY, InternetConnection=OnTheFritz, BadLast"
                parse.StringToParse = stringToParse;
                DisplayParsedItems(parse);
                GetParseKey(parse, "Nothing");
                GetParseKey(parse, "Application");
                GetParseKey(parse, "username");
                GetParseKey(parse, "password");
                GetParseKey(parse, "InternetConnection");

                Program._messageLog.WriteLine("\r\n\r\n");
                Program._messageLog.WriteLine("NamedKey (second run)");
                parse.KeyType = PFParseString.PFArgumentKeyType.NamedKey;
                parse.Delimiters = " ,";
                parse.QuotedValues = false;
                stringToParse = "Mojo, Application=MineVer01, MidLife and cries, Username=moi, Password=moi1999, Server=DOWN24HRADAY, InternetConnection=OnTheFritz, BadLast";
                parse.StringToParse = stringToParse;
                Program._messageLog.WriteLine("StringToParse = " + stringToParse);
                DisplayParsedItems(parse);
                GetParseKey(parse, "Nothing");
                GetParseKey(parse, "Application");
                GetParseKey(parse, "username");
                GetParseKey(parse, "password");
                GetParseKey(parse, "InternetConnection");
                GetParseKey(parse, parse.NumItems - 1);
                GetParseKey(parse, "BadLast");

                Program._messageLog.WriteLine("\r\n\r\n");
                Program._messageLog.WriteLine("CharKey (using keylist");
                parse.KeyType = PFParseString.PFArgumentKeyType.CharKey;
                parse.Delimiters = "-/";
                parse.QuotedValues = false;
                stringToParse = "/Usa -xme -ppassword -sSERV01 /Iineturl /eerror";
                Program._messageLog.WriteLine("StringToParse = " + stringToParse);
                parse.StringToParse = stringToParse;
                DisplayParsedItems(parse);
                Program._messageLog.WriteLine("Show KV first to last ...");
                if (parse.NumKeys > 0)
                {
                    PFParseString.PFKeyValuePair kv = parse.GetKeyValuePair(PFParseString.PFScroll.First);
                    while (kv.Key != string.Empty)
                    {
                        _msg.Length = 0;
                        _msg.Append("Key = ");
                        _msg.Append(kv.Key);
                        _msg.Append(", value = ");
                        _msg.Append(kv.Value);
                        Program._messageLog.WriteLine(_msg.ToString());
                        kv = parse.GetKeyValuePair(PFParseString.PFScroll.Next);
                    }

                }
                else
                {
                    Program._messageLog.WriteLine("ERROR: No keys found in string that was parsed. There should have been some keys.");
                }


                Program._messageLog.WriteLine("\r\n\r\nShow KV last to first ...");
                if (parse.NumKeys > 0)
                {
                    PFParseString.PFKeyValuePair kv = parse.GetKeyValuePair(PFParseString.PFScroll.Last);
                    while (kv.Key != string.Empty)
                    {
                        _msg.Length = 0;
                        _msg.Append("Key = ");
                        _msg.Append(kv.Key);
                        _msg.Append(", value = ");
                        _msg.Append(kv.Value);
                        Program._messageLog.WriteLine(_msg.ToString());
                        kv = parse.GetKeyValuePair(PFParseString.PFScroll.Previous);
                    }

                }
                else
                {
                    Program._messageLog.WriteLine("ERROR: No keys found in string that was parsed. There should have been some keys.");
                }

                Program._messageLog.WriteLine("\r\nShow params first to last ...");
                if (parse.NumItems > 0)
                {
                    string itm = parse.Get(PFParseString.PFScroll.First);
                    while (parse.EOL == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Parsed Item = ");
                        _msg.Append(itm);
                        Program._messageLog.WriteLine(_msg.ToString());
                        itm = parse.Get(PFParseString.PFScroll.Next);
                    }

                }
                else
                {
                    Program._messageLog.WriteLine("ERROR: No parsed items found in string that was parsed. There should have been some parameters.");
                }

                Program._messageLog.WriteLine("\r\nShow params last to first ...");
                if (parse.NumItems > 0)
                {
                    string itm = parse.Get(PFParseString.PFScroll.Last);
                    while (parse.EOL == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Parsed Item = ");
                        _msg.Append(itm);
                        Program._messageLog.WriteLine(_msg.ToString());
                        itm = parse.Get(PFParseString.PFScroll.Previous);
                    }

                }
                else
                {
                    Program._messageLog.WriteLine("ERROR: No parsed items found in string that was parsed. There should have been some parameters.");
                }



            }
            catch (Exception ex)
            {

                string sMessage;
                sMessage = (string)("Error occured while trying to run parse test." + "\r\n" + "\r\n" + AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(sMessage, true, true);

            }


        }
        private static void DisplayParsedItems(PFParseString parse)
        {

            int numItems;
            int inx;
            string item;

            numItems = System.Convert.ToInt32(parse.NumItems);

            Program._messageLog.WriteLine("Use get first and next");
            item = (string)parse.GetFirst();
            while (!parse.EOL)
            {
                Program._messageLog.WriteLine(item);
                item = (string)parse.GetNext();
            }

            Program._messageLog.WriteLine("Use get last and prev");
            item = (string)parse.GetLast();
            while (!parse.EOL)
            {
                Program._messageLog.WriteLine(item);
                item = (string)parse.GetPrev();
            }

            Program._messageLog.WriteLine("for 1 to count");
            for (inx = 0; inx <= numItems - 1; inx++)
            {
                item = (string)(parse.Get(inx));
                Program._messageLog.WriteLine(inx.ToString() + ": " + item);
            }

        }

        private static void GetParseKey(PFParseString parse, string key)
        {
            string sItem;

            sItem = (string)(parse.Get(key));
            Program._messageLog.WriteLine("Key: " + key + "   Value: " + sItem + "   EOL: " + parse.EOL);

        }

        private static void GetParseKey(PFParseString parse, int key)
        {
            string sItem;

            sItem = (string)(parse.Get(key));
            Program._messageLog.WriteLine("Key: " + key.ToString() + "   Value: " + sItem + "   EOL: " + parse.EOL);

        }



        public static void SearchPatternTest()
        {
            PFSearchPattern[] regexInclude = new PFSearchPattern[2];
            PFSearchPattern[] regexExclude = new PFSearchPattern[2];
            string textToSearch = string.Empty;
            bool isMatch = false;

            try
            {
                _msg.Length = 0;
                _msg.Append("SearchPatternTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                regexInclude[0] = new PFSearchPattern("*test*");
                regexInclude[1] = new PFSearchPattern("First*");
                regexExclude[0] = new PFSearchPattern("*remove*");
                regexExclude[1] = new PFSearchPattern("DELETE:*");

                textToSearch = "This test is the first.";
                isMatch = IsMatchToPattern(regexInclude, textToSearch);
                OutputSearchResult("INCLUDE", regexInclude, textToSearch, isMatch);
                
                textToSearch = "FIRST: this is it.";
                isMatch = IsMatchToPattern(regexInclude, textToSearch);
                OutputSearchResult("INCLUDE", regexInclude, textToSearch, isMatch);

                textToSearch = "This text is the first.";
                isMatch = IsMatchToPattern(regexInclude, textToSearch);
                OutputSearchResult("INCLUDE", regexInclude, textToSearch, isMatch);

                textToSearch = "Please remove this.";
                isMatch = IsMatchToPattern(regexExclude, textToSearch);
                OutputSearchResult("EXCLUDE", regexInclude, textToSearch, isMatch);

                textToSearch = "FIRST: this is it.";
                isMatch = ExtendedSearch(regexInclude, regexExclude, textToSearch);
                OutputSearchResultExt("INCLUDE-EXCLUDE", regexInclude, regexExclude, textToSearch, isMatch);

                textToSearch = "FIRST: this this is the one to remove.";
                isMatch = ExtendedSearch(regexInclude, regexExclude, textToSearch);
                OutputSearchResultExt("INCLUDE-EXCLUDE", regexInclude, regexExclude, textToSearch, isMatch);

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
                _msg.Append("\r\n... SearchPatternTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        private static bool IsMatchToPattern(PFSearchPattern[] regexPatterns, string textToSearch)
        {
            bool isMatch = false;

            for (int i = 0; i < regexPatterns.Length; i++)
            {
                if (regexPatterns[i].IsMatch(textToSearch))
                {
                    isMatch = true;
                    break;
                }
            }

            return isMatch;
        }

        private static bool ExtendedSearch(PFSearchPattern[] includePatterns, PFSearchPattern[] excludePatterns, string textToSearch)
        {
            bool isMatch = false;
            bool include = false;
            bool exclude = false;

            include = IsMatchToPattern(includePatterns, textToSearch);
            exclude = IsMatchToPattern(excludePatterns, textToSearch);

            if (include == true && exclude == false)
                isMatch = true;


            return isMatch;
        }


        private static void OutputSearchResult(string IncludeExclude, PFSearchPattern[] searchPattern, string textToSearch, bool isMatch)
        {
            _msg.Length = 0;
            _msg.Append(IncludeExclude);
            _msg.Append(Environment.NewLine);
            _msg.Append("Search patterns:");
            _msg.Append(Environment.NewLine);
            for (int i = 0; i < searchPattern.Length; i++)
            {
                _msg.Append(searchPattern[i].ToString());
                _msg.Append(Environment.NewLine);
            }
            _msg.Append("Text to search:");
            _msg.Append(Environment.NewLine);
            _msg.Append(textToSearch);
            _msg.Append(Environment.NewLine);

            _msg.Append("IsMatch =");
            _msg.Append(Environment.NewLine);
            _msg.Append(isMatch.ToString());
            _msg.Append(Environment.NewLine);

            Program._messageLog.WriteLine(_msg.ToString());
        }

        private static void OutputSearchResultExt(string IncludeExclude, PFSearchPattern[] includePatterns, PFSearchPattern[] excludePatterns, string textToSearch, bool isMatch)
        {
            _msg.Length = 0;
            _msg.Append(IncludeExclude);
            _msg.Append(Environment.NewLine);
            _msg.Append("Include patterns:");
            _msg.Append(Environment.NewLine);
            for (int i = 0; i < includePatterns.Length; i++)
            {
                _msg.Append(includePatterns[i].ToString());
                _msg.Append(Environment.NewLine);
            }
            _msg.Append("Exclude patterns:");
            _msg.Append(Environment.NewLine);
            for (int i = 0; i < excludePatterns.Length; i++)
            {
                _msg.Append(excludePatterns[i].ToString());
                _msg.Append(Environment.NewLine);
            }
            _msg.Append("Text to search:");
            _msg.Append(Environment.NewLine);
            _msg.Append(textToSearch);
            _msg.Append(Environment.NewLine);

            _msg.Append("IsMatch =");
            _msg.Append(Environment.NewLine);
            _msg.Append(isMatch.ToString());
            _msg.Append(Environment.NewLine);

            Program._messageLog.WriteLine(_msg.ToString());
        }


    }//end class
}//end namespace

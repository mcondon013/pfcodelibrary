using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFListObjects;

namespace TestprogListObjects
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;
        private static string _listsDatabaseConnectionString = "data source ='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PFListEx.sdf") + "'";

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

        public static void GenericListTest()
        {
            PFListEx<string> testList = new PFListEx<string>();
            PFListEx<string> testList2 = new PFListEx<string>();
            PFListEx<string> testList3 = new PFListEx<string>();
            PFListEx<string> testList4 = new PFListEx<string>();
            string testListXmlFile = @"c:\temp\testlist.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;
            try
            {
                _msg.Length = 0;
                _msg.Append("GenericListTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("ListsDatabaseFile", string.Empty);
                if (configValue.Length > 0)
                    connectionString = configValue;
                else
                    connectionString = _listsDatabaseConnectionString;


                _msg.Length = 0;
                _msg.Append("First = ");
                _msg.Append(testList.FirstItem == null ? "<null>" : testList.FirstItem);
                Program._messageLog.WriteLine(_msg.ToString());

                testList.Add("First item");
                testList.Add("Second item");
                testList.Add("Third item");

                _msg.Length = 0;
                _msg.Append("Number of items in list: ");
                Program._messageLog.WriteLine(_msg.ToString());

                foreach (string s in testList)
                {
                    _msg.Length = 0;
                    _msg.Append(s);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("First = ");
                _msg.Append(testList.FirstItem);
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("Last = ");
                _msg.Append(testList.LastItem);
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append("NextItem loop: ");
                Program._messageLog.WriteLine(_msg.ToString());

                string res = testList.FirstItem;
                while (!testList.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(res);
                    Program._messageLog.WriteLine(_msg.ToString());
                    res = testList.NextItem;
                }

                _msg.Length = 0;
                _msg.Append("PrevItem loop: ");
                Program._messageLog.WriteLine(_msg.ToString());

                res = testList.LastItem;
                while (!testList.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(res);
                    Program._messageLog.WriteLine(_msg.ToString());
                    res = testList.PrevItem;
                }

                _msg.Length = 0;
                _msg.Append("ToString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(testList.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("SaveToXmlFile Test: ");
                _msg.Append(testListXmlFile);
                Program._messageLog.WriteLine(_msg.ToString());

                testList.SaveToXmlFile(testListXmlFile);

                _msg.Length = 0;
                _msg.Append("LoadFromXmlFile Test: ");
                _msg.Append(testListXmlFile);
                Program._messageLog.WriteLine(_msg.ToString());

                testList2 = PFListEx<string>.LoadFromXmlFile(testListXmlFile);

                _msg.Length = 0;
                _msg.Append("testList2 ToString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(testList2.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                //save and read to database tests next
                testList.SaveToDatabase(connectionString, "TestprogListObjects_testList");

                testList3 = PFListEx<string>.LoadFromDatabase(connectionString, "TestprogListObjects_testList");

                _msg.Length = 0;
                _msg.Append("testList3 ToXmlString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(testList3.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("testList4 Sort Test: ");
                Program._messageLog.WriteLine(_msg.ToString());

                testList4.Add("First item");
                testList4.Add("ZFourth item");
                testList4.Add("Third item");
                testList4.Add("Second item");

                _msg.Length = 0;
                _msg.Append("Number of items in list4: ");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("List4 Before Sort: ");
                Program._messageLog.WriteLine(_msg.ToString());

                foreach (string s in testList4)
                {
                    _msg.Length = 0;
                    _msg.Append(s);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                testList4.Sort();

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("List4 After Sort: ");
                Program._messageLog.WriteLine(_msg.ToString());

                foreach (string s in testList4)
                {
                    _msg.Length = 0;
                    _msg.Append(s);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                Program._messageLog.WriteLine(Environment.NewLine);


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
                _msg.Append("... GenericListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }


        }//end GenericListTest



        public static void GenericKeyValueListTest()
        {
            PFKeyValueListEx<int, string> kvlist = new PFKeyValueListEx<int, string>();
            PFKeyValueListEx<int, string> kvlist4 = new PFKeyValueListEx<int, string>();
            PFKeyValueListEx<string, string> kvlistString = new PFKeyValueListEx<string, string>();
            string kvlistFilename = @"c:\temp\testkvlist.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenericKeyValueListTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("ListsDatabaseFile", string.Empty);
                if (configValue.Length > 0)
                    connectionString = configValue;
                else
                    connectionString = _listsDatabaseConnectionString;

                Program._messageLog.WriteLine(Environment.NewLine);

                kvlistString.Add(new pfKeyValuePair<string, string>("one", "First value"));
                kvlistString.Add(new pfKeyValuePair<string, string>("two", "Second value"));
                kvlistString.Add(new pfKeyValuePair<string, string>("three", "Third value"));
                kvlistString.Add(new pfKeyValuePair<string, string>("four", "Fourth value is just this"));
                kvlistString.Add(new pfKeyValuePair<string, string>("five", "First value is whatever comes here"));

                Program._messageLog.WriteLine("Before delete: " + kvlistString.Count.ToString());
                if (kvlistString.Exists("three"))
                {
                    kvlistString.Remove("three");
                }
                Program._messageLog.WriteLine("After delete:  " + kvlistString.Count.ToString());

                Program._messageLog.WriteLine(Environment.NewLine);

                kvlist.Add(new pfKeyValuePair<int, string>(1, "First value"));
                kvlist.Add(new pfKeyValuePair<int, string>(2, "Second value"));
                kvlist.Add(new pfKeyValuePair<int, string>(3, "Third value"));
                kvlist.Add(new pfKeyValuePair<int, string>(4, "Fourth value is just this"));
                kvlist.Add(new pfKeyValuePair<int, string>(5, "First value is whatever comes here"));

                kvlist.SaveToXmlFile(kvlistFilename);

                PFKeyValueListEx<int, string> kvlist2 = PFKeyValueListEx<int, string>.LoadFromXmlFile(kvlistFilename);

                kvlist2.SetToBOF();
                pfKeyValuePair<int, string> kvp = kvlist2.FirstItem;
                while (!kvlist2.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp = kvlist2.NextItem;
                }

                kvlist.SaveToDatabase(connectionString, "TestprogListObjects_kvlist");

                PFKeyValueListEx<int, string> kvlist3 = PFKeyValueListEx<int, string>.LoadFromDatabase(connectionString,"TestprogListObjects_kvlist");

                _msg.Length = 0;
                _msg.Append("kvlist3 ToXmlString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(kvlist3.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("kvlist4 Sort Test: ");
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                kvlist4.Add(new pfKeyValuePair<int, string>(5, "YFifth value"));
                kvlist4.Add(new pfKeyValuePair<int, string>(4, "XFourth value"));
                kvlist4.Add(new pfKeyValuePair<int, string>(3, "Third value"));
                kvlist4.Add(new pfKeyValuePair<int, string>(2, "Second value"));
                kvlist4.Add(new pfKeyValuePair<int, string>(1, "First value"));

                kvlist4.SetToBOF();
                pfKeyValuePair<int, string> kvp4 = kvlist4.FirstItem;
                while (!kvlist4.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp4.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp4.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp4 = kvlist4.NextItem;
                }

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("kvlist4 after sort: ");
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                kvlist4.SetToBOF();
                kvlist4.Sort(PFKeyValueListExComparers.CompareKeyValueListIntString);

                kvlist4.SetToBOF();
                kvp4 = kvlist4.FirstItem;
                while (!kvlist4.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp4.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp4.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp4 = kvlist4.NextItem;
                }

                Program._messageLog.WriteLine(Environment.NewLine);



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
                _msg.Append("\r\n... GenericKeyValueListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public static void GenericSortedKeyValueListTest()
        {
            PFKeyValueListExSorted<int, string> kvlist = new PFKeyValueListExSorted<int, string>();
            string kvlistFilename = @"c:\temp\testkvlistsorted.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenericSortedKeyValueListTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("ListsDatabaseFile", string.Empty);
                if (configValue.Length > 0)
                    connectionString = configValue;
                else
                    connectionString = _listsDatabaseConnectionString;

                kvlist.Add((int)5, "YFifth value");
                kvlist.Add((int)4, "XFourth value");
                kvlist.Add((int)3, "Third value");
                kvlist.Add((int)2, "Second value");
                kvlist.Add((int)1, "First value");


                kvlist.SaveToXmlFile(kvlistFilename);

                PFKeyValueListExSorted<int, string> kvlist2 = PFKeyValueListExSorted<int, string>.LoadFromXmlFile(kvlistFilename);

                kvlist2.SetToBOF();
                pfKeyValuePair<int, string> kvp = kvlist2.FirstItem;
                while (!kvlist2.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp = kvlist2.NextItem;
                }

                kvlist.SaveToDatabase(connectionString, "TestprogListObjects_kvlistsorted");

                PFKeyValueListExSorted<int, string> kvlist3 = PFKeyValueListExSorted<int, string>.LoadFromDatabase(connectionString, "TestprogListObjects_kvlistsorted");

                _msg.Length = 0;
                _msg.Append("kvlist3 ToXmlString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(kvlist3.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                string xmlString3 = kvlist3.ToXmlString();
                PFKeyValueListExSorted<int, string> kvlist4 = PFKeyValueListExSorted<int, string>.LoadFromXmlString(xmlString3);
                kvlist4.SetToBOF();
                kvp = kvlist4.FirstItem;
                while (!kvlist4.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp = kvlist4.NextItem;
                }

                System.Xml.XmlDocument xmlDoc = kvlist4.ToXmlDocument();
                xmlDoc.Save(@"c:\temp\kvlist4_xmldoc.xml");

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
                _msg.Append("\r\n... GenericSortedKeyValueListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void ListMergeTest()
        {
            PFListEx<string> list1 = new PFListEx<string>();
            PFListEx<string> list2 = new PFListEx<string>();
            PFListEx<string> list3 = new PFListEx<string>();


            try
            {
                _msg.Length = 0;
                _msg.Append("ListMergeTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                list1.Add("First");
                list1.Add("Second");
                list1.Add("Third");

                list2.Add("fourth");
                list2.Add("fifth");
                list2.Add("sixth");

                list3.Add("Seventh");
                list3.Add("Eighth");
                list3.Add("Ninth");
                list3.Add("Tenth");

                PFListEx<string>[] listArray = { list1, list2, list3 };
                PFListEx<PFListEx<string>> listOfLists = new PFListEx<PFListEx<string>>();
                listOfLists.Add(list1);
                listOfLists.Add(list2);
                listOfLists.Add(list3);

                PFListEx<string> concatListFromArray = PFListEx<string>.ConcatenateLists(listArray);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromArray.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                PFListEx<string> concatListFromList = PFListEx<string>.ConcatenateLists(listOfLists);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFListEx<string> mergedList = list1.Merge(list2);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFListEx<string>[2] { list1, list2 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFListEx<PFListEx<string>> { list2, list1 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
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
                _msg.Append("\r\n... ListMergeTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void KeyValueListMergeTest()
        {
            PFKeyValueListEx<int, string> list1 = new PFKeyValueListEx<int, string>();
            PFKeyValueListEx<int, string> list2 = new PFKeyValueListEx<int, string>();
            PFKeyValueListEx<int, string> list3 = new PFKeyValueListEx<int, string>();

            try
            {
                _msg.Length = 0;
                _msg.Append("KeyValueListMergeTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                list1.Add(new pfKeyValuePair<int, string>(1, "First"));
                list1.Add(new pfKeyValuePair<int, string>(2, "Second"));
                list1.Add(new pfKeyValuePair<int, string>(3, "Third"));

                list2.Add(new pfKeyValuePair<int, string>(4, "fourth"));
                list2.Add(new pfKeyValuePair<int, string>(5, "fifth"));
                list2.Add(new pfKeyValuePair<int, string>(6, "sixth"));

                list3.Add(new pfKeyValuePair<int, string>(7, "Seventh"));
                list3.Add(new pfKeyValuePair<int, string>(8, "Eighth"));
                list3.Add(new pfKeyValuePair<int, string>(9, "Ninth"));
                list3.Add(new pfKeyValuePair<int, string>(10, "Tenth"));

                PFKeyValueListEx<int, string>[] listArray = { list1, list2, list3 };
                PFListEx<PFKeyValueListEx<int, string>> listOfLists = new PFListEx<PFKeyValueListEx<int, string>>();
                listOfLists.Add(list1);
                listOfLists.Add(list2);
                listOfLists.Add(list3);

                PFKeyValueListEx<int, string> concatListFromArray = PFKeyValueListEx<int, string>.ConcatenateLists(listArray);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromArray.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                PFKeyValueListEx<int, string> concatListFromList = PFKeyValueListEx<int, string>.ConcatenateLists(listOfLists);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFKeyValueListEx<int, string> mergedList = list1.Merge(list2);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFKeyValueListEx<int, string>[2] { list1, list2 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFListEx<PFKeyValueListEx<int, string>> { list2, list1 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
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
                _msg.Append("\r\n... KeyValueListMergeTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 



    }//end class
}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Xml;
using PFCollectionsObjects;
using PFDataAccessObjects;

namespace TestprogCollections
{
    public class Tests
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;
        private string _listsDatabaseConnectionString = "data source ='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PFListEx.sdf") + "'";

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

        //tests
        public void GenericListTest()
        {
            PFList<string> testList = new PFList<string>();
            PFList<string> testList2 = new PFList<string>();
            PFList<string> testList3 = new PFList<string>();
            PFList<string> testList4 = new PFList<string>();
            string testListXmlFile = @"c:\temp\testlist.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;
            PFDBListProcessor<string> listProcessor = new PFDBListProcessor<string>();

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

                testList2 = PFList<string>.LoadFromXmlFile(testListXmlFile);

                _msg.Length = 0;
                _msg.Append("testList2 ToString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(testList2.ToString());
                Program._messageLog.WriteLine(_msg.ToString());

                //save and read to database tests next
                listProcessor.SaveToDatabase(testList, connectionString, "TestprogCollections_testList");

                testList3 = listProcessor.LoadFromDatabase(connectionString, "TestprogCollections_testList");

                if (testList3 != null)
                {
                    _msg.Length = 0;
                    _msg.Append("testList3 ToXmlString Test: ");
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Length = 0;
                    _msg.Append(testList3.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("+++ testlist3 retrieved. +++");
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("testList3 ToXmlString Test: ");
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Length = 0;
                    _msg.Append("*** TESTLIST3 IS NULL ***");
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
                listProcessor = null;
                _msg.Length = 0;
                _msg.Append("... GenericListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }


        }//end GenericListTest



        public void GenericKeyValueListTest()
        {
            PFKeyValueList<int, string> kvlist = new PFKeyValueList<int, string>();
            PFKeyValueList<int, string> kvlist3 = new PFKeyValueList<int, string>();
            string kvlistFilename = @"c:\temp\testkvlist.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;
            PFDBKeyValueListProcessor<int, string> kvlistProcessor = new PFDBKeyValueListProcessor<int, string>();
            
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

                kvlist.Add(new stKeyValuePair<int, string>(1, "First value"));
                kvlist.Add(new stKeyValuePair<int, string>(2, "Second value"));
                kvlist.Add(new stKeyValuePair<int, string>(3, "Third value"));
                kvlist.Add(new stKeyValuePair<int, string>(4, "Fourth value is just this"));
                kvlist.Add(new stKeyValuePair<int, string>(5, "First value is whatever comes here"));

                kvlist.SaveToXmlFile(kvlistFilename);

                PFKeyValueList<int, string> kvlist2 = PFKeyValueList<int, string>.LoadFromXmlFile(kvlistFilename);

                kvlist2.SetToBOF();
                stKeyValuePair<int, string> kvp = kvlist2.FirstItem;
                while(!kvlist2.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp = kvlist2.NextItem;
                }

                kvlistProcessor.SaveToDatabase(kvlist, connectionString, "TestprogCollections_kvlist");

                kvlist3 = kvlistProcessor.LoadFromDatabase(connectionString, "TestprogCollections_kvlist");

                if (kvlist3 != null)
                {
                    _msg.Length = 0;
                    _msg.Append("kvlist3 ToXmlString Test: ");
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Length = 0;
                    _msg.Append(kvlist3.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("+++ kvlist3 retrieved. +++");
                    Program._messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("kvlist3 ToXmlString Test: ");
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Length = 0;
                    _msg.Append("*** KVLIST3 IS NULL ***");
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
                kvlistProcessor = null;
                _msg.Length = 0;
                _msg.Append("\r\n... GenericKeyValueListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void ToXmlTest()
        {
            PFList<string> testList = new PFList<string>();

            try
            {
                _msg.Length = 0;
                _msg.Append("ToXmlTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                testList.Add("First item");
                testList.Add("Second item");
                testList.Add("Third item");

                string xmlString = testList.ToXmlString();
                XmlDocument xmlDoc = testList.ToXmlDocument();
                PFList<string> testList2 = PFList<string>.LoadFromXmlString(xmlString);

                _msg.Length = 0;
                _msg.Append("\r\n\r\n");
                _msg.Append(xmlString);
                _msg.Append("\r\n\r\n");
                _msg.Append(xmlDoc.OuterXml);
                _msg.Append("\r\n\r\n");
                _msg.Append(testList2.ToString());
                _msg.Append("\r\n\r\n");
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
                _msg.Append("\r\n... ToXmlTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void ListMergeTest()
        {
            PFList<string> list1 = new PFList<string>();
            PFList<string> list2 = new PFList<string>();
            PFList<string> list3 = new PFList<string>();


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

                PFList<string>[] listArray = {list1, list2, list3};
                PFList<PFList<string>> listOfLists = new PFList<PFList<string>>();
                listOfLists.Add(list1);
                listOfLists.Add(list2);
                listOfLists.Add(list3);

                PFList<string>concatListFromArray =  PFList<string>.ConcatenateLists(listArray);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromArray.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                
                PFList<string> concatListFromList = PFList<string>.ConcatenateLists(listOfLists);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFList<string> mergedList = list1.Merge(list2);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFList<string>[2] { list1, list2 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFList<PFList<string>> { list2, list1 });
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



        public void KeyValueListMergeTest()
        {
            PFKeyValueList<int, string> list1 = new PFKeyValueList<int, string>();
            PFKeyValueList<int, string> list2 = new PFKeyValueList<int, string>();
            PFKeyValueList<int, string> list3 = new PFKeyValueList<int, string>();

            try
            {
                _msg.Length = 0;
                _msg.Append("KeyValueListMergeTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                list1.Add(new stKeyValuePair<int, string>(1, "First"));
                list1.Add(new stKeyValuePair<int, string>(2, "Second"));
                list1.Add(new stKeyValuePair<int, string>(3, "Third"));

                list2.Add(new stKeyValuePair<int, string>(4, "fourth"));
                list2.Add(new stKeyValuePair<int, string>(5, "fifth"));
                list2.Add(new stKeyValuePair<int, string>(6, "sixth"));

                list3.Add(new stKeyValuePair<int, string>(7, "Seventh"));
                list3.Add(new stKeyValuePair<int, string>(8, "Eighth"));
                list3.Add(new stKeyValuePair<int, string>(9, "Ninth"));
                list3.Add(new stKeyValuePair<int, string>(10, "Tenth"));

                PFKeyValueList<int, string>[] listArray = { list1, list2, list3 };
                PFList<PFKeyValueList<int, string>> listOfLists = new PFList<PFKeyValueList<int, string>>();
                listOfLists.Add(list1);
                listOfLists.Add(list2);
                listOfLists.Add(list3);

                PFKeyValueList<int, string> concatListFromArray = PFKeyValueList<int, string>.ConcatenateLists(listArray);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromArray.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                PFKeyValueList<int, string> concatListFromList = PFKeyValueList<int, string>.ConcatenateLists(listOfLists);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFKeyValueList<int, string> mergedList = list1.Merge(list2);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFKeyValueList<int, string>[2] { list1, list2 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFList<PFKeyValueList<int, string>> { list2, list1 });
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


        public void GenericSortedKeyValueListTest()
        {
            PFKeyValueListSorted<int, string> kvlist = new PFKeyValueListSorted<int, string>();
            string kvlistFilename = @"c:\temp\testkvlistsorted.xml";
            string connectionString = string.Empty;
            string configValue = string.Empty;
            PFDBKeyValueListSortedProcessor<int, string> listProcessor = new PFDBKeyValueListSortedProcessor<int, string>();

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

                PFKeyValueListSorted<int, string> kvlist2 = PFKeyValueListSorted<int, string>.LoadFromXmlFile(kvlistFilename);

                kvlist2.SetToBOF();
                stKeyValuePair<int, string> kvp = kvlist2.FirstItem;
                while (!kvlist2.EOF)
                {
                    _msg.Length = 0;
                    _msg.Append(kvp.Key.ToString());
                    _msg.Append(", ");
                    _msg.Append(kvp.Value);
                    Program._messageLog.WriteLine(_msg.ToString());
                    kvp = kvlist2.NextItem;
                }

                listProcessor.SaveToDatabase(kvlist, connectionString, "TestprogCollectionsObjects_kvlistGenericSorted2");


                PFKeyValueListSorted<int, string> kvlist3 = listProcessor.LoadFromDatabase(connectionString, "TestprogCollectionsObjects_kvlistGenericSorted2");

                _msg.Length = 0;
                _msg.Append("kvlist3 ToXmlString Test: ");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(kvlist3.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                string xmlString3 = kvlist3.ToXmlString();
                PFKeyValueListSorted<int, string> kvlist4 = PFKeyValueListSorted<int, string>.LoadFromXmlString(xmlString3);
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

                string xmlFilePath = @"c:\temp\kvlist4_xmldoc.xml";
                System.Xml.XmlDocument xmlDoc = kvlist4.ToXmlDocument();
                if (File.Exists(xmlFilePath))
                    File.Delete(xmlFilePath);
                xmlDoc.Save(xmlFilePath);

                _msg.Length = 0;
                _msg.Append("XML document created: " + xmlFilePath);
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
                _msg.Append("\r\n... GenericSortedKeyValueListTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void SortedKeyValueListMergeTest()
        {
            PFKeyValueListSorted<int, string> list1 = new PFKeyValueListSorted<int, string>();
            PFKeyValueListSorted<int, string> list2 = new PFKeyValueListSorted<int, string>();
            PFKeyValueListSorted<int, string> list3 = new PFKeyValueListSorted<int, string>();

            try
            {
                _msg.Length = 0;
                _msg.Append("SortedKeyValueListMergeTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                list1.Add(1, "First");
                list1.Add(2, "Second");
                list1.Add(3, "Third");

                list2.Add(4, "fourth");
                list2.Add(5, "fifth");
                list2.Add(6, "sixth");

                list3.Add(7, "Seventh");
                list3.Add(8, "Eighth");
                list3.Add(9, "Ninth");
                list3.Add(10, "Tenth");

                PFKeyValueListSorted<int, string>[] listArray = { list1, list2, list3 };
                PFList<PFKeyValueListSorted<int, string>> listOfLists = new PFList<PFKeyValueListSorted<int, string>>();
                listOfLists.Add(list1);
                listOfLists.Add(list2);
                listOfLists.Add(list3);

                PFKeyValueListSorted<int, string> concatListFromArray = PFKeyValueListSorted<int, string>.ConcatenateLists(listArray);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromArray.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());


                PFKeyValueListSorted<int, string> concatListFromList = PFKeyValueListSorted<int, string>.ConcatenateLists(listOfLists);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Concatenated list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(concatListFromList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFKeyValueListSorted<int, string> mergedList = list1.Merge(list2);
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFKeyValueListSorted<int, string>[2] { list1, list2 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from Array");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                mergedList.Clear();
                mergedList = list3.Merge(new PFList<PFKeyValueListSorted<int, string>> { list2, list1 });
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Merged list from List");
                _msg.Append(Environment.NewLine);
                _msg.Append(mergedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                PFKeyValueListSorted<int, string> copyOfList = list2.Copy();
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Copy of list");
                _msg.Append(Environment.NewLine);
                _msg.Append(copyOfList.ToXmlString());
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
                _msg.Append("\r\n... SortedKeyValueListMergeTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomizeListsTest()
        {
            PFList<string> list1 = new PFList<string>();
            PFKeyValueList<int, string> kvlist2 = new PFKeyValueList<int, string>();
            PFKeyValueListSorted<int, string> sortlist3 = new PFKeyValueListSorted<int, string>();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomizeListsTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                list1.Add("First");
                list1.Add("Second");
                list1.Add("Third");
                list1.Add("fourth");
                list1.Add("fifth");
                list1.Add("sixth");
                list1.Add("Seventh");
                list1.Add("Eighth");
                list1.Add("Ninth");
                list1.Add("Tenth");

                PFList<string> randomizedList = list1.Randomize();

                _msg.Length = 0;
                _msg.Append("randomizedList: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(randomizedList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                kvlist2.Add(new stKeyValuePair<int, string>(1, "First"));
                kvlist2.Add(new stKeyValuePair<int, string>(2, "Second"));
                kvlist2.Add(new stKeyValuePair<int, string>(3, "Third"));
                kvlist2.Add(new stKeyValuePair<int, string>(4, "fourth"));
                kvlist2.Add(new stKeyValuePair<int, string>(5, "fifth"));
                kvlist2.Add(new stKeyValuePair<int, string>(6, "sixth"));
                kvlist2.Add(new stKeyValuePair<int, string>(7, "Seventh"));
                kvlist2.Add(new stKeyValuePair<int, string>(8, "Eighth"));
                kvlist2.Add(new stKeyValuePair<int, string>(9, "Ninth"));
                kvlist2.Add(new stKeyValuePair<int, string>(10, "Tenth"));

                PFKeyValueList<int, string> randomizedKvList = kvlist2.Randomize();

                _msg.Length = 0;
                _msg.Append("randomizedKvList: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(randomizedKvList.ToXmlString());
                Program._messageLog.WriteLine(_msg.ToString());

                sortlist3.Add(1, "First");
                sortlist3.Add(2, "Second");
                sortlist3.Add(3, "Third");
                sortlist3.Add(4, "fourth");
                sortlist3.Add(5, "fifth");
                sortlist3.Add(6, "sixth");
                sortlist3.Add(7, "Seventh");
                sortlist3.Add(8, "Eighth");
                sortlist3.Add(9, "Ninth");
                sortlist3.Add(10, "Tenth");

                PFKeyValueList<int, string> randomizedSortList = sortlist3.Randomize();

                _msg.Length = 0;
                _msg.Append("randomizedSortList: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(randomizedSortList.ToXmlString());
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
                _msg.Append("\r\n... RandomizeListsTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace

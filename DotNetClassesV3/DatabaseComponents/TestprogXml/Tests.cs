#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Xml;
using PFXmlObjects;
using PFTextFiles;

namespace TestprogXml
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;
        private static PFXmlDocument _xmlDoc = new PFXmlDocument("TestRoot");

        //constructor

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
        public static void ZeroDivideTest()
        {
            int x = 1;
            int y = 0;
            int z;

            z = x / y;
        }

        public static void LoadBadXMLTest()
        {
            string xml = "<root></notroot>";
            PFXmlDocument xmlDoc = new PFXmlDocument();
            try
            {
                xmlDoc.LoadFromString(xml);
            }
            catch (System.Exception ex)
            {
                Program._messageLog.WriteLine(AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception("LoadBadXMLTest reports an error.\n", ex);
            }
        }

        public static void ValidateXmlTest()
        {
            bool isValid = false;
            XMLValidator xmlValidator = new XMLValidator();

            isValid = xmlValidator.Validate(@"c:\temp\TestDataTable.xml", @"c:\temp\TestDataTable.xsd");
            if (isValid)
                Program._messageLog.WriteLine("OK: Xml file has valid format.");
            else
            {
                Program._messageLog.WriteLine("ERROR: Xml file does not have valid format.");
                Program._messageLog.WriteLine(xmlValidator.ValidationErrors);
            }
        }

        public static void CreateXMLDoc(string _filename, string _rootNode)
        {
            _xmlDoc = new PFXmlDocument(_rootNode);
            XmlDeclaration xmldec = _xmlDoc.XmlDoc.CreateXmlDeclaration("1.0", "utf-16", null);
            //XmlElement root = _xmlDoc.XmlDoc.DocumentElement;
            //_xmlDoc.XmlDoc.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //_xmlDoc.XmlDoc.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            //_xmlDoc.XmlDoc.InsertBefore(xmldec, root);
            _xmlDoc.SaveToFile(_filename);
            _msg.Length = 0;
            if (File.Exists(_filename))
            {
                _msg.Append("File created: ");
                _msg.Append(_filename);
            }
            else
            {
                _msg.Append("File create failed for : ");
                _msg.Append(_filename);
            }
            Program._messageLog.WriteLine(_msg.ToString());
        }

        public static void OpenXMLDoc(string _filename)
        {
            _xmlDoc.LoadFromFile(_filename);

            Program._messageLog.WriteLine("OuterXml:\r\n" + _xmlDoc.OuterXml + "\r\n");
            //Program._messageLog.WriteLine(_xmlDoc.InnerXml);
            //Program._messageLog.WriteLine(_xmlDoc.InnerText);
            Program._messageLog.WriteLine("ToString:\r\n" + _xmlDoc.ToString() + "\r\n");
            
        }

        public static void RunTest_Memory(string _filename, string _rootNode)
        {
            _xmlDoc = new PFXmlDocument(_rootNode);

            XmlElement xmlElement;
            XmlElement saveXmlElement;
            xmlElement = _xmlDoc.AddNewElement(_xmlDoc.DocumentRootNode, "ProcessingDate", DateTime.Now.ToString());
            saveXmlElement = _xmlDoc.AddNewElement(_xmlDoc.DocumentRootNode, "IsEnabled", "True");
            xmlElement = _xmlDoc.AddNewElement(saveXmlElement, "node1", "text1");
            xmlElement = _xmlDoc.AddNewElement(saveXmlElement, "node2", "text2");
            _xmlDoc.AddNewAttribute(xmlElement, "attr1", "val1");
            xmlElement = _xmlDoc.AddNewElement(saveXmlElement, "node3", "text3");
            _xmlDoc.AddNewAttribute(xmlElement, "attr3a", "val3a");
            _xmlDoc.AddNewAttribute(xmlElement, "attr3b", "val3b");
            xmlElement = _xmlDoc.AddNewElement(_xmlDoc.DocumentRootNode, "FormBackground", "Red");
            _xmlDoc.SaveToFile(_filename);

            PFXmlDocument xmlDoc2 = new PFXmlDocument();
            xmlDoc2.LoadFromFile(_filename);
            Program._messageLog.WriteLine(xmlDoc2.OuterXml);
            Program._messageLog.WriteLine("\r\n" + xmlDoc2.InnerText);
            Program._messageLog.WriteLine("\r\n" + xmlDoc2.InnerXml);
        }

        public static void AddNodes(MainForm frm)
        {

            bool err = false;
            XmlElement xmlElement;
            StringBuilder nodePath = new StringBuilder();

            Program._messageLog.Clear();

            if (frm.txtNodePath.Text.Length == 0)
            {
                Program._messageLog.WriteLine("Node path must be specified.");
                err = true;
            }
            if (err)
                return;

            if (frm.txtNodePath.Text.Length > 0)
            {
                if (frm.txtNodeTag.Text.Length > 0)
                {
                    nodePath.Length = 0;
                    nodePath.Append(frm.txtRootNode.Text);
                    nodePath.Append("/");
                    nodePath.Append(frm.txtNodePath.Text);
                    try
                    {
                        xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                        if (xmlElement == null)
                        {
                            xmlElement = _xmlDoc.FindElement(frm.txtRootNode.Text);
                            _xmlDoc.AddNewElement(xmlElement, frm.txtNodePath.Text, string.Empty);
                            xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                        }
                        _xmlDoc.AddNewElement((XmlNode)xmlElement, frm.txtNodeTag.Text, frm.txtNodeInnerText.Text);
                    }
                    catch (Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Attempt to add element failed.\n");
                        _msg.Append("Node tag: ");
                        _msg.Append(frm.txtNodeTag.Text);
                        _msg.Append("\n\n");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                    }
                    _xmlDoc.SaveToFile(frm.txtFilename.Text);
                }

                if (frm.txtAttribute.Text.Length > 0)
                {
                    nodePath.Length = 0;
                    nodePath.Append(frm.txtRootNode.Text);
                    nodePath.Append("/");
                    nodePath.Append(frm.txtNodePath.Text);
                    if (frm.txtNodeTag.Text.Length > 0)
                    {
                        nodePath.Append("/");
                        nodePath.Append(frm.txtNodeTag.Text);
                    }
                    try
                    {
                        xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                        _xmlDoc.AddNewAttribute(xmlElement, frm.txtAttribute.Text, frm.txtAttributeValue.Text);
                    }
                    catch (Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Attempt to add attribute failed.\n");
                        _msg.Append("Node tag: ");
                        _msg.Append(frm.txtNodeTag.Text);
                        _msg.Append("\n\n");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                    }
                    _xmlDoc.SaveToFile(frm.txtFilename.Text);
                }

            }

            OutputContentsOfFile(frm.txtFilename.Text);

        }


        public static void DeleteNode(MainForm frm)
        {
            bool err = false;
            XmlElement xmlElement;
            StringBuilder nodePath = new StringBuilder();

            Program._messageLog.Clear();

            if (frm.txtNodePath.Text.Length == 0)
            {
                Program._messageLog.WriteLine("Node path must be specified.");
                err = true;
            }
            if (err)
                return;

            nodePath.Length = 0;
            nodePath.Append(frm.txtRootNode.Text);
            nodePath.Append("/");
            nodePath.Append(frm.txtNodePath.Text);
            try
            {
                xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                _xmlDoc.DeleteNode(xmlElement);
                _xmlDoc.SaveToFile(frm.txtFilename.Text);
                OutputContentsOfFile(frm.txtFilename.Text);
            }
            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to delete node failed.\n");
                _msg.Append("Node tag: ");
                _msg.Append(frm.txtNodeTag.Text);
                _msg.Append("\n\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }


        }


        public static void DeleteChildNodes(MainForm frm)
        {
            bool err = false;
            XmlElement xmlElement;
            StringBuilder nodePath = new StringBuilder();

            Program._messageLog.Clear();

            if (frm.txtNodePath.Text.Length == 0)
            {
                Program._messageLog.WriteLine("Node path must be specified.");
                err = true;
            }
            if (err)
                return;

            nodePath.Length = 0;
            nodePath.Append(frm.txtRootNode.Text);
            nodePath.Append("/");
            nodePath.Append(frm.txtNodePath.Text);
            try
            {
                xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                _xmlDoc.DeleteChildNodes(xmlElement);
                _xmlDoc.SaveToFile(frm.txtFilename.Text);
                OutputContentsOfFile(frm.txtFilename.Text);
            }
            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to delete child nodes failed.\n");
                _msg.Append("Node tag: ");
                _msg.Append(frm.txtNodeTag.Text);
                _msg.Append("\n\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }

        }

        public static void DeleteAllNodes(MainForm frm)
        {
            Program._messageLog.Clear();

            try
            {
                _xmlDoc.DeleteAllNodesInDocument();
                _xmlDoc.SaveToFile(frm.txtFilename.Text);
                OutputContentsOfFile(frm.txtFilename.Text);
            }
            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to delete all nodes in document failed.\n");
                _msg.Append("\n\n");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
                Program._messageLog.WriteLine(_msg.ToString());
            }
        }

        public static void UpdateNodes(MainForm frm)
        {
            bool err = false;
            XmlElement xmlElement;
            StringBuilder nodePath = new StringBuilder();

            Program._messageLog.Clear();

            if (frm.txtNodePath.Text.Length == 0)
            {
                Program._messageLog.WriteLine("Node path must be specified.");
                err = true;
            }
            if (err)
                return;

            if (frm.txtNodePath.Text.Length > 0)
            {
                if (frm.txtNodeTag.Text.Length > 0)
                {
                    nodePath.Length = 0;
                    nodePath.Append(frm.txtRootNode.Text);
                    nodePath.Append("/");
                    nodePath.Append(frm.txtNodePath.Text);
                    nodePath.Append("/");
                    nodePath.Append(frm.txtNodeTag.Text);
                    try
                    {
                        xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                        _xmlDoc.UpdateElement(xmlElement, frm.txtNodeInnerText.Text);
                    }
                    catch (Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Attempt to update element failed.\n");
                        _msg.Append("Node tag: ");
                        _msg.Append(frm.txtNodeTag.Text);
                        _msg.Append("\n\n");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                    }
                    _xmlDoc.SaveToFile(frm.txtFilename.Text);
                }

                if (frm.txtAttribute.Text.Length > 0)
                {
                    nodePath.Length = 0;
                    nodePath.Append(frm.txtRootNode.Text);
                    nodePath.Append("/");
                    nodePath.Append(frm.txtNodePath.Text);
                    if (frm.txtNodeTag.Text.Length > 0)
                    {
                        nodePath.Append("/");
                        nodePath.Append(frm.txtNodeTag.Text);
                    }
                    try
                    {
                        xmlElement = _xmlDoc.FindElement(nodePath.ToString());
                        _xmlDoc.UpdateAttribute(xmlElement, frm.txtAttribute.Text, frm.txtAttributeValue.Text);
                    }
                    catch (Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Attempt to update attribute failed.\n");
                        _msg.Append("Node tag: ");
                        _msg.Append(frm.txtNodeTag.Text);
                        _msg.Append("\n\n");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                    }
                    _xmlDoc.SaveToFile(frm.txtFilename.Text);
                }

            }

            OutputContentsOfFile(frm.txtFilename.Text);


        }

        public static void FindNode(MainForm frm)
        {
            XmlElement xmlElement;

            Program._messageLog.Clear();
            try
            {
                //oXMLElement = moXMLFile.FindElement("/AppSettings/Mike01/add[@key='" + this.txtNodePath.Text + "']");
                if (frm.txtAttribute.Text.Length > 0)
                {
                    xmlElement = _xmlDoc.FindElement("/" + frm.txtRootNode.Text + "/" + frm.txtNodePath.Text + "[@" + frm.txtAttribute.Text + " ='" + frm.txtAttributeValue.Text + "']");
                }
                else
                {
                    xmlElement = _xmlDoc.FindElement("/" + frm.txtRootNode.Text + "/" + frm.txtNodePath.Text);
                }

                if (xmlElement != null)
                {
                    Program._messageLog.WriteLine("Found " + frm.txtNodePath.Text + ".");
                }
                else
                {
                    Program._messageLog.WriteLine("Unable to find " + frm.txtNodePath.Text + ".");
                }
            }
            catch (System.Exception ex)
            {
                Program._messageLog.WriteLine("");
                Program._messageLog.WriteLine("ERROR: " + ex.Message);
            }

            OutputContentsOfFile(frm.txtFilename.Text);
        }


        public static void FindNodes_Memory()
        {
            Program._messageLog.Clear();

            StringBuilder nodes = new StringBuilder();
            nodes.Append("Parent Node: ");
            nodes.Append(_xmlDoc.DocumentRootNodeName);
            nodes.Append("\n");
            Program._messageLog.WriteLine(nodes.ToString());

            //XmlNodeList xmlNodeList = _xmlDoc.XmlDoc.ChildNodes;
            XmlNodeList xmlNodeList = _xmlDoc.ChildNodes;
            OutputNodeList(xmlNodeList);

            nodes.Length = 0;
            nodes.Append("\n");
            nodes.Append("Document element: ");
            nodes.Append(_xmlDoc.XmlDoc.DocumentElement.Name);
            nodes.Append("\n");
            Program._messageLog.WriteLine(nodes.ToString());

            xmlNodeList = _xmlDoc.XmlDoc.DocumentElement.ChildNodes;
            OutputNodeList(xmlNodeList);
        }

        private static void OutputNodeList(XmlNodeList poXmlNodeList)
        {
            StringBuilder sNodes = new StringBuilder();
            int nInx = 0;
            int nMaxInx = poXmlNodeList.Count - 1;
            for (nInx = 0; nInx <= nMaxInx; nInx++)
            {
                XmlNode oNode = poXmlNodeList[nInx];
                sNodes.Append("Node # ");
                sNodes.Append(nInx.ToString());
                sNodes.Append(" Name: ");
                sNodes.Append(oNode.Name);
                sNodes.Append(" Outer XML: ");
                sNodes.Append(oNode.OuterXml);
                sNodes.Append("\n");
            }
            Program._messageLog.WriteLine(sNodes.ToString());
        }


    
         private static void OutputContentsOfFile(string filename)
        {
            PFTextFile file = new PFTextFile(filename, PFFileOpenOperation.OpenFileToRead);
            Program._messageLog.WriteLine(file.ReadAllText());
            if (file.FileIsOpen)
                file.CloseFile();
            file = null;
        }



    }//end class
}//end namespace
#pragma warning restore 1591

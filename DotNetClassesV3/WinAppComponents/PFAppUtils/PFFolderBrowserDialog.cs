//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace PFAppUtils
{
    /// <summary>
    /// Class to manage display of Windows folder browser dialog.
    /// </summary>
    public class PFFolderBrowserDialog
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private varialbles for properties
        private FolderBrowserDialog _diag = new FolderBrowserDialog();
        private string _initialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _selectedFolderPath = string.Empty;
        private bool _showNewFolderButton = true;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFolderBrowserDialog()
        {
            ;
        }

        //properties
        /// <summary>
        /// Read-only property. Returns the FolderBrowserDialog object used by this instance. This gives you access to all the public properties and methods of that object.
        /// </summary>
        public FolderBrowserDialog Diag
        {
            get
            {
                return _diag;
            }
        }

        /// <summary>
        /// Specifies the start folder for the dialog.
        /// </summary>
        public string InitialFolderPath
        {
            get
            {
                return _initialFolderPath;
            }
            set
            {
                _initialFolderPath = value;
            }
        }

        /// <summary>
        /// Returns path to selected folder.
        /// </summary>
        public string SelectedFolderPath
        {
            get
            {
                return _selectedFolderPath;
            }
            set
            {
                _selectedFolderPath = value;
            }
        }

        /// <summary>
        /// Specifies whether or not dialog shows button that allows creation of new folders.
        /// </summary>
        public bool ShowNewFolderButton
        {
            get
            {
                return _showNewFolderButton;
            }
            set
            {
                _showNewFolderButton = value;
            }
        }


        //methods

        /// <summary>
        /// Displays the folder browser dialog window.
        /// </summary>
        /// <returns>DialogResult value.</returns>
        public DialogResult ShowFolderBrowserDialog()
        {
            DialogResult res = DialogResult.None;

            string folderPath = string.Empty;

            if (this.InitialFolderPath .Length > 0)
                folderPath = this.InitialFolderPath;
            else
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _diag.ShowNewFolderButton = this.ShowNewFolderButton;
            //_diag.RootFolder = 
            _diag.SelectedPath = folderPath;
            res = _diag.ShowDialog();
            this.SelectedFolderPath = string.Empty;
            if (res != DialogResult.Cancel)
            {
                folderPath = _diag.SelectedPath;
                _str.Length = 0;
                _str.Append(folderPath);
                if (folderPath.EndsWith(@"\") == false)
                    _str.Append(@"\");
                this.SelectedFolderPath = folderPath;
                this.InitialFolderPath = folderPath;
            }


            return res;
        }



        /// <summary>
        /// Saves the the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFFolderBrowserDialog));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFInitClassExtended.</returns>
        public static PFFolderBrowserDialog LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFFolderBrowserDialog));
            TextReader textReader = new StreamReader(filePath);
            PFFolderBrowserDialog diag;
            diag = (PFFolderBrowserDialog)deserializer.Deserialize(textReader);
            textReader.Close();
            return diag;
        }

        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFFolderBrowserDialog));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }

        /// <summary>
        /// Overrides ToString function to output values of all public properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("  ");
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");
            }

            return data.ToString();
        }



    }//end class
}//end namespace

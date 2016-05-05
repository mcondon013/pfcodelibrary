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
    /// Class for managing a SaveFileDialog session.
    /// </summary>
    public class PFSaveFileDialog
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties
        private SaveFileDialog _diag = new SaveFileDialog();
        private string _fileName = string.Empty;
        private string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _filter = "Text Files|*.txt|All Files|*.*";
        private int _filterIndex = 1;    //index is 1-based
        private bool _showCreatePrompt = true;
        private bool _showOverwritePrompt = true;

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFSaveFileDialog()
        {
            ;
        }

        //properties
        /// <summary>
        /// Read-only property. Returned the SaveFileDialog object used by this instance. This gives you access to all the public properties and methods of that object.
        /// </summary>
        public SaveFileDialog Diag
        {
            get
            {
                return _diag;
            }
        }

        /// <summary>
        /// Sets initial file name to search for. Returns file name specified in the dialog.
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        /// <summary>
        /// Specifies the initial directory to show in the dialog. Will be updated to reference last directory looked at during dialog session.
        /// </summary>
        public string InitialDirectory
        {
            get
            {
                return _initialDirectory;
            }
            set
            {
                _initialDirectory = value;
            }
        }

        /// <summary>
        /// Specifies filter to use when selecting files to display.
        /// </summary>
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
            }
        }

        /// <summary>
        /// Get the index of the filter that is currently being used. 
        /// Set the index of the filter to be used.
        /// </summary>
        public int FilterIndex
        {
            get
            {
                return _filterIndex;
            }
            set
            {
                _filterIndex = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show caller a prompt to verify that new file will be created.
        /// </summary>
        public bool ShowCreatePrompt
        {
            get
            {
                return _showCreatePrompt;
            }
            set
            {
                _showCreatePrompt = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show caller a prompt to alert that selected file already exists and to ask if that file should be overwritten.
        /// </summary>
        public bool ShowOverwritePrompt
        {
            get
            {
                return _showOverwritePrompt;
            }
            set
            {
                _showOverwritePrompt = value;
            }
        }


        //methods

        /// <summary>
        /// Used to select file name for a save operation.
        /// </summary>
        /// <returns>Dialog result.</returns>
        public DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            _diag.InitialDirectory = this.InitialDirectory;
            _diag.FileName = this.FileName;
            _diag.Filter = this.Filter;
            _diag.FilterIndex = this.FilterIndex;
            _diag.CreatePrompt = this.ShowCreatePrompt;
            _diag.OverwritePrompt = this.ShowOverwritePrompt;
            res = _diag.ShowDialog();
            this.FileName = string.Empty;
            if (res == DialogResult.OK)
            {
                this.InitialDirectory = Path.GetDirectoryName(_diag.FileName);
                this.FileName =Diag.FileName;
                this.FilterIndex = _diag.FilterIndex;
            }
            return res;
        }



        /// <summary>
        /// Saves the the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFSaveFileDialog));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFInitClassExtended.</returns>
        public static PFSaveFileDialog LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFSaveFileDialog));
            TextReader textReader = new StreamReader(filePath);
            PFSaveFileDialog diag;
            diag = (PFSaveFileDialog)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFSaveFileDialog));
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
        /// Outputs public property values to a string.
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

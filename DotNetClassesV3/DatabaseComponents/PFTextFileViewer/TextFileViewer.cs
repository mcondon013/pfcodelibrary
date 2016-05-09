using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PFTextFileViewer
{
    /// <summary>
    /// Routines for displaying a text file on a TextFileViewerForm.
    /// </summary>
    public class TextFileViewer
    {
        // Fields
        private bool _retainFocus = true;
        private TextFileViewerForm _textFileViewerForm = new TextFileViewerForm();
        private string _caption = "Text File Viewer";
        private string _font = "Microsoft Sans Serif";
        private float _fontSize = (float)8.25;

        // Methods
        /// <summary>
        /// Constructor
        /// </summary>
        public TextFileViewer()
        {
            TextFileViewerForm textViewerForm = this._textFileViewerForm;
            textViewerForm.WindowState = FormWindowState.Normal;
            textViewerForm.Text = this._caption;
            textViewerForm.txtViewer.Text = "";
            textViewerForm.txtViewer.Font = new System.Drawing.Font(_font, _fontSize);
            textViewerForm.Hide();
            textViewerForm = null;
        }

        /// <summary>
        /// Erases all text on the form.
        /// </summary>
        public void Clear()
        {
            this._textFileViewerForm.txtViewer.Clear();
        }

        /// <summary>
        /// Closes the viewer.
        /// </summary>
        public void CloseWindow()
        {
            this._textFileViewerForm.Close();
        }

        /// <summary>
        /// Set application focus to the viewer form.
        /// </summary>
        public void Focus()
        {
            this._textFileViewerForm.Focus();
        }

        /// <summary>
        /// Hides the viewer.
        /// </summary>
        public void HideWindow()
        {
            this._textFileViewerForm.Hide();
        }

        /// <summary>
        /// Reads a text file into the viewer's text area.
        /// </summary>
        /// <param name="filename">Path to the file to be loaded.</param>
        public void LoadFile(string filename)
        {
            this._textFileViewerForm.txtViewer.LoadFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Saves contents of vieweer to specified file.
        /// </summary>
        /// <param name="filename">Name of save file.</param>
        public void SaveFile(string filename)
        {
            this._textFileViewerForm.txtViewer.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Renders the viewer visible.
        /// </summary>
        public void ShowWindow()
        {
            this._textFileViewerForm.txtViewer.Font = new System.Drawing.Font(_font, _fontSize);
            this._textFileViewerForm.Show();
        }

        /// <summary>
        /// Shows viewer as a dialog window.
        /// </summary>
        public void ShowDialog()
        {
            this._textFileViewerForm.txtViewer.Font = new System.Drawing.Font(_font, _fontSize);
            this._textFileViewerForm.ShowDialog();
        }

        /// <summary>
        /// Writes specified message text to the viewer window.
        /// </summary>
        /// <param name="message">Text to display in the viewer.</param>
        /// <remakrs>Text is appended to the end of any existing text in the viewer.</remakrs>
        public void WriteLine(string message)
        {
            string text = message.Replace("\0", " ");
            this._textFileViewerForm.txtViewer.Select(this._textFileViewerForm.txtViewer.Text.Length, 1);
            this._textFileViewerForm.txtViewer.SelectedText = text + "\r\n";
            if (this.RetainFocus)
            {
                Application.DoEvents();
                this._textFileViewerForm.Focus();
            }
        }

        // Properties
        /// <summary>
        /// Set to True to allow calling application to erase contents of the viewer text window.
        /// </summary>
        public bool AllowFileErase
        {
            get
            {
                return this._textFileViewerForm.AllowFileErase;
            }
            set
            {
                this._textFileViewerForm.AllowFileErase = value;
            }
        }

        /// <summary>
        /// Specifies the caption for the viewer window.
        /// </summary>
        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                this._caption = value;
                this._textFileViewerForm.Text = value;
            }
        }

        /// <summary>
        /// Returns the form object for the viewer.
        /// </summary>
        /// <remarks>Allows the calling application to manipulate all public windows forms properties and methods.</remarks>
        public Form Form
        {
            get
            {
                return this._textFileViewerForm;
            }
        }

        /// <summary>
        /// Returns true if viewer is current visible.
        /// </summary>
        public bool FormIsVisible
        {
            get
            {
                return this._textFileViewerForm.Visible;
            }
        }

        /// <summary>
        /// Allows viewer to retain focus.
        /// </summary>
        public bool RetainFocus
        {
            get
            {
                return this._retainFocus;
            }
            set
            {
                this._retainFocus = value;
            }
        }

        /// <summary>
        /// Used to reset the font used by the viewe.
        /// </summary>
        public string Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        /// <summary>
        /// Used to reset the display font size for the viewer.
        /// </summary>
        public float FontSize
        {
            get
            {
                return this._fontSize;
            }
            set
            {
                this._fontSize = value;
            }
        }


    }//end class

}//end namespace

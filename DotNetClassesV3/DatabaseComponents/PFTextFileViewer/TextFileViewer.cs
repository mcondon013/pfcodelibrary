using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PFTextFileViewer
{
    public class TextFileViewer
    {
        // Fields
        private bool _retainFocus = true;
        private TextFileViewerForm _textFileViewerForm = new TextFileViewerForm();
        private string _caption = "Text File Viewer";
        private string _font = "Microsoft Sans Serif";
        private float _fontSize = (float)8.25;

        // Methods
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

        public void Clear()
        {
            this._textFileViewerForm.txtViewer.Clear();
        }

        public void CloseWindow()
        {
            this._textFileViewerForm.Close();
        }

        public void Focus()
        {
            this._textFileViewerForm.Focus();
        }

        public void HideWindow()
        {
            this._textFileViewerForm.Hide();
        }

        public void LoadFile(string filename)
        {
            this._textFileViewerForm.txtViewer.LoadFile(filename, RichTextBoxStreamType.PlainText);
        }

        public void SaveFile(string filename)
        {
            this._textFileViewerForm.txtViewer.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        public void ShowWindow()
        {
            this._textFileViewerForm.txtViewer.Font = new System.Drawing.Font(_font, _fontSize);
            this._textFileViewerForm.Show();
        }

        public void ShowDialog()
        {
            this._textFileViewerForm.txtViewer.Font = new System.Drawing.Font(_font, _fontSize);
            this._textFileViewerForm.ShowDialog();
        }

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

        public Form Form
        {
            get
            {
                return this._textFileViewerForm;
            }
        }

        public bool FormIsVisible
        {
            get
            {
                return this._textFileViewerForm.Visible;
            }
        }

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

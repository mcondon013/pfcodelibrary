using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.VisualBasic;

namespace PFTextFileViewer
{
    public partial class TextFileViewerForm : Form
    {
        private int _findStartPos = 0;
        private int _findEndPos = 0;
        private int _prevSelStart = 0;
        private string _stringToFind = string.Empty;
        private string _saveToFilename = string.Empty;
        private string _saveToFolderPath = Application.StartupPath;

        private TextPrinter _textPrinter = new TextPrinter();

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool DeleteMenu(int hMenu, int uPosition, int uFlags);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetSystemMenu(int hWnd, bool bRevert);
        private const int MF_BYPOSITION = 0x400;

        public TextFileViewerForm()
        {
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
            this.txtViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlBox = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            RichTextBox txtViewerCtl = this.txtViewer;
            if (txtViewerCtl.SelectedText.Length > 0)
            {
                Clipboard.SetDataObject(txtViewerCtl.SelectedText, true);
            }
            txtViewerCtl = null;
        }

        private void mnuEditFind_Click(object sender, EventArgs e)
        {
            this.EditFind(false);
        }

        private void mnuEditFindNext_Click(object sender, EventArgs e)
        {
            this.EditFind(true);
        }

        private void mnuEditSelectAll_Click(object sender, EventArgs e)
        {
            RichTextBox txtViewerCtl = this.txtViewer;
            txtViewerCtl.SelectionStart = 0;
            txtViewerCtl.SelectionLength = txtViewerCtl.Text.Length + 2;
            txtViewerCtl = null;
        }

        private void mnuFileErase_Click(object sender, EventArgs e)
        {
            string caption = this.Text;
            string text = "Press OK to erase contents of text file viewer.\r\n\r\nPress Cancel to exit without erasing text file viewer.";
            if (MessageBox.Show(text, caption, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.txtViewer.Clear();
            }
            this.txtViewer.Focus();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuFilePageSettings_Click(object sender, EventArgs e)
        {
            this.ShowPageSettings();
        }

        private void mnuFilePrint_Click(object sender, EventArgs e)
        {
            this.PrintReport();
        }

        private void mnuFilePrintPreview_Click(object sender, EventArgs e)
        {
            this.ShowPrintPreview();
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            this.FileSave();
        }

        private void mnuFormatFont_Click(object sender, EventArgs e)
        {
            this.SpecifyFont();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            string caption = "About ...";
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName name = executingAssembly.GetName();
            AssemblyProductAttribute attribute = (AssemblyProductAttribute)executingAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            MessageBox.Show(attribute.Product.ToString() + "\r\n\r\nPFTextFileViewer\r\n\r\n" + name.Version.ToString() + "\r\n", caption, MessageBoxButtons.OK);
            this.txtViewer.Focus();
        }

        private void EditFind(bool findNext)
        {
            string stringToFind = this._stringToFind;
            this._findEndPos = this.txtViewer.Text.Length;
            if (findNext == false || stringToFind == string.Empty)
            {
                this._findStartPos = this.txtViewer.SelectionStart;
                string prompt = "Find what: ";
                this._stringToFind = Interaction.InputBox(prompt, "Find", this._stringToFind, -1, -1);
            }
            else if (this.txtViewer.SelectionStart != this._prevSelStart)
            {
                this._findStartPos = this.txtViewer.SelectionStart;
            }
            if (this._stringToFind.Length > 0)
            {
                int num = this.txtViewer.Find(this._stringToFind, this._findStartPos, this._findEndPos, RichTextBoxFinds.None);
                if (num == -1)
                {
                    MessageBox.Show("String not found.", "Find", MessageBoxButtons.OK);
                    this._prevSelStart = this.txtViewer.SelectionStart;
                    this._findStartPos = 0;
                    this.txtViewer.Focus();
                }
                else
                {
                    this._findStartPos = num + 1;
                    this._prevSelStart = num;
                    if (this._findStartPos > this._findEndPos)
                    {
                        this._findStartPos = 0;
                    }
                }
            }
            else
            {
                this._stringToFind = stringToFind;
            }
        }

        private void FileSave()
        {
            if (this.SpecifyFilenameToSave())
            {
                this.txtViewer.SaveFile(this._saveToFilename, RichTextBoxStreamType.PlainText);
            }
            this.txtViewer.Focus();
        }

        public void FileSave(bool pbRunningInBatchMode)
        {
            this.Cursor = Cursors.WaitCursor;
            string fileName = this.GetFileName();
            if (fileName.Length > 0)
            {
                StreamWriter writer = new StreamWriter(fileName, false);
                string text = this.txtViewer.Text.Replace("\n", "\r\n");
                writer.Write(text);
                writer.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private string GetDefaultReportDirectoryName()
        {
            string path = "";
            path = Path.GetDirectoryName(Application.ExecutablePath) + @"\Logs";
            if (!Directory.Exists(path))
            {
                path = Path.GetDirectoryName(Application.ExecutablePath);
            }
            return path;
        }

        private string GetFileName()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            string defaultReportDirectoryName = this.GetDefaultReportDirectoryName();
            SaveFileDialog dialog2 = dialog;
            dialog2.Title = "Save " + this.Text;
            dialog2.InitialDirectory = defaultReportDirectoryName;
            dialog2.FileName = "MessageLog.txt";
            dialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog2.RestoreDirectory = true;
            if (dialog2.ShowDialog() == DialogResult.OK)
            {
                return dialog2.FileName;
            }
            return "";
        }



        private void PrintReport()
        {
            this.PrintReport(true);
        }

        private void PrintReport(bool showPrintDialog)
        {
            TextPrinter textPrinter = this._textPrinter;
            textPrinter.Title = this.Text;
            textPrinter.ShowPageNumbers = true;
            textPrinter.TextToPrint = this.txtViewer.Text;
            textPrinter.Font = this.txtViewer.Font;
            if (showPrintDialog)
            {
                textPrinter.ShowPrintDialog();
            }
            else
            {
                textPrinter.Print();
            }
            textPrinter = null;
        }

        private void RemoveCancelMenuItem()
        {
            int systemMenu = GetSystemMenu(this.Handle.ToInt32(), false);
            DeleteMenu(systemMenu, 6, 0x400);
            DeleteMenu(systemMenu, 5, 0x400);
        }

        private void ShowPageSettings()
        {
            this._textPrinter.ShowPageSettings();
        }

        private void ShowPrintPreview()
        {
            TextPrinter textPrinter = this._textPrinter;
            textPrinter.Title = this.Text;
            textPrinter.ShowPageNumbers = true;
            textPrinter.TextToPrint = this.txtViewer.Text;
            textPrinter.Font = this.txtViewer.Font;
            textPrinter.ShowPrintPreview();
            textPrinter = null;
        }

        private bool SpecifyFilenameToSave()
        {
            bool flag = false;
            string path = "";
            SaveFileDialog dialog = this.SaveFileDialog1;
            dialog.InitialDirectory = this._saveToFolderPath;
            dialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Title = Application.ProductName + " Text File Save";
            dialog.DefaultExt = "*.txt";
            dialog.CreatePrompt = false;
            dialog.OverwritePrompt = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
                this._saveToFolderPath = Path.GetDirectoryName(path);
                this._saveToFilename = path;
                flag = true;
            }
            dialog = null;
            return flag;
        }

        private void SpecifyFont()
        {
            FontDialog dialog = this.FontDialog1;
            dialog.Font = this.txtViewer.Font;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtViewer.Font = dialog.Font;
            }
            dialog = null;
            this.txtViewer.Focus();
        }

        // Properties
        public bool AllowFileErase
        {
            get
            {
                return this.mnuFileErase.Visible;
            }
            set
            {
                this.mnuFileErase.Visible = value;
                this.mnuFileEraseSeparator.Visible = value;
            }
        }


    }
}
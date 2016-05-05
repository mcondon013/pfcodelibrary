using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using AppGlobals;

namespace PFPrinterObjects
{
    /// <summary>
    /// Class for text output to a printer.
    /// </summary>
    public class PFTextPrinter
    {
        private StringBuilder _msg = new StringBuilder();
        private PrintDocument pdoc = new PrintDocument();  //to do : event handlers
        private string _textToPrint = "No text specified for print. You must set the TextToPrint property.";
        private Font _font = new Font("Microsoft Sans Serif", 10);
        private string _title = "";
        private bool _showPageNumbers = true;
        private int _pageCount = 0;

        //consructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTextPrinter()
        {
            InitInstance();
        }


        private void InitInstance()
        {
            PrintPageEventHandler pdoc_page_handler = new PrintPageEventHandler(this.pdoc_PrintPage);
            PrintEventHandler pdoc_event_handler = new PrintEventHandler(this.pdoc_BeginPrint);
            if (this.pdoc != null)
            {
                this.pdoc.PrintPage += pdoc_page_handler;
                this.pdoc.BeginPrint += pdoc_event_handler;
            }
        }

        //properties
        /// <summary>
        /// Text to be printed.
        /// </summary>
        public string TextToPrint
        {
            get
            {
                return _textToPrint;
            }
            set
            {
                _textToPrint = value;
            }
        }

        /// <summary>
        /// Font to use when printing.
        /// </summary>
        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        /// <summary>
        /// Title text to print at top of page.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to include page numbers with the output.
        /// </summary>
        public bool ShowPageNumbers
        {
            get
            {
                return _showPageNumbers;
            }
            set
            {
                _showPageNumbers = value;
            }
        }

        /// <summary>
        /// Number of pages printed.
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }

        //methods
        /// <summary>
        /// Prints the text specified in the TextToPrint property. No print dialogs shown.
        /// </summary>
        public void Print()
        {
            pdoc.DocumentName = _title;
            pdoc.Print();
        }

        /// <summary>
        /// Prints the document. 
        /// </summary>
        /// <param name="showPrintDialog">Specifies whether or not to show the Windows Print Dialog.</param>
        public void Print(bool showPrintDialog)
        {
            if (showPrintDialog == true) 
            {
                PrintDialog oPrintDialog = new PrintDialog();
                oPrintDialog.Document = pdoc;
                oPrintDialog.Document.DocumentName = _title;
                oPrintDialog.UseEXDialog = true;
                if (oPrintDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Print();
                }
            }
            else
            {
                this.Print();
            }
        }

        /// <summary>
        /// Prints the text specified in the parameter.
        /// </summary>
        /// <param name="textToPrint">Text to print.</param>
        public void Print(string textToPrint)
        {
            _textToPrint = textToPrint;
            this.Print();
        }

        /// <summary>
        /// Prints the text specified in the parameter. 
        /// </summary>
        /// <param name="textToPrint"></param>
        /// <param name="showPrintDialog">Specifies whether or not to show the Windows Print Dialog.</param>
        public void Print(string textToPrint,  bool showPrintDialog)
        {
            _textToPrint = textToPrint;
            this.Print(showPrintDialog);
        }

        /// <summary>
        /// Displays the Windows Print Dialog.
        /// </summary>
        public void ShowPrintDialog()
        {
            PrintDialog oPrintDialog = new PrintDialog();
            DialogResult nDialogResult = new DialogResult();
            oPrintDialog.Document = pdoc;
            oPrintDialog.Document.DocumentName = _title;
            oPrintDialog.UseEXDialog = true;
            nDialogResult = oPrintDialog.ShowDialog();
            if (nDialogResult == DialogResult.OK)
            {
                this.Print();
            }

        }

        /// <summary>
        /// Displays Print Preview dialog.
        /// </summary>
        public void PrintPreview()
        {
            PrintPreview(false);
        }

        /// <summary>
        /// Displays Print Preview dialog. Will first display the printer selection dialog if showPrintDialog parameter is true.
        /// </summary>
        /// <param name="showPrintDialog">Determines if printer selection dialog is shown before the preview dialog.</param>
        public void PrintPreview(bool showPrintDialog)
        {
            if (showPrintDialog == true)
            {
                PrintDialog oPrintDialog = new PrintDialog();
                oPrintDialog.Document = pdoc;
                oPrintDialog.Document.DocumentName = _title;
                oPrintDialog.UseEXDialog = true;
                if (oPrintDialog.ShowDialog() == DialogResult.OK)
                {
                    ShowPrintPreview();
                }
            }
            else
            {
                ShowPrintPreview();
            }
        }

        /// <summary>
        /// Displays the Print Preview Dialog.
        /// </summary>
        public void ShowPrintPreview()
        {
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            PrintPreviewDialogExt ppd = new PrintPreviewDialogExt();
            try
            {
                ppd.Document = pdoc;
                ppd.Document.DocumentName = _title;
                ppd.ShowDialog();
            }
            catch (Exception exp)
            {
                _msg.Length = 0;
                _msg.Append("An error occurred while trying to load the ");
                _msg.Append("document for Print Preview. Make sure you currently have ");
                _msg.Append("access to a printer. A printer must be connected and ");
                _msg.Append("accessible for Print Preview to work.");
                _msg.Append("\r\n");
                _msg.Append("Error Message: ");
                _msg.Append(AppMessages.FormatErrorMessage(exp));

                AppMessages.DisplayErrorMessage(_msg.ToString());

            }

        }
        /// <summary>
        /// Shows Page Setup Dialog.
        /// </summary>
        public void ShowPageSettings()
        {
            PageSetupDialog psd = new PageSetupDialog();
            psd.Document = pdoc;
            psd.Document.DocumentName = _title;
            psd.PageSettings = pdoc.DefaultPageSettings;

            if (psd.ShowDialog() == DialogResult.OK)
            {
                pdoc.DefaultPageSettings = psd.PageSettings;
            }

        }

        private void pdoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e) //Handles pdoc.BeginPrint
        {
            _pageCount = 0;
        }


        // Declare a variable to hold the position of the last printed char. Declare
        // as static so that subsequent PrintPage events can reference it.
        private static int _currentChar;

        private void pdoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //Handles pdoc.PrintPage
        {
            int printAreaHeight, printAreaWidth, marginLeft, marginTop, marginRight, marginBottom; 
            int headerHeight, headerWidth, footerHeight, footerWidth, footerBegin;

            // Initialize local variables that contain the bounds of the printing 
            // area rectangle.
            if (pdoc.DefaultPageSettings.Landscape == false)
            {
                printAreaHeight = pdoc.DefaultPageSettings.PaperSize.Height - pdoc.DefaultPageSettings.Margins.Top - pdoc.DefaultPageSettings.Margins.Bottom;
                printAreaWidth = pdoc.DefaultPageSettings.PaperSize.Width - pdoc.DefaultPageSettings.Margins.Left - pdoc.DefaultPageSettings.Margins.Right;
            }
            else
            {
                printAreaHeight = pdoc.DefaultPageSettings.PaperSize.Width - pdoc.DefaultPageSettings.Margins.Top - pdoc.DefaultPageSettings.Margins.Bottom;
                printAreaWidth = pdoc.DefaultPageSettings.PaperSize.Height - pdoc.DefaultPageSettings.Margins.Left - pdoc.DefaultPageSettings.Margins.Right;
            }

            // Initialize local variables to hold margin values that will serve
            // as the X and Y coordinates for the upper left corner of the printing 
            // area rectangle.
            marginLeft = pdoc.DefaultPageSettings.Margins.Left; // X coordinate
            marginTop = pdoc.DefaultPageSettings.Margins.Top; // Y coordinate
            marginRight = pdoc.DefaultPageSettings.Margins.Right;
            marginBottom = pdoc.DefaultPageSettings.Margins.Bottom;
            footerBegin = marginTop + printAreaHeight;

            // Instantiate the StringFormat class, which encapsulates text layout 
            // information (such as alignment and line spacing), display manipulations 
            // (such as ellipsis insertion and national digit substitution) and OpenType 
            // features. Use of StringFormat causes MeasureString and DrawString to use
            // only an integer number of lines when printing each page, ignoring partial
            // lines that would otherwise likely be printed if the number of lines per 
            // page do not divide up cleanly for each page (which is usually the case).
            // See further discussion in the SDK documentation about StringFormatFlags.
            StringFormat fmt = new StringFormat(StringFormatFlags.LineLimit);
            StringFormat HeaderFmt = new StringFormat(StringFormatFlags.LineLimit);
            HeaderFmt.Alignment = StringAlignment.Center;
            StringFormat FooterFmt = new StringFormat(StringFormatFlags.LineLimit);
            FooterFmt.Alignment = StringAlignment.Center;

            //increment the page counter
            _pageCount += 1;

            //print header first
            headerHeight = marginTop;
            headerWidth = printAreaWidth;
            RectangleF rectHeaderPrintingArea = new RectangleF(marginLeft, Convert.ToSingle((marginTop / 2)), printAreaWidth, Convert.ToSingle((marginTop / 2)));
            if (_title.Length > 0)
            {
                //Print the text to the page.
                e.Graphics.DrawString(_title, _font, Brushes.Black, rectHeaderPrintingArea, HeaderFmt);
            }

            //print footer next
            footerHeight = marginBottom;
            footerWidth = printAreaWidth;
            RectangleF rectFooterPrintingArea = new RectangleF(marginLeft, Convert.ToSingle(footerBegin), printAreaWidth, Convert.ToSingle((marginBottom / 2)));
            if (_showPageNumbers == true)
            {
                // Print the text to the page.
                e.Graphics.DrawString("Page " + _pageCount.ToString("#,##0"), _font, Brushes.Black, rectFooterPrintingArea, FooterFmt);
            }


            // Calculate the total number of lines in the document based on the height of
            // the printing area and the height of the font.
            int lineCount = Convert.ToInt32(printAreaHeight / _font.Height);
            // Initialize the rectangle structure that defines the printing area.
            RectangleF printingArea = new RectangleF(marginLeft, marginTop, printAreaWidth, printAreaHeight);

            // Call MeasureString to determine the number of characters that will fit in
            // the printing area rectangle. The CharFitted Int32 is passed ByRef and used
            // later when calculating nCurrentChar and thus HasMorePages. LinesFilled 
            // is not needed for this sample but must be passed when passing CharsFitted.
            // Mid is used to pass the segment of remaining text left off from the 
            // previous page of printing (recall that nCurrentChar was declared as 
            // static.
            int linesFilled, charsFitted;

            //e.Graphics.MeasureString(msTextToPrint.Substring(nCurrentChar + 1), moFont, new SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, out intCharsFitted, out intLinesFilled);
            e.Graphics.MeasureString(_textToPrint.Substring(_currentChar), _font, new SizeF(printAreaWidth, printAreaHeight), fmt, out charsFitted, out linesFilled);

            // Print the text to the page.
            //e.Graphics.DrawString(msTextToPrint.Substring(nCurrentChar + 1), moFont, Brushes.Black, rectPrintingArea, fmt);
            e.Graphics.DrawString(_textToPrint.Substring(_currentChar), _font, Brushes.Black, printingArea, fmt);

            // Advance the current char to the last char printed on this page. As 
            // nCurrentChar is a static variable, its value can be used for the next
            // page to be printed. It is advanced by 1 and passed to Mid() to print the
            // next page (see above in MeasureString()).
                _currentChar += charsFitted;
                ;

            // HasMorePages tells the printing module whether another PrintPage event
            // should be fired.
            if (_currentChar < _textToPrint.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                // You must explicitly reset nCurrentChar as it is static.
                _currentChar = 0;
            }


        }



    }//end class
}//end namespace

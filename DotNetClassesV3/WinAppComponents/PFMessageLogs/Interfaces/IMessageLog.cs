using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFMessageLogs.Interfaces
{
    /// <summary>
    /// Interface for implementing MessageLog functionality.
    /// </summary>
    public interface IMessageLog
    {
        /// <summary>
        /// If true the File/Erase menu item will be displayed on the MessageLogForm.
        /// </summary>
        bool AllowFileErase { get; set; }
        /// <summary>
        /// Sets MessageLogForm caption at top of form.
        /// </summary>
        string Caption { get; set; }
        /// <summary>
        /// Erases currently displayed text in the MessageLogForm.
        /// </summary>
        void Clear();
        /// <summary>
        /// Closes the MessageLogForm.
        /// </summary>
        void CloseWindow();
        /// <summary>
        /// Shifts focus to MessageLogForm.
        /// </summary>
        void Focus();
        /// <summary>
        /// Specifies the name of the font to use for the displayed messages.
        /// </summary>
        string Font { get; set; }
        /// <summary>
        /// Specifies the font size to use for displayed messages.
        /// </summary>
        float FontSize { get; set; }
        /// <summary>
        /// Retrieves reference to the MessageLogForm. 
        /// This allows caller to directly control the form.
        /// </summary>
        System.Windows.Forms.Form Form { get; }
        /// <summary>
        /// Returns whether or not the MessageLogForm is currently visible.
        /// </summary>
        bool FormIsVisible { get; }
        /// <summary>
        /// Hides MessageLogForm windows.
        /// </summary>
        void HideWindow();
        /// <summary>
        /// Loads contents of specified file to the MessageLogForm display.
        /// </summary>
        /// <param name="filename">Path to file containing text to load.</param>
        void LoadFile(string filename);
        /// <summary>
        /// Forces focus to remain on MessageLogForm after a line of text is written.
        /// </summary>
        bool RetainFocus { get; set; }
        /// <summary>
        /// Saves contents of the MessageLogForm display to a file.
        /// </summary>
        /// <param name="filename">Saves contents of message log window to specified file.</param>
        void SaveFile(string filename);
        /// <summary>
        /// Specifies whether or not to show the date and time with each message displayed on MessageLogForm.
        /// </summary>
        bool ShowDatetime { get; set; }
        /// <summary>
        /// Makes the MessageLogForm visible.
        /// </summary>
        void ShowWindow();
        /// <summary>
        /// Writes a line of text to the MessageLogForm display.
        /// </summary>
        /// <param name="message">Text to output.</param>
        void WriteLine(string message);
    }
}

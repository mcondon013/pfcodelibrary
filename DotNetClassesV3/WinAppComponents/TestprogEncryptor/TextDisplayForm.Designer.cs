namespace pfEncryptor
{
    partial class TextDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextDisplayForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panelTextToProcess = new System.Windows.Forms.Panel();
            this.txtTextToProcess = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.textToEncryptTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.panelTextToProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(168, 272);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // panelTextToProcess
            // 
            this.panelTextToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTextToProcess.Controls.Add(this.txtTextToProcess);
            this.panelTextToProcess.Location = new System.Drawing.Point(1, 1);
            this.panelTextToProcess.Name = "panelTextToProcess";
            this.panelTextToProcess.Size = new System.Drawing.Size(281, 236);
            this.panelTextToProcess.TabIndex = 2;
            // 
            // txtTextToProcess
            // 
            this.txtTextToProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTextToProcess.Location = new System.Drawing.Point(0, 0);
            this.txtTextToProcess.Multiline = true;
            this.txtTextToProcess.Name = "txtTextToProcess";
            this.txtTextToProcess.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextToProcess.Size = new System.Drawing.Size(281, 236);
            this.txtTextToProcess.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(168, 243);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 23);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCopy.Location = new System.Drawing.Point(12, 243);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(98, 23);
            this.cmdCopy.TabIndex = 2;
            this.cmdCopy.Text = "&Copy";
            this.textToEncryptTooltips.SetToolTip(this.cmdCopy, "Copies text displayed above to the Windows clipboard.");
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // TextDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 310);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.panelTextToProcess);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text to Encrypt";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panelTextToProcess.ResumeLayout(false);
            this.panelTextToProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel panelTextToProcess;
        private System.Windows.Forms.TextBox txtTextToProcess;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.ToolTip textToEncryptTooltips;
    }
}
namespace PFConnectionStrings
{
#pragma warning disable 1591
    partial class PFConnectionStringManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFConnectionStringManagerForm));
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.optSQLServer = new System.Windows.Forms.RadioButton();
            this.optSQLServerCE35 = new System.Windows.Forms.RadioButton();
            this.optSQLServerCE40 = new System.Windows.Forms.RadioButton();
            this.optMicrosoftAccess = new System.Windows.Forms.RadioButton();
            this.optODBC = new System.Windows.Forms.RadioButton();
            this.optOLEDB = new System.Windows.Forms.RadioButton();
            this.optOracleNative = new System.Windows.Forms.RadioButton();
            this.optMySQL = new System.Windows.Forms.RadioButton();
            this.optDB2 = new System.Windows.Forms.RadioButton();
            this.optInformix = new System.Windows.Forms.RadioButton();
            this.optSybase = new System.Windows.Forms.RadioButton();
            this.optSQLAnywhere = new System.Windows.Forms.RadioButton();
            this.optSQLAnywhereUL = new System.Windows.Forms.RadioButton();
            this.optMSOracle = new System.Windows.Forms.RadioButton();
            this.chkShowInstalledProvidersOnly = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(254, 315);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(98, 34);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExitClick);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(254, 22);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 34);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "&Edit Connection";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // optSQLServer
            // 
            this.optSQLServer.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSQLServer, true);
            this.optSQLServer.Location = new System.Drawing.Point(3, 3);
            this.optSQLServer.Name = "optSQLServer";
            this.optSQLServer.Size = new System.Drawing.Size(80, 17);
            this.optSQLServer.TabIndex = 2;
            this.optSQLServer.TabStop = true;
            this.optSQLServer.Text = "SQL Server";
            this.optSQLServer.UseVisualStyleBackColor = true;
            // 
            // optSQLServerCE35
            // 
            this.optSQLServerCE35.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSQLServerCE35, true);
            this.optSQLServerCE35.Location = new System.Drawing.Point(3, 26);
            this.optSQLServerCE35.Name = "optSQLServerCE35";
            this.optSQLServerCE35.Size = new System.Drawing.Size(115, 17);
            this.optSQLServerCE35.TabIndex = 3;
            this.optSQLServerCE35.TabStop = true;
            this.optSQLServerCE35.Text = "SQL Server CE 3.5";
            this.optSQLServerCE35.UseVisualStyleBackColor = true;
            // 
            // optSQLServerCE40
            // 
            this.optSQLServerCE40.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSQLServerCE40, true);
            this.optSQLServerCE40.Location = new System.Drawing.Point(3, 49);
            this.optSQLServerCE40.Name = "optSQLServerCE40";
            this.optSQLServerCE40.Size = new System.Drawing.Size(115, 17);
            this.optSQLServerCE40.TabIndex = 4;
            this.optSQLServerCE40.TabStop = true;
            this.optSQLServerCE40.Text = "SQL Server CE 4.0";
            this.optSQLServerCE40.UseVisualStyleBackColor = true;
            // 
            // optMicrosoftAccess
            // 
            this.optMicrosoftAccess.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optMicrosoftAccess, true);
            this.optMicrosoftAccess.Location = new System.Drawing.Point(3, 72);
            this.optMicrosoftAccess.Name = "optMicrosoftAccess";
            this.optMicrosoftAccess.Size = new System.Drawing.Size(106, 17);
            this.optMicrosoftAccess.TabIndex = 5;
            this.optMicrosoftAccess.TabStop = true;
            this.optMicrosoftAccess.Text = "Microsoft Access";
            this.optMicrosoftAccess.UseVisualStyleBackColor = true;
            // 
            // optODBC
            // 
            this.optODBC.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optODBC, true);
            this.optODBC.Location = new System.Drawing.Point(3, 95);
            this.optODBC.Name = "optODBC";
            this.optODBC.Size = new System.Drawing.Size(55, 17);
            this.optODBC.TabIndex = 6;
            this.optODBC.TabStop = true;
            this.optODBC.Text = "ODBC";
            this.optODBC.UseVisualStyleBackColor = true;
            // 
            // optOLEDB
            // 
            this.optOLEDB.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optOLEDB, true);
            this.optOLEDB.Location = new System.Drawing.Point(3, 118);
            this.optOLEDB.Name = "optOLEDB";
            this.optOLEDB.Size = new System.Drawing.Size(61, 17);
            this.optOLEDB.TabIndex = 7;
            this.optOLEDB.TabStop = true;
            this.optOLEDB.Text = "OLEDB";
            this.optOLEDB.UseVisualStyleBackColor = true;
            // 
            // optOracleNative
            // 
            this.optOracleNative.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optOracleNative, true);
            this.optOracleNative.Location = new System.Drawing.Point(3, 141);
            this.optOracleNative.Name = "optOracleNative";
            this.optOracleNative.Size = new System.Drawing.Size(90, 17);
            this.optOracleNative.TabIndex = 8;
            this.optOracleNative.TabStop = true;
            this.optOracleNative.Text = "Oracle Native";
            this.optOracleNative.UseVisualStyleBackColor = true;
            // 
            // optMySQL
            // 
            this.optMySQL.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optMySQL, true);
            this.optMySQL.Location = new System.Drawing.Point(3, 164);
            this.optMySQL.Name = "optMySQL";
            this.optMySQL.Size = new System.Drawing.Size(60, 17);
            this.optMySQL.TabIndex = 9;
            this.optMySQL.TabStop = true;
            this.optMySQL.Text = "MySQL";
            this.optMySQL.UseVisualStyleBackColor = true;
            // 
            // optDB2
            // 
            this.optDB2.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optDB2, true);
            this.optDB2.Location = new System.Drawing.Point(3, 187);
            this.optDB2.Name = "optDB2";
            this.optDB2.Size = new System.Drawing.Size(46, 17);
            this.optDB2.TabIndex = 10;
            this.optDB2.TabStop = true;
            this.optDB2.Text = "DB2";
            this.optDB2.UseVisualStyleBackColor = true;
            // 
            // optInformix
            // 
            this.optInformix.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optInformix, true);
            this.optInformix.Location = new System.Drawing.Point(3, 210);
            this.optInformix.Name = "optInformix";
            this.optInformix.Size = new System.Drawing.Size(61, 17);
            this.optInformix.TabIndex = 11;
            this.optInformix.TabStop = true;
            this.optInformix.Text = "Informix";
            this.optInformix.UseVisualStyleBackColor = true;
            // 
            // optSybase
            // 
            this.optSybase.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSybase, true);
            this.optSybase.Location = new System.Drawing.Point(3, 233);
            this.optSybase.Name = "optSybase";
            this.optSybase.Size = new System.Drawing.Size(60, 17);
            this.optSybase.TabIndex = 12;
            this.optSybase.TabStop = true;
            this.optSybase.Text = "Sybase";
            this.optSybase.UseVisualStyleBackColor = true;
            // 
            // optSQLAnywhere
            // 
            this.optSQLAnywhere.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSQLAnywhere, true);
            this.optSQLAnywhere.Location = new System.Drawing.Point(3, 256);
            this.optSQLAnywhere.Name = "optSQLAnywhere";
            this.optSQLAnywhere.Size = new System.Drawing.Size(96, 17);
            this.optSQLAnywhere.TabIndex = 13;
            this.optSQLAnywhere.TabStop = true;
            this.optSQLAnywhere.Text = "SQL Anywhere";
            this.optSQLAnywhere.UseVisualStyleBackColor = true;
            // 
            // optSQLAnywhereUL
            // 
            this.optSQLAnywhereUL.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optSQLAnywhereUL, true);
            this.optSQLAnywhereUL.Location = new System.Drawing.Point(3, 279);
            this.optSQLAnywhereUL.Name = "optSQLAnywhereUL";
            this.optSQLAnywhereUL.Size = new System.Drawing.Size(141, 17);
            this.optSQLAnywhereUL.TabIndex = 14;
            this.optSQLAnywhereUL.TabStop = true;
            this.optSQLAnywhereUL.Text = "SQL Anywhere Ultra Lite";
            this.optSQLAnywhereUL.UseVisualStyleBackColor = true;
            // 
            // optMSOracle
            // 
            this.optMSOracle.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.optMSOracle, true);
            this.optMSOracle.Location = new System.Drawing.Point(3, 302);
            this.optMSOracle.Name = "optMSOracle";
            this.optMSOracle.Size = new System.Drawing.Size(187, 17);
            this.optMSOracle.TabIndex = 15;
            this.optMSOracle.TabStop = true;
            this.optMSOracle.Text = "Microsoft Oracle Provider for .NET";
            this.optMSOracle.UseVisualStyleBackColor = true;
            // 
            // chkShowInstalledProvidersOnly
            // 
            this.chkShowInstalledProvidersOnly.AutoSize = true;
            this.chkShowInstalledProvidersOnly.Location = new System.Drawing.Point(254, 109);
            this.chkShowInstalledProvidersOnly.Name = "chkShowInstalledProvidersOnly";
            this.chkShowInstalledProvidersOnly.Size = new System.Drawing.Size(95, 30);
            this.chkShowInstalledProvidersOnly.TabIndex = 16;
            this.chkShowInstalledProvidersOnly.Text = "Show Installed\r\nProviders Only";
            this.chkShowInstalledProvidersOnly.UseVisualStyleBackColor = true;
            this.chkShowInstalledProvidersOnly.CheckedChanged += new System.EventHandler(this.chkShowInstalledProvidersOnly_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.optSQLServer);
            this.flowLayoutPanel1.Controls.Add(this.optSQLServerCE35);
            this.flowLayoutPanel1.Controls.Add(this.optSQLServerCE40);
            this.flowLayoutPanel1.Controls.Add(this.optMicrosoftAccess);
            this.flowLayoutPanel1.Controls.Add(this.optODBC);
            this.flowLayoutPanel1.Controls.Add(this.optOLEDB);
            this.flowLayoutPanel1.Controls.Add(this.optOracleNative);
            this.flowLayoutPanel1.Controls.Add(this.optMySQL);
            this.flowLayoutPanel1.Controls.Add(this.optDB2);
            this.flowLayoutPanel1.Controls.Add(this.optInformix);
            this.flowLayoutPanel1.Controls.Add(this.optSybase);
            this.flowLayoutPanel1.Controls.Add(this.optSQLAnywhere);
            this.flowLayoutPanel1.Controls.Add(this.optSQLAnywhereUL);
            this.flowLayoutPanel1.Controls.Add(this.optMSOracle);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(224, 327);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // PFConnectionStringManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 369);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.chkShowInstalledProvidersOnly);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PFConnectionStringManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PFConnectionStringManagerForm";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.RadioButton optSQLServer;
        private System.Windows.Forms.RadioButton optSQLServerCE35;
        private System.Windows.Forms.RadioButton optSQLServerCE40;
        private System.Windows.Forms.RadioButton optMicrosoftAccess;
        private System.Windows.Forms.RadioButton optODBC;
        private System.Windows.Forms.RadioButton optOLEDB;
        private System.Windows.Forms.RadioButton optOracleNative;
        private System.Windows.Forms.RadioButton optMySQL;
        private System.Windows.Forms.RadioButton optDB2;
        private System.Windows.Forms.RadioButton optInformix;
        private System.Windows.Forms.RadioButton optSybase;
        private System.Windows.Forms.RadioButton optSQLAnywhere;
        private System.Windows.Forms.RadioButton optSQLAnywhereUL;
        private System.Windows.Forms.RadioButton optMSOracle;
        private System.Windows.Forms.CheckBox chkShowInstalledProvidersOnly;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
#pragma warning restore 1591
}
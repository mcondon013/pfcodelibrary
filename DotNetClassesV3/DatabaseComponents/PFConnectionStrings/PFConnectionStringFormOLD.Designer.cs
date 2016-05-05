namespace PFConnectionStrings
{
    partial class PFConnectionStringFormOLD
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
            this.txtDatabasePlatform = new System.Windows.Forms.TextBox();
            this.cmdParseConnectionString = new System.Windows.Forms.Button();
            this.cmdBuildConnectionString = new System.Windows.Forms.Button();
            this.txtConnectionName = new System.Windows.Forms.TextBox();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblInputPrompt = new System.Windows.Forms.Label();
            this.lblDatabasePlatform = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOpenExistingDefinition = new System.Windows.Forms.Button();
            this.cmdSaveCurrentDefinition = new System.Windows.Forms.Button();
            this.cmdNewDefinition = new System.Windows.Forms.Button();
            this.connectingStringToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // txtDatabasePlatform
            // 
            this.txtDatabasePlatform.BackColor = System.Drawing.SystemColors.Window;
            this.txtDatabasePlatform.Location = new System.Drawing.Point(147, 72);
            this.txtDatabasePlatform.Name = "txtDatabasePlatform";
            this.txtDatabasePlatform.ReadOnly = true;
            this.txtDatabasePlatform.Size = new System.Drawing.Size(172, 20);
            this.txtDatabasePlatform.TabIndex = 69;
            // 
            // cmdParseConnectionString
            // 
            this.cmdParseConnectionString.Location = new System.Drawing.Point(526, 346);
            this.cmdParseConnectionString.Name = "cmdParseConnectionString";
            this.cmdParseConnectionString.Size = new System.Drawing.Size(98, 25);
            this.cmdParseConnectionString.TabIndex = 68;
            this.cmdParseConnectionString.Text = "Verify\r";
            this.cmdParseConnectionString.UseVisualStyleBackColor = true;
            this.cmdParseConnectionString.Click += new System.EventHandler(this.cmdVerifyConnectionString_Click);
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(526, 244);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(98, 25);
            this.cmdBuildConnectionString.TabIndex = 67;
            this.cmdBuildConnectionString.Text = "Build";
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // txtConnectionName
            // 
            this.txtConnectionName.Location = new System.Drawing.Point(147, 107);
            this.txtConnectionName.Name = "txtConnectionName";
            this.txtConnectionName.Size = new System.Drawing.Size(348, 20);
            this.txtConnectionName.TabIndex = 66;
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Location = new System.Drawing.Point(43, 108);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(92, 13);
            this.lblConnectionName.TabIndex = 65;
            this.lblConnectionName.Text = "Connection Name";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(184, 27);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(289, 24);
            this.lblFormTitle.TabIndex = 64;
            this.lblFormTitle.Text = "Connection String Definition Utility";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(145, 343);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(348, 132);
            this.txtConnectionString.TabIndex = 63;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(41, 346);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 62;
            this.lblConnectionString.Text = "Connection String";
            // 
            // lblInputPrompt
            // 
            this.lblInputPrompt.AutoSize = true;
            this.lblInputPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputPrompt.Location = new System.Drawing.Point(43, 148);
            this.lblInputPrompt.Name = "lblInputPrompt";
            this.lblInputPrompt.Size = new System.Drawing.Size(260, 16);
            this.lblInputPrompt.TabIndex = 61;
            this.lblInputPrompt.Text = "Enter Connection String Key Values Below:";
            // 
            // lblDatabasePlatform
            // 
            this.lblDatabasePlatform.AutoSize = true;
            this.lblDatabasePlatform.Location = new System.Drawing.Point(43, 75);
            this.lblDatabasePlatform.Name = "lblDatabasePlatform";
            this.lblDatabasePlatform.Size = new System.Drawing.Size(94, 13);
            this.lblDatabasePlatform.TabIndex = 60;
            this.lblDatabasePlatform.Text = "Database Platform";
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(526, 73);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 34);
            this.cmdOK.TabIndex = 59;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(526, 453);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 34);
            this.cmdCancel.TabIndex = 58;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOpenExistingDefinition
            // 
            this.cmdOpenExistingDefinition.Location = new System.Drawing.Point(526, 182);
            this.cmdOpenExistingDefinition.Name = "cmdOpenExistingDefinition";
            this.cmdOpenExistingDefinition.Size = new System.Drawing.Size(98, 25);
            this.cmdOpenExistingDefinition.TabIndex = 70;
            this.cmdOpenExistingDefinition.Text = "&Open";
            this.cmdOpenExistingDefinition.UseVisualStyleBackColor = true;
            this.cmdOpenExistingDefinition.Click += new System.EventHandler(this.cmdOpenExistingDefinition_Click);
            // 
            // cmdSaveCurrentDefinition
            // 
            this.cmdSaveCurrentDefinition.Location = new System.Drawing.Point(526, 213);
            this.cmdSaveCurrentDefinition.Name = "cmdSaveCurrentDefinition";
            this.cmdSaveCurrentDefinition.Size = new System.Drawing.Size(98, 25);
            this.cmdSaveCurrentDefinition.TabIndex = 71;
            this.cmdSaveCurrentDefinition.Text = "&Save";
            this.cmdSaveCurrentDefinition.UseVisualStyleBackColor = true;
            this.cmdSaveCurrentDefinition.Click += new System.EventHandler(this.cmdSaveCurrentDefinition_Click);
            // 
            // cmdNewDefinition
            // 
            this.cmdNewDefinition.Location = new System.Drawing.Point(526, 148);
            this.cmdNewDefinition.Name = "cmdNewDefinition";
            this.cmdNewDefinition.Size = new System.Drawing.Size(98, 25);
            this.cmdNewDefinition.TabIndex = 72;
            this.cmdNewDefinition.Text = "&New";
            this.cmdNewDefinition.UseVisualStyleBackColor = true;
            this.cmdNewDefinition.Click += new System.EventHandler(this.cmdNewDefinition_Click);
            // 
            // PFConnectionStringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 514);
            this.Controls.Add(this.cmdNewDefinition);
            this.Controls.Add(this.cmdSaveCurrentDefinition);
            this.Controls.Add(this.cmdOpenExistingDefinition);
            this.Controls.Add(this.txtDatabasePlatform);
            this.Controls.Add(this.cmdParseConnectionString);
            this.Controls.Add(this.cmdBuildConnectionString);
            this.Controls.Add(this.txtConnectionName);
            this.Controls.Add(this.lblConnectionName);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.lblInputPrompt);
            this.Controls.Add(this.lblDatabasePlatform);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Name = "PFConnectionStringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection String Input";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatabasePlatform;
        private System.Windows.Forms.Button cmdParseConnectionString;
        private System.Windows.Forms.Button cmdBuildConnectionString;
        private System.Windows.Forms.TextBox txtConnectionName;
        private System.Windows.Forms.Label lblConnectionName;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Label lblInputPrompt;
        private System.Windows.Forms.Label lblDatabasePlatform;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOpenExistingDefinition;
        private System.Windows.Forms.Button cmdSaveCurrentDefinition;
        private System.Windows.Forms.Button cmdNewDefinition;
        private System.Windows.Forms.ToolTip connectingStringToolTips;

    }
}
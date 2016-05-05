namespace TestprogXml
{
    partial class MainForm
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdDeleteAllNodes = new System.Windows.Forms.Button();
            this.cmdDeleteChildNodes = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNodeTag = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNodeInnerText = new System.Windows.Forms.TextBox();
            this.txtAttributeValue = new System.Windows.Forms.TextBox();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtNodePath = new System.Windows.Forms.TextBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdCreateXMLDoc = new System.Windows.Forms.Button();
            this.cmdOpenXMLDocument = new System.Windows.Forms.Button();
            this.cmdRunTest = new System.Windows.Forms.Button();
            this.cmdAddNodes = new System.Windows.Forms.Button();
            this.cmdDeleteNode = new System.Windows.Forms.Button();
            this.cmdUpdateNodes = new System.Windows.Forms.Button();
            this.cmdFindNode = new System.Windows.Forms.Button();
            this.cmdFindNodes = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtRootNode = new System.Windows.Forms.TextBox();
            this.cmdTest1 = new System.Windows.Forms.Button();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(610, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogXml\\TestprogXml\\InitWinFormsHelpFi" +
    "le.chm";
            // 
            // cmdDeleteAllNodes
            // 
            this.cmdDeleteAllNodes.Location = new System.Drawing.Point(418, 280);
            this.cmdDeleteAllNodes.Name = "cmdDeleteAllNodes";
            this.cmdDeleteAllNodes.Size = new System.Drawing.Size(160, 33);
            this.cmdDeleteAllNodes.TabIndex = 44;
            this.cmdDeleteAllNodes.Text = "&7 Delete All Nodes";
            this.cmdDeleteAllNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDeleteAllNodes.UseVisualStyleBackColor = true;
            this.cmdDeleteAllNodes.Click += new System.EventHandler(this.cmdDeleteAllNodes_Click);
            // 
            // cmdDeleteChildNodes
            // 
            this.cmdDeleteChildNodes.Location = new System.Drawing.Point(418, 244);
            this.cmdDeleteChildNodes.Name = "cmdDeleteChildNodes";
            this.cmdDeleteChildNodes.Size = new System.Drawing.Size(160, 32);
            this.cmdDeleteChildNodes.TabIndex = 43;
            this.cmdDeleteChildNodes.Text = "&6 Delete Child Nodes";
            this.cmdDeleteChildNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDeleteChildNodes.UseVisualStyleBackColor = true;
            this.cmdDeleteChildNodes.Click += new System.EventHandler(this.cmdDeleteChildNodes_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(33, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 55;
            this.label7.Text = "Node Tag:";
            // 
            // txtNodeTag
            // 
            this.txtNodeTag.Location = new System.Drawing.Point(33, 206);
            this.txtNodeTag.Name = "txtNodeTag";
            this.txtNodeTag.Size = new System.Drawing.Size(312, 20);
            this.txtNodeTag.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(33, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 54;
            this.label6.Text = "Node Inner Text:";
            // 
            // txtNodeInnerText
            // 
            this.txtNodeInnerText.Location = new System.Drawing.Point(33, 250);
            this.txtNodeInnerText.Name = "txtNodeInnerText";
            this.txtNodeInnerText.Size = new System.Drawing.Size(312, 20);
            this.txtNodeInnerText.TabIndex = 34;
            // 
            // txtAttributeValue
            // 
            this.txtAttributeValue.Location = new System.Drawing.Point(36, 351);
            this.txtAttributeValue.Name = "txtAttributeValue";
            this.txtAttributeValue.Size = new System.Drawing.Size(309, 20);
            this.txtAttributeValue.TabIndex = 36;
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(36, 298);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(309, 20);
            this.txtAttribute.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(33, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 22);
            this.label5.TabIndex = 49;
            this.label5.Text = "Attribute Value:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(33, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 48;
            this.label4.Text = "Attribute:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(33, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 50;
            this.label3.Text = "Node Path:";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(33, 88);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(120, 16);
            this.Label2.TabIndex = 52;
            this.Label2.Text = "Root Node:";
            // 
            // txtNodePath
            // 
            this.txtNodePath.Location = new System.Drawing.Point(33, 161);
            this.txtNodePath.Name = "txtNodePath";
            this.txtNodePath.Size = new System.Drawing.Size(312, 20);
            this.txtNodePath.TabIndex = 30;
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(418, 441);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(160, 32);
            this.cmdExit.TabIndex = 51;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdCreateXMLDoc
            // 
            this.cmdCreateXMLDoc.Location = new System.Drawing.Point(418, 46);
            this.cmdCreateXMLDoc.Name = "cmdCreateXMLDoc";
            this.cmdCreateXMLDoc.Size = new System.Drawing.Size(160, 32);
            this.cmdCreateXMLDoc.TabIndex = 38;
            this.cmdCreateXMLDoc.Text = "&1 Create XML Document";
            this.cmdCreateXMLDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCreateXMLDoc.Click += new System.EventHandler(this.cmdCreateXMLDoc_Click);
            // 
            // cmdOpenXMLDocument
            // 
            this.cmdOpenXMLDocument.Location = new System.Drawing.Point(418, 78);
            this.cmdOpenXMLDocument.Name = "cmdOpenXMLDocument";
            this.cmdOpenXMLDocument.Size = new System.Drawing.Size(160, 32);
            this.cmdOpenXMLDocument.TabIndex = 39;
            this.cmdOpenXMLDocument.Text = "&2 Open XML Document";
            this.cmdOpenXMLDocument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOpenXMLDocument.Click += new System.EventHandler(this.cmdOpenXMLDocument_Click);
            // 
            // cmdRunTest
            // 
            this.cmdRunTest.Location = new System.Drawing.Point(418, 126);
            this.cmdRunTest.Name = "cmdRunTest";
            this.cmdRunTest.Size = new System.Drawing.Size(160, 32);
            this.cmdRunTest.TabIndex = 40;
            this.cmdRunTest.Text = "&3 Run Test";
            this.cmdRunTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRunTest.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // cmdAddNodes
            // 
            this.cmdAddNodes.Location = new System.Drawing.Point(418, 165);
            this.cmdAddNodes.Name = "cmdAddNodes";
            this.cmdAddNodes.Size = new System.Drawing.Size(160, 32);
            this.cmdAddNodes.TabIndex = 41;
            this.cmdAddNodes.Text = "&4 Add Nodes";
            this.cmdAddNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAddNodes.Click += new System.EventHandler(this.cmdAddNodes_Click);
            // 
            // cmdDeleteNode
            // 
            this.cmdDeleteNode.Location = new System.Drawing.Point(418, 210);
            this.cmdDeleteNode.Name = "cmdDeleteNode";
            this.cmdDeleteNode.Size = new System.Drawing.Size(160, 32);
            this.cmdDeleteNode.TabIndex = 42;
            this.cmdDeleteNode.Text = "&5 Delete Node";
            this.cmdDeleteNode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDeleteNode.Click += new System.EventHandler(this.cmdDeleteNode_Click);
            // 
            // cmdUpdateNodes
            // 
            this.cmdUpdateNodes.Location = new System.Drawing.Point(418, 324);
            this.cmdUpdateNodes.Name = "cmdUpdateNodes";
            this.cmdUpdateNodes.Size = new System.Drawing.Size(160, 32);
            this.cmdUpdateNodes.TabIndex = 45;
            this.cmdUpdateNodes.Text = "&6 Update Nodes";
            this.cmdUpdateNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdateNodes.Click += new System.EventHandler(this.cmdUpdateNodes_Click);
            // 
            // cmdFindNode
            // 
            this.cmdFindNode.Location = new System.Drawing.Point(418, 356);
            this.cmdFindNode.Name = "cmdFindNode";
            this.cmdFindNode.Size = new System.Drawing.Size(160, 32);
            this.cmdFindNode.TabIndex = 46;
            this.cmdFindNode.Text = "&7 Find Node";
            this.cmdFindNode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFindNode.Click += new System.EventHandler(this.cmdFindNode_Click);
            // 
            // cmdFindNodes
            // 
            this.cmdFindNodes.Location = new System.Drawing.Point(418, 388);
            this.cmdFindNodes.Name = "cmdFindNodes";
            this.cmdFindNodes.Size = new System.Drawing.Size(160, 32);
            this.cmdFindNodes.TabIndex = 47;
            this.cmdFindNodes.Text = "&8 Find Nodes";
            this.cmdFindNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdFindNodes.Click += new System.EventHandler(this.cmdFindNodes_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(33, 61);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(312, 20);
            this.txtFilename.TabIndex = 27;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(33, 45);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(120, 16);
            this.Label1.TabIndex = 33;
            this.Label1.Text = "Document filename:";
            // 
            // txtRootNode
            // 
            this.txtRootNode.Location = new System.Drawing.Point(33, 112);
            this.txtRootNode.Name = "txtRootNode";
            this.txtRootNode.Size = new System.Drawing.Size(312, 20);
            this.txtRootNode.TabIndex = 28;
            // 
            // cmdTest1
            // 
            this.cmdTest1.Location = new System.Drawing.Point(36, 441);
            this.cmdTest1.Name = "cmdTest1";
            this.cmdTest1.Size = new System.Drawing.Size(75, 32);
            this.cmdTest1.TabIndex = 53;
            this.cmdTest1.Text = "Test &1";
            this.cmdTest1.UseVisualStyleBackColor = true;
            this.cmdTest1.Click += new System.EventHandler(this.cmdTest1_Click);
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(36, 388);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 56;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 519);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.cmdDeleteAllNodes);
            this.Controls.Add(this.cmdDeleteChildNodes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNodeTag);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNodeInnerText);
            this.Controls.Add(this.txtAttributeValue);
            this.Controls.Add(this.txtAttribute);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtNodePath);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdCreateXMLDoc);
            this.Controls.Add(this.cmdOpenXMLDocument);
            this.Controls.Add(this.cmdRunTest);
            this.Controls.Add(this.cmdAddNodes);
            this.Controls.Add(this.cmdDeleteNode);
            this.Controls.Add(this.cmdUpdateNodes);
            this.Controls.Add(this.cmdFindNode);
            this.Controls.Add(this.cmdFindNodes);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtRootNode);
            this.Controls.Add(this.cmdTest1);
            this.Controls.Add(this.MainMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testprog for PFXmlDocument";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.Button cmdDeleteAllNodes;
        private System.Windows.Forms.Button cmdDeleteChildNodes;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtNodeTag;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtNodeInnerText;
        internal System.Windows.Forms.TextBox txtAttributeValue;
        internal System.Windows.Forms.TextBox txtAttribute;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtNodePath;
        internal System.Windows.Forms.Button cmdExit;
        internal System.Windows.Forms.Button cmdCreateXMLDoc;
        internal System.Windows.Forms.Button cmdOpenXMLDocument;
        internal System.Windows.Forms.Button cmdRunTest;
        internal System.Windows.Forms.Button cmdAddNodes;
        internal System.Windows.Forms.Button cmdDeleteNode;
        internal System.Windows.Forms.Button cmdUpdateNodes;
        internal System.Windows.Forms.Button cmdFindNode;
        internal System.Windows.Forms.Button cmdFindNodes;
        internal System.Windows.Forms.TextBox txtFilename;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtRootNode;
        private System.Windows.Forms.Button cmdTest1;
        internal System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
    }
}


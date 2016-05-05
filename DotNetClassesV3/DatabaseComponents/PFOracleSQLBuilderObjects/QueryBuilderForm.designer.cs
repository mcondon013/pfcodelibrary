#pragma warning disable 1591
namespace PFOracleSQLBuilderObjects
{
    partial class QueryBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryBuilderForm));
            this.plainTextSQLBuilder1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(this.components);
            this.queryBuilder = new ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSQL = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.qbfMenu = new System.Windows.Forms.MenuStrip();
            this.qbfMenuQBF = new System.Windows.Forms.ToolStripMenuItem();
            this.qbfMenuQBFAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.qbfMenuQBFCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.qbfMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.qbfMenuHelpTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.qbfHelpProvider = new System.Windows.Forms.HelpProvider();
            this.tabControl1.SuspendLayout();
            this.tabPageSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.qbfMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // plainTextSQLBuilder1
            // 
            this.plainTextSQLBuilder1.DynamicIndents = true;
            this.plainTextSQLBuilder1.DynamicRightMargin = false;
            this.plainTextSQLBuilder1.QueryBuilder = this.queryBuilder;
            this.plainTextSQLBuilder1.SQLUpdated += new System.EventHandler(this.plainTextSQLBuilder1_SQLUpdated);
            // 
            // queryBuilder
            // 
            this.queryBuilder.AddObjectFormOptions.Height = 342;
            this.queryBuilder.AddObjectFormOptions.MinimumSize = new System.Drawing.Size(430, 430);
            this.queryBuilder.AddObjectFormOptions.Width = 294;
            this.queryBuilder.DatabaseSchemaTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.DatabaseSchemaTreeOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryBuilder.DatabaseSchemaTreeOptions.SortingType = ActiveDatabaseSoftware.ActiveQueryBuilder.ObjectsSortingType.TypeName;
            this.queryBuilder.DatabaseSchemaTreeOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.queryBuilder.DataSourceOptions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.DataSourceOptions.DescriptionColumnOptions.Color = System.Drawing.Color.LightBlue;
            this.queryBuilder.DataSourceOptions.FocusedBackgroundColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.DataSourceOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.queryBuilder.DataSourceOptions.MarkColumnOptions.PrimaryKeyIcon = ((System.Drawing.Image)(resources.GetObject("resource.PrimaryKeyIcon")));
            this.queryBuilder.DataSourceOptions.NameColumnOptions.Color = System.Drawing.SystemColors.WindowText;
            this.queryBuilder.DataSourceOptions.NameColumnOptions.PrimaryKeyColor = System.Drawing.SystemColors.WindowText;
            this.queryBuilder.DataSourceOptions.TypeColumnOptions.Color = System.Drawing.SystemColors.GrayText;
            this.queryBuilder.DesignPaneOptions.BackColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryBuilder.Location = new System.Drawing.Point(0, 0);
            this.queryBuilder.MetadataContainer.AltName = "";
            this.queryBuilder.MetadataStructureOptions.ProceduresFolderText = "Procedures";
            this.queryBuilder.MetadataStructureOptions.SynonymsFolderText = "Synonyms";
            this.queryBuilder.MetadataStructureOptions.TablesFolderText = "Tables";
            this.queryBuilder.MetadataStructureOptions.ViewsFolderText = "Views";
            this.queryBuilder.Name = "queryBuilder";
            this.queryBuilder.PanesConfigurationOptions.RightTreePaneWidth = 230;
            this.queryBuilder.QueryColumnListOptions.AlternateRowColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.QueryColumnListOptions.BackColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.QueryColumnListOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryBuilder.QueryColumnListOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.queryBuilder.QueryStructureTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.queryBuilder.QueryStructureTreeOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.queryBuilder.QueryStructureTreeOptions.QueriesImageIndex = 17;
            this.queryBuilder.QueryStructureTreeOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.queryBuilder.Size = new System.Drawing.Size(649, 346);
            this.queryBuilder.SQLChanging = false;
            this.queryBuilder.TabIndex = 0;
            this.queryBuilder.SQLUpdated += new System.EventHandler(this.queryBuilder_SQLUpdated);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSQL);
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 487);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.Controls.Add(this.splitContainer1);
            this.tabPageSQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSQL.Size = new System.Drawing.Size(655, 461);
            this.tabPageSQL.TabIndex = 0;
            this.tabPageSQL.Text = "SQL";
            this.tabPageSQL.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.queryBuilder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer1.Panel2.Controls.Add(this.cmdAccept);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(649, 455);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(542, 62);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Location = new System.Drawing.Point(542, 15);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(90, 23);
            this.cmdAccept.TabIndex = 1;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(523, 100);
            this.textBox1.TabIndex = 0;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataGridView1);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(773, 478);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Text = "Data";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(857, 483);
            this.dataGridView1.TabIndex = 0;
            // 
            // qbfMenu
            // 
            this.qbfMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qbfMenuQBF,
            this.qbfMenuHelp});
            this.qbfMenu.Location = new System.Drawing.Point(0, 0);
            this.qbfMenu.Name = "qbfMenu";
            this.qbfMenu.Size = new System.Drawing.Size(663, 24);
            this.qbfMenu.TabIndex = 2;
            this.qbfMenu.Text = "menuStrip1";
            // 
            // qbfMenuQBF
            // 
            this.qbfMenuQBF.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qbfMenuQBFAccept,
            this.qbfMenuQBFCancel});
            this.qbfMenuQBF.Name = "qbfMenuQBF";
            this.qbfMenuQBF.Size = new System.Drawing.Size(41, 20);
            this.qbfMenuQBF.Text = "&QBF";
            // 
            // qbfMenuQBFAccept
            // 
            this.qbfMenuQBFAccept.Name = "qbfMenuQBFAccept";
            this.qbfMenuQBFAccept.Size = new System.Drawing.Size(111, 22);
            this.qbfMenuQBFAccept.Text = "&Accept";
            this.qbfMenuQBFAccept.Click += new System.EventHandler(this.qbfMenuQBFAccept_Click);
            // 
            // qbfMenuQBFCancel
            // 
            this.qbfMenuQBFCancel.Name = "qbfMenuQBFCancel";
            this.qbfMenuQBFCancel.Size = new System.Drawing.Size(111, 22);
            this.qbfMenuQBFCancel.Text = "&Cancel";
            this.qbfMenuQBFCancel.Click += new System.EventHandler(this.qbfMenuQBFCancel_Click);
            // 
            // qbfMenuHelp
            // 
            this.qbfMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qbfMenuHelpTutorial});
            this.qbfMenuHelp.Name = "qbfMenuHelp";
            this.qbfMenuHelp.Size = new System.Drawing.Size(44, 20);
            this.qbfMenuHelp.Text = "&Help";
            // 
            // qbfMenuHelpTutorial
            // 
            this.qbfMenuHelpTutorial.Name = "qbfMenuHelpTutorial";
            this.qbfMenuHelpTutorial.Size = new System.Drawing.Size(152, 22);
            this.qbfMenuHelpTutorial.Text = "&Tutorial";
            this.qbfMenuHelpTutorial.Click += new System.EventHandler(this.qbfMenuHelpTutorial_Click);
            // 
            // QueryBuilderForm
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(663, 511);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.qbfMenu);
            this.Name = "QueryBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QueryBuilderForm";
            this.Load += new System.EventHandler(this.QueryBuilderForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSQL.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.qbfMenu.ResumeLayout(false);
            this.qbfMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder plainTextSQLBuilder1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSQL;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder queryBuilder;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.MenuStrip qbfMenu;
        private System.Windows.Forms.ToolStripMenuItem qbfMenuQBF;
        private System.Windows.Forms.ToolStripMenuItem qbfMenuQBFAccept;
        private System.Windows.Forms.ToolStripMenuItem qbfMenuQBFCancel;
        private System.Windows.Forms.ToolStripMenuItem qbfMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem qbfMenuHelpTutorial;
        private System.Windows.Forms.HelpProvider qbfHelpProvider;
    }
}//end namespace

#pragma warning restore 1591


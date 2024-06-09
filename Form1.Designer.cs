namespace AppSmith {
  partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tvBuilder = new System.Windows.Forms.TreeView();
      this.msBuilder = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.columnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.apiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddMethodParamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.meToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddClassMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddPropertyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.props = new PropertyGridEx.PropertyGridEx();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tpInput = new System.Windows.Forms.TabPage();
      this.edInput = new FastColoredTextBoxNS.FastColoredTextBox();
      this.msInput = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.inputClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.inputCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.inputPasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.inputParseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tpSqlOut = new System.Windows.Forms.TabPage();
      this.edSQL = new FastColoredTextBoxNS.FastColoredTextBox();
      this.msOutput = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.tpCOut = new System.Windows.Forms.TabPage();
      this.edCSharp = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tpLog = new System.Windows.Forms.TabPage();
      this.edLogMsg = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.btnOpenClose = new System.Windows.Forms.Button();
      this.lbFocusedItem = new System.Windows.Forms.Label();
      this.pbMain = new System.Windows.Forms.ProgressBar();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.odMain = new System.Windows.Forms.OpenFileDialog();
      this.importAPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.msBuilder.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tpInput.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.edInput)).BeginInit();
      this.msInput.SuspendLayout();
      this.tpSqlOut.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.edSQL)).BeginInit();
      this.msOutput.SuspendLayout();
      this.tpCOut.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.edCSharp)).BeginInit();
      this.tpLog.SuspendLayout();
      this.panel1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(0, 79);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 3, 4, 3);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer1.Size = new System.Drawing.Size(818, 438);
      this.splitContainer1.SplitterDistance = 270;
      this.splitContainer1.SplitterWidth = 5;
      this.splitContainer1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tvBuilder);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.props);
      this.splitContainer2.Size = new System.Drawing.Size(270, 438);
      this.splitContainer2.SplitterDistance = 202;
      this.splitContainer2.SplitterWidth = 5;
      this.splitContainer2.TabIndex = 0;
      // 
      // tvBuilder
      // 
      this.tvBuilder.ContextMenuStrip = this.msBuilder;
      this.tvBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvBuilder.ImageIndex = 0;
      this.tvBuilder.ImageList = this.imageList1;
      this.tvBuilder.Location = new System.Drawing.Point(0, 0);
      this.tvBuilder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tvBuilder.Name = "tvBuilder";
      this.tvBuilder.SelectedImageIndex = 0;
      this.tvBuilder.Size = new System.Drawing.Size(270, 202);
      this.tvBuilder.TabIndex = 0;
      this.tvBuilder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvBuilder_BeforeExpand);
      this.tvBuilder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBuilder_AfterSelect);
      // 
      // msBuilder
      // 
      this.msBuilder.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.msBuilder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTemplateToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.importAPIToolStripMenuItem});
      this.msBuilder.Name = "contextMenuStrip1";
      this.msBuilder.Size = new System.Drawing.Size(181, 164);
      this.msBuilder.Opening += new System.ComponentModel.CancelEventHandler(this.msBuilder_Opening);
      // 
      // addTemplateToolStripMenuItem
      // 
      this.addTemplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddServerToolStripMenuItem,
            this.AddDatabaseToolStripMenuItem,
            this.AddTableToolStripMenuItem,
            this.columnToolStripMenuItem,
            this.apiToolStripMenuItem,
            this.controllerToolStripMenuItem,
            this.AddMethodParamMenuItem,
            this.meToolStripMenuItem,
            this.AddClassMenuItem,
            this.AddPropertyMenuItem});
      this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
      this.addTemplateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.addTemplateToolStripMenuItem.Text = "Add";
      this.addTemplateToolStripMenuItem.DropDownOpening += new System.EventHandler(this.addTemplateToolStripMenuItem_DropDownOpening);
      // 
      // AddServerToolStripMenuItem
      // 
      this.AddServerToolStripMenuItem.Name = "AddServerToolStripMenuItem";
      this.AddServerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddServerToolStripMenuItem.Text = "Server";
      this.AddServerToolStripMenuItem.Click += new System.EventHandler(this.AddServerToolStripMenuItem_Click);
      // 
      // AddDatabaseToolStripMenuItem
      // 
      this.AddDatabaseToolStripMenuItem.Name = "AddDatabaseToolStripMenuItem";
      this.AddDatabaseToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddDatabaseToolStripMenuItem.Text = "Database";
      this.AddDatabaseToolStripMenuItem.Click += new System.EventHandler(this.AddDatabaseToolStripMenuItem_Click);
      // 
      // AddTableToolStripMenuItem
      // 
      this.AddTableToolStripMenuItem.Name = "AddTableToolStripMenuItem";
      this.AddTableToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddTableToolStripMenuItem.Text = "Table";
      this.AddTableToolStripMenuItem.Click += new System.EventHandler(this.AddTableToolStripMenuItem_Click);
      // 
      // columnToolStripMenuItem
      // 
      this.columnToolStripMenuItem.Name = "columnToolStripMenuItem";
      this.columnToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.columnToolStripMenuItem.Text = "Column";
      this.columnToolStripMenuItem.Click += new System.EventHandler(this.columnToolStripMenuItem_Click);
      // 
      // apiToolStripMenuItem
      // 
      this.apiToolStripMenuItem.Name = "apiToolStripMenuItem";
      this.apiToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.apiToolStripMenuItem.Text = "Api";
      this.apiToolStripMenuItem.Click += new System.EventHandler(this.apiToolStripMenuItem_Click);
      // 
      // controllerToolStripMenuItem
      // 
      this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
      this.controllerToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.controllerToolStripMenuItem.Text = "Controller";
      this.controllerToolStripMenuItem.Click += new System.EventHandler(this.controllerToolStripMenuItem_Click);
      // 
      // AddMethodParamMenuItem
      // 
      this.AddMethodParamMenuItem.Name = "AddMethodParamMenuItem";
      this.AddMethodParamMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddMethodParamMenuItem.Text = "Method Param";
      this.AddMethodParamMenuItem.Click += new System.EventHandler(this.AddMethodParamMenuItem_Click);
      // 
      // meToolStripMenuItem
      // 
      this.meToolStripMenuItem.Name = "meToolStripMenuItem";
      this.meToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.meToolStripMenuItem.Text = "Method";
      this.meToolStripMenuItem.Click += new System.EventHandler(this.meToolStripMenuItem_Click);
      // 
      // AddClassMenuItem
      // 
      this.AddClassMenuItem.Name = "AddClassMenuItem";
      this.AddClassMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddClassMenuItem.Text = "Class";
      this.AddClassMenuItem.Click += new System.EventHandler(this.AddClassMenuItem_Click);
      // 
      // AddPropertyMenuItem
      // 
      this.AddPropertyMenuItem.Name = "AddPropertyMenuItem";
      this.AddPropertyMenuItem.Size = new System.Drawing.Size(153, 22);
      this.AddPropertyMenuItem.Text = "Property";
      this.AddPropertyMenuItem.Click += new System.EventHandler(this.AddPropertyMenuItem_Click);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // moveUpToolStripMenuItem
      // 
      this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
      this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.moveUpToolStripMenuItem.Text = "Move Up";
      this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
      // 
      // moveDownToolStripMenuItem
      // 
      this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
      this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.moveDownToolStripMenuItem.Text = "Move Down";
      this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.deleteToolStripMenuItem.Text = "Delete";
      this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "collection-710-16.png");
      this.imageList1.Images.SetKeyName(1, "server-38-16.png");
      this.imageList1.Images.SetKeyName(2, "server-53-16.png");
      this.imageList1.Images.SetKeyName(3, "folder-457-16.png");
      this.imageList1.Images.SetKeyName(4, "coupon-316-16.png");
      this.imageList1.Images.SetKeyName(5, "data-331-16.png");
      this.imageList1.Images.SetKeyName(6, "gift-551-16 (1).png");
      this.imageList1.Images.SetKeyName(7, "find-228-16.png");
      this.imageList1.Images.SetKeyName(8, "scanning-92-16.png");
      this.imageList1.Images.SetKeyName(9, "news-773-16.png");
      this.imageList1.Images.SetKeyName(10, "lookup-61-16.png");
      this.imageList1.Images.SetKeyName(11, "gift-551-16.png");
      this.imageList1.Images.SetKeyName(12, "add-50-16.png");
      this.imageList1.Images.SetKeyName(13, "play-277-16.png");
      this.imageList1.Images.SetKeyName(14, "stop-62-16.png");
      this.imageList1.Images.SetKeyName(15, "delete-1203-16.png");
      this.imageList1.Images.SetKeyName(16, "label-387-16.png");
      this.imageList1.Images.SetKeyName(17, "share-978-16.png");
      this.imageList1.Images.SetKeyName(18, "set-up-1014-16.png");
      this.imageList1.Images.SetKeyName(19, "flame-_1_.ico");
      // 
      // props
      // 
      // 
      // 
      // 
      this.props.DocCommentDescription.AutoEllipsis = true;
      this.props.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
      this.props.DocCommentDescription.Location = new System.Drawing.Point(4, 24);
      this.props.DocCommentDescription.Margin = new System.Windows.Forms.Padding(26, 0, 26, 0);
      this.props.DocCommentDescription.Name = "";
      this.props.DocCommentDescription.Size = new System.Drawing.Size(0, 43);
      this.props.DocCommentDescription.TabIndex = 1;
      this.props.DocCommentImage = null;
      // 
      // 
      // 
      this.props.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
      this.props.DocCommentTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.props.DocCommentTitle.Location = new System.Drawing.Point(4, 3);
      this.props.DocCommentTitle.Margin = new System.Windows.Forms.Padding(26, 0, 26, 0);
      this.props.DocCommentTitle.Name = "";
      this.props.DocCommentTitle.Size = new System.Drawing.Size(0, 18);
      this.props.DocCommentTitle.TabIndex = 0;
      this.props.DocCommentTitle.UseMnemonic = false;
      this.props.Dock = System.Windows.Forms.DockStyle.Fill;
      this.props.HelpVisible = false;
      this.props.Location = new System.Drawing.Point(0, 0);
      this.props.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.props.Name = "props";
      this.props.PropertySort = System.Windows.Forms.PropertySort.NoSort;
      this.props.Size = new System.Drawing.Size(270, 231);
      this.props.TabIndex = 1;
      // 
      // 
      // 
      this.props.ToolStrip.AccessibleName = "Property Grid";
      this.props.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
      this.props.ToolStrip.AllowMerge = false;
      this.props.ToolStrip.AutoSize = false;
      this.props.ToolStrip.CanOverflow = false;
      this.props.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.props.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.props.ToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.props.ToolStrip.Location = new System.Drawing.Point(0, 1);
      this.props.ToolStrip.Name = "";
      this.props.ToolStrip.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
      this.props.ToolStrip.Size = new System.Drawing.Size(270, 25);
      this.props.ToolStrip.TabIndex = 1;
      this.props.ToolStrip.TabStop = true;
      this.props.ToolStrip.Text = "PropertyGridToolBar";
      this.props.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.props_PropertyValueChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tpInput);
      this.tabControl1.Controls.Add(this.tpSqlOut);
      this.tabControl1.Controls.Add(this.tpCOut);
      this.tabControl1.Controls.Add(this.tpLog);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(543, 438);
      this.tabControl1.TabIndex = 0;
      // 
      // tpInput
      // 
      this.tpInput.Controls.Add(this.edInput);
      this.tpInput.Location = new System.Drawing.Point(4, 24);
      this.tpInput.Name = "tpInput";
      this.tpInput.Size = new System.Drawing.Size(535, 410);
      this.tpInput.TabIndex = 3;
      this.tpInput.Text = "Input";
      this.tpInput.UseVisualStyleBackColor = true;
      // 
      // edInput
      // 
      this.edInput.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.edInput.AutoIndentCharsPatterns = "";
      this.edInput.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.edInput.BackBrush = null;
      this.edInput.CharHeight = 14;
      this.edInput.CharWidth = 8;
      this.edInput.CommentPrefix = "--";
      this.edInput.ContextMenuStrip = this.msInput;
      this.edInput.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.edInput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.edInput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edInput.Font = new System.Drawing.Font("Courier New", 9.75F);
      this.edInput.IsReplaceMode = false;
      this.edInput.Language = FastColoredTextBoxNS.Language.SQL;
      this.edInput.LeftBracket = '(';
      this.edInput.Location = new System.Drawing.Point(0, 0);
      this.edInput.Name = "edInput";
      this.edInput.Paddings = new System.Windows.Forms.Padding(0);
      this.edInput.RightBracket = ')';
      this.edInput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.edInput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("edInput.ServiceColors")));
      this.edInput.Size = new System.Drawing.Size(535, 410);
      this.edInput.TabIndex = 2;
      this.edInput.Zoom = 100;
      // 
      // msInput
      // 
      this.msInput.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.msInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputClearToolStripMenuItem,
            this.inputCopyToolStripMenuItem,
            this.inputPasteToolStripMenuItem,
            this.inputParseToolStripMenuItem});
      this.msInput.Name = "msInput";
      this.msInput.Size = new System.Drawing.Size(127, 92);
      // 
      // inputClearToolStripMenuItem
      // 
      this.inputClearToolStripMenuItem.Name = "inputClearToolStripMenuItem";
      this.inputClearToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.inputClearToolStripMenuItem.Text = "Clear";
      this.inputClearToolStripMenuItem.Click += new System.EventHandler(this.inputClearToolStripMenuItem_Click);
      // 
      // inputCopyToolStripMenuItem
      // 
      this.inputCopyToolStripMenuItem.Name = "inputCopyToolStripMenuItem";
      this.inputCopyToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.inputCopyToolStripMenuItem.Text = "Copy";
      this.inputCopyToolStripMenuItem.Click += new System.EventHandler(this.inputCopyToolStripMenuItem_Click);
      // 
      // inputPasteToolStripMenuItem
      // 
      this.inputPasteToolStripMenuItem.Name = "inputPasteToolStripMenuItem";
      this.inputPasteToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.inputPasteToolStripMenuItem.Text = "Paste";
      this.inputPasteToolStripMenuItem.Click += new System.EventHandler(this.inputPasteToolStripMenuItem_Click);
      // 
      // inputParseToolStripMenuItem
      // 
      this.inputParseToolStripMenuItem.Name = "inputParseToolStripMenuItem";
      this.inputParseToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.inputParseToolStripMenuItem.Text = "Parse SQL";
      this.inputParseToolStripMenuItem.Click += new System.EventHandler(this.inputParseToolStripMenuItem_Click);
      // 
      // tpSqlOut
      // 
      this.tpSqlOut.Controls.Add(this.edSQL);
      this.tpSqlOut.Location = new System.Drawing.Point(4, 22);
      this.tpSqlOut.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tpSqlOut.Name = "tpSqlOut";
      this.tpSqlOut.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tpSqlOut.Size = new System.Drawing.Size(535, 412);
      this.tpSqlOut.TabIndex = 1;
      this.tpSqlOut.Text = "Sql Out";
      this.tpSqlOut.UseVisualStyleBackColor = true;
      // 
      // edSQL
      // 
      this.edSQL.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.edSQL.AutoIndentCharsPatterns = "";
      this.edSQL.AutoScrollMinSize = new System.Drawing.Size(2, 14);
      this.edSQL.BackBrush = null;
      this.edSQL.CharHeight = 14;
      this.edSQL.CharWidth = 8;
      this.edSQL.CommentPrefix = "--";
      this.edSQL.ContextMenuStrip = this.msOutput;
      this.edSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.edSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.edSQL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edSQL.Font = new System.Drawing.Font("Courier New", 9.75F);
      this.edSQL.IsReplaceMode = false;
      this.edSQL.Language = FastColoredTextBoxNS.Language.SQL;
      this.edSQL.LeftBracket = '(';
      this.edSQL.Location = new System.Drawing.Point(4, 3);
      this.edSQL.Name = "edSQL";
      this.edSQL.Paddings = new System.Windows.Forms.Padding(0);
      this.edSQL.RightBracket = ')';
      this.edSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.edSQL.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("edSQL.ServiceColors")));
      this.edSQL.Size = new System.Drawing.Size(527, 406);
      this.edSQL.TabIndex = 1;
      this.edSQL.Zoom = 100;
      // 
      // msOutput
      // 
      this.msOutput.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.msOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
      this.msOutput.Name = "msInput";
      this.msOutput.Size = new System.Drawing.Size(103, 26);
      this.msOutput.Click += new System.EventHandler(this.msOutput_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(102, 22);
      this.toolStripMenuItem3.Text = "Copy";
      this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
      // 
      // tpCOut
      // 
      this.tpCOut.Controls.Add(this.edCSharp);
      this.tpCOut.Location = new System.Drawing.Point(4, 22);
      this.tpCOut.Name = "tpCOut";
      this.tpCOut.Size = new System.Drawing.Size(535, 412);
      this.tpCOut.TabIndex = 2;
      this.tpCOut.Text = "C# Out";
      this.tpCOut.UseVisualStyleBackColor = true;
      // 
      // edCSharp
      // 
      this.edCSharp.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.edCSharp.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
      this.edCSharp.AutoScrollMinSize = new System.Drawing.Size(2, 14);
      this.edCSharp.BackBrush = null;
      this.edCSharp.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
      this.edCSharp.CharHeight = 14;
      this.edCSharp.CharWidth = 8;
      this.edCSharp.ContextMenuStrip = this.msOutput;
      this.edCSharp.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.edCSharp.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.edCSharp.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edCSharp.Font = new System.Drawing.Font("Courier New", 9.75F);
      this.edCSharp.IsReplaceMode = false;
      this.edCSharp.Language = FastColoredTextBoxNS.Language.CSharp;
      this.edCSharp.LeftBracket = '(';
      this.edCSharp.LeftBracket2 = '{';
      this.edCSharp.Location = new System.Drawing.Point(0, 0);
      this.edCSharp.Name = "edCSharp";
      this.edCSharp.Paddings = new System.Windows.Forms.Padding(0);
      this.edCSharp.RightBracket = ')';
      this.edCSharp.RightBracket2 = '}';
      this.edCSharp.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.edCSharp.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("edCSharp.ServiceColors")));
      this.edCSharp.Size = new System.Drawing.Size(535, 412);
      this.edCSharp.TabIndex = 1;
      this.edCSharp.Zoom = 100;
      // 
      // tpLog
      // 
      this.tpLog.Controls.Add(this.edLogMsg);
      this.tpLog.Location = new System.Drawing.Point(4, 22);
      this.tpLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tpLog.Name = "tpLog";
      this.tpLog.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.tpLog.Size = new System.Drawing.Size(535, 412);
      this.tpLog.TabIndex = 0;
      this.tpLog.Text = "Log";
      this.tpLog.UseVisualStyleBackColor = true;
      // 
      // edLogMsg
      // 
      this.edLogMsg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edLogMsg.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.edLogMsg.Location = new System.Drawing.Point(4, 3);
      this.edLogMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.edLogMsg.Multiline = true;
      this.edLogMsg.Name = "edLogMsg";
      this.edLogMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.edLogMsg.Size = new System.Drawing.Size(527, 406);
      this.edLogMsg.TabIndex = 3;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.comboBox1);
      this.panel1.Controls.Add(this.btnOpenClose);
      this.panel1.Controls.Add(this.lbFocusedItem);
      this.panel1.Controls.Add(this.pbMain);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 24);
      this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(818, 39);
      this.panel1.TabIndex = 1;
      // 
      // comboBox1
      // 
      this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(79, 7);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(662, 23);
      this.comboBox1.TabIndex = 16;
      // 
      // btnOpenClose
      // 
      this.btnOpenClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOpenClose.Location = new System.Drawing.Point(747, 7);
      this.btnOpenClose.Name = "btnOpenClose";
      this.btnOpenClose.Size = new System.Drawing.Size(59, 26);
      this.btnOpenClose.TabIndex = 14;
      this.btnOpenClose.Text = "Open";
      this.btnOpenClose.UseVisualStyleBackColor = true;
      this.btnOpenClose.Click += new System.EventHandler(this.btnOpenClose_Click);
      // 
      // lbFocusedItem
      // 
      this.lbFocusedItem.AutoSize = true;
      this.lbFocusedItem.Location = new System.Drawing.Point(11, 10);
      this.lbFocusedItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbFocusedItem.Name = "lbFocusedItem";
      this.lbFocusedItem.Size = new System.Drawing.Size(0, 15);
      this.lbFocusedItem.TabIndex = 13;
      // 
      // pbMain
      // 
      this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pbMain.ForeColor = System.Drawing.Color.Blue;
      this.pbMain.Location = new System.Drawing.Point(16, 36);
      this.pbMain.Maximum = 10000;
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(790, 10);
      this.pbMain.TabIndex = 12;
      this.pbMain.Value = 500;
      this.pbMain.Visible = false;
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(818, 24);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStripMenuItem,
            this.closeStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
      // 
      // openStripMenuItem
      // 
      this.openStripMenuItem.Name = "openStripMenuItem";
      this.openStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.openStripMenuItem.Text = "Open";
      this.openStripMenuItem.Click += new System.EventHandler(this.openStripMenuItem_Click);
      // 
      // closeStripMenuItem
      // 
      this.closeStripMenuItem.Name = "closeStripMenuItem";
      this.closeStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.closeStripMenuItem.Text = "Close";
      this.closeStripMenuItem.Click += new System.EventHandler(this.closeStripMenuItem_Click);
      // 
      // odMain
      // 
      this.odMain.CheckFileExists = false;
      this.odMain.DefaultExt = "asm";
      this.odMain.Filter = "AppSmithModel|*.asm";
      this.odMain.Title = "Open Archive";
      // 
      // importAPIToolStripMenuItem
      // 
      this.importAPIToolStripMenuItem.Name = "importAPIToolStripMenuItem";
      this.importAPIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.importAPIToolStripMenuItem.Text = "Import API";
      this.importAPIToolStripMenuItem.Click += new System.EventHandler(this.importAPIToolStripMenuItem_ClickAsync);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(818, 517);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "Form1";
      this.Text = "AppSmith";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.Resize += new System.EventHandler(this.Form1_Resize);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.msBuilder.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tpInput.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.edInput)).EndInit();
      this.msInput.ResumeLayout(false);
      this.tpSqlOut.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.edSQL)).EndInit();
      this.msOutput.ResumeLayout(false);
      this.tpCOut.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.edCSharp)).EndInit();
      this.tpLog.ResumeLayout(false);
      this.tpLog.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.TreeView tvBuilder;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpLog;
    private System.Windows.Forms.TabPage tpSqlOut;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox edLogMsg;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeStripMenuItem;
    private System.Windows.Forms.OpenFileDialog odMain;
    private System.Windows.Forms.ProgressBar pbMain;
    private System.Windows.Forms.ContextMenuStrip msBuilder;
    private System.Windows.Forms.ToolStripMenuItem addTemplateToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AddServerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AddDatabaseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AddTableToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private PropertyGridEx.PropertyGridEx props;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolStripMenuItem columnToolStripMenuItem;
    private FastColoredTextBoxNS.FastColoredTextBox edSQL;
    private System.Windows.Forms.TabPage tpCOut;
    private FastColoredTextBoxNS.FastColoredTextBox edCSharp;
    private System.Windows.Forms.TabPage tpInput;
    private FastColoredTextBoxNS.FastColoredTextBox edInput;
    private System.Windows.Forms.Label lbFocusedItem;
    private System.Windows.Forms.ContextMenuStrip msInput;
    private System.Windows.Forms.ToolStripMenuItem inputParseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem inputPasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem inputClearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem inputCopyToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip msOutput;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem apiToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem meToolStripMenuItem;
    private System.Windows.Forms.Button btnOpenClose;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ToolStripMenuItem AddMethodParamMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AddClassMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AddPropertyMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importAPIToolStripMenuItem;
  }
}


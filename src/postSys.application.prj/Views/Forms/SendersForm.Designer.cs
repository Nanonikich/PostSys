namespace PostSys.Application.Views.Forms;

partial class SendersForm
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
		if(disposing && (components != null))
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendersForm));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this._btnAdd = new System.Windows.Forms.ToolStripButton();
		this._btnEdit = new System.Windows.Forms.ToolStripButton();
		this._btnDelete = new System.Windows.Forms.ToolStripButton();
		this._txtSearchSender = new System.Windows.Forms.ToolStripTextBox();
		this._lblSearch = new System.Windows.Forms.ToolStripLabel();
		this._dgvSenders = new System.Windows.Forms.DataGridView();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this._dgvSenders)).BeginInit();
		this.SuspendLayout();
		// 
		// toolStrip1
		// 
		this.toolStrip1.AutoSize = false;
		this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnAdd,
            this._btnEdit,
            this._btnDelete,
            this._txtSearchSender,
            this._lblSearch});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(800, 51);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		// 
		// _btnAdd
		// 
		this._btnAdd.AutoSize = false;
		this._btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("_btnAdd.Image")));
		this._btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this._btnAdd.Name = "_btnAdd";
		this._btnAdd.Size = new System.Drawing.Size(40, 48);
		this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
		// 
		// _btnEdit
		// 
		this._btnEdit.AutoSize = false;
		this._btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("_btnEdit.Image")));
		this._btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
		this._btnEdit.Name = "_btnEdit";
		this._btnEdit.Size = new System.Drawing.Size(40, 48);
		this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
		// 
		// _btnDelete
		// 
		this._btnDelete.AutoSize = false;
		this._btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this._btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("_btnDelete.Image")));
		this._btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
		this._btnDelete.Name = "_btnDelete";
		this._btnDelete.Size = new System.Drawing.Size(40, 48);
		this._btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
		// 
		// _txtSearchSender
		// 
		this._txtSearchSender.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this._txtSearchSender.AutoSize = false;
		this._txtSearchSender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this._txtSearchSender.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSearchSender.Name = "_txtSearchSender";
		this._txtSearchSender.Size = new System.Drawing.Size(160, 26);
		this._txtSearchSender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchSenderKeyDown);
		this._txtSearchSender.TextChanged += new System.EventHandler(this.OnSearchSenderTextChanged);
		// 
		// _lblSearch
		// 
		this._lblSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this._lblSearch.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._lblSearch.Name = "_lblSearch";
		this._lblSearch.Size = new System.Drawing.Size(132, 48);
		this._lblSearch.Text = "Фамилия отправителя:";
		// 
		// _dgvSenders
		// 
		this._dgvSenders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this._dgvSenders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this._dgvSenders.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this._dgvSenders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this._dgvSenders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this._dgvSenders.Location = new System.Drawing.Point(0, 54);
		this._dgvSenders.MultiSelect = false;
		this._dgvSenders.Name = "_dgvSenders";
		this._dgvSenders.RowHeadersVisible = false;
		this._dgvSenders.RowTemplate.Height = 25;
		this._dgvSenders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this._dgvSenders.Size = new System.Drawing.Size(800, 397);
		this._dgvSenders.TabIndex = 1;
		// 
		// SendersForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(800, 450);
		this.Controls.Add(this._dgvSenders);
		this.Controls.Add(this.toolStrip1);
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "SendersForm";
		this.Text = "Отправители";
		this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.Load += new System.EventHandler(this.SendersFormLoad);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this._dgvSenders)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private ToolStrip toolStrip1;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private DataGridView _dgvSenders;
	private ToolStripTextBox _txtSearchSender;
	private ToolStripLabel _lblSearch;
}
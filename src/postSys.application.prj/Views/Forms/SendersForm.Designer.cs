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
		toolStrip1 = new ToolStrip();
		_btnAdd = new ToolStripButton();
		_btnEdit = new ToolStripButton();
		_btnDelete = new ToolStripButton();
		_txtSearchSender = new ToolStripTextBox();
		_lblSearch = new ToolStripLabel();
		_pnlSenders = new Panel();
		toolStrip1.SuspendLayout();
		SuspendLayout();
		// 
		// toolStrip1
		// 
		toolStrip1.AutoSize = false;
		toolStrip1.BackColor = SystemColors.ActiveCaption;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { _btnAdd, _btnEdit, _btnDelete, _txtSearchSender, _lblSearch });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 51);
		toolStrip1.TabIndex = 0;
		toolStrip1.Text = "toolStrip1";
		// 
		// _btnAdd
		// 
		_btnAdd.AutoSize = false;
		_btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnAdd.Image = (Image)resources.GetObject("_btnAdd.Image");
		_btnAdd.ImageTransparentColor = Color.Magenta;
		_btnAdd.Name = "_btnAdd";
		_btnAdd.Size = new Size(40, 48);
		_btnAdd.Click += OnAddClick;
		// 
		// _btnEdit
		// 
		_btnEdit.AutoSize = false;
		_btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnEdit.Image = (Image)resources.GetObject("_btnEdit.Image");
		_btnEdit.ImageTransparentColor = Color.Magenta;
		_btnEdit.Name = "_btnEdit";
		_btnEdit.Size = new Size(40, 48);
		_btnEdit.Click += OnEditClick;
		// 
		// _btnDelete
		// 
		_btnDelete.AutoSize = false;
		_btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnDelete.Image = (Image)resources.GetObject("_btnDelete.Image");
		_btnDelete.ImageTransparentColor = Color.Magenta;
		_btnDelete.Name = "_btnDelete";
		_btnDelete.Size = new Size(40, 48);
		_btnDelete.Click += OnDeleteClick;
		// 
		// _txtSearchSender
		// 
		_txtSearchSender.Alignment = ToolStripItemAlignment.Right;
		_txtSearchSender.AutoSize = false;
		_txtSearchSender.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchSender.Font = new Font("Times New Roman", 12F);
		_txtSearchSender.Name = "_txtSearchSender";
		_txtSearchSender.Size = new Size(160, 26);
		_txtSearchSender.KeyDown += OnSearchSenderKeyDown;
		_txtSearchSender.TextChanged += OnSearchSenderTextChanged;
		// 
		// _lblSearch
		// 
		_lblSearch.Alignment = ToolStripItemAlignment.Right;
		_lblSearch.Font = new Font("Times New Roman", 9.75F);
		_lblSearch.Name = "_lblSearch";
		_lblSearch.Size = new Size(132, 48);
		_lblSearch.Text = "Фамилия отправителя:";
		// 
		// _pnlSenders
		// 
		_pnlSenders.Dock = DockStyle.Fill;
		_pnlSenders.Location = new Point(0, 51);
		_pnlSenders.Name = "_pnlSenders";
		_pnlSenders.Size = new Size(800, 399);
		_pnlSenders.TabIndex = 1;
		// 
		// SendersForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(_pnlSenders);
		Controls.Add(toolStrip1);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "SendersForm";
		Text = "Отправители";
		WindowState = FormWindowState.Maximized;
		FormClosed += OnSendersFormClosed;
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private ToolStrip toolStrip1;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private ToolStripTextBox _txtSearchSender;
	private ToolStripLabel _lblSearch;
	private Panel _pnlSenders;
}
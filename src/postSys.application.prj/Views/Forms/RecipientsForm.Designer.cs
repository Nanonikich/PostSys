namespace PostSys.Application.Views.Forms;

partial class RecipientsForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipientsForm));
		toolStrip1 = new ToolStrip();
		_btnAdd = new ToolStripButton();
		_btnEdit = new ToolStripButton();
		_btnDelete = new ToolStripButton();
		_txtSearchSender = new ToolStripTextBox();
		_lblSender = new ToolStripLabel();
		_txtSearchRecipient = new ToolStripTextBox();
		_lblRecipient = new ToolStripLabel();
		_pnlRecipients = new Panel();
		toolStrip1.SuspendLayout();
		SuspendLayout();
		// 
		// toolStrip1
		// 
		toolStrip1.AutoSize = false;
		toolStrip1.BackColor = SystemColors.ActiveCaption;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { _btnAdd, _btnEdit, _btnDelete, _txtSearchSender, _lblSender, _txtSearchRecipient, _lblRecipient });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 51);
		toolStrip1.TabIndex = 1;
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
		_txtSearchSender.KeyDown += OnTxtBoxesKeyDown;
		_txtSearchSender.TextChanged += OnSearchSenderOrRecipientTextChanged;
		// 
		// _lblSender
		// 
		_lblSender.Alignment = ToolStripItemAlignment.Right;
		_lblSender.Font = new Font("Times New Roman", 9.75F);
		_lblSender.Name = "_lblSender";
		_lblSender.Size = new Size(80, 48);
		_lblSender.Text = "Отправитель:";
		// 
		// _txtSearchRecipient
		// 
		_txtSearchRecipient.Alignment = ToolStripItemAlignment.Right;
		_txtSearchRecipient.AutoSize = false;
		_txtSearchRecipient.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchRecipient.Font = new Font("Times New Roman", 12F);
		_txtSearchRecipient.Name = "_txtSearchRecipient";
		_txtSearchRecipient.Size = new Size(160, 26);
		_txtSearchRecipient.KeyDown += OnTxtBoxesKeyDown;
		_txtSearchRecipient.TextChanged += OnSearchSenderOrRecipientTextChanged;
		// 
		// _lblRecipient
		// 
		_lblRecipient.Alignment = ToolStripItemAlignment.Right;
		_lblRecipient.Font = new Font("Times New Roman", 9.75F);
		_lblRecipient.Name = "_lblRecipient";
		_lblRecipient.Size = new Size(75, 48);
		_lblRecipient.Text = "Получатель:";
		// 
		// _pnlRecipients
		// 
		_pnlRecipients.Dock = DockStyle.Fill;
		_pnlRecipients.Location = new Point(0, 51);
		_pnlRecipients.Name = "_pnlRecipients";
		_pnlRecipients.Size = new Size(800, 399);
		_pnlRecipients.TabIndex = 2;
		// 
		// RecipientsForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(_pnlRecipients);
		Controls.Add(toolStrip1);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "RecipientsForm";
		Text = "Получатели";
		WindowState = FormWindowState.Maximized;
		FormClosed += OnRecipientsFormClosed;
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion
	private ToolStrip toolStrip1;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private ToolStripTextBox _txtSearchRecipient;
	private ToolStripLabel _lblSender;
	private ToolStripTextBox _txtSearchSender;
	private ToolStripLabel _lblRecipient;
	private Panel _pnlRecipients;
}
namespace PostSys.Application.Views.Forms;

partial class UsersForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
		toolStrip1 = new ToolStrip();
		_btnAdd = new ToolStripButton();
		_btnEdit = new ToolStripButton();
		_btnDelete = new ToolStripButton();
		_txtSearchUser = new ToolStripTextBox();
		_lblSearch = new ToolStripLabel();
		_pnlUsers = new Panel();
		toolStrip1.SuspendLayout();
		SuspendLayout();
		// 
		// toolStrip1
		// 
		toolStrip1.AutoSize = false;
		toolStrip1.BackColor = SystemColors.ActiveCaption;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { _btnAdd, _btnEdit, _btnDelete, _txtSearchUser, _lblSearch });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 50);
		toolStrip1.TabIndex = 0;
		toolStrip1.Text = "toolStrip1";
		// 
		// _btnAdd
		// 
		_btnAdd.AutoSize = false;
		_btnAdd.BackgroundImageLayout = ImageLayout.None;
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
		_btnDelete.Text = "Delete";
		_btnDelete.Click += OnDeleteClick;
		// 
		// _txtSearchUser
		// 
		_txtSearchUser.Alignment = ToolStripItemAlignment.Right;
		_txtSearchUser.AutoSize = false;
		_txtSearchUser.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchUser.Font = new Font("Times New Roman", 12F);
		_txtSearchUser.Name = "_txtSearchUser";
		_txtSearchUser.Size = new Size(160, 26);
		_txtSearchUser.KeyDown += OnSearchUserKeyDown;
		_txtSearchUser.TextChanged += OnSearchUserTextChanged;
		// 
		// _lblSearch
		// 
		_lblSearch.Alignment = ToolStripItemAlignment.Right;
		_lblSearch.Font = new Font("Times New Roman", 9.75F);
		_lblSearch.Name = "_lblSearch";
		_lblSearch.Size = new Size(116, 47);
		_lblSearch.Text = "Поиск по фамилии:";
		// 
		// _pnlUsers
		// 
		_pnlUsers.Dock = DockStyle.Fill;
		_pnlUsers.Location = new Point(0, 50);
		_pnlUsers.Name = "_pnlUsers";
		_pnlUsers.Size = new Size(800, 400);
		_pnlUsers.TabIndex = 1;
		// 
		// UsersForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(_pnlUsers);
		Controls.Add(toolStrip1);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "UsersForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Пользователи";
		WindowState = FormWindowState.Maximized;
		FormClosed += OnUserFormClosed;
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private ToolStrip toolStrip1;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private ToolStripTextBox _txtSearchUser;
	private ToolStripLabel _lblSearch;
	private Panel _pnlUsers;
}
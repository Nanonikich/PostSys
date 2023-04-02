namespace PostSys.Application.Views.Forms;

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
		components = new System.ComponentModel.Container();
		DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		_dgvAddresses = new DataGridView();
		toolStrip1 = new ToolStrip();
		_btnAdd = new ToolStripButton();
		_btnEdit = new ToolStripButton();
		_btnDelete = new ToolStripButton();
		_txtSearchPostmen = new ToolStripTextBox();
		_lblSearchPostmen = new ToolStripLabel();
		_txtSearchRecipient = new ToolStripTextBox();
		_lblSearchRecipient = new ToolStripLabel();
		toolStrip2 = new ToolStrip();
		_btnPostmens = new ToolStripButton();
		_btnSenders = new ToolStripButton();
		_btnRecipients = new ToolStripButton();
		_btnPlots = new ToolStripButton();
		_btnStreetCity = new ToolStripButton();
		_btnUsers = new ToolStripButton();
		timer = new System.Windows.Forms.Timer(components);
		((System.ComponentModel.ISupportInitialize)_dgvAddresses).BeginInit();
		toolStrip1.SuspendLayout();
		toolStrip2.SuspendLayout();
		SuspendLayout();
		// 
		// _dgvAddresses
		// 
		_dgvAddresses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		_dgvAddresses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		_dgvAddresses.BackgroundColor = SystemColors.Control;
		dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = SystemColors.Control;
		dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
		dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
		_dgvAddresses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		_dgvAddresses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = SystemColors.Window;
		dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
		dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
		_dgvAddresses.DefaultCellStyle = dataGridViewCellStyle2;
		_dgvAddresses.Location = new Point(129, 50);
		_dgvAddresses.MultiSelect = false;
		_dgvAddresses.Name = "_dgvAddresses";
		_dgvAddresses.RowHeadersVisible = false;
		_dgvAddresses.RowTemplate.Height = 25;
		_dgvAddresses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		_dgvAddresses.Size = new Size(671, 400);
		_dgvAddresses.TabIndex = 0;
		_dgvAddresses.TabStop = false;
		// 
		// toolStrip1
		// 
		toolStrip1.AutoSize = false;
		toolStrip1.BackColor = SystemColors.ActiveCaption;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { _btnAdd, _btnEdit, _btnDelete, _txtSearchPostmen, _lblSearchPostmen, _txtSearchRecipient, _lblSearchRecipient });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 50);
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
		// _txtSearchPostmen
		// 
		_txtSearchPostmen.Alignment = ToolStripItemAlignment.Right;
		_txtSearchPostmen.AutoSize = false;
		_txtSearchPostmen.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchPostmen.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_txtSearchPostmen.Name = "_txtSearchPostmen";
		_txtSearchPostmen.Size = new Size(160, 26);
		_txtSearchPostmen.KeyDown += OnTxtBoxesKeyDown;
		_txtSearchPostmen.TextChanged += OnSearchRecipientOrPostmenTextChanged;
		// 
		// _lblSearchPostmen
		// 
		_lblSearchPostmen.Alignment = ToolStripItemAlignment.Right;
		_lblSearchPostmen.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
		_lblSearchPostmen.Name = "_lblSearchPostmen";
		_lblSearchPostmen.Size = new Size(70, 47);
		_lblSearchPostmen.Text = "Почтальон:";
		// 
		// _txtSearchRecipient
		// 
		_txtSearchRecipient.Alignment = ToolStripItemAlignment.Right;
		_txtSearchRecipient.AutoSize = false;
		_txtSearchRecipient.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchRecipient.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_txtSearchRecipient.Name = "_txtSearchRecipient";
		_txtSearchRecipient.Size = new Size(160, 26);
		_txtSearchRecipient.KeyDown += OnTxtBoxesKeyDown;
		_txtSearchRecipient.TextChanged += OnSearchRecipientOrPostmenTextChanged;
		// 
		// _lblSearchRecipient
		// 
		_lblSearchRecipient.Alignment = ToolStripItemAlignment.Right;
		_lblSearchRecipient.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
		_lblSearchRecipient.Name = "_lblSearchRecipient";
		_lblSearchRecipient.Size = new Size(75, 47);
		_lblSearchRecipient.Text = "Получатель:";
		// 
		// toolStrip2
		// 
		toolStrip2.AutoSize = false;
		toolStrip2.BackColor = SystemColors.ActiveCaption;
		toolStrip2.Dock = DockStyle.Left;
		toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip2.Items.AddRange(new ToolStripItem[] { _btnPostmens, _btnSenders, _btnRecipients, _btnPlots, _btnStreetCity, _btnUsers });
		toolStrip2.Location = new Point(0, 50);
		toolStrip2.Name = "toolStrip2";
		toolStrip2.Size = new Size(129, 400);
		toolStrip2.TabIndex = 2;
		toolStrip2.Text = "toolStrip2";
		// 
		// _btnPostmens
		// 
		_btnPostmens.AutoSize = false;
		_btnPostmens.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnPostmens.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnPostmens.ForeColor = Color.Black;
		_btnPostmens.ImageTransparentColor = Color.Magenta;
		_btnPostmens.Name = "_btnPostmens";
		_btnPostmens.Size = new Size(123, 35);
		_btnPostmens.Text = "Почтальоны";
		_btnPostmens.Click += OnPostmensClick;
		// 
		// _btnSenders
		// 
		_btnSenders.AutoSize = false;
		_btnSenders.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnSenders.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnSenders.ForeColor = Color.Black;
		_btnSenders.Image = (Image)resources.GetObject("_btnSenders.Image");
		_btnSenders.ImageTransparentColor = Color.Magenta;
		_btnSenders.Name = "_btnSenders";
		_btnSenders.Size = new Size(123, 35);
		_btnSenders.Text = "Отправители";
		_btnSenders.Click += OnSendersClick;
		// 
		// _btnRecipients
		// 
		_btnRecipients.AutoSize = false;
		_btnRecipients.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnRecipients.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnRecipients.ForeColor = Color.Black;
		_btnRecipients.ImageTransparentColor = Color.Magenta;
		_btnRecipients.Name = "_btnRecipients";
		_btnRecipients.Size = new Size(123, 35);
		_btnRecipients.Text = "Получатели";
		_btnRecipients.Click += OnClientsClick;
		// 
		// _btnPlots
		// 
		_btnPlots.AutoSize = false;
		_btnPlots.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnPlots.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnPlots.ForeColor = Color.Black;
		_btnPlots.ImageTransparentColor = Color.Magenta;
		_btnPlots.Name = "_btnPlots";
		_btnPlots.Size = new Size(123, 35);
		_btnPlots.Text = "Участки";
		_btnPlots.Click += OnPlotsClick;
		// 
		// _btnStreetCity
		// 
		_btnStreetCity.AutoSize = false;
		_btnStreetCity.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnStreetCity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnStreetCity.ForeColor = Color.Black;
		_btnStreetCity.Image = (Image)resources.GetObject("_btnStreetCity.Image");
		_btnStreetCity.ImageTransparentColor = Color.Magenta;
		_btnStreetCity.Name = "_btnStreetCity";
		_btnStreetCity.Size = new Size(123, 35);
		_btnStreetCity.Text = "Улица / Город";
		_btnStreetCity.Click += OnCitiesClick;
		// 
		// _btnUsers
		// 
		_btnUsers.AutoSize = false;
		_btnUsers.DisplayStyle = ToolStripItemDisplayStyle.Text;
		_btnUsers.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnUsers.Image = (Image)resources.GetObject("_btnUsers.Image");
		_btnUsers.ImageTransparentColor = Color.Magenta;
		_btnUsers.Name = "_btnUsers";
		_btnUsers.Size = new Size(123, 35);
		_btnUsers.Text = "Пользователи";
		_btnUsers.Click += OnUsersClick;
		// 
		// timer
		// 
		timer.Interval = 2000;
		timer.Tick += OnTimerTick;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(toolStrip2);
		Controls.Add(_dgvAddresses);
		Controls.Add(toolStrip1);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Адреса";
		WindowState = FormWindowState.Maximized;
		FormClosed += MainFormClosed;
		Load += MainFormLoad;
		((System.ComponentModel.ISupportInitialize)_dgvAddresses).EndInit();
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		toolStrip2.ResumeLayout(false);
		toolStrip2.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private DataGridView _dgvAddresses;
	private ToolStrip toolStrip1;
	private ToolStrip toolStrip2;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private ToolStripButton _btnPostmens;
	private ToolStripButton _btnRecipients;
	private ToolStripButton _btnPlots;
	private ToolStripButton _btnStreetCity;
	private ToolStripButton _btnSenders;
	private System.Windows.Forms.Timer timer;
	private ToolStripButton _btnUsers;
	private ToolStripTextBox _txtSearchPostmen;
	private ToolStripTextBox _txtSearchRecipient;
	private ToolStripLabel _lblSearchRecipient;
	private ToolStripLabel _lblSearchPostmen;
}
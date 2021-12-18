namespace postSys.application.prj.Views.Forms
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._dgvAddresses = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this._btnAdd = new System.Windows.Forms.ToolStripButton();
			this._btnEdit = new System.Windows.Forms.ToolStripButton();
			this._btnDelete = new System.Windows.Forms.ToolStripButton();
			this._txtSearchPostmen = new System.Windows.Forms.ToolStripTextBox();
			this._lblSearchPostmen = new System.Windows.Forms.ToolStripLabel();
			this._txtSearchRecipient = new System.Windows.Forms.ToolStripTextBox();
			this._lblSearchRecipient = new System.Windows.Forms.ToolStripLabel();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this._btnPostmens = new System.Windows.Forms.ToolStripButton();
			this._btnSenders = new System.Windows.Forms.ToolStripButton();
			this._btnRecipients = new System.Windows.Forms.ToolStripButton();
			this._btnPlots = new System.Windows.Forms.ToolStripButton();
			this._btnStreetCity = new System.Windows.Forms.ToolStripButton();
			this._btnUsers = new System.Windows.Forms.ToolStripButton();
			this.timer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this._dgvAddresses)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// _dgvAddresses
			// 
			this._dgvAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvAddresses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvAddresses.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvAddresses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this._dgvAddresses.DefaultCellStyle = dataGridViewCellStyle2;
			this._dgvAddresses.Location = new System.Drawing.Point(129, 50);
			this._dgvAddresses.MultiSelect = false;
			this._dgvAddresses.Name = "_dgvAddresses";
			this._dgvAddresses.RowHeadersVisible = false;
			this._dgvAddresses.RowTemplate.Height = 25;
			this._dgvAddresses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvAddresses.Size = new System.Drawing.Size(671, 400);
			this._dgvAddresses.TabIndex = 0;
			this._dgvAddresses.TabStop = false;
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
            this._txtSearchPostmen,
            this._lblSearchPostmen,
            this._txtSearchRecipient,
            this._lblSearchRecipient});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(800, 50);
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
			// _txtSearchPostmen
			// 
			this._txtSearchPostmen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._txtSearchPostmen.AutoSize = false;
			this._txtSearchPostmen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtSearchPostmen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtSearchPostmen.Name = "_txtSearchPostmen";
			this._txtSearchPostmen.Size = new System.Drawing.Size(160, 26);
			this._txtSearchPostmen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTxtBoxesKeyDown);
			this._txtSearchPostmen.TextChanged += new System.EventHandler(this.OnSearchRecipientOrPostmenTextChanged);
			// 
			// _lblSearchPostmen
			// 
			this._lblSearchPostmen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._lblSearchPostmen.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._lblSearchPostmen.Name = "_lblSearchPostmen";
			this._lblSearchPostmen.Size = new System.Drawing.Size(70, 47);
			this._lblSearchPostmen.Text = "Почтальон:";
			// 
			// _txtSearchRecipient
			// 
			this._txtSearchRecipient.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._txtSearchRecipient.AutoSize = false;
			this._txtSearchRecipient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtSearchRecipient.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtSearchRecipient.Name = "_txtSearchRecipient";
			this._txtSearchRecipient.Size = new System.Drawing.Size(160, 26);
			this._txtSearchRecipient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTxtBoxesKeyDown);
			this._txtSearchRecipient.TextChanged += new System.EventHandler(this.OnSearchRecipientOrPostmenTextChanged);
			// 
			// _lblSearchRecipient
			// 
			this._lblSearchRecipient.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._lblSearchRecipient.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._lblSearchRecipient.Name = "_lblSearchRecipient";
			this._lblSearchRecipient.Size = new System.Drawing.Size(75, 47);
			this._lblSearchRecipient.Text = "Получатель:";
			// 
			// toolStrip2
			// 
			this.toolStrip2.AutoSize = false;
			this.toolStrip2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnPostmens,
            this._btnSenders,
            this._btnRecipients,
            this._btnPlots,
            this._btnStreetCity,
            this._btnUsers});
			this.toolStrip2.Location = new System.Drawing.Point(0, 50);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(129, 400);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// _btnPostmens
			// 
			this._btnPostmens.AutoSize = false;
			this._btnPostmens.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnPostmens.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnPostmens.ForeColor = System.Drawing.Color.Black;
			this._btnPostmens.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnPostmens.Name = "_btnPostmens";
			this._btnPostmens.Size = new System.Drawing.Size(123, 35);
			this._btnPostmens.Text = "Почтальоны";
			this._btnPostmens.Click += new System.EventHandler(this.OnPostmensClick);
			// 
			// _btnSenders
			// 
			this._btnSenders.AutoSize = false;
			this._btnSenders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnSenders.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnSenders.ForeColor = System.Drawing.Color.Black;
			this._btnSenders.Image = ((System.Drawing.Image)(resources.GetObject("_btnSenders.Image")));
			this._btnSenders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnSenders.Name = "_btnSenders";
			this._btnSenders.Size = new System.Drawing.Size(123, 35);
			this._btnSenders.Text = "Отправители";
			this._btnSenders.Click += new System.EventHandler(this.OnSendersClick);
			// 
			// _btnRecipients
			// 
			this._btnRecipients.AutoSize = false;
			this._btnRecipients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnRecipients.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnRecipients.ForeColor = System.Drawing.Color.Black;
			this._btnRecipients.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnRecipients.Name = "_btnRecipients";
			this._btnRecipients.Size = new System.Drawing.Size(123, 35);
			this._btnRecipients.Text = "Получатели";
			this._btnRecipients.Click += new System.EventHandler(this.OnClientsClick);
			// 
			// _btnPlots
			// 
			this._btnPlots.AutoSize = false;
			this._btnPlots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnPlots.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnPlots.ForeColor = System.Drawing.Color.Black;
			this._btnPlots.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnPlots.Name = "_btnPlots";
			this._btnPlots.Size = new System.Drawing.Size(123, 35);
			this._btnPlots.Text = "Участки";
			this._btnPlots.Click += new System.EventHandler(this.OnPlotsClick);
			// 
			// _btnStreetCity
			// 
			this._btnStreetCity.AutoSize = false;
			this._btnStreetCity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnStreetCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnStreetCity.ForeColor = System.Drawing.Color.Black;
			this._btnStreetCity.Image = ((System.Drawing.Image)(resources.GetObject("_btnStreetCity.Image")));
			this._btnStreetCity.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnStreetCity.Name = "_btnStreetCity";
			this._btnStreetCity.Size = new System.Drawing.Size(123, 35);
			this._btnStreetCity.Text = "Улица / Город";
			this._btnStreetCity.Click += new System.EventHandler(this.OnCitiesClick);
			// 
			// _btnUsers
			// 
			this._btnUsers.AutoSize = false;
			this._btnUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._btnUsers.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("_btnUsers.Image")));
			this._btnUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnUsers.Name = "_btnUsers";
			this._btnUsers.Size = new System.Drawing.Size(123, 35);
			this._btnUsers.Text = "Пользователи";
			this._btnUsers.Click += new System.EventHandler(this.OnUsersClick);
			// 
			// timer
			// 
			this.timer.Interval = 2000;
			this.timer.Tick += new System.EventHandler(this.OnTimerTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.toolStrip2);
			this.Controls.Add(this._dgvAddresses);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Адреса";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this._dgvAddresses)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);

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
}
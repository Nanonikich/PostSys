namespace postSys.application.prj.Views.Forms
{
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipientsForm));
			this._dgvRecipients = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this._btnAdd = new System.Windows.Forms.ToolStripButton();
			this._btnEdit = new System.Windows.Forms.ToolStripButton();
			this._btnDelete = new System.Windows.Forms.ToolStripButton();
			this._txtSearchRecipient = new System.Windows.Forms.ToolStripTextBox();
			this._lblRecipient = new System.Windows.Forms.ToolStripLabel();
			this._txtSearchSender = new System.Windows.Forms.ToolStripTextBox();
			this._lblSender = new System.Windows.Forms.ToolStripLabel();
			((System.ComponentModel.ISupportInitialize)(this._dgvRecipients)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _dgvRecipients
			// 
			this._dgvRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvRecipients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvRecipients.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvRecipients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvRecipients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvRecipients.Location = new System.Drawing.Point(0, 54);
			this._dgvRecipients.MultiSelect = false;
			this._dgvRecipients.Name = "_dgvRecipients";
			this._dgvRecipients.RowHeadersVisible = false;
			this._dgvRecipients.RowTemplate.Height = 25;
			this._dgvRecipients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvRecipients.Size = new System.Drawing.Size(800, 395);
			this._dgvRecipients.TabIndex = 0;
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
            this._lblSender,
            this._txtSearchRecipient,
            this._lblRecipient});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(800, 51);
			this.toolStrip1.TabIndex = 1;
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
			// _txtSearchRecipient
			// 
			this._txtSearchRecipient.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._txtSearchRecipient.AutoSize = false;
			this._txtSearchRecipient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtSearchRecipient.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtSearchRecipient.Name = "_txtSearchRecipient";
			this._txtSearchRecipient.Size = new System.Drawing.Size(160, 26);
			this._txtSearchRecipient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTxtBoxesKeyDown);
			this._txtSearchRecipient.TextChanged += new System.EventHandler(this.OnSearchSenderOrRecipientTextChanged);
			// 
			// _lblRecipient
			// 
			this._lblRecipient.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._lblRecipient.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._lblRecipient.Name = "_lblRecipient";
			this._lblRecipient.Size = new System.Drawing.Size(75, 48);
			this._lblRecipient.Text = "Получатель:";
			// 
			// _txtSearchSender
			// 
			this._txtSearchSender.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._txtSearchSender.AutoSize = false;
			this._txtSearchSender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._txtSearchSender.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtSearchSender.Name = "_txtSearchSender";
			this._txtSearchSender.Size = new System.Drawing.Size(160, 26);
			this._txtSearchSender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTxtBoxesKeyDown);
			this._txtSearchSender.TextChanged += new System.EventHandler(this.OnSearchSenderOrRecipientTextChanged);
			// 
			// _lblSender
			// 
			this._lblSender.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._lblSender.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._lblSender.Name = "_lblSender";
			this._lblSender.Size = new System.Drawing.Size(80, 48);
			this._lblSender.Text = "Отправитель:";
			// 
			// RecipientsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this._dgvRecipients);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RecipientsForm";
			this.Text = "Получатели";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.ClientsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this._dgvRecipients)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DataGridView _dgvRecipients;
		private ToolStrip toolStrip1;
		private ToolStripButton _btnAdd;
		private ToolStripButton _btnEdit;
		private ToolStripButton _btnDelete;
		private ToolStripTextBox _txtSearchRecipient;
		private ToolStripLabel _lblSender;
		private ToolStripTextBox _txtSearchSender;
		private ToolStripLabel _lblRecipient;
	}
}
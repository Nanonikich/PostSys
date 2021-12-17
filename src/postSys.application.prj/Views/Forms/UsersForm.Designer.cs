namespace postSys.application.prj.Views.Forms
{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this._btnAdd = new System.Windows.Forms.ToolStripButton();
			this._btnEdit = new System.Windows.Forms.ToolStripButton();
			this._btnDelete = new System.Windows.Forms.ToolStripButton();
			this._dgvUsers = new System.Windows.Forms.DataGridView();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._dgvUsers)).BeginInit();
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
            this._btnDelete});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(800, 50);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// _btnAdd
			// 
			this._btnAdd.AutoSize = false;
			this._btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
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
			this._btnDelete.Text = "Delete";
			this._btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
			// 
			// _dgvUsers
			// 
			this._dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvUsers.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvUsers.Location = new System.Drawing.Point(0, 51);
			this._dgvUsers.MultiSelect = false;
			this._dgvUsers.Name = "_dgvUsers";
			this._dgvUsers.RowHeadersVisible = false;
			this._dgvUsers.RowTemplate.Height = 25;
			this._dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvUsers.Size = new System.Drawing.Size(800, 399);
			this._dgvUsers.TabIndex = 2;
			// 
			// UsersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this._dgvUsers);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "UsersForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Пользователи";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.UsersForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._dgvUsers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ToolStrip toolStrip1;
		private DataGridView _dgvUsers;
		private ToolStripButton _btnAdd;
		private ToolStripButton _btnEdit;
		private ToolStripButton _btnDelete;
	}
}
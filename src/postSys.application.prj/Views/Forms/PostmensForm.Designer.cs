namespace mUse.application.prj.Views.Forms
{
	partial class PostmensForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostmensForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this._cbxPlot = new System.Windows.Forms.ToolStripComboBox();
			this._btnAdd = new System.Windows.Forms.ToolStripButton();
			this._btnEdit = new System.Windows.Forms.ToolStripButton();
			this._btnDelete = new System.Windows.Forms.ToolStripButton();
			this._dgvPostmens = new System.Windows.Forms.DataGridView();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._dgvPostmens)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cbxPlot,
            this._btnAdd,
            this._btnEdit,
            this._btnDelete});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(800, 51);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// _cbxPlot
			// 
			this._cbxPlot.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._cbxPlot.AutoSize = false;
			this._cbxPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxPlot.Name = "_cbxPlot";
			this._cbxPlot.Size = new System.Drawing.Size(130, 23);
			this._cbxPlot.SelectedIndexChanged += new System.EventHandler(this.OnComboBoxSelectedIndexChanged);
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
			// _dgvPostmens
			// 
			this._dgvPostmens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvPostmens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvPostmens.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvPostmens.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvPostmens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvPostmens.Location = new System.Drawing.Point(0, 51);
			this._dgvPostmens.MultiSelect = false;
			this._dgvPostmens.Name = "_dgvPostmens";
			this._dgvPostmens.RowHeadersVisible = false;
			this._dgvPostmens.RowTemplate.Height = 25;
			this._dgvPostmens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvPostmens.Size = new System.Drawing.Size(800, 399);
			this._dgvPostmens.TabIndex = 3;
			// 
			// PostmensForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this._dgvPostmens);
			this.Controls.Add(this.toolStrip1);
			this.MaximumSize = new System.Drawing.Size(1366, 768);
			this.MinimumSize = new System.Drawing.Size(816, 489);
			this.Name = "PostmensForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Почтальоны";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._dgvPostmens)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private ToolStrip toolStrip1;
		private ToolStripComboBox _cbxPlot;
		private ToolStripButton _btnAdd;
		private ToolStripButton _btnEdit;
		private ToolStripButton _btnDelete;
		private DataGridView _dgvPostmens;
	}
}
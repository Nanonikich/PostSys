namespace postSys.application.prj.Views.Forms
{
	partial class PlotsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotsForm));
			this._dgvStreets = new System.Windows.Forms.DataGridView();
			this._txtPlot = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._btnAdd = new System.Windows.Forms.Button();
			this._btnDelete = new System.Windows.Forms.Button();
			this._cbxCity = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._txtHouses = new System.Windows.Forms.TextBox();
			this._cbxStreet = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this._dgvStreets)).BeginInit();
			this.SuspendLayout();
			// 
			// _dgvStreets
			// 
			this._dgvStreets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dgvStreets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvStreets.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvStreets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvStreets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvStreets.Location = new System.Drawing.Point(210, 0);
			this._dgvStreets.MultiSelect = false;
			this._dgvStreets.Name = "_dgvStreets";
			this._dgvStreets.RowHeadersVisible = false;
			this._dgvStreets.RowTemplate.Height = 25;
			this._dgvStreets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvStreets.Size = new System.Drawing.Size(643, 346);
			this._dgvStreets.TabIndex = 0;
			this._dgvStreets.TabStop = false;
			// 
			// _txtPlot
			// 
			this._txtPlot.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtPlot.Location = new System.Drawing.Point(13, 98);
			this._txtPlot.MaxLength = 6;
			this._txtPlot.Multiline = true;
			this._txtPlot.Name = "_txtPlot";
			this._txtPlot.Size = new System.Drawing.Size(191, 28);
			this._txtPlot.TabIndex = 2;
			this._txtPlot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPlotHomeKeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Город";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(12, 76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Участок";
			// 
			// _btnAdd
			// 
			this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnAdd.Location = new System.Drawing.Point(13, 272);
			this._btnAdd.Name = "_btnAdd";
			this._btnAdd.Size = new System.Drawing.Size(191, 29);
			this._btnAdd.TabIndex = 5;
			this._btnAdd.Text = "Добавить";
			this._btnAdd.UseVisualStyleBackColor = true;
			this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// _btnDelete
			// 
			this._btnDelete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnDelete.Location = new System.Drawing.Point(13, 306);
			this._btnDelete.Name = "_btnDelete";
			this._btnDelete.Size = new System.Drawing.Size(191, 29);
			this._btnDelete.TabIndex = 6;
			this._btnDelete.Text = "Удалить";
			this._btnDelete.UseVisualStyleBackColor = true;
			this._btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
			// 
			// _cbxCity
			// 
			this._cbxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxCity.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxCity.FormattingEnabled = true;
			this._cbxCity.Location = new System.Drawing.Point(12, 31);
			this._cbxCity.Name = "_cbxCity";
			this._cbxCity.Size = new System.Drawing.Size(192, 29);
			this._cbxCity.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(12, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 19);
			this.label3.TabIndex = 8;
			this.label3.Text = "Улица";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(12, 196);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(106, 19);
			this.label4.TabIndex = 9;
			this.label4.Text = "Список домов";
			// 
			// _txtHouses
			// 
			this._txtHouses.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtHouses.Location = new System.Drawing.Point(12, 218);
			this._txtHouses.MaxLength = 300;
			this._txtHouses.Multiline = true;
			this._txtHouses.Name = "_txtHouses";
			this._txtHouses.Size = new System.Drawing.Size(191, 28);
			this._txtHouses.TabIndex = 4;
			this._txtHouses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPlotHomeKeyDown);
			// 
			// _cbxStreet
			// 
			this._cbxStreet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxStreet.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxStreet.FormattingEnabled = true;
			this._cbxStreet.Location = new System.Drawing.Point(12, 161);
			this._cbxStreet.Name = "_cbxStreet";
			this._cbxStreet.Size = new System.Drawing.Size(192, 29);
			this._cbxStreet.TabIndex = 3;
			// 
			// PlotsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(854, 347);
			this.Controls.Add(this._cbxStreet);
			this.Controls.Add(this._txtHouses);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._cbxCity);
			this.Controls.Add(this._btnDelete);
			this.Controls.Add(this._btnAdd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._txtPlot);
			this.Controls.Add(this._dgvStreets);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlotsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Участки";
			this.Load += new System.EventHandler(this.ServiceFormLoad);
			((System.ComponentModel.ISupportInitialize)(this._dgvStreets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DataGridView _dgvStreets;
		private TextBox _txtPlot;
		private Label label1;
		private Label label2;
		private Button _btnAdd;
		private Button _btnDelete;
		private ComboBox _cbxCity;
		private Label label3;
		private Label label4;
		private TextBox _txtHouses;
		private ComboBox _cbxStreet;
	}
}
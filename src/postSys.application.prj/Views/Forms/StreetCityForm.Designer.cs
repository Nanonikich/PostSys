namespace postSys.application.prj.Views.Forms
{
	partial class StreetCityForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new System.Windows.Forms.Label();
			this._txtCity = new System.Windows.Forms.TextBox();
			this._btnAddCity = new System.Windows.Forms.Button();
			this._btnDeleteCity = new System.Windows.Forms.Button();
			this._dgvCities = new System.Windows.Forms.DataGridView();
			this._dgvStreets = new System.Windows.Forms.DataGridView();
			this.label2 = new System.Windows.Forms.Label();
			this._txtStreet = new System.Windows.Forms.TextBox();
			this._btnAddStreet = new System.Windows.Forms.Button();
			this._btnDeleteStreet = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this._dgvCities)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._dgvStreets)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 239);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Город";
			// 
			// _txtCity
			// 
			this._txtCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtCity.Location = new System.Drawing.Point(12, 261);
			this._txtCity.MaxLength = 30;
			this._txtCity.Multiline = true;
			this._txtCity.Name = "_txtCity";
			this._txtCity.Size = new System.Drawing.Size(178, 28);
			this._txtCity.TabIndex = 4;
			this._txtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
			// 
			// _btnAddCity
			// 
			this._btnAddCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnAddCity.Location = new System.Drawing.Point(12, 368);
			this._btnAddCity.Name = "_btnAddCity";
			this._btnAddCity.Size = new System.Drawing.Size(178, 30);
			this._btnAddCity.TabIndex = 5;
			this._btnAddCity.Text = "Добавить";
			this._btnAddCity.UseVisualStyleBackColor = true;
			this._btnAddCity.Click += new System.EventHandler(this.OnAddCityClick);
			// 
			// _btnDeleteCity
			// 
			this._btnDeleteCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnDeleteCity.Location = new System.Drawing.Point(12, 404);
			this._btnDeleteCity.Name = "_btnDeleteCity";
			this._btnDeleteCity.Size = new System.Drawing.Size(178, 30);
			this._btnDeleteCity.TabIndex = 6;
			this._btnDeleteCity.Text = "Удалить";
			this._btnDeleteCity.UseVisualStyleBackColor = true;
			this._btnDeleteCity.Click += new System.EventHandler(this.OnDeleteCityClick);
			// 
			// _dgvCities
			// 
			this._dgvCities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvCities.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvCities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this._dgvCities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvCities.Location = new System.Drawing.Point(203, 228);
			this._dgvCities.MultiSelect = false;
			this._dgvCities.Name = "_dgvCities";
			this._dgvCities.RowHeadersVisible = false;
			this._dgvCities.RowTemplate.Height = 25;
			this._dgvCities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvCities.Size = new System.Drawing.Size(253, 212);
			this._dgvCities.TabIndex = 4;
			this._dgvCities.TabStop = false;
			// 
			// _dgvStreets
			// 
			this._dgvStreets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._dgvStreets.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this._dgvStreets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this._dgvStreets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgvStreets.Location = new System.Drawing.Point(203, 0);
			this._dgvStreets.MultiSelect = false;
			this._dgvStreets.Name = "_dgvStreets";
			this._dgvStreets.RowHeadersVisible = false;
			this._dgvStreets.RowTemplate.Height = 25;
			this._dgvStreets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dgvStreets.Size = new System.Drawing.Size(253, 206);
			this._dgvStreets.TabIndex = 5;
			this._dgvStreets.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 19);
			this.label2.TabIndex = 6;
			this.label2.Text = "Улица";
			// 
			// _txtStreet
			// 
			this._txtStreet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtStreet.Location = new System.Drawing.Point(12, 31);
			this._txtStreet.MaxLength = 30;
			this._txtStreet.Multiline = true;
			this._txtStreet.Name = "_txtStreet";
			this._txtStreet.Size = new System.Drawing.Size(178, 28);
			this._txtStreet.TabIndex = 1;
			this._txtStreet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
			// 
			// _btnAddStreet
			// 
			this._btnAddStreet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnAddStreet.Location = new System.Drawing.Point(12, 134);
			this._btnAddStreet.Name = "_btnAddStreet";
			this._btnAddStreet.Size = new System.Drawing.Size(178, 30);
			this._btnAddStreet.TabIndex = 2;
			this._btnAddStreet.Text = "Добавить";
			this._btnAddStreet.UseVisualStyleBackColor = true;
			this._btnAddStreet.Click += new System.EventHandler(this.OnAddStreetClick);
			// 
			// _btnDeleteStreet
			// 
			this._btnDeleteStreet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnDeleteStreet.Location = new System.Drawing.Point(12, 170);
			this._btnDeleteStreet.Name = "_btnDeleteStreet";
			this._btnDeleteStreet.Size = new System.Drawing.Size(178, 30);
			this._btnDeleteStreet.TabIndex = 3;
			this._btnDeleteStreet.Text = "Удалить";
			this._btnDeleteStreet.UseVisualStyleBackColor = true;
			this._btnDeleteStreet.Click += new System.EventHandler(this.OnDeleteStreetClick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.panel1.Location = new System.Drawing.Point(0, 206);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 22);
			this.panel1.TabIndex = 10;
			// 
			// StreetCityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(456, 440);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this._btnDeleteStreet);
			this.Controls.Add(this._btnAddStreet);
			this.Controls.Add(this._txtStreet);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._dgvStreets);
			this.Controls.Add(this._dgvCities);
			this.Controls.Add(this._btnDeleteCity);
			this.Controls.Add(this._btnAddCity);
			this.Controls.Add(this._txtCity);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "StreetCityForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Редактор улицы/города";
			this.Load += new System.EventHandler(this.CitiesFormLoad);
			((System.ComponentModel.ISupportInitialize)(this._dgvCities)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._dgvStreets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
		private TextBox _txtCity;
		private Button _btnAddCity;
		private Button _btnDeleteCity;
		private DataGridView _dgvCities;
		private DataGridView _dgvStreets;
		private Label label2;
		private TextBox _txtStreet;
		private Button _btnAddStreet;
		private Button _btnDeleteStreet;
		private Panel panel1;
	}
}
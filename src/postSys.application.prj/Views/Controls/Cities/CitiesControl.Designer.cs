namespace PostSys.Application.Views.Controls.StreetCity
{
	partial class CitiesControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			_pnlCitiesTable = new Panel();
			_btnDeleteCity = new Button();
			_btnAddCity = new Button();
			_txtCity = new TextBox();
			_lblCity = new Label();
			SuspendLayout();
			// 
			// _pnlCitiesTable
			// 
			_pnlCitiesTable.Location = new Point(211, 15);
			_pnlCitiesTable.Name = "_pnlCitiesTable";
			_pnlCitiesTable.Size = new Size(241, 215);
			_pnlCitiesTable.TabIndex = 18;
			// 
			// _btnDeleteCity
			// 
			_btnDeleteCity.Font = new Font("Times New Roman", 12F);
			_btnDeleteCity.Location = new Point(14, 200);
			_btnDeleteCity.Name = "_btnDeleteCity";
			_btnDeleteCity.Size = new Size(178, 30);
			_btnDeleteCity.TabIndex = 17;
			_btnDeleteCity.Text = "Удалить";
			_btnDeleteCity.UseVisualStyleBackColor = true;
			_btnDeleteCity.Click += OnDeleteCityClick;
			// 
			// _btnAddCity
			// 
			_btnAddCity.Font = new Font("Times New Roman", 12F);
			_btnAddCity.Location = new Point(14, 164);
			_btnAddCity.Name = "_btnAddCity";
			_btnAddCity.Size = new Size(178, 30);
			_btnAddCity.TabIndex = 16;
			_btnAddCity.Text = "Добавить";
			_btnAddCity.UseVisualStyleBackColor = true;
			_btnAddCity.Click += OnAddCityClick;
			// 
			// _txtCity
			// 
			_txtCity.Font = new Font("Times New Roman", 12F);
			_txtCity.Location = new Point(14, 48);
			_txtCity.MaxLength = 30;
			_txtCity.Multiline = true;
			_txtCity.Name = "_txtCity";
			_txtCity.Size = new Size(178, 28);
			_txtCity.TabIndex = 15;
			// 
			// _lblCity
			// 
			_lblCity.AutoSize = true;
			_lblCity.Font = new Font("Times New Roman", 12F);
			_lblCity.Location = new Point(14, 26);
			_lblCity.Name = "_lblCity";
			_lblCity.Size = new Size(49, 19);
			_lblCity.TabIndex = 14;
			_lblCity.Text = "Город";
			// 
			// CitiesControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaption;
			Controls.Add(_pnlCitiesTable);
			Controls.Add(_btnDeleteCity);
			Controls.Add(_btnAddCity);
			Controls.Add(_txtCity);
			Controls.Add(_lblCity);
			Name = "CitiesControl";
			Size = new Size(466, 244);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel _pnlCitiesTable;
		private Button _btnDeleteCity;
		private Button _btnAddCity;
		private TextBox _txtCity;
		private Label _lblCity;
	}
}

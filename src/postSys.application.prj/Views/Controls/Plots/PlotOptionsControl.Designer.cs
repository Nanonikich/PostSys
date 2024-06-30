namespace PostSys.Application.Views.Controls.Plots
{
	partial class PlotOptionsControl
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
			_cbxStreet = new ComboBox();
			_txtHouses = new TextBox();
			label4 = new Label();
			label3 = new Label();
			_cbxCity = new ComboBox();
			_btnDelete = new Button();
			_btnAdd = new Button();
			label2 = new Label();
			label1 = new Label();
			_txtPlot = new TextBox();
			SuspendLayout();
			// 
			// _cbxStreet
			// 
			_cbxStreet.DropDownStyle = ComboBoxStyle.DropDownList;
			_cbxStreet.Font = new Font("Times New Roman", 14.25F);
			_cbxStreet.FormattingEnabled = true;
			_cbxStreet.Location = new Point(17, 162);
			_cbxStreet.Name = "_cbxStreet";
			_cbxStreet.Size = new Size(192, 29);
			_cbxStreet.TabIndex = 12;
			// 
			// _txtHouses
			// 
			_txtHouses.Font = new Font("Times New Roman", 12F);
			_txtHouses.Location = new Point(17, 219);
			_txtHouses.MaxLength = 300;
			_txtHouses.Multiline = true;
			_txtHouses.Name = "_txtHouses";
			_txtHouses.Size = new Size(191, 28);
			_txtHouses.TabIndex = 14;
			_txtHouses.KeyDown += TxtPlotHousesKeyDown;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Times New Roman", 12F);
			label4.Location = new Point(17, 197);
			label4.Name = "label4";
			label4.Size = new Size(106, 19);
			label4.TabIndex = 19;
			label4.Text = "Список домов";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Times New Roman", 12F);
			label3.Location = new Point(17, 140);
			label3.Name = "label3";
			label3.Size = new Size(51, 19);
			label3.TabIndex = 18;
			label3.Text = "Улица";
			// 
			// _cbxCity
			// 
			_cbxCity.DropDownStyle = ComboBoxStyle.DropDownList;
			_cbxCity.Font = new Font("Times New Roman", 14.25F);
			_cbxCity.FormattingEnabled = true;
			_cbxCity.Location = new Point(17, 32);
			_cbxCity.Name = "_cbxCity";
			_cbxCity.Size = new Size(192, 29);
			_cbxCity.TabIndex = 10;
			// 
			// _btnDelete
			// 
			_btnDelete.Font = new Font("Times New Roman", 12F);
			_btnDelete.Location = new Point(18, 307);
			_btnDelete.Name = "_btnDelete";
			_btnDelete.Size = new Size(191, 29);
			_btnDelete.TabIndex = 17;
			_btnDelete.Text = "Удалить";
			_btnDelete.UseVisualStyleBackColor = true;
			_btnDelete.Click += OnDeleteClick;
			// 
			// _btnAdd
			// 
			_btnAdd.Font = new Font("Times New Roman", 12F);
			_btnAdd.Location = new Point(18, 273);
			_btnAdd.Name = "_btnAdd";
			_btnAdd.Size = new Size(191, 29);
			_btnAdd.TabIndex = 16;
			_btnAdd.Text = "Добавить";
			_btnAdd.UseVisualStyleBackColor = true;
			_btnAdd.Click += OnAddClick;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Times New Roman", 12F);
			label2.Location = new Point(17, 77);
			label2.Name = "label2";
			label2.Size = new Size(64, 19);
			label2.TabIndex = 15;
			label2.Text = "Участок";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Times New Roman", 12F);
			label1.Location = new Point(17, 10);
			label1.Name = "label1";
			label1.Size = new Size(49, 19);
			label1.TabIndex = 13;
			label1.Text = "Город";
			// 
			// _txtPlot
			// 
			_txtPlot.Font = new Font("Times New Roman", 12F);
			_txtPlot.Location = new Point(18, 99);
			_txtPlot.MaxLength = 6;
			_txtPlot.Multiline = true;
			_txtPlot.Name = "_txtPlot";
			_txtPlot.Size = new Size(191, 28);
			_txtPlot.TabIndex = 11;
			_txtPlot.KeyDown += TxtPlotHousesKeyDown;
			// 
			// PlotOptionsControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaption;
			Controls.Add(_cbxStreet);
			Controls.Add(_txtHouses);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(_cbxCity);
			Controls.Add(_btnDelete);
			Controls.Add(_btnAdd);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(_txtPlot);
			Name = "PlotOptionsControl";
			Size = new Size(227, 346);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox _cbxStreet;
		private TextBox _txtHouses;
		private Label label4;
		private Label label3;
		private ComboBox _cbxCity;
		private Button _btnDelete;
		private Button _btnAdd;
		private Label label2;
		private Label label1;
		private TextBox _txtPlot;
	}
}

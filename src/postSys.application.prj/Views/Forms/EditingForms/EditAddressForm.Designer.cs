namespace postSys.application.prj.Views.Forms
{
	partial class EditAddressForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAddressForm));
			this._cbxRecipient = new System.Windows.Forms.ComboBox();
			this._cbxCity = new System.Windows.Forms.ComboBox();
			this._btnAdd = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this._btnEdit = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._cbxHome = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._cbxPostmen = new System.Windows.Forms.ComboBox();
			this._txtGoods = new System.Windows.Forms.TextBox();
			this.numberPlot = new System.Windows.Forms.Label();
			this._cbxStreet = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this._lblPlot = new System.Windows.Forms.Label();
			this._txtApartment = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _cbxRecipient
			// 
			this._cbxRecipient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxRecipient.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxRecipient.FormattingEnabled = true;
			this._cbxRecipient.Location = new System.Drawing.Point(128, 58);
			this._cbxRecipient.Name = "_cbxRecipient";
			this._cbxRecipient.Size = new System.Drawing.Size(153, 29);
			this._cbxRecipient.TabIndex = 1;
			// 
			// _cbxCity
			// 
			this._cbxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxCity.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxCity.FormattingEnabled = true;
			this._cbxCity.Location = new System.Drawing.Point(128, 91);
			this._cbxCity.Name = "_cbxCity";
			this._cbxCity.Size = new System.Drawing.Size(153, 29);
			this._cbxCity.TabIndex = 2;
			this._cbxCity.SelectedIndexChanged += new System.EventHandler(this.CbxCitySelectedIndexChanged);
			// 
			// _btnAdd
			// 
			this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnAdd.Location = new System.Drawing.Point(11, 342);
			this._btnAdd.Name = "_btnAdd";
			this._btnAdd.Size = new System.Drawing.Size(121, 31);
			this._btnAdd.TabIndex = 8;
			this._btnAdd.Text = "Добавить";
			this._btnAdd.UseVisualStyleBackColor = true;
			this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
			// 
			// _btnCancel
			// 
			this._btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnCancel.Location = new System.Drawing.Point(160, 342);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(121, 31);
			this._btnCancel.TabIndex = 9;
			this._btnCancel.Text = "Отменить";
			this._btnCancel.UseVisualStyleBackColor = true;
			this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// _btnEdit
			// 
			this._btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnEdit.Location = new System.Drawing.Point(12, 342);
			this._btnEdit.Name = "_btnEdit";
			this._btnEdit.Size = new System.Drawing.Size(121, 31);
			this._btnEdit.TabIndex = 8;
			this._btnEdit.Text = "Изменить";
			this._btnEdit.UseVisualStyleBackColor = true;
			this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 19);
			this.label1.TabIndex = 6;
			this.label1.Text = "Участок";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(12, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 19);
			this.label2.TabIndex = 7;
			this.label2.Text = "Получатель";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(12, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 19);
			this.label3.TabIndex = 8;
			this.label3.Text = "Город";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(12, 161);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 19);
			this.label4.TabIndex = 9;
			this.label4.Text = "Дом";
			// 
			// _cbxHome
			// 
			this._cbxHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxHome.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxHome.FormattingEnabled = true;
			this._cbxHome.Location = new System.Drawing.Point(128, 157);
			this._cbxHome.Name = "_cbxHome";
			this._cbxHome.Size = new System.Drawing.Size(153, 29);
			this._cbxHome.TabIndex = 4;
			this._cbxHome.SelectedIndexChanged += new System.EventHandler(this.CbxHomeSelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label5.Location = new System.Drawing.Point(12, 227);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 19);
			this.label5.TabIndex = 11;
			this.label5.Text = "Почтальон";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label6.Location = new System.Drawing.Point(12, 260);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 19);
			this.label6.TabIndex = 12;
			this.label6.Text = "Товар";
			// 
			// _cbxPostmen
			// 
			this._cbxPostmen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxPostmen.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxPostmen.FormattingEnabled = true;
			this._cbxPostmen.Location = new System.Drawing.Point(128, 223);
			this._cbxPostmen.Name = "_cbxPostmen";
			this._cbxPostmen.Size = new System.Drawing.Size(153, 29);
			this._cbxPostmen.TabIndex = 6;
			// 
			// _txtGoods
			// 
			this._txtGoods.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtGoods.Location = new System.Drawing.Point(128, 257);
			this._txtGoods.MaxLength = 120;
			this._txtGoods.Multiline = true;
			this._txtGoods.Name = "_txtGoods";
			this._txtGoods.Size = new System.Drawing.Size(152, 27);
			this._txtGoods.TabIndex = 7;
			this._txtGoods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
			// 
			// numberPlot
			// 
			this.numberPlot.AutoSize = true;
			this.numberPlot.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.numberPlot.Location = new System.Drawing.Point(129, 29);
			this.numberPlot.Name = "numberPlot";
			this.numberPlot.Size = new System.Drawing.Size(0, 19);
			this.numberPlot.TabIndex = 15;
			// 
			// _cbxStreet
			// 
			this._cbxStreet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbxStreet.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._cbxStreet.FormattingEnabled = true;
			this._cbxStreet.Location = new System.Drawing.Point(128, 124);
			this._cbxStreet.Name = "_cbxStreet";
			this._cbxStreet.Size = new System.Drawing.Size(153, 29);
			this._cbxStreet.TabIndex = 3;
			this._cbxStreet.SelectedIndexChanged += new System.EventHandler(this.CbxStreetSelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label7.Location = new System.Drawing.Point(11, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(51, 19);
			this.label7.TabIndex = 17;
			this.label7.Text = "Улица";
			// 
			// _lblPlot
			// 
			this._lblPlot.AutoSize = true;
			this._lblPlot.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._lblPlot.Location = new System.Drawing.Point(128, 29);
			this._lblPlot.Name = "_lblPlot";
			this._lblPlot.Size = new System.Drawing.Size(0, 19);
			this._lblPlot.TabIndex = 18;
			// 
			// _txtApartment
			// 
			this._txtApartment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtApartment.Location = new System.Drawing.Point(128, 191);
			this._txtApartment.MaxLength = 5;
			this._txtApartment.Multiline = true;
			this._txtApartment.Name = "_txtApartment";
			this._txtApartment.Size = new System.Drawing.Size(152, 27);
			this._txtApartment.TabIndex = 5;
			this._txtApartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label8.Location = new System.Drawing.Point(11, 194);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 19);
			this.label8.TabIndex = 20;
			this.label8.Text = "Квартира";
			// 
			// EditAddressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(297, 385);
			this.Controls.Add(this.label8);
			this.Controls.Add(this._txtApartment);
			this.Controls.Add(this._lblPlot);
			this.Controls.Add(this.label7);
			this.Controls.Add(this._cbxStreet);
			this.Controls.Add(this.numberPlot);
			this.Controls.Add(this._txtGoods);
			this.Controls.Add(this._cbxPostmen);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this._cbxHome);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._btnCancel);
			this.Controls.Add(this._btnAdd);
			this.Controls.Add(this._cbxCity);
			this.Controls.Add(this._cbxRecipient);
			this.Controls.Add(this._btnEdit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "EditAddressForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Редактор адреса";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnAddressFormClosed);
			this.Load += new System.EventHandler(this.EditAddressForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private ComboBox _cbxRecipient;
		private ComboBox _cbxCity;
		private Button _btnAdd;
		private Button _btnCancel;
		private Button _btnEdit;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private ComboBox _cbxHome;
		private Label label5;
		private Label label6;
		private ComboBox _cbxPostmen;
		private TextBox _txtGoods;
		private Label numberPlot;
		private ComboBox _cbxStreet;
		private Label label7;
		private Label _lblPlot;
		private TextBox _txtApartment;
		private Label label8;
	}
}
namespace PostSys.Application.Views.Forms.EditingForms;

partial class EditRecipientForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRecipientForm));
		this._txtSeries = new System.Windows.Forms.TextBox();
		this._txtNumber = new System.Windows.Forms.TextBox();
		this._txtSurname = new System.Windows.Forms.TextBox();
		this._txtName = new System.Windows.Forms.TextBox();
		this._txtPatronymic = new System.Windows.Forms.TextBox();
		this._txtPhone = new System.Windows.Forms.TextBox();
		this._txtApartments = new System.Windows.Forms.TextBox();
		this._btnAdd = new System.Windows.Forms.Button();
		this._btnCancel = new System.Windows.Forms.Button();
		this._btnEdit = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this._cbxStreet = new System.Windows.Forms.ComboBox();
		this._cbxCity = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this._cbxHome = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this._cbxSenders = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this._lblSender = new System.Windows.Forms.Label();
		this.SuspendLayout();
		// 
		// _txtSeries
		// 
		this._txtSeries.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSeries.Location = new System.Drawing.Point(106, 27);
		this._txtSeries.MaxLength = 4;
		this._txtSeries.Multiline = true;
		this._txtSeries.Name = "_txtSeries";
		this._txtSeries.Size = new System.Drawing.Size(203, 31);
		this._txtSeries.TabIndex = 1;
		this._txtSeries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtNumber
		// 
		this._txtNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtNumber.Location = new System.Drawing.Point(106, 64);
		this._txtNumber.MaxLength = 6;
		this._txtNumber.Multiline = true;
		this._txtNumber.Name = "_txtNumber";
		this._txtNumber.Size = new System.Drawing.Size(203, 31);
		this._txtNumber.TabIndex = 2;
		this._txtNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtSurname
		// 
		this._txtSurname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSurname.Location = new System.Drawing.Point(106, 101);
		this._txtSurname.MaxLength = 30;
		this._txtSurname.Multiline = true;
		this._txtSurname.Name = "_txtSurname";
		this._txtSurname.Size = new System.Drawing.Size(203, 31);
		this._txtSurname.TabIndex = 3;
		this._txtSurname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtName
		// 
		this._txtName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtName.Location = new System.Drawing.Point(106, 138);
		this._txtName.MaxLength = 30;
		this._txtName.Multiline = true;
		this._txtName.Name = "_txtName";
		this._txtName.Size = new System.Drawing.Size(203, 31);
		this._txtName.TabIndex = 4;
		this._txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPatronymic
		// 
		this._txtPatronymic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPatronymic.Location = new System.Drawing.Point(106, 175);
		this._txtPatronymic.MaxLength = 35;
		this._txtPatronymic.Multiline = true;
		this._txtPatronymic.Name = "_txtPatronymic";
		this._txtPatronymic.Size = new System.Drawing.Size(203, 31);
		this._txtPatronymic.TabIndex = 5;
		this._txtPatronymic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPhone
		// 
		this._txtPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPhone.Location = new System.Drawing.Point(106, 354);
		this._txtPhone.MaxLength = 60;
		this._txtPhone.Multiline = true;
		this._txtPhone.Name = "_txtPhone";
		this._txtPhone.Size = new System.Drawing.Size(203, 31);
		this._txtPhone.TabIndex = 10;
		this._txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtApartments
		// 
		this._txtApartments.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtApartments.Location = new System.Drawing.Point(106, 317);
		this._txtApartments.MaxLength = 5;
		this._txtApartments.Multiline = true;
		this._txtApartments.Name = "_txtApartments";
		this._txtApartments.Size = new System.Drawing.Size(203, 31);
		this._txtApartments.TabIndex = 9;
		this._txtApartments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _btnAdd
		// 
		this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnAdd.Location = new System.Drawing.Point(12, 509);
		this._btnAdd.Name = "_btnAdd";
		this._btnAdd.Size = new System.Drawing.Size(137, 32);
		this._btnAdd.TabIndex = 12;
		this._btnAdd.Text = "Добавить";
		this._btnAdd.UseVisualStyleBackColor = true;
		this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
		// 
		// _btnCancel
		// 
		this._btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnCancel.Location = new System.Drawing.Point(172, 509);
		this._btnCancel.Name = "_btnCancel";
		this._btnCancel.Size = new System.Drawing.Size(137, 32);
		this._btnCancel.TabIndex = 13;
		this._btnCancel.Text = "Отменить";
		this._btnCancel.UseVisualStyleBackColor = true;
		this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
		// 
		// _btnEdit
		// 
		this._btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnEdit.Location = new System.Drawing.Point(10, 509);
		this._btnEdit.Name = "_btnEdit";
		this._btnEdit.Size = new System.Drawing.Size(137, 32);
		this._btnEdit.TabIndex = 12;
		this._btnEdit.Text = "Изменить";
		this._btnEdit.UseVisualStyleBackColor = true;
		this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label1.Location = new System.Drawing.Point(12, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(51, 19);
		this.label1.TabIndex = 13;
		this.label1.Text = "Серия";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label2.Location = new System.Drawing.Point(12, 67);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 19);
		this.label2.TabIndex = 14;
		this.label2.Text = "Номер";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label3.Location = new System.Drawing.Point(12, 104);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(72, 19);
		this.label3.TabIndex = 15;
		this.label3.Text = "Фамилия";
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label4.Location = new System.Drawing.Point(12, 141);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(37, 19);
		this.label4.TabIndex = 16;
		this.label4.Text = "Имя";
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label5.Location = new System.Drawing.Point(12, 178);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(73, 19);
		this.label5.TabIndex = 17;
		this.label5.Text = "Отчество";
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label6.Location = new System.Drawing.Point(12, 251);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(51, 19);
		this.label6.TabIndex = 18;
		this.label6.Text = "Улица";
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label7.Location = new System.Drawing.Point(12, 357);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 19);
		this.label7.TabIndex = 19;
		this.label7.Text = "Телефон";
		// 
		// label8
		// 
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label8.Location = new System.Drawing.Point(12, 286);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(38, 19);
		this.label8.TabIndex = 20;
		this.label8.Text = "Дом";
		// 
		// label9
		// 
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label9.Location = new System.Drawing.Point(10, 320);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(74, 19);
		this.label9.TabIndex = 21;
		this.label9.Text = "Квартира";
		// 
		// _cbxStreets
		// 
		this._cbxStreet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxStreet.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxStreet.FormattingEnabled = true;
		this._cbxStreet.Location = new System.Drawing.Point(106, 247);
		this._cbxStreet.Name = "_cbxStreets";
		this._cbxStreet.Size = new System.Drawing.Size(203, 29);
		this._cbxStreet.TabIndex = 7;
		this._cbxStreet.SelectedIndexChanged += new System.EventHandler(this.CbxStreetsSelectedIndexChanged);
		// 
		// _cbxCities
		// 
		this._cbxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxCity.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxCity.FormattingEnabled = true;
		this._cbxCity.Location = new System.Drawing.Point(106, 212);
		this._cbxCity.Name = "_cbxCities";
		this._cbxCity.Size = new System.Drawing.Size(203, 29);
		this._cbxCity.TabIndex = 6;
		this._cbxCity.SelectedIndexChanged += new System.EventHandler(this.CbxCitiesSelectedIndexChanged);
		// 
		// label11
		// 
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label11.Location = new System.Drawing.Point(12, 216);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(49, 19);
		this.label11.TabIndex = 23;
		this.label11.Text = "Город";
		// 
		// _cbxHome
		// 
		this._cbxHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxHome.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxHome.FormattingEnabled = true;
		this._cbxHome.Location = new System.Drawing.Point(106, 282);
		this._cbxHome.Name = "_cbxHome";
		this._cbxHome.Size = new System.Drawing.Size(203, 29);
		this._cbxHome.TabIndex = 8;
		// 
		// label10
		// 
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label10.Location = new System.Drawing.Point(10, 398);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(98, 19);
		this.label10.TabIndex = 24;
		this.label10.Text = "Отправитель";
		// 
		// _cbxSenders
		// 
		this._cbxSenders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxSenders.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxSenders.FormattingEnabled = true;
		this._cbxSenders.Location = new System.Drawing.Point(106, 394);
		this._cbxSenders.Name = "_cbxSenders";
		this._cbxSenders.Size = new System.Drawing.Size(203, 29);
		this._cbxSenders.TabIndex = 11;
		this._cbxSenders.SelectedIndexChanged += new System.EventHandler(this.CbxSendersSelectedIndexChanged);
		// 
		// label12
		// 
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label12.Location = new System.Drawing.Point(10, 451);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(114, 19);
		this.label12.TabIndex = 26;
		this.label12.Text = "ID отправителя";
		// 
		// _lblSender
		// 
		this._lblSender.AutoSize = true;
		this._lblSender.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._lblSender.Location = new System.Drawing.Point(272, 451);
		this._lblSender.Name = "_lblSender";
		this._lblSender.Size = new System.Drawing.Size(37, 19);
		this._lblSender.TabIndex = 27;
		this._lblSender.Text = "label";
		// 
		// EditRecipientForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.ClientSize = new System.Drawing.Size(321, 553);
		this.Controls.Add(this._lblSender);
		this.Controls.Add(this.label12);
		this.Controls.Add(this._cbxSenders);
		this.Controls.Add(this.label10);
		this.Controls.Add(this._cbxHome);
		this.Controls.Add(this.label11);
		this.Controls.Add(this._cbxCity);
		this.Controls.Add(this._cbxStreet);
		this.Controls.Add(this.label9);
		this.Controls.Add(this.label8);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this._btnCancel);
		this.Controls.Add(this._btnAdd);
		this.Controls.Add(this._txtApartments);
		this.Controls.Add(this._txtPhone);
		this.Controls.Add(this._txtPatronymic);
		this.Controls.Add(this._txtName);
		this.Controls.Add(this._txtSurname);
		this.Controls.Add(this._txtNumber);
		this.Controls.Add(this._txtSeries);
		this.Controls.Add(this._btnEdit);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.Name = "EditRecipientForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Редактор получателя";
		this.Load += new System.EventHandler(this.EditClientFormLoad);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private TextBox _txtSeries;
	private TextBox _txtNumber;
	private TextBox _txtSurname;
	private TextBox _txtName;
	private TextBox _txtPatronymic;
	private TextBox _txtPhone;
	private TextBox _txtApartments;
	private Button _btnAdd;
	private Button _btnCancel;
	private Button _btnEdit;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label label4;
	private Label label5;
	private Label label6;
	private Label label7;
	private Label label8;
	private Label label9;
	private ComboBox _cbxStreet;
	private ComboBox _cbxCity;
	private Label label11;
	private ComboBox _cbxHome;
	private Label label10;
	private ComboBox _cbxSenders;
	private Label label12;
	private Label _lblSender;
}
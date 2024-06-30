namespace PostSys.Application.Views.Forms.EditingForms;

partial class EditSenderForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSenderForm));
		this.label1 = new System.Windows.Forms.Label();
		this._txtSurname = new System.Windows.Forms.TextBox();
		this._btnAdd = new System.Windows.Forms.Button();
		this._btnCancel = new System.Windows.Forms.Button();
		this._btnEdit = new System.Windows.Forms.Button();
		this._txtPatronymic = new System.Windows.Forms.TextBox();
		this._txtName = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this._cbxCity = new System.Windows.Forms.ComboBox();
		this._cbxStreet = new System.Windows.Forms.ComboBox();
		this._txtApartment = new System.Windows.Forms.TextBox();
		this._txtPhone = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this._cbxHome = new System.Windows.Forms.ComboBox();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label1.Location = new System.Drawing.Point(11, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "Фамилия";
		// 
		// _txtSurname
		// 
		this._txtSurname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSurname.Location = new System.Drawing.Point(104, 26);
		this._txtSurname.MaxLength = 30;
		this._txtSurname.Multiline = true;
		this._txtSurname.Name = "_txtSurname";
		this._txtSurname.Size = new System.Drawing.Size(203, 31);
		this._txtSurname.TabIndex = 1;
		this._txtSurname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _btnAdd
		// 
		this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnAdd.Location = new System.Drawing.Point(8, 404);
		this._btnAdd.Name = "_btnAdd";
		this._btnAdd.Size = new System.Drawing.Size(137, 32);
		this._btnAdd.TabIndex = 9;
		this._btnAdd.Text = "Добавить";
		this._btnAdd.UseVisualStyleBackColor = true;
		this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
		// 
		// _btnCancel
		// 
		this._btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnCancel.Location = new System.Drawing.Point(170, 404);
		this._btnCancel.Name = "_btnCancel";
		this._btnCancel.Size = new System.Drawing.Size(137, 32);
		this._btnCancel.TabIndex = 10;
		this._btnCancel.Text = "Отменить";
		this._btnCancel.UseVisualStyleBackColor = true;
		this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
		// 
		// _btnEdit
		// 
		this._btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnEdit.Location = new System.Drawing.Point(8, 404);
		this._btnEdit.Name = "_btnEdit";
		this._btnEdit.Size = new System.Drawing.Size(137, 32);
		this._btnEdit.TabIndex = 9;
		this._btnEdit.Text = "Изменить";
		this._btnEdit.UseVisualStyleBackColor = true;
		this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
		// 
		// _txtPatronymic
		// 
		this._txtPatronymic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPatronymic.Location = new System.Drawing.Point(104, 100);
		this._txtPatronymic.MaxLength = 35;
		this._txtPatronymic.Multiline = true;
		this._txtPatronymic.Name = "_txtPatronymic";
		this._txtPatronymic.Size = new System.Drawing.Size(203, 31);
		this._txtPatronymic.TabIndex = 3;
		this._txtPatronymic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtName
		// 
		this._txtName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtName.Location = new System.Drawing.Point(104, 63);
		this._txtName.MaxLength = 30;
		this._txtName.Multiline = true;
		this._txtName.Name = "_txtName";
		this._txtName.Size = new System.Drawing.Size(203, 31);
		this._txtName.TabIndex = 2;
		this._txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label2.Location = new System.Drawing.Point(11, 66);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 19);
		this.label2.TabIndex = 6;
		this.label2.Text = "Имя";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label3.Location = new System.Drawing.Point(11, 103);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 19);
		this.label3.TabIndex = 7;
		this.label3.Text = "Отчество";
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label4.Location = new System.Drawing.Point(11, 178);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 19);
		this.label4.TabIndex = 8;
		this.label4.Text = "Город";
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label5.Location = new System.Drawing.Point(11, 213);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 19);
		this.label5.TabIndex = 9;
		this.label5.Text = "Улица";
		// 
		// _cbxCity
		// 
		this._cbxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxCity.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxCity.FormattingEnabled = true;
		this._cbxCity.Location = new System.Drawing.Point(104, 174);
		this._cbxCity.Name = "_cbxCity";
		this._cbxCity.Size = new System.Drawing.Size(203, 29);
		this._cbxCity.TabIndex = 5;
		this._cbxCity.SelectedIndexChanged += new System.EventHandler(this.OnCbxCitySelectedIndexChanged);
		// 
		// _cbxStreet
		// 
		this._cbxStreet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxStreet.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxStreet.FormattingEnabled = true;
		this._cbxStreet.Location = new System.Drawing.Point(104, 209);
		this._cbxStreet.Name = "_cbxStreet";
		this._cbxStreet.Size = new System.Drawing.Size(203, 29);
		this._cbxStreet.TabIndex = 6;
		this._cbxStreet.SelectedIndexChanged += new System.EventHandler(this.OnCbxStreetSelectedIndexChanged);
		// 
		// _txtApartment
		// 
		this._txtApartment.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtApartment.Location = new System.Drawing.Point(103, 279);
		this._txtApartment.MaxLength = 5;
		this._txtApartment.Multiline = true;
		this._txtApartment.Name = "_txtApartment";
		this._txtApartment.Size = new System.Drawing.Size(203, 31);
		this._txtApartment.TabIndex = 8;
		this._txtApartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPhone
		// 
		this._txtPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPhone.Location = new System.Drawing.Point(104, 137);
		this._txtPhone.MaxLength = 60;
		this._txtPhone.Multiline = true;
		this._txtPhone.Name = "_txtPhone";
		this._txtPhone.Size = new System.Drawing.Size(203, 31);
		this._txtPhone.TabIndex = 4;
		this._txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label6.Location = new System.Drawing.Point(11, 248);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(38, 19);
		this.label6.TabIndex = 15;
		this.label6.Text = "Дом";
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label7.Location = new System.Drawing.Point(11, 282);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(74, 19);
		this.label7.TabIndex = 16;
		this.label7.Text = "Квартира";
		// 
		// label8
		// 
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label8.Location = new System.Drawing.Point(11, 140);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 19);
		this.label8.TabIndex = 17;
		this.label8.Text = "Телефон";
		// 
		// _cbxHome
		// 
		this._cbxHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxHome.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxHome.FormattingEnabled = true;
		this._cbxHome.Location = new System.Drawing.Point(103, 244);
		this._cbxHome.Name = "_cbxHome";
		this._cbxHome.Size = new System.Drawing.Size(204, 29);
		this._cbxHome.TabIndex = 7;
		// 
		// EditSenderForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.ClientSize = new System.Drawing.Size(321, 448);
		this.Controls.Add(this._cbxHome);
		this.Controls.Add(this.label8);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.label6);
		this.Controls.Add(this._txtPhone);
		this.Controls.Add(this._txtApartment);
		this.Controls.Add(this._cbxStreet);
		this.Controls.Add(this._cbxCity);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this._txtName);
		this.Controls.Add(this._txtPatronymic);
		this.Controls.Add(this._btnCancel);
		this.Controls.Add(this._btnAdd);
		this.Controls.Add(this._txtSurname);
		this.Controls.Add(this.label1);
		this.Controls.Add(this._btnEdit);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.Name = "EditSenderForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Редактор отправителя";
		this.Load += new System.EventHandler(this.EditSenderFormLoad);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private Label label1;
	private TextBox _txtSurname;
	private Button _btnAdd;
	private Button _btnCancel;
	private Button _btnEdit;
	private TextBox _txtPatronymic;
	private TextBox _txtName;
	private Label label2;
	private Label label3;
	private Label label4;
	private Label label5;
	private ComboBox _cbxCity;
	private ComboBox _cbxStreet;
	private TextBox _txtApartment;
	private TextBox _txtPhone;
	private Label label6;
	private Label label7;
	private Label label8;
	private ComboBox _cbxHome;
}
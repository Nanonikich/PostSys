namespace PostSys.Application.Views.Forms.EditingForms;

partial class EditUserForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserForm));
		this._txtSurname = new System.Windows.Forms.TextBox();
		this._txtName = new System.Windows.Forms.TextBox();
		this._txtPatronymic = new System.Windows.Forms.TextBox();
		this._txtEmail = new System.Windows.Forms.TextBox();
		this._txtPhone = new System.Windows.Forms.TextBox();
		this._txtCity = new System.Windows.Forms.TextBox();
		this._txtUsername = new System.Windows.Forms.TextBox();
		this._txtPassword = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this._btnAdd = new System.Windows.Forms.Button();
		this._btnCancel = new System.Windows.Forms.Button();
		this._btnEdit = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// 
		// _txtSurname
		// 
		this._txtSurname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSurname.Location = new System.Drawing.Point(119, 27);
		this._txtSurname.MaxLength = 30;
		this._txtSurname.Multiline = true;
		this._txtSurname.Name = "_txtSurname";
		this._txtSurname.Size = new System.Drawing.Size(187, 27);
		this._txtSurname.TabIndex = 0;
		this._txtSurname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtName
		// 
		this._txtName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtName.Location = new System.Drawing.Point(119, 60);
		this._txtName.MaxLength = 30;
		this._txtName.Multiline = true;
		this._txtName.Name = "_txtName";
		this._txtName.Size = new System.Drawing.Size(187, 27);
		this._txtName.TabIndex = 1;
		this._txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPatronymic
		// 
		this._txtPatronymic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPatronymic.Location = new System.Drawing.Point(119, 93);
		this._txtPatronymic.MaxLength = 35;
		this._txtPatronymic.Multiline = true;
		this._txtPatronymic.Name = "_txtPatronymic";
		this._txtPatronymic.Size = new System.Drawing.Size(187, 27);
		this._txtPatronymic.TabIndex = 2;
		this._txtPatronymic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtEmail
		// 
		this._txtEmail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtEmail.Location = new System.Drawing.Point(119, 126);
		this._txtEmail.MaxLength = 30;
		this._txtEmail.Multiline = true;
		this._txtEmail.Name = "_txtEmail";
		this._txtEmail.Size = new System.Drawing.Size(187, 27);
		this._txtEmail.TabIndex = 3;
		this._txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPhone
		// 
		this._txtPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPhone.Location = new System.Drawing.Point(119, 159);
		this._txtPhone.MaxLength = 16;
		this._txtPhone.Multiline = true;
		this._txtPhone.Name = "_txtPhone";
		this._txtPhone.Size = new System.Drawing.Size(187, 27);
		this._txtPhone.TabIndex = 4;
		this._txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtCity
		// 
		this._txtCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtCity.Location = new System.Drawing.Point(119, 192);
		this._txtCity.MaxLength = 30;
		this._txtCity.Multiline = true;
		this._txtCity.Name = "_txtCity";
		this._txtCity.Size = new System.Drawing.Size(187, 27);
		this._txtCity.TabIndex = 5;
		this._txtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtUsername
		// 
		this._txtUsername.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtUsername.Location = new System.Drawing.Point(119, 225);
		this._txtUsername.MaxLength = 30;
		this._txtUsername.Multiline = true;
		this._txtUsername.Name = "_txtUsername";
		this._txtUsername.Size = new System.Drawing.Size(187, 27);
		this._txtUsername.TabIndex = 6;
		this._txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPassword
		// 
		this._txtPassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPassword.Location = new System.Drawing.Point(119, 258);
		this._txtPassword.MaxLength = 20;
		this._txtPassword.Multiline = true;
		this._txtPassword.Name = "_txtPassword";
		this._txtPassword.Size = new System.Drawing.Size(187, 27);
		this._txtPassword.TabIndex = 7;
		this._txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label1.Location = new System.Drawing.Point(12, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 19);
		this.label1.TabIndex = 12;
		this.label1.Text = "Фамилия";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label2.Location = new System.Drawing.Point(12, 68);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 19);
		this.label2.TabIndex = 13;
		this.label2.Text = "Имя";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label3.Location = new System.Drawing.Point(12, 101);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 19);
		this.label3.TabIndex = 14;
		this.label3.Text = "Отчество";
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label4.Location = new System.Drawing.Point(12, 167);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 19);
		this.label4.TabIndex = 15;
		this.label4.Text = "Телефон";
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label5.Location = new System.Drawing.Point(12, 134);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(42, 19);
		this.label5.TabIndex = 15;
		this.label5.Text = "Email";
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label6.Location = new System.Drawing.Point(12, 200);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(49, 19);
		this.label6.TabIndex = 16;
		this.label6.Text = "Город";
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label7.Location = new System.Drawing.Point(12, 233);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(52, 19);
		this.label7.TabIndex = 17;
		this.label7.Text = "Логин";
		// 
		// label8
		// 
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label8.Location = new System.Drawing.Point(12, 266);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(58, 19);
		this.label8.TabIndex = 18;
		this.label8.Text = "Пароль";
		// 
		// _btnAdd
		// 
		this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnAdd.Location = new System.Drawing.Point(12, 352);
		this._btnAdd.Name = "_btnAdd";
		this._btnAdd.Size = new System.Drawing.Size(137, 32);
		this._btnAdd.TabIndex = 19;
		this._btnAdd.Text = "Добавить";
		this._btnAdd.UseVisualStyleBackColor = true;
		this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
		// 
		// _btnCancel
		// 
		this._btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnCancel.Location = new System.Drawing.Point(169, 352);
		this._btnCancel.Name = "_btnCancel";
		this._btnCancel.Size = new System.Drawing.Size(137, 32);
		this._btnCancel.TabIndex = 20;
		this._btnCancel.Text = "Отменить";
		this._btnCancel.UseVisualStyleBackColor = true;
		this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
		// 
		// _btnEdit
		// 
		this._btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnEdit.Location = new System.Drawing.Point(12, 352);
		this._btnEdit.Name = "_btnEdit";
		this._btnEdit.Size = new System.Drawing.Size(137, 32);
		this._btnEdit.TabIndex = 21;
		this._btnEdit.Text = "Изменить";
		this._btnEdit.UseVisualStyleBackColor = true;
		this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
		// 
		// EditUserForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.ClientSize = new System.Drawing.Size(318, 396);
		this.Controls.Add(this._btnCancel);
		this.Controls.Add(this._btnAdd);
		this.Controls.Add(this.label8);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this._txtPassword);
		this.Controls.Add(this._txtUsername);
		this.Controls.Add(this._txtCity);
		this.Controls.Add(this._txtPhone);
		this.Controls.Add(this._txtEmail);
		this.Controls.Add(this._txtPatronymic);
		this.Controls.Add(this._txtName);
		this.Controls.Add(this._txtSurname);
		this.Controls.Add(this._btnEdit);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.Name = "EditUserForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Редактор пользователя";
		this.Load += new System.EventHandler(this.EditUserFormLoad);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private TextBox _txtSurname;
	private TextBox _txtName;
	private TextBox _txtPatronymic;
	private TextBox _txtEmail;
	private TextBox _txtPhone;
	private TextBox _txtCity;
	private TextBox _txtUsername;
	private TextBox _txtPassword;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label label4;
	private Label label5;
	private Label label6;
	private Label label7;
	private Label label8;
	private Button _btnAdd;
	private Button _btnCancel;
	private Button _btnEdit;
}
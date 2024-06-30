namespace PostSys.Application.Views.Forms.EditingForms;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAddressForm));
		_cbxRecipient = new ComboBox();
		_cbxCity = new ComboBox();
		_btnAdd = new Button();
		_btnCancel = new Button();
		_btnEdit = new Button();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label4 = new Label();
		_cbxHome = new ComboBox();
		label5 = new Label();
		label6 = new Label();
		_cbxPostman = new ComboBox();
		_txtGoods = new TextBox();
		numberPlot = new Label();
		_cbxStreet = new ComboBox();
		label7 = new Label();
		_lblPlot = new Label();
		_txtApartment = new TextBox();
		label8 = new Label();
		SuspendLayout();
		// 
		// _cbxRecipient
		// 
		_cbxRecipient.DropDownStyle = ComboBoxStyle.DropDownList;
		_cbxRecipient.Font = new Font("Times New Roman", 14.25F);
		_cbxRecipient.FormattingEnabled = true;
		_cbxRecipient.Location = new Point(128, 58);
		_cbxRecipient.Name = "_cbxRecipient";
		_cbxRecipient.Size = new Size(153, 29);
		_cbxRecipient.TabIndex = 1;
		// 
		// _cbxCity
		// 
		_cbxCity.DropDownStyle = ComboBoxStyle.DropDownList;
		_cbxCity.Font = new Font("Times New Roman", 14.25F);
		_cbxCity.FormattingEnabled = true;
		_cbxCity.Location = new Point(128, 91);
		_cbxCity.Name = "_cbxCity";
		_cbxCity.Size = new Size(153, 29);
		_cbxCity.TabIndex = 2;
		_cbxCity.SelectedIndexChanged += CbxCitySelectedIndexChanged;
		// 
		// _btnAdd
		// 
		_btnAdd.Font = new Font("Times New Roman", 12F);
		_btnAdd.Location = new Point(11, 342);
		_btnAdd.Name = "_btnAdd";
		_btnAdd.Size = new Size(121, 31);
		_btnAdd.TabIndex = 8;
		_btnAdd.Text = "Добавить";
		_btnAdd.UseVisualStyleBackColor = true;
		_btnAdd.Click += OnAddClick;
		// 
		// _btnCancel
		// 
		_btnCancel.Font = new Font("Times New Roman", 12F);
		_btnCancel.Location = new Point(160, 342);
		_btnCancel.Name = "_btnCancel";
		_btnCancel.Size = new Size(121, 31);
		_btnCancel.TabIndex = 9;
		_btnCancel.Text = "Отменить";
		_btnCancel.UseVisualStyleBackColor = true;
		_btnCancel.Click += OnCancelClick;
		// 
		// _btnEdit
		// 
		_btnEdit.Font = new Font("Times New Roman", 12F);
		_btnEdit.Location = new Point(12, 342);
		_btnEdit.Name = "_btnEdit";
		_btnEdit.Size = new Size(121, 31);
		_btnEdit.TabIndex = 8;
		_btnEdit.Text = "Изменить";
		_btnEdit.UseVisualStyleBackColor = true;
		_btnEdit.Click += OnEditClick;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Times New Roman", 12F);
		label1.Location = new Point(12, 29);
		label1.Name = "label1";
		label1.Size = new Size(64, 19);
		label1.TabIndex = 6;
		label1.Text = "Участок";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Times New Roman", 12F);
		label2.Location = new Point(12, 62);
		label2.Name = "label2";
		label2.Size = new Size(87, 19);
		label2.TabIndex = 7;
		label2.Text = "Получатель";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Times New Roman", 12F);
		label3.Location = new Point(12, 95);
		label3.Name = "label3";
		label3.Size = new Size(49, 19);
		label3.TabIndex = 8;
		label3.Text = "Город";
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Font = new Font("Times New Roman", 12F);
		label4.Location = new Point(12, 161);
		label4.Name = "label4";
		label4.Size = new Size(38, 19);
		label4.TabIndex = 9;
		label4.Text = "Дом";
		// 
		// _cbxHome
		// 
		_cbxHome.DropDownStyle = ComboBoxStyle.DropDownList;
		_cbxHome.Font = new Font("Times New Roman", 14.25F);
		_cbxHome.FormattingEnabled = true;
		_cbxHome.Location = new Point(128, 157);
		_cbxHome.Name = "_cbxHome";
		_cbxHome.Size = new Size(153, 29);
		_cbxHome.TabIndex = 4;
		_cbxHome.SelectedIndexChanged += CbxHomeSelectedIndexChanged;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Font = new Font("Times New Roman", 12F);
		label5.Location = new Point(12, 227);
		label5.Name = "label5";
		label5.Size = new Size(82, 19);
		label5.TabIndex = 11;
		label5.Text = "Почтальон";
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Font = new Font("Times New Roman", 12F);
		label6.Location = new Point(12, 260);
		label6.Name = "label6";
		label6.Size = new Size(48, 19);
		label6.TabIndex = 12;
		label6.Text = "Товар";
		// 
		// _cbxPostman
		// 
		_cbxPostman.DropDownStyle = ComboBoxStyle.DropDownList;
		_cbxPostman.Font = new Font("Times New Roman", 14.25F);
		_cbxPostman.FormattingEnabled = true;
		_cbxPostman.Location = new Point(128, 223);
		_cbxPostman.Name = "_cbxPostman";
		_cbxPostman.Size = new Size(153, 29);
		_cbxPostman.TabIndex = 6;
		// 
		// _txtGoods
		// 
		_txtGoods.Font = new Font("Times New Roman", 12F);
		_txtGoods.Location = new Point(128, 257);
		_txtGoods.MaxLength = 120;
		_txtGoods.Multiline = true;
		_txtGoods.Name = "_txtGoods";
		_txtGoods.Size = new Size(152, 27);
		_txtGoods.TabIndex = 7;
		_txtGoods.KeyDown += TxtBoxesKeyDown;
		// 
		// numberPlot
		// 
		numberPlot.AutoSize = true;
		numberPlot.Font = new Font("Times New Roman", 12F);
		numberPlot.Location = new Point(129, 29);
		numberPlot.Name = "numberPlot";
		numberPlot.Size = new Size(0, 19);
		numberPlot.TabIndex = 15;
		// 
		// _cbxStreet
		// 
		_cbxStreet.DropDownStyle = ComboBoxStyle.DropDownList;
		_cbxStreet.Font = new Font("Times New Roman", 14.25F);
		_cbxStreet.FormattingEnabled = true;
		_cbxStreet.Location = new Point(128, 124);
		_cbxStreet.Name = "_cbxStreet";
		_cbxStreet.Size = new Size(153, 29);
		_cbxStreet.TabIndex = 3;
		_cbxStreet.SelectedIndexChanged += CbxStreetSelectedIndexChanged;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Font = new Font("Times New Roman", 12F);
		label7.Location = new Point(11, 128);
		label7.Name = "label7";
		label7.Size = new Size(51, 19);
		label7.TabIndex = 17;
		label7.Text = "Улица";
		// 
		// _lblPlot
		// 
		_lblPlot.AutoSize = true;
		_lblPlot.Font = new Font("Times New Roman", 12F);
		_lblPlot.Location = new Point(128, 29);
		_lblPlot.Name = "_lblPlot";
		_lblPlot.Size = new Size(0, 19);
		_lblPlot.TabIndex = 18;
		// 
		// _txtApartment
		// 
		_txtApartment.Font = new Font("Times New Roman", 12F);
		_txtApartment.Location = new Point(128, 191);
		_txtApartment.MaxLength = 5;
		_txtApartment.Multiline = true;
		_txtApartment.Name = "_txtApartment";
		_txtApartment.Size = new Size(152, 27);
		_txtApartment.TabIndex = 5;
		_txtApartment.KeyDown += TxtBoxesKeyDown;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Font = new Font("Times New Roman", 12F);
		label8.Location = new Point(11, 194);
		label8.Name = "label8";
		label8.Size = new Size(74, 19);
		label8.TabIndex = 20;
		label8.Text = "Квартира";
		// 
		// EditAddressForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(297, 385);
		Controls.Add(label8);
		Controls.Add(_txtApartment);
		Controls.Add(_lblPlot);
		Controls.Add(label7);
		Controls.Add(_cbxStreet);
		Controls.Add(numberPlot);
		Controls.Add(_txtGoods);
		Controls.Add(_cbxPostman);
		Controls.Add(label6);
		Controls.Add(label5);
		Controls.Add(_cbxHome);
		Controls.Add(label4);
		Controls.Add(label3);
		Controls.Add(label2);
		Controls.Add(label1);
		Controls.Add(_btnCancel);
		Controls.Add(_btnAdd);
		Controls.Add(_cbxCity);
		Controls.Add(_cbxRecipient);
		Controls.Add(_btnEdit);
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Icon = (Icon)resources.GetObject("$this.Icon");
		MaximizeBox = false;
		Name = "EditAddressForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Редактор адреса";
		Load += EditAddressFormLoad;
		ResumeLayout(false);
		PerformLayout();
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
	private ComboBox _cbxPostman;
	private TextBox _txtGoods;
	private Label numberPlot;
	private ComboBox _cbxStreet;
	private Label label7;
	private Label _lblPlot;
	private TextBox _txtApartment;
	private Label label8;
}
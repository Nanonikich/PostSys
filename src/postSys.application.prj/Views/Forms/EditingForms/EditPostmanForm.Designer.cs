namespace PostSys.Application.Views.Forms.EditingForms;

partial class EditPostmanForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPostmanForm));
		this._btnAdd = new System.Windows.Forms.Button();
		this._btnCancel = new System.Windows.Forms.Button();
		this._txtSurname = new System.Windows.Forms.TextBox();
		this._txtName = new System.Windows.Forms.TextBox();
		this._txtPatronymic = new System.Windows.Forms.TextBox();
		this._txtPhone = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this._btnEdit = new System.Windows.Forms.Button();
		this._cbxPlot = new System.Windows.Forms.ComboBox();
		this.SuspendLayout();
		// 
		// _btnAdd
		// 
		this._btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnAdd.Location = new System.Drawing.Point(11, 334);
		this._btnAdd.Name = "_btnAdd";
		this._btnAdd.Size = new System.Drawing.Size(137, 32);
		this._btnAdd.TabIndex = 6;
		this._btnAdd.Text = "Добавить";
		this._btnAdd.UseVisualStyleBackColor = true;
		this._btnAdd.Click += new System.EventHandler(this.OnAddClick);
		// 
		// _btnCancel
		// 
		this._btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnCancel.Location = new System.Drawing.Point(172, 334);
		this._btnCancel.Name = "_btnCancel";
		this._btnCancel.Size = new System.Drawing.Size(137, 32);
		this._btnCancel.TabIndex = 7;
		this._btnCancel.Text = "Отменить";
		this._btnCancel.UseVisualStyleBackColor = true;
		this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
		// 
		// _txtSurname
		// 
		this._txtSurname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtSurname.Location = new System.Drawing.Point(106, 27);
		this._txtSurname.MaxLength = 30;
		this._txtSurname.Multiline = true;
		this._txtSurname.Name = "_txtSurname";
		this._txtSurname.Size = new System.Drawing.Size(203, 31);
		this._txtSurname.TabIndex = 1;
		this._txtSurname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtName
		// 
		this._txtName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtName.Location = new System.Drawing.Point(106, 74);
		this._txtName.MaxLength = 30;
		this._txtName.Multiline = true;
		this._txtName.Name = "_txtName";
		this._txtName.Size = new System.Drawing.Size(203, 31);
		this._txtName.TabIndex = 2;
		this._txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPatronymic
		// 
		this._txtPatronymic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPatronymic.Location = new System.Drawing.Point(106, 120);
		this._txtPatronymic.MaxLength = 35;
		this._txtPatronymic.Multiline = true;
		this._txtPatronymic.Name = "_txtPatronymic";
		this._txtPatronymic.Size = new System.Drawing.Size(203, 31);
		this._txtPatronymic.TabIndex = 3;
		this._txtPatronymic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// _txtPhone
		// 
		this._txtPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._txtPhone.Location = new System.Drawing.Point(106, 166);
		this._txtPhone.MaxLength = 60;
		this._txtPhone.Multiline = true;
		this._txtPhone.Name = "_txtPhone";
		this._txtPhone.Size = new System.Drawing.Size(203, 31);
		this._txtPhone.TabIndex = 4;
		this._txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxesKeyDown);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label1.Location = new System.Drawing.Point(12, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 19);
		this.label1.TabIndex = 11;
		this.label1.Text = "Фамилия";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label2.Location = new System.Drawing.Point(12, 82);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 19);
		this.label2.TabIndex = 12;
		this.label2.Text = "Имя";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label3.Location = new System.Drawing.Point(11, 132);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 19);
		this.label3.TabIndex = 13;
		this.label3.Text = "Отчество";
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label7.Location = new System.Drawing.Point(11, 178);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 19);
		this.label7.TabIndex = 17;
		this.label7.Text = "Телефон";
		// 
		// label9
		// 
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this.label9.Location = new System.Drawing.Point(11, 218);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(64, 19);
		this.label9.TabIndex = 19;
		this.label9.Text = "Участок";
		// 
		// _btnEdit
		// 
		this._btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._btnEdit.Location = new System.Drawing.Point(12, 334);
		this._btnEdit.Name = "_btnEdit";
		this._btnEdit.Size = new System.Drawing.Size(137, 32);
		this._btnEdit.TabIndex = 20;
		this._btnEdit.Text = "Изменить";
		this._btnEdit.UseVisualStyleBackColor = true;
		this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
		// 
		// _cbxPlot
		// 
		this._cbxPlot.BackColor = System.Drawing.SystemColors.Window;
		this._cbxPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this._cbxPlot.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		this._cbxPlot.FormattingEnabled = true;
		this._cbxPlot.Location = new System.Drawing.Point(106, 212);
		this._cbxPlot.Name = "_cbxPlot";
		this._cbxPlot.Size = new System.Drawing.Size(203, 29);
		this._cbxPlot.TabIndex = 5;
		// 
		// EditPostmanForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.ClientSize = new System.Drawing.Size(322, 378);
		this.Controls.Add(this._cbxPlot);
		this.Controls.Add(this._btnAdd);
		this.Controls.Add(this._btnEdit);
		this.Controls.Add(this.label9);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this._txtPhone);
		this.Controls.Add(this._txtPatronymic);
		this.Controls.Add(this._txtName);
		this.Controls.Add(this._txtSurname);
		this.Controls.Add(this._btnCancel);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximizeBox = false;
		this.Name = "EditPostmanForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Редактор почтальона";
		this.Load += new System.EventHandler(this.EditPostmanFormLoad);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private Button _btnAdd;
	private Button _btnCancel;
	private TextBox _txtSurname;
	private TextBox _txtName;
	private TextBox _txtPatronymic;
	private TextBox _txtPhone;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label label7;
	private Label label9;
	private Button _btnEdit;
	private ComboBox _cbxPlot;
}
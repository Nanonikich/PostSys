namespace PostSys.Application.Views.Forms;

partial class AuthentificationForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthentificationForm));
		_postPicture = new PictureBox();
		label1 = new Label();
		_txtUsername = new TextBox();
		label2 = new Label();
		_txtPassword = new TextBox();
		_btnAuthentification = new Button();
		((System.ComponentModel.ISupportInitialize)_postPicture).BeginInit();
		SuspendLayout();
		// 
		// _postPicture
		// 
		_postPicture.Image = (Image)resources.GetObject("_postPicture.Image");
		_postPicture.Location = new Point(62, 26);
		_postPicture.Name = "_postPicture";
		_postPicture.Size = new Size(165, 147);
		_postPicture.SizeMode = PictureBoxSizeMode.Zoom;
		_postPicture.TabIndex = 6;
		_postPicture.TabStop = false;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Times New Roman", 12F);
		label1.Location = new Point(48, 206);
		label1.Name = "label1";
		label1.Size = new Size(52, 19);
		label1.TabIndex = 7;
		label1.Text = "Логин";
		// 
		// _txtUsername
		// 
		_txtUsername.Font = new Font("Times New Roman", 12F);
		_txtUsername.Location = new Point(48, 234);
		_txtUsername.MaxLength = 30;
		_txtUsername.Multiline = true;
		_txtUsername.Name = "_txtUsername";
		_txtUsername.Size = new Size(192, 28);
		_txtUsername.TabIndex = 8;
		_txtUsername.KeyDown += TxtBoxesKeyDown;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Times New Roman", 12F);
		label2.Location = new Point(48, 279);
		label2.Name = "label2";
		label2.Size = new Size(58, 19);
		label2.TabIndex = 9;
		label2.Text = "Пароль";
		// 
		// _txtPassword
		// 
		_txtPassword.Font = new Font("Times New Roman", 12F);
		_txtPassword.Location = new Point(48, 301);
		_txtPassword.MaxLength = 20;
		_txtPassword.Multiline = true;
		_txtPassword.Name = "_txtPassword";
		_txtPassword.Size = new Size(192, 28);
		_txtPassword.TabIndex = 10;
		_txtPassword.UseSystemPasswordChar = true;
		_txtPassword.KeyDown += TxtBoxesKeyDown;
		// 
		// _btnAuthentification
		// 
		_btnAuthentification.Font = new Font("Times New Roman", 12F);
		_btnAuthentification.Location = new Point(48, 356);
		_btnAuthentification.Name = "_btnAuthentification";
		_btnAuthentification.Size = new Size(192, 36);
		_btnAuthentification.TabIndex = 11;
		_btnAuthentification.Text = "Войти";
		_btnAuthentification.UseVisualStyleBackColor = true;
		_btnAuthentification.Click += OnLoginClick;
		// 
		// AuthentificationForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(283, 415);
		Controls.Add(_btnAuthentification);
		Controls.Add(_txtPassword);
		Controls.Add(label2);
		Controls.Add(_txtUsername);
		Controls.Add(label1);
		Controls.Add(_postPicture);
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Icon = (Icon)resources.GetObject("$this.Icon");
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "AuthentificationForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Вход";
		FormClosed += LoginFormClosed;
		((System.ComponentModel.ISupportInitialize)_postPicture).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private PictureBox _postPicture;
	private Label label1;
	private TextBox _txtUsername;
	private Label label2;
	private TextBox _txtPassword;
	private Button _btnAuthentification;
}
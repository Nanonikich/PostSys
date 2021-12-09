namespace mUse.application.prj.Views.Forms
{
	partial class LoginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this._loginPicture = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this._txtUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._txtPassword = new System.Windows.Forms.TextBox();
			this._btnLogin = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._loginPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// _loginPicture
			// 
			this._loginPicture.Image = ((System.Drawing.Image)(resources.GetObject("_loginPicture.Image")));
			this._loginPicture.Location = new System.Drawing.Point(62, 26);
			this._loginPicture.Name = "_loginPicture";
			this._loginPicture.Size = new System.Drawing.Size(165, 147);
			this._loginPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this._loginPicture.TabIndex = 6;
			this._loginPicture.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(48, 206);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 19);
			this.label1.TabIndex = 7;
			this.label1.Text = "Логин";
			// 
			// _txtUsername
			// 
			this._txtUsername.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtUsername.Location = new System.Drawing.Point(48, 234);
			this._txtUsername.Multiline = true;
			this._txtUsername.Name = "_txtUsername";
			this._txtUsername.Size = new System.Drawing.Size(192, 28);
			this._txtUsername.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(48, 279);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "Пароль";
			// 
			// _txtPassword
			// 
			this._txtPassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._txtPassword.Location = new System.Drawing.Point(48, 301);
			this._txtPassword.Multiline = true;
			this._txtPassword.Name = "_txtPassword";
			this._txtPassword.Size = new System.Drawing.Size(192, 28);
			this._txtPassword.TabIndex = 10;
			// 
			// _btnLogin
			// 
			this._btnLogin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this._btnLogin.Location = new System.Drawing.Point(48, 356);
			this._btnLogin.Name = "_btnLogin";
			this._btnLogin.Size = new System.Drawing.Size(192, 36);
			this._btnLogin.TabIndex = 11;
			this._btnLogin.Text = "Войти";
			this._btnLogin.UseVisualStyleBackColor = true;
			this._btnLogin.Click += new System.EventHandler(this.OnLoginClick);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 415);
			this.Controls.Add(this._btnLogin);
			this.Controls.Add(this._txtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._txtUsername);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._loginPicture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Вход";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this._loginPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PictureBox _loginPicture;
		private Label label1;
		private TextBox _txtUsername;
		private Label label2;
		private TextBox _txtPassword;
		private Button _btnLogin;
	}
}
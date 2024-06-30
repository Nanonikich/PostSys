namespace PostSys.Application.Views.Controls.StreetCity
{
	partial class StreetsControl
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
			_pnlStreetsTable = new Panel();
			_btnDeleteStreet = new Button();
			_btnAddStreet = new Button();
			_txtStreet = new TextBox();
			_lblStreet = new Label();
			SuspendLayout();
			// 
			// _pnlStreetsTable
			// 
			_pnlStreetsTable.Location = new Point(211, 15);
			_pnlStreetsTable.Name = "_pnlStreetsTable";
			_pnlStreetsTable.Size = new Size(241, 215);
			_pnlStreetsTable.TabIndex = 17;
			// 
			// _btnDeleteStreet
			// 
			_btnDeleteStreet.Font = new Font("Times New Roman", 12F);
			_btnDeleteStreet.Location = new Point(14, 200);
			_btnDeleteStreet.Name = "_btnDeleteStreet";
			_btnDeleteStreet.Size = new Size(178, 30);
			_btnDeleteStreet.TabIndex = 15;
			_btnDeleteStreet.Text = "Удалить";
			_btnDeleteStreet.UseVisualStyleBackColor = true;
			_btnDeleteStreet.Click += OnDeleteStreetClick;
			// 
			// _btnAddStreet
			// 
			_btnAddStreet.Font = new Font("Times New Roman", 12F);
			_btnAddStreet.Location = new Point(14, 164);
			_btnAddStreet.Name = "_btnAddStreet";
			_btnAddStreet.Size = new Size(178, 30);
			_btnAddStreet.TabIndex = 14;
			_btnAddStreet.Text = "Добавить";
			_btnAddStreet.UseVisualStyleBackColor = true;
			_btnAddStreet.Click += OnAddStreetClick;
			// 
			// _txtStreet
			// 
			_txtStreet.Font = new Font("Times New Roman", 12F);
			_txtStreet.Location = new Point(14, 48);
			_txtStreet.MaxLength = 30;
			_txtStreet.Multiline = true;
			_txtStreet.Name = "_txtStreet";
			_txtStreet.Size = new Size(178, 28);
			_txtStreet.TabIndex = 13;
			// 
			// _lblStreet
			// 
			_lblStreet.AutoSize = true;
			_lblStreet.Font = new Font("Times New Roman", 12F);
			_lblStreet.Location = new Point(14, 26);
			_lblStreet.Name = "_lblStreet";
			_lblStreet.Size = new Size(51, 19);
			_lblStreet.TabIndex = 16;
			_lblStreet.Text = "Улица";
			// 
			// StreetsControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ActiveCaption;
			Controls.Add(_pnlStreetsTable);
			Controls.Add(_btnDeleteStreet);
			Controls.Add(_btnAddStreet);
			Controls.Add(_txtStreet);
			Controls.Add(_lblStreet);
			Name = "StreetsControl";
			Size = new Size(466, 244);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel _pnlStreetsTable;
		private Button _btnDeleteStreet;
		private Button _btnAddStreet;
		private TextBox _txtStreet;
		private Label _lblStreet;
	}
}

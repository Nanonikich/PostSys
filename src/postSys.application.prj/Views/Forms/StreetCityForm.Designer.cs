namespace PostSys.Application.Views.Forms;

partial class StreetCityForm
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
		DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreetCityForm));
		_tabControl = new TabControl();
		tabPage1 = new TabPage();
		_btnDeleteStreet = new Button();
		_btnAddStreet = new Button();
		_txtStreet = new TextBox();
		_lblStreet = new Label();
		_dgvStreets = new DataGridView();
		tabPage2 = new TabPage();
		_dgvCities = new DataGridView();
		_btnDeleteCity = new Button();
		_btnAddCity = new Button();
		_txtCity = new TextBox();
		_lblCity = new Label();
		_tabControl.SuspendLayout();
		tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)_dgvStreets).BeginInit();
		tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)_dgvCities).BeginInit();
		SuspendLayout();
		// 
		// _tabControl
		// 
		_tabControl.Controls.Add(tabPage1);
		_tabControl.Controls.Add(tabPage2);
		_tabControl.Location = new Point(1, 1);
		_tabControl.Name = "_tabControl";
		_tabControl.SelectedIndex = 0;
		_tabControl.Size = new Size(474, 272);
		_tabControl.TabIndex = 11;
		_tabControl.SelectedIndexChanged += TabControlSelectedIndexChanged;
		// 
		// tabPage1
		// 
		tabPage1.BackColor = SystemColors.ActiveCaption;
		tabPage1.Controls.Add(_btnDeleteStreet);
		tabPage1.Controls.Add(_btnAddStreet);
		tabPage1.Controls.Add(_txtStreet);
		tabPage1.Controls.Add(_lblStreet);
		tabPage1.Controls.Add(_dgvStreets);
		tabPage1.Location = new Point(4, 24);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(466, 244);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Улицы";
		// 
		// _btnDeleteStreet
		// 
		_btnDeleteStreet.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnDeleteStreet.Location = new Point(12, 202);
		_btnDeleteStreet.Name = "_btnDeleteStreet";
		_btnDeleteStreet.Size = new Size(178, 30);
		_btnDeleteStreet.TabIndex = 9;
		_btnDeleteStreet.Text = "Удалить";
		_btnDeleteStreet.UseVisualStyleBackColor = true;
		_btnDeleteStreet.Click += OnDeleteStreetClick;
		// 
		// _btnAddStreet
		// 
		_btnAddStreet.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnAddStreet.Location = new Point(12, 166);
		_btnAddStreet.Name = "_btnAddStreet";
		_btnAddStreet.Size = new Size(178, 30);
		_btnAddStreet.TabIndex = 8;
		_btnAddStreet.Text = "Добавить";
		_btnAddStreet.UseVisualStyleBackColor = true;
		_btnAddStreet.Click += OnAddStreetClick;
		// 
		// _txtStreet
		// 
		_txtStreet.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_txtStreet.Location = new Point(12, 50);
		_txtStreet.MaxLength = 30;
		_txtStreet.Multiline = true;
		_txtStreet.Name = "_txtStreet";
		_txtStreet.Size = new Size(178, 28);
		_txtStreet.TabIndex = 7;
		// 
		// _lblStreet
		// 
		_lblStreet.AutoSize = true;
		_lblStreet.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_lblStreet.Location = new Point(12, 28);
		_lblStreet.Name = "_lblStreet";
		_lblStreet.Size = new Size(51, 19);
		_lblStreet.TabIndex = 11;
		_lblStreet.Text = "Улица";
		// 
		// _dgvStreets
		// 
		_dgvStreets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		_dgvStreets.BackgroundColor = SystemColors.Control;
		dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = SystemColors.Control;
		dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
		dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
		_dgvStreets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		_dgvStreets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		_dgvStreets.Location = new Point(203, 19);
		_dgvStreets.MultiSelect = false;
		_dgvStreets.Name = "_dgvStreets";
		_dgvStreets.RowHeadersVisible = false;
		_dgvStreets.RowTemplate.Height = 25;
		_dgvStreets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		_dgvStreets.Size = new Size(253, 212);
		_dgvStreets.TabIndex = 10;
		_dgvStreets.TabStop = false;
		// 
		// tabPage2
		// 
		tabPage2.BackColor = SystemColors.ActiveCaption;
		tabPage2.Controls.Add(_dgvCities);
		tabPage2.Controls.Add(_btnDeleteCity);
		tabPage2.Controls.Add(_btnAddCity);
		tabPage2.Controls.Add(_txtCity);
		tabPage2.Controls.Add(_lblCity);
		tabPage2.Location = new Point(4, 24);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(466, 244);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "Города";
		// 
		// _dgvCities
		// 
		_dgvCities.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		_dgvCities.BackgroundColor = SystemColors.Control;
		dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = SystemColors.Control;
		dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
		dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
		_dgvCities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		_dgvCities.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		_dgvCities.Location = new Point(203, 19);
		_dgvCities.MultiSelect = false;
		_dgvCities.Name = "_dgvCities";
		_dgvCities.RowHeadersVisible = false;
		_dgvCities.RowTemplate.Height = 25;
		_dgvCities.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		_dgvCities.Size = new Size(253, 212);
		_dgvCities.TabIndex = 8;
		_dgvCities.TabStop = false;
		// 
		// _btnDeleteCity
		// 
		_btnDeleteCity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnDeleteCity.Location = new Point(12, 202);
		_btnDeleteCity.Name = "_btnDeleteCity";
		_btnDeleteCity.Size = new Size(178, 30);
		_btnDeleteCity.TabIndex = 11;
		_btnDeleteCity.Text = "Удалить";
		_btnDeleteCity.UseVisualStyleBackColor = true;
		_btnDeleteCity.Click += OnDeleteCityClick;
		// 
		// _btnAddCity
		// 
		_btnAddCity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_btnAddCity.Location = new Point(12, 166);
		_btnAddCity.Name = "_btnAddCity";
		_btnAddCity.Size = new Size(178, 30);
		_btnAddCity.TabIndex = 10;
		_btnAddCity.Text = "Добавить";
		_btnAddCity.UseVisualStyleBackColor = true;
		_btnAddCity.Click += OnAddCityClick;
		// 
		// _txtCity
		// 
		_txtCity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_txtCity.Location = new Point(12, 50);
		_txtCity.MaxLength = 30;
		_txtCity.Multiline = true;
		_txtCity.Name = "_txtCity";
		_txtCity.Size = new Size(178, 28);
		_txtCity.TabIndex = 9;
		// 
		// _lblCity
		// 
		_lblCity.AutoSize = true;
		_lblCity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
		_lblCity.Location = new Point(12, 28);
		_lblCity.Name = "_lblCity";
		_lblCity.Size = new Size(49, 19);
		_lblCity.TabIndex = 7;
		_lblCity.Text = "Город";
		// 
		// StreetCityForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(477, 277);
		Controls.Add(_tabControl);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		Icon = (Icon)resources.GetObject("$this.Icon");
		MaximizeBox = false;
		Name = "StreetCityForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Редактор улицы/города";
		Load += StreetCityFormLoad;
		_tabControl.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)_dgvStreets).EndInit();
		tabPage2.ResumeLayout(false);
		tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)_dgvCities).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private TabControl _tabControl;
	private TabPage tabPage1;
	private Button _btnDeleteStreet;
	private Button _btnAddStreet;
	private TextBox _txtStreet;
	private Label _lblStreet;
	private DataGridView _dgvStreets;
	private TabPage tabPage2;
	private DataGridView _dgvCities;
	private Button _btnDeleteCity;
	private Button _btnAddCity;
	private TextBox _txtCity;
	private Label _lblCity;
}
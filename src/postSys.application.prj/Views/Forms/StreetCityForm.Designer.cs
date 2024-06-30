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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreetCityForm));
		_tabControl = new TabControl();
		tabPageOfStreets = new TabPage();
		tabPageOfCities = new TabPage();
		_tabControl.SuspendLayout();
		SuspendLayout();
		// 
		// _tabControl
		// 
		_tabControl.Controls.Add(tabPageOfStreets);
		_tabControl.Controls.Add(tabPageOfCities);
		_tabControl.Location = new Point(1, 1);
		_tabControl.Name = "_tabControl";
		_tabControl.SelectedIndex = 0;
		_tabControl.Size = new Size(474, 272);
		_tabControl.TabIndex = 11;
		_tabControl.SelectedIndexChanged += TabControlSelectedIndexChanged;
		// 
		// tabPageOfStreets
		// 
		tabPageOfStreets.BackColor = SystemColors.ActiveCaption;
		tabPageOfStreets.Location = new Point(4, 24);
		tabPageOfStreets.Name = "tabPageOfStreets";
		tabPageOfStreets.Padding = new Padding(3);
		tabPageOfStreets.Size = new Size(466, 244);
		tabPageOfStreets.TabIndex = 0;
		tabPageOfStreets.Text = "Улицы";
		// 
		// tabPageOfCities
		// 
		tabPageOfCities.BackColor = SystemColors.ActiveCaption;
		tabPageOfCities.Location = new Point(4, 24);
		tabPageOfCities.Name = "tabPageOfCities";
		tabPageOfCities.Padding = new Padding(3);
		tabPageOfCities.Size = new Size(466, 244);
		tabPageOfCities.TabIndex = 1;
		tabPageOfCities.Text = "Города";
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
		FormClosed += StreetCityFormClosed;
		_tabControl.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private TabControl _tabControl;
	private TabPage tabPageOfStreets;
	private TabPage tabPageOfCities;
}
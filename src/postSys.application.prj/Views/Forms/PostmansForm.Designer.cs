namespace PostSys.Application.Views.Forms;

partial class PostmansForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostmansForm));
		toolStrip1 = new ToolStrip();
		_btnAdd = new ToolStripButton();
		_btnEdit = new ToolStripButton();
		_btnDelete = new ToolStripButton();
		_txtSearchPlot = new ToolStripTextBox();
		_lblPlot = new ToolStripLabel();
		_pnlPostmans = new Panel();
		toolStrip1.SuspendLayout();
		SuspendLayout();
		// 
		// toolStrip1
		// 
		toolStrip1.AutoSize = false;
		toolStrip1.BackColor = SystemColors.ActiveCaption;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { _btnAdd, _btnEdit, _btnDelete, _txtSearchPlot, _lblPlot });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 51);
		toolStrip1.TabIndex = 1;
		toolStrip1.Tag = "2";
		toolStrip1.Text = "toolStrip1";
		// 
		// _btnAdd
		// 
		_btnAdd.AutoSize = false;
		_btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnAdd.Image = (Image)resources.GetObject("_btnAdd.Image");
		_btnAdd.ImageTransparentColor = Color.Magenta;
		_btnAdd.Name = "_btnAdd";
		_btnAdd.Size = new Size(40, 48);
		_btnAdd.Click += OnAddClick;
		// 
		// _btnEdit
		// 
		_btnEdit.AutoSize = false;
		_btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnEdit.Image = (Image)resources.GetObject("_btnEdit.Image");
		_btnEdit.ImageTransparentColor = Color.Magenta;
		_btnEdit.Name = "_btnEdit";
		_btnEdit.Size = new Size(40, 48);
		_btnEdit.Click += OnEditClick;
		// 
		// _btnDelete
		// 
		_btnDelete.AutoSize = false;
		_btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
		_btnDelete.Image = (Image)resources.GetObject("_btnDelete.Image");
		_btnDelete.ImageTransparentColor = Color.Magenta;
		_btnDelete.Name = "_btnDelete";
		_btnDelete.Size = new Size(40, 48);
		_btnDelete.Click += OnDeleteClick;
		// 
		// _txtSearchPlot
		// 
		_txtSearchPlot.Alignment = ToolStripItemAlignment.Right;
		_txtSearchPlot.AutoSize = false;
		_txtSearchPlot.BorderStyle = BorderStyle.FixedSingle;
		_txtSearchPlot.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
		_txtSearchPlot.Name = "_txtSearchPlot";
		_txtSearchPlot.Size = new Size(160, 26);
		_txtSearchPlot.KeyDown += OnSearchPlotKeyDown;
		_txtSearchPlot.TextChanged += OnSearchPlotTextChanged;
		// 
		// _lblPlot
		// 
		_lblPlot.Alignment = ToolStripItemAlignment.Right;
		_lblPlot.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
		_lblPlot.Name = "_lblPlot";
		_lblPlot.Size = new Size(52, 48);
		_lblPlot.Text = "Участок:";
		// 
		// _pnlPostmans
		// 
		_pnlPostmans.Dock = DockStyle.Fill;
		_pnlPostmans.Location = new Point(0, 51);
		_pnlPostmans.Name = "_pnlPostmans";
		_pnlPostmans.Size = new Size(800, 399);
		_pnlPostmans.TabIndex = 2;
		// 
		// PostmansForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(_pnlPostmans);
		Controls.Add(toolStrip1);
		Icon = (Icon)resources.GetObject("$this.Icon");
		MaximumSize = new Size(1366, 768);
		MinimumSize = new Size(816, 489);
		Name = "PostmansForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Почтальоны";
		WindowState = FormWindowState.Maximized;
		FormClosed += PostmansFormClosed;
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion
	private ToolStrip toolStrip1;
	private ToolStripButton _btnAdd;
	private ToolStripButton _btnEdit;
	private ToolStripButton _btnDelete;
	private Panel _pnlPostmans;
	private ToolStripLabel _lblPlot;
	private ToolStripTextBox _txtSearchPlot;
}
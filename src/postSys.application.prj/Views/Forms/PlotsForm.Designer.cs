namespace PostSys.Application.Views.Forms;

partial class PlotsForm
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
		components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotsForm));
		_pnlPlots = new Panel();
		_pnlPlotOptions = new Panel();
		_timer = new System.Windows.Forms.Timer(components);
		SuspendLayout();
		// 
		// _pnlPlots
		// 
		_pnlPlots.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		_pnlPlots.Location = new Point(216, 0);
		_pnlPlots.Name = "_pnlPlots";
		_pnlPlots.Size = new Size(637, 346);
		_pnlPlots.TabIndex = 10;
		// 
		// _pnlPlotOptions
		// 
		_pnlPlotOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
		_pnlPlotOptions.Location = new Point(0, 0);
		_pnlPlotOptions.Name = "_pnlPlotOptions";
		_pnlPlotOptions.Size = new Size(210, 347);
		_pnlPlotOptions.TabIndex = 11;
		// 
		// _timer
		// 
		_timer.Interval = 2000;
		_timer.Tick += OnTimerTick;
		// 
		// PlotsForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ActiveCaption;
		ClientSize = new Size(854, 347);
		Controls.Add(_pnlPlotOptions);
		Controls.Add(_pnlPlots);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "PlotsForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Участки";
		FormClosed += PlotsFormClosed;
		Load += PlotsFormLoad;
		ResumeLayout(false);
	}

	#endregion
	private Panel _pnlPlots;
	private Panel _pnlPlotOptions;
	private System.Windows.Forms.Timer _timer;
}
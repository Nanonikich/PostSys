namespace PostSys.Application.Views.Controls
{
	partial class BaseDgvControl
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
		protected void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			_timer = new System.Windows.Forms.Timer(components);
			_dgvClassBase = new DataGridView();
			((System.ComponentModel.ISupportInitialize)_dgvClassBase).BeginInit();
			SuspendLayout();
			// 
			// _timer
			// 
			_timer.Interval = 3000;
			_timer.Tick += OnTimerTick;
			// 
			// _dgvClassBase
			// 
			_dgvClassBase.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			_dgvClassBase.BackgroundColor = SystemColors.Control;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = SystemColors.Control;
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			_dgvClassBase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			_dgvClassBase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			_dgvClassBase.DefaultCellStyle = dataGridViewCellStyle2;
			_dgvClassBase.Dock = DockStyle.Fill;
			_dgvClassBase.Location = new Point(0, 0);
			_dgvClassBase.MultiSelect = false;
			_dgvClassBase.Name = "_dgvClassBase";
			_dgvClassBase.RowHeadersVisible = false;
			_dgvClassBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			_dgvClassBase.Size = new Size(699, 425);
			_dgvClassBase.TabIndex = 2;
			_dgvClassBase.TabStop = false;
			// 
			// BaseDgvControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(_dgvClassBase);
			Name = "BaseDgvControl";
			Size = new Size(699, 425);
			Load += BaseDgvControlLoad;
			((System.ComponentModel.ISupportInitialize)_dgvClassBase).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Timer _timer;
		private DataGridView _dgvClassBase;
	}
}

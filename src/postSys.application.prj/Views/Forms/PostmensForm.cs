using postSys.application.prj;
using postSys.application.prj.Views.Forms;
using System.Data;

namespace mUse.application.prj.Views.Forms
{
	public partial class PostmensForm : Form
	{
		#region Properties

		public bool ButtonEditClick { get; set; }
		public List<Postmen> DgvCurrentRow { get; set; } = null!;

		#endregion

		#region .ctor

		public PostmensForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ShowTable()
		{
			using PostSysContext db = new();

			_dgvPostmens.DataSource = (from postmen in db.Postmens
									   select new { 
										   ID = postmen.PostmenId,
										   Фамилия = postmen.PostmenSurname,
										   Имя = postmen.PostmenName,
										   Отчество = postmen.PostmenPatronymic,
										   Телефон = postmen.PostmenPhone,
										   Участок = postmen.PostmenPlot
												  }).ToList();
			db.Dispose();
		}

		public void ComboBoxUpd()
		{
			_cbxPlot.Items.Clear();
			_cbxPlot.Items.Add("Все участки");
			_cbxPlot.SelectedIndex = 0;

			using PostSysContext db = new();

			foreach (var number in (from codes in db.CodesAddresses
									select codes.CodeAddressPlot).ToList())
			{
				_cbxPlot.Items.Add(number);
			}

			db.Dispose();
		}

		#endregion

		#region Events

		private void MainForm_Load(object sender, EventArgs e)
		{
			ShowTable();

			ComboBoxUpd();
		}

		private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			using PostSysContext db = new();
			if (_cbxPlot.SelectedItem.ToString() == "Все участки")
			{
				ShowTable();
			}
			else
			{
				_dgvPostmens.DataSource = (from postmen in db.Postmens
										   where postmen.PostmenPlot.ToString() == _cbxPlot.SelectedItem.ToString()
										   select postmen).ToList();

				_dgvPostmens.Columns.Remove("Addresses");
			}
			db.Dispose();
		}

		private void OnAddClick(object sender, EventArgs e) => new EditPostmenForm(this).ShowDialog(); 

		private void OnEditClick(object sender, EventArgs e)
		{
			if (_dgvPostmens.CurrentRow != null)
			{
				ButtonEditClick = true;

				using PostSysContext db = new();
				DgvCurrentRow = (from postmen in db.Postmens
								 where postmen.PostmenId.ToString() == _dgvPostmens.CurrentRow.Cells[0].Value.ToString()
								 select postmen).ToList();
				db.Dispose();

				new EditPostmenForm(this).ShowDialog();
			}
			else
			{
				MessageBox.Show("В таблице нет данных");
			}
		}

		private void OnDeleteClick(object sender, EventArgs e)
		{
			try
			{
				if (_dgvPostmens.CurrentRow != null)
				{
					using PostSysContext db = new();

					db.Remove((from postmen in db.Postmens
							   where postmen.PostmenId.ToString() == _dgvPostmens.CurrentRow.Cells[0].Value.ToString()
							   select postmen).First());

					db.SaveChanges();
					db.Dispose();

					ShowTable();
				}
				else
				{
					MessageBox.Show("В таблице нет данных");
				}
			}
			catch
			{
				MessageBox.Show("Почтальон занят");
			}
		}

		#endregion
	}
}

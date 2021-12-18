using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class SendersForm : Form
	{
		#region Properties

		public bool ButtonEditClick { get; set; }
		public List<Sender> DgvCurrentRow { get; set; } = null!;

		#endregion

		#region .ctor

		public SendersForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ShowTable()
		{
			using PostSysContext db = new();

			_dgvSenders.DataSource = (from sender in db.Senders
									  select new { 
										  ID = sender.SenderId, 
										  Фамилия = sender.SenderSurname, 
										  Имя = sender.SenderName, 
										  Отчество = sender.SenderPatronymic, 
										  Город = sender.SenderCityNavigation.CityName, 
										  Улица = sender.SenderStreetNavigation.CodeAddressStreetNavigation.StreetName,
										  Дом = sender.SenderHome, 
										  Квартира = sender.SenderApartment, 
										  Телефон = sender.SenderPhone }).ToList();
			db.Dispose();
		}

		#endregion

		#region Events

		private void SendersForm_Load(object sender, EventArgs e) => ShowTable();

		private void OnAddClick(object sender, EventArgs e) => new EditSenderForm(this).ShowDialog();

		private void OnEditClick(object sender, EventArgs e)
		{
			if (_dgvSenders.CurrentRow != null)
			{
				ButtonEditClick = true;

				using PostSysContext db = new();
				DgvCurrentRow = (from senders in db.Senders
								 where senders.SenderId.ToString() == _dgvSenders.CurrentRow.Cells[0].Value.ToString()
								 select senders).ToList();
				db.Dispose();

				new EditSenderForm(this).ShowDialog();
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
				if (_dgvSenders.CurrentRow != null)
				{
					using PostSysContext db = new();

					db.Remove((from senders in db.Senders
							   where senders.SenderId.ToString() == _dgvSenders.CurrentRow.Cells[0].Value.ToString()
							   select senders).First());

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
				MessageBox.Show("Отправитель используется");
			}
		}

		private void OnSearchSenderTextChanged(object sender, EventArgs e)
		{
			using PostSysContext db = new();

			if (_txtSearchSender.Text == "")
			{
				ShowTable();
			}
			else
			{
				_dgvSenders.DataSource = (from senders in db.Senders
										  where senders.SenderSurname.Contains(_txtSearchSender.Text.ToLower())
										  select new
										  {
											  ID = senders.SenderId,
											  Фамилия = senders.SenderSurname,
											  Имя = senders.SenderName,
											  Отчество = senders.SenderPatronymic,
											  Город = senders.SenderCityNavigation.CityName,
											  Улица = senders.SenderStreetNavigation.CodeAddressStreetNavigation.StreetName,
											  Дом = senders.SenderHome,
											  Квартира = senders.SenderApartment,
											  Телефон = senders.SenderPhone
										  }).ToList();
			}

			db.Dispose();
		}

		private void OnSearchSenderKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				e.SuppressKeyPress = true;
		}

		#endregion
	}
}

using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class RecipientsForm : Form
	{
		#region Properties

		public bool ButtonEditClick { get; set; }
		public List<Recipient> DgvCurrentRow { get; set; } = null!;

		#endregion

		#region .ctor
		public RecipientsForm()
		{
			InitializeComponent();
		}
		#endregion

		#region Methods
		public void ShowTable()
		{
			using PostSysContext db = new();

			_dgvRecipients.DataSource = (from recipient in db.Recipients
										 select new
										 {
										  ID = recipient.RecipientId,
										  Серия = recipient.RecipientSeries,
										  Номер = recipient.RecipientNumber,
										  Фамилия = recipient.RecipientSurname,
										  Имя = recipient.RecipientName,
										  Отчество = recipient.RecipientPatronymic,
										  Город = recipient.RecipientCityNavigation.CityName,
										  Улица = recipient.RecipientStreetNavigation.CodeAddressStreetNavigation.StreetName,
										  Дом = recipient.RecipientHome,
										  Квартира = recipient.RecipientApartment,
										  Телефон = recipient.RecipientPhone,
										  Отправитель = recipient.RecipientSenderNavigation.SenderSurname
										 }).ToList();
			db.Dispose();
		}

		#endregion

		#region Events

		private void ClientsForm_Load(object sender, EventArgs e)
		{
			ShowTable();
		}

		private void OnAddClick(object sender, EventArgs e) => new EditRecipientForm(this).ShowDialog();

		private void OnEditClick(object sender, EventArgs e)
		{
			if (_dgvRecipients.CurrentRow != null)
			{
				ButtonEditClick = true;

				using PostSysContext db = new();
				DgvCurrentRow = (from recipients in db.Recipients
								 where recipients.RecipientId.ToString() == _dgvRecipients.CurrentRow.Cells[0].Value.ToString()
								 select recipients).ToList();
				db.Dispose();

				new EditRecipientForm(this).ShowDialog();
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
				if (_dgvRecipients.CurrentRow != null)
				{
					using PostSysContext db = new();

					db.Remove((from recipients in db.Recipients
							   where recipients.RecipientId.ToString() == _dgvRecipients.CurrentRow.Cells[0].Value.ToString()
							   select recipients).First());

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
				MessageBox.Show("Получатель используется");
			}
		}

		#endregion
	}
}
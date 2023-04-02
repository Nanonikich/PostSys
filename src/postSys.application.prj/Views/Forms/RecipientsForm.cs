using System.Data;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с информацией о получателях.</summary>
public partial class RecipientsForm : Form
{
	public bool ButtonEditClick { get; set; }

	public List<Recipient> DgvCurrentRow { get; set; } = null!;

	/// <summary>Создаёт экземпляр класса <see cref="RecipientsForm"/>.</summary>
	public RecipientsForm()
	{
		InitializeComponent();
	}


	#region Methods

	/// <summary>Загрузка таблицы с информацией о получателях.</summary>
	public void ShowTable()
	{
		using PostSysContext db = new();

		_dgvRecipients.DataSource = db.Recipients.Select(x => new
		{
			ID = x.RecipientId,
			Серия = x.RecipientSeries,
			Номер = x.RecipientNumber,
			Фамилия = x.RecipientSurname,
			Имя = x.RecipientName,
			Отчество = x.RecipientPatronymic,
			Город = x.RecipientCityNavigation.CityName,
			Улица = x.RecipientStreetNavigation.CodeAddressStreetNavigation.StreetName,
			Дом = x.RecipientHome,
			Квартира = x.RecipientApartment,
			Телефон = x.RecipientPhone,
			Отправитель = x.RecipientSenderNavigation.SenderSurname,
		}).ToList();

		db.Dispose();
	}

	#endregion

	#region Handlers

	private void RecipientsFormLoad(object sender, EventArgs e) => ShowTable();

	private void OnAddClick(object sender, EventArgs e) => new EditRecipientForm(this).ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvRecipients.CurrentRow != null)
		{
			ButtonEditClick = true;

			using PostSysContext db = new();

			DgvCurrentRow = db.Recipients
				.Where(x => x.RecipientId == (int)_dgvRecipients.CurrentRow.Cells[0].Value)
				.Select(x => x).ToList();

			db.Dispose();

			new EditRecipientForm(this).ShowDialog();
		}
		else
		{
			MessageBox.Show("В таблице нет данных.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e)
	{
		try
		{
			if(_dgvRecipients.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove(db.Recipients
					.Where(x => x.RecipientId == (int)_dgvRecipients.CurrentRow.Cells[0].Value)
					.Select(x => x).First());

				db.SaveChanges();
				db.Dispose();

				ShowTable();
			}
			else
			{
				MessageBox.Show("В таблице нет данных.");
			}
		}
		catch
		{
			MessageBox.Show("Получатель используется.");
		}
	}

	/// <summary>Поиск получателя по фамилии.</summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnSearchSenderOrRecipientTextChanged(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(_txtSearchSender.Text == "" && _txtSearchRecipient.Text == "")
		{
			ShowTable();
		}
		else
		{
			_dgvRecipients.DataSource = db.Recipients
				.Where(x => x.RecipientSenderNavigation.SenderSurname.Contains(_txtSearchSender.Text.ToLower()) &&
							x.RecipientSurname.Contains(_txtSearchRecipient.Text.ToLower()))
				.Select(x => new
				{
					ID = x.RecipientId,
					Серия = x.RecipientSeries,
					Номер = x.RecipientNumber,
					Фамилия = x.RecipientSurname,
					Имя = x.RecipientName,
					Отчество = x.RecipientPatronymic,
					Город = x.RecipientCityNavigation.CityName,
					Улица = x.RecipientStreetNavigation.CodeAddressStreetNavigation.StreetName,
					Дом = x.RecipientHome,
					Квартира = x.RecipientApartment,
					Телефон = x.RecipientPhone,
					Отправитель = x.RecipientSenderNavigation.SenderSurname,
				}).ToList();
		}

		db.Dispose();
	}

	private void OnTxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	#endregion
}
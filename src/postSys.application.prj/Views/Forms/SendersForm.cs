using System.Data;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с отправителями.</summary>
public partial class SendersForm : Form
{
	public bool ButtonEditClick { get; set; }

	public List<Sender> DgvCurrentRow { get; set; } = null!;

	/// <summary>Создаёт экземпляр класса <see cref="SendersForm"/>.</summary>
	public SendersForm()
	{
		InitializeComponent();
	}

	#region Methods

	/// <summary>Загружает данные об отправителях в таблицу.</summary>
	public void ShowTable()
	{
		using PostSysContext db = new();

		_dgvSenders.DataSource = db.Senders.Select(x => new
		{
			ID = x.SenderId,
			Фамилия = x.SenderSurname,
			Имя = x.SenderName,
			Отчество = x.SenderPatronymic,
			Город = x.SenderCityNavigation.CityName,
			Улица = x.SenderStreetNavigation.CodeAddressStreetNavigation.StreetName,
			Дом = x.SenderHome,
			Квартира = x.SenderApartment,
			Телефон = x.SenderPhone,
		}).ToList();

		db.Dispose();
	}

	#endregion

	#region Handlers

	private void SendersFormLoad(object sender, EventArgs e) => ShowTable();

	private void OnAddClick(object sender, EventArgs e) => new EditSenderForm(this).ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvSenders.CurrentRow != null)
		{
			ButtonEditClick = true;

			using PostSysContext db = new();
			
			DgvCurrentRow = db.Senders
				.Where(x => x.SenderId == (int)_dgvSenders.CurrentRow.Cells[0].Value)
				.Select(x => x).ToList();

			db.Dispose();

			new EditSenderForm(this).ShowDialog();
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
			if(_dgvSenders.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove(db.Senders
					.Where(x => x.SenderId == (int)_dgvSenders.CurrentRow.Cells[0].Value)
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
			MessageBox.Show("Отправитель уже задействован.");
		}
	}

	/// <summary>Поиск отправителя по фамилии.</summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnSearchSenderTextChanged(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(_txtSearchSender.Text == "")
		{
			ShowTable();
		}
		else
		{
			_dgvSenders.DataSource = db.Senders
				.Where(x => x.SenderSurname.Contains(_txtSearchSender.Text.ToLower()))
				.Select(x => new
				{
					ID = x.SenderId,
					Фамилия = x.SenderSurname,
					Имя = x.SenderName,
					Отчество = x.SenderPatronymic,
					Город = x.SenderCityNavigation.CityName,
					Улица = x.SenderStreetNavigation.CodeAddressStreetNavigation.StreetName,
					Дом = x.SenderHome,
					Квартира = x.SenderApartment,
					Телефон = x.SenderPhone,
				}).ToList();
		}

		db.Dispose();
	}

	private void OnSearchSenderKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	#endregion
}

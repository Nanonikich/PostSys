using System.Data;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Главная форма.</summary>
public partial class MainForm : Form
{
	private readonly (int Id, string Status) _activeUser;

	#region Properties

	public bool ButtonEditClick { get; set; }

	public List<Address> DgvCurrentRow { get; set; } = null!;

	private string? CurrentRowInDgv { get; set; }

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="MainForm"/>.</summary>
	/// <param name="activeUserId">Идентификатор пользователя системы.</param>
	/// <param name="activeUserId">Статус пользователя системы.</param>
	public MainForm(int activeUserId, string activeUserStatus)
	{
		_activeUser = (activeUserId, activeUserStatus);

		InitializeComponent();
	}

	#endregion

	#region Methods

	/// <summary>Загрузка таблицы.</summary>
	public void ShowTable()
	{
		if(_dgvAddresses.CurrentRow != null)
		{
			CurrentRowInDgv = _dgvAddresses.CurrentRow.Cells[1].Value.ToString();
		}

		timer.Start();

		using PostSysContext db = new();

		_dgvAddresses.DataSource = db.Addresses.Select(x => new
		{
			ID = x.AddressId,
			Участок = x.AddressPostmenNavigation.PostmenPlot,
			Получатель = x.AddressRecipientNavigation.RecipientSurname,
			Город = x.AddressRecipientNavigation.RecipientCityNavigation.CityName,
			Улица = x.AddressRecipientNavigation.RecipientStreetNavigation.CodeAddressStreetNavigation.StreetName,
			Дом = x.AddressHome,
			Квартира = x.AddressApartment,
			Почтальон = x.AddressPostmenNavigation.PostmenSurname,
			Товары = x.AddressGoods,
		}).ToList();

		db.Dispose();

		if(_dgvAddresses != null)
		{
			// закрашивание ряда
			foreach(DataGridViewRow row in _dgvAddresses.Rows)
			{
				if(row.Cells[1].Value.ToString().Equals(CurrentRowInDgv))
				{
					_dgvAddresses.CurrentCell = row.Cells[1];
				}
			}
		}
	}

	#endregion

	#region Handlers

	private void MainFormLoad(object sender, EventArgs e)
	{
		if(_activeUser.Status != "Admin")
			_btnUsers.Visible = false;

		ShowTable();
	}

	private void OnAddClick(object sender, EventArgs e) => new EditAddressForm(this).ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvAddresses.CurrentRow != null)
		{
			ButtonEditClick = true;

			using PostSysContext db = new();

			DgvCurrentRow = db.Addresses
				.Where(x => x.AddressId == (int)_dgvAddresses.CurrentRow.Cells[0].Value)
				.Select(x => x).ToList();

			db.Dispose();

			new EditAddressForm(this).ShowDialog();
		}
		else
		{
			MessageBox.Show("В таблице нет данных.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e)
	{
		if(_dgvAddresses.CurrentRow != null)
		{
			using PostSysContext db = new();

			db.Remove(db.Addresses
				.Where(x => x.AddressId == (int)_dgvAddresses.CurrentCell.Value)
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

	/// <summary>Поиск отправителя или почтальона.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchRecipientOrPostmenTextChanged(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(_txtSearchRecipient.Text == "" && _txtSearchPostmen.Text == "")
		{
			ShowTable();
		}
		else
		{
			_dgvAddresses.DataSource = db.Addresses
				.Where(s => s.AddressRecipientNavigation.RecipientSurname.Contains(_txtSearchRecipient.Text.ToLower()) &&
							s.AddressPostmenNavigation.PostmenSurname.Contains(_txtSearchPostmen.Text.ToLower()))
				.Select(x => new
				{
					ID = x.AddressId,
					Участок = x.AddressPostmenNavigation.PostmenPlot,
					Получатель = x.AddressRecipientNavigation.RecipientSurname,
					Город = x.AddressRecipientNavigation.RecipientCityNavigation.CityName,
					Улица = x.AddressRecipientNavigation.RecipientStreetNavigation.CodeAddressStreetNavigation.StreetName,
					Дом = x.AddressHome,
					Квартира = x.AddressApartment,
					Почтальон = x.AddressPostmenNavigation.PostmenSurname,
					Товары = x.AddressGoods

				}).ToList();
			timer.Stop();
		}

		db.Dispose();
	}

	private void OnTxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}


	private void OnPostmensClick(object sender, EventArgs e) => new PostmensForm().ShowDialog();

	private void OnSendersClick(object sender, EventArgs e) => new SendersForm().ShowDialog();

	private void OnClientsClick(object sender, EventArgs e) => new RecipientsForm().ShowDialog();

	private void OnCitiesClick(object sender, EventArgs e) => new StreetCityForm().ShowDialog();

	private void OnPlotsClick(object sender, EventArgs e) => new PlotsForm().ShowDialog();

	private void OnUsersClick(object sender, EventArgs e) => new UsersForm().ShowDialog();


	private void OnTimerTick(object sender, EventArgs e) => ShowTable();

	private void MainFormClosed(object sender, FormClosedEventArgs e)
		=> System.Windows.Forms.Application.Exit();

	#endregion

}
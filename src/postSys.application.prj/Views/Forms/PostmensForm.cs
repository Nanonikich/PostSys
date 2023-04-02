using System.Data;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с почтальонами.</summary>
public partial class PostmensForm : Form
{
	public bool ButtonEditClick { get; set; }

	public List<Postmen> DgvCurrentRow { get; set; } = null!;


	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="PostmensForm"/>.</summary>
	public PostmensForm()
	{
		InitializeComponent();
	}

	#endregion

	#region Methods

	/// <summary>Загрузка данных о почтальонах в таблицу.</summary>
	public void ShowTable()
	{
		using PostSysContext db = new();

		_dgvPostmens.DataSource = db.Postmens.Select(x => new
		{
			ID = x.PostmenId,
			Фамилия = x.PostmenSurname,
			Имя = x.PostmenName,
			Отчество = x.PostmenPatronymic,
			Телефон = x.PostmenPhone,
			Участок = x.PostmenPlot,
		}).ToList();

		db.Dispose();
	}

	/// <summary>Обновление ComboBox с участками.</summary>
	public void ComboBoxUpd()
	{
		_cbxPlot.Items.Clear();
		_cbxPlot.Items.Add("Все участки");
		_cbxPlot.SelectedIndex = 0;

		using PostSysContext db = new();

		foreach(var number in db.CodesAddresses.Select(x => x.CodeAddressPlot).ToList())
		{
			_cbxPlot.Items.Add(number);
		}

		db.Dispose();
	}

	#endregion

	#region Handlers

	private void PostmensFormLoad(object sender, EventArgs e)
	{
		ShowTable();

		ComboBoxUpd();
	}

	/// <summary>Обновление выбора участка в ComboBox.</summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
	{
		using PostSysContext db = new();
		if(_cbxPlot.SelectedItem.ToString() == "Все участки")
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
		if(_dgvPostmens.CurrentRow != null)
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
			MessageBox.Show("В таблице нет данных.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e)
	{
		try
		{
			if(_dgvPostmens.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove(db.Postmens
					.Where(x => x.PostmenId == (int)_dgvPostmens.CurrentRow.Cells[0].Value)
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
			MessageBox.Show("Почтальон уже задействован.");
		}
	}

	#endregion
}

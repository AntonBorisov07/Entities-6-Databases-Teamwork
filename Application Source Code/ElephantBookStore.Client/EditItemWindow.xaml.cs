namespace ElephantBookStore.Client
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Windows;
	using System.Windows.Input;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Models;

	/// <summary>
	/// Interaction logic for EditItemWindow.xaml
	/// </summary>
	public partial class EditItemWindow : Window, INotifyPropertyChanged
	{
		private Item item;
		private BookStoreContext context;
		private ICommand saveItemCommand;

		public EditItemWindow()
		{
		}

		public EditItemWindow(Item item, BookStoreContext context)
		{
			InitializeComponent();
			this.ItemToEdit = item;
			this.context = context;
		}

		public ICommand SaveItemCommand
		{
			get
			{
				if (this.saveItemCommand == null)
				{
					this.saveItemCommand = new RelayCommand(this.HandleItemSaving);
				}

				return this.saveItemCommand;
			}
		}

		private void HandleItemSaving(object obj)
		{
			this.context.SaveChanges();
			this.Close();
		}

		public ICollection<Category> Categories
		{
			get
			{
				var name = this.ItemToEdit.GetType().Name;
				return this.context.ProductTypes.First(p => p.ProductTypeName.Contains(name)).Categories.Where(c => c.IsDeleted == false).ToList();
			}
		}

		public Item ItemToEdit
		{
			get
			{
				return this.item;
			}
			set
			{
				NotifyPropertyChanged();
				this.item = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

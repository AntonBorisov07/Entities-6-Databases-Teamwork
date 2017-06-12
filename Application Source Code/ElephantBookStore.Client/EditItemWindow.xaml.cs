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
	using ElephantBookStore.Data.Contracts;

	/// <summary>
	/// Interaction logic for EditItemWindow.xaml
	/// </summary>
	public partial class EditItemWindow : Window, INotifyPropertyChanged
	{
		private Item item;
		private RelayCommand saveItemCommand;
		private IBookStoreContext context;

		public EditItemWindow()
		{
		}

		public EditItemWindow(Item item, IBookStoreContext context)
		{
			InitializeComponent();
			this.ItemToEdit = item;
			this.context = context;
		}

		public RelayCommand SaveItemCommand
		{
			get
			{
				if (this.saveItemCommand == null)
				{
					this.saveItemCommand = new RelayCommand(this.HandleItemSaving, this.HandleCanSaveItem);
				}

				return this.saveItemCommand;
			}
		}

		private bool HandleCanSaveItem(object obj)
		{
			var result = ItemValidator.ValidateItemName(this.ItemToEdit.Name) && ItemValidator.ValidateItemPrice(this.ItemToEdit.Price.ToString()) && ItemValidator.ValidateCategoryName(this.ItemToEdit.Category.CategoryName);

			if (this.ItemToEdit is Book)
			{
				result = result && ItemValidator.ValidateBookAuthor((this.ItemToEdit as Book).Author);
			}

			return result;
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

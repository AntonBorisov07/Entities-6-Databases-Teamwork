namespace ElephantBookStore.Client
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Windows;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Models;
	using ElephantBookStore.Data.Contracts;

	/// <summary>
	/// Interaction logic for AddItemWindow.xaml
	/// </summary>
	public partial class AddItemWindow : Window, INotifyPropertyChanged
	{
		private RelayCommand confirmItemAddition;
		private Type selectedType;
		private string newItemName;
		private string newItemAuthor;
		private string newItemPrice;
		private Category newItemCategory;
		private string newItemDescription;
		private IBookStoreContext dbContext;

		public event PropertyChangedEventHandler PropertyChanged;

		public AddItemWindow()
		{
		}

		public AddItemWindow(IBookStoreContext dbContext)
		{
			InitializeComponent();
			this.dbContext = dbContext;
		}

		public string NewItemName
		{
			get
			{
				return this.newItemName;
			}
			set
			{
				this.newItemName = value;
				this.ConfirmItemAddition.RaiseCanExecuteChanged();
			}
		}

		public string NewItemAuthor
		{
			get
			{
				return this.newItemAuthor;
			}
			set
			{
				this.newItemAuthor = value;
				this.ConfirmItemAddition.RaiseCanExecuteChanged();
			}
		}

		public string NewItemPrice
		{
			get
			{
				return this.newItemPrice;
			}
			set
			{
				this.newItemPrice = value;
				this.ConfirmItemAddition.RaiseCanExecuteChanged();
			}
		}

		public Category NewItemCategory
		{
			get
			{
				return this.newItemCategory;
			}
			set
			{
				this.newItemCategory = value;
				this.ConfirmItemAddition.RaiseCanExecuteChanged();
			}
		}

		public string NewItemDescription
		{
			get
			{
				return this.newItemDescription;
			}
			set
			{
				this.newItemDescription = value;
				this.ConfirmItemAddition.RaiseCanExecuteChanged();
			}
		}

		public Item NewItem { get; set; }

		public Type SelectedType
		{
			get { return this.selectedType; }
			set
			{
				this.selectedType = value;
				this.NewItem = Activator.CreateInstance(this.selectedType) as Item;
				NotifyPropertyChanged("NewItem");
			}
		}

		public IEnumerable<Category> Categories
		{
			get
			{
				return this.dbContext.Categories.Where(c => c.ProductType.ProductTypeName.Contains(this.SelectedType.Name) && c.IsDeleted == false).ToList();
			}
		}

		public IEnumerable<Type> ItemTypes
		{
			get
			{
				return from t in Assembly.GetAssembly(typeof(Item)).GetTypes()
					   where t.IsSubclassOf(typeof(Item))
					   select t;
			}
		}

		public RelayCommand ConfirmItemAddition
		{
			get
			{
				if (this.confirmItemAddition == null)
				{
					this.confirmItemAddition = new RelayCommand(this.AddNewItem, this.CanAddNewItem);
				}

				return this.confirmItemAddition;
			}
		}

		private void AddNewItem(object obj)
		{
			if (this.NewItem is Book)
			{
				(this.NewItem as Book).Author = this.NewItemAuthor;
			}

			this.NewItem.Name = this.NewItemName;
			this.NewItem.Price = decimal.Parse(this.NewItemPrice);
			this.NewItem.Category = this.NewItemCategory;

			this.dbContext.Items.Add(this.NewItem);
			this.dbContext.SaveChanges();
			this.Close();
		}

		private bool CanAddNewItem(object obj)
		{
			if (SelectedType == typeof(Book))
			{
				if (!ItemValidator.ValidateBookAuthor(this.NewItemAuthor))
				{
					return false;
				}
			}

			if (!ItemValidator.ValidateItemName(this.NewItemName) ||
				!ItemValidator.ValidateItemPrice(this.NewItemPrice))
			{
				return false;
			}


			return true;
		}

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

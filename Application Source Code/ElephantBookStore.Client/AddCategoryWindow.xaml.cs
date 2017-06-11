namespace ElephantBookStore.Client
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Models;

	/// <summary>
	/// Interaction logic for AddCategoryWindow.xaml
	/// </summary>
	public partial class AddCategoryWindow : Window
	{
		private BookStoreContext context;
		private RelayCommand addCategoryCommand;
		private string newCategoryName;

		public AddCategoryWindow(BookStoreContext context)
		{
			InitializeComponent();
			this.context = context;
		}

		public ICollection<ProductType> ProductTypes
		{
			get
			{
				return this.context.ProductTypes.ToList();
			}
		}

		public string NewCategoryName
		{
			get
			{
				return this.newCategoryName;
			}
			set
			{
				this.newCategoryName = value;
				this.AddCategoryCommand.RaiseCanExecuteChanged();
			}
		}

		public RelayCommand AddCategoryCommand
		{
			get
			{
				if (this.addCategoryCommand == null)
				{
					this.addCategoryCommand = new RelayCommand(this.HandleCategoryAddition, this.HandleCanAddCategory);
				}

				return this.addCategoryCommand;
			}
		}

		private bool HandleCanAddCategory(object obj)
		{
			return this.ValidateNewCategoryName();
		}

		private bool ValidateNewCategoryName()
		{
			if (string.IsNullOrEmpty(this.NewCategoryName) ||
							this.NewCategoryName.Length < 3 ||
							!char.IsUpper(this.NewCategoryName[0]) ||
							!this.NewCategoryName.ContainsOnlyLetters())
			{
				return false;
			}

			return true;
		}

		private void HandleCategoryAddition(object obj)
		{
			if (!this.ValidateNewCategoryName())
			{
				// Log into application logger
				this.Close();
			}

			(obj as ProductType).Categories.Add(new Category()
			{
				CategoryName = newCategoryName
			});

			this.context.SaveChanges();
			this.Close();
		}
	}
}

namespace ElephantBookStore.Client
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Input;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Models;
	using ElephantBookStore.Data.Contracts;
	using System;

	/// <summary>
	/// Interaction logic for DeleteCategoryWindow.xaml
	/// </summary>
	public partial class DeleteCategoryWindow : Window
	{
		private ICommand confirmCategoryDeletion;
		private IBookStoreContext dbContext;
		
		public DeleteCategoryWindow(IBookStoreContext dbContext)
		{
			InitializeComponent();

			if (dbContext == null)
			{
				throw new ArgumentException("Context cannot be null");
			}

			this.dbContext = dbContext;
		}

		public IEnumerable<Category> AllCategories
		{
			get
			{
				return this.dbContext.Categories.ToList();
			}
		}

		public ICommand ConfirmCategoryDeletion
		{
			get
			{
				if (this.confirmCategoryDeletion == null)
				{
					this.confirmCategoryDeletion = new RelayCommand(this.HandleCategoryDeletion);
				}

				return this.confirmCategoryDeletion;
			}
		}

		private void HandleCategoryDeletion(object obj)
		{
			var objAsCategory = obj as ICategory;

			objAsCategory.IsDeleted = true;

			foreach (var item in objAsCategory.Items)
			{
				item.IsDeleted = true;
			}

			this.dbContext.SaveChanges();
			this.Close();
		}
	}
}

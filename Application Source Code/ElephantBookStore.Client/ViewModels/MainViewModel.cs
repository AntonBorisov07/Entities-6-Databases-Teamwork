using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElephantBookStore.Client.Helpers;
using ElephantBookStore.Data;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Importers;
using ElephantBookStore.Data.Models;
using Microsoft.Win32;

namespace ElephantBookStore.Client.ViewModels
{
	public class MainViewModel
	{
		private Category selectedCategory;

		#region Commands
		private ICommand importProductTypesCommand;
		private ICommand importBooksJSON;
		private ICommand importGiftsJSON;

		public ICommand ImportProductTypesWithTheirCategories
		{
			get
			{
				if (this.importProductTypesCommand == null)
				{
					this.importProductTypesCommand = new RelayCommand(this.HandleProductTypesImport);
				}

				return this.importProductTypesCommand;
			}
		}

		public ICommand ImportBooksJSON
		{
			get
			{
				if (this.importBooksJSON == null)
				{
					this.importBooksJSON = new RelayCommand(this.HandleJSONBooksImport);
				}

				return this.importBooksJSON;
			}
		}

		public ICommand ImportGiftsJSON
		{
			get
			{
				if (this.importGiftsJSON == null)
				{
					this.importGiftsJSON = new RelayCommand(this.HandleJSONGiftsImport);
				}

				return this.importGiftsJSON;
			}
		}

		#endregion

		public ICollection<ProductType> ProductTypes
		{
			get
			{
				var cont = new BookStoreContext();
				var result = cont.ProductTypes.ToList();
				cont.Dispose();
				return result;
			}
		}

		public Category SelectedCategory
		{
			get
			{
				return this.selectedCategory;
			}
			set
			{
				this.selectedCategory = value;
			}
		}

		#region Commands Handlers
		private void HandleProductTypesImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var categoriesImporter = new XMLProductTypesImporter();
			categoriesImporter.ImportXMLToDBContext(new BookStoreContext(), dialog.FileName);
		}

		private void HandleJSONBooksImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var booksImporter = new JSONBooksImporter();
			booksImporter.ImportJSONToDBContext(new BookStoreContext(), dialog.FileName);
		}

		private void HandleJSONGiftsImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var giftsImporter = new JSONGiftsImporter();
			giftsImporter.ImportJSONToDBContext(new BookStoreContext(), dialog.FileName);
		}
		#endregion

	}
}

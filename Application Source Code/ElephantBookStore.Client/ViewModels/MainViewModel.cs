namespace ElephantBookStore.Client.ViewModels
{
	using Microsoft.Win32;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Windows.Input;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Contracts;
	using ElephantBookStore.Data.Importers;
	using ElephantBookStore.Data.Models;

	public class MainViewModel : INotifyPropertyChanged
	{
		private BookStoreContext dbContext;
		private OpenFileDialog dialog;
		private IXMLImporter productsImporter;
		private IJSONImporter booksImporter;
		private IJSONImporter giftsImporter;
		private IExcelImporter magazinesImporter;
		private IExcelImporter comicsImporter;

		public MainViewModel()
		{
			this.dbContext = new BookStoreContext();
			this.productsImporter = new XMLProductTypesImporter();
			this.booksImporter = new JSONBooksImporter();
			this.giftsImporter = new JSONGiftsImporter();
			this.magazinesImporter = new ExcelMagazinesImporter();
			this.comicsImporter = new ExcelComicsImporter();
			this.dialog = new OpenFileDialog()
			{
				InitialDirectory = Assembly.GetCallingAssembly().Location,
				Multiselect = true
			};
		}

		public MainViewModel(BookStoreContext context) : this()
		{
			if (context != null)
			{
				this.dbContext = context;
			}
		}

		#region Commands
		private ICommand importProductTypesCommand;
		private ICommand importBooksJSON;
		private ICommand importGiftsJSON;
		private ICommand importMagazines;
		private ICommand importComics;
		private ICommand editItemCommand;
		private ICommand deleteItemCommand;
		private ICommand addNewCategory;
		private ICommand addNewItem;
		private ICommand createPDFReport;

		private ICommand deleteCategory;

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

		public ICommand ImportMagazines
		{
			get
			{
				if (this.importMagazines == null)
				{
					this.importMagazines = new RelayCommand(this.HandleXLSMagazinesImport);
				}

				return this.importMagazines;
			}
		}

		public ICommand ImportComics
		{
			get
			{
				if (this.importComics == null)
				{
					importComics = new RelayCommand(this.HandleXLSComicsImport);
				}

				return this.importComics;
			}
		}

		public ICommand EditItemCommand
		{
			get
			{
				if (this.editItemCommand == null)
				{
					this.editItemCommand = new RelayCommand(this.HandleItemEditting);
				}

				return this.editItemCommand;
			}
		}

		public ICommand DeleteItemCommand
		{
			get
			{
				if (this.deleteItemCommand == null)
				{
					this.deleteItemCommand = new RelayCommand(this.HandleItemDeleting);
				}

				return this.deleteItemCommand;
			}
		}

		public ICommand AddNewCategory
		{
			get
			{
				if (this.addNewCategory == null)
				{
					this.addNewCategory = new RelayCommand(this.HandleCategoryAddition);
				}

				return this.addNewCategory;
			}
		}

		public ICommand AddNewItem
		{
			get
			{
				if (this.addNewItem == null)
				{
					this.addNewItem = new RelayCommand(this.HandleItemAddition);
				}

				return this.addNewItem;
			}
		}

		public ICommand DeleteCategory
		{
			get
			{
				if (this.deleteCategory == null)
				{
					this.deleteCategory = new RelayCommand(this.HandleCategoryDeletion);
				}

				return this.deleteCategory;
			}
		}

		public ICommand CreatePDFReportForCategory
		{
			get
			{
				if (this.createPDFReport == null)
				{
					this.createPDFReport = new RelayCommand(this.HandlePDFReportGenerating);
				}

				return this.createPDFReport;
			}
		}
		#endregion

		#region Commands Handlers
		private void HandleProductTypesImport(object obj)
		{
			this.dialog.Filter = "Xml Files (*.xml)|*.xml";
			this.dialog.ShowDialog();

			foreach (var file in this.dialog.FileNames)
			{
				this.productsImporter.ImportXMLToDBContext(this.dbContext, file);
			}

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleJSONBooksImport(object obj)
		{
			this.dialog.Filter = "Json Files (*.json)|*.json";
			this.dialog.ShowDialog();

			foreach (var file in dialog.FileNames)
			{
				this.booksImporter.ImportJSONToDBContext(this.dbContext, file);
			}

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleJSONGiftsImport(object obj)
		{
			this.dialog.Filter = "Json Files (*.json	)|*.json";
			this.dialog.ShowDialog();

			foreach (var file in this.dialog.FileNames)
			{
				this.giftsImporter.ImportJSONToDBContext(this.dbContext, file);
			}

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleXLSMagazinesImport(object obj)
		{
			this.dialog.Filter = "Excel Files (*.xls)|*.xls";
			this.dialog.ShowDialog();

			foreach (var file in this.dialog.FileNames)
			{
				this.magazinesImporter.ImportDataToContext(this.dbContext, file);
			}

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleXLSComicsImport(object obj)
		{
			this.dialog.Filter = "Excel Files (*.xls)|*.xls";
			this.dialog.ShowDialog();

			foreach (var file in this.dialog.FileNames)
			{
				this.comicsImporter.ImportDataToContext(this.dbContext, file);
			}

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleItemEditting(object obj)
		{
			Item item = obj as Item;

			var editItemWindow = new EditItemWindow(item, this.dbContext);
			editItemWindow.ShowDialog();
			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleItemDeleting(object obj)
		{
			var item = obj as IItem;
			item.IsDeleted = true;
			this.dbContext.SaveChanges();
			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleCategoryAddition(object obj)
		{
			var addCategoryWindow = new AddCategoryWindow(this.dbContext);
			addCategoryWindow.ShowDialog();
			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleItemAddition(object obj)
		{
			var addItemWindow = new AddItemWindow(this.dbContext);
			addItemWindow.ShowDialog();
			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleCategoryDeletion(object obj)
		{
			var deleteCategoryWindow = new DeleteCategoryWindow(this.dbContext);
			deleteCategoryWindow.ShowDialog();
			NotifyPropertyChanged("ProductTypes");
		}

		private void HandlePDFReportGenerating(object obj)
		{
			var generateReportWindow = new GenerateReportWindow(this.dbContext);
			generateReportWindow.ShowDialog();
		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		public ICollection<ProductType> ProductTypes
		{
			get
			{
				if (this.dbContext.ProductTypes.Any())
				{
					return this.dbContext.ProductTypes.ToList();
				}
				else
				{
					return null;
				}
			}
		}

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
	public class MainViewModel : INotifyPropertyChanged
	{
		#region Commands
		private ICommand importProductTypesCommand;
		private ICommand importBooksJSON;
		private ICommand importGiftsJSON;
		private ICommand editItemCommand;

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
		#endregion

		#region Commands Handlers
		private void HandleProductTypesImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var categoriesImporter = new XMLProductTypesImporter();
			categoriesImporter.ImportXMLToDBContext(new BookStoreContext(), dialog.FileName);

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleJSONBooksImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var booksImporter = new JSONBooksImporter();
			booksImporter.ImportJSONToDBContext(new BookStoreContext(), dialog.FileName);

			NotifyPropertyChanged("ProductTypes");
		}

		private void HandleJSONGiftsImport(object obj)
		{
			var dialog = new OpenFileDialog();
			dialog.InitialDirectory = Assembly.GetExecutingAssembly().Location;
			dialog.ShowDialog();

			var giftsImporter = new JSONGiftsImporter();
			giftsImporter.ImportJSONToDBContext(new BookStoreContext(), dialog.FileName);

			NotifyPropertyChanged("ProductTypes");
		}


		private void HandleItemEditting(object obj)
		{
			Item item = obj as Item;

			var editItemWindow = new EditItemWindow(item);
			editItemWindow.Show();
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		public ICollection<ProductType> ProductTypes
		{
			get
			{
				var cont = new BookStoreContext();
				return cont.ProductTypes.ToList();
			}
		}

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
	}
}

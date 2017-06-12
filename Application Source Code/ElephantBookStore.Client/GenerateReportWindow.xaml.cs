namespace ElephantBookStore.Client
{
	using Microsoft.Win32;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Input;

	using Helpers;
	using ElephantBookStore.Data;
	using ElephantBookStore.Data.Models;
	using ElephantBookStore.Data.Contracts;

	/// <summary>
	/// Interaction logic for GenerateReportWindow.xaml
	/// </summary>
	public partial class GenerateReportWindow : Window
	{
		private ICommand generateReport;
		private IBookStoreContext dbContext;

		public GenerateReportWindow()
		{
		}

		public GenerateReportWindow(IBookStoreContext dbContext)
		{
			InitializeComponent();
			this.dbContext = dbContext;
		}

		public IEnumerable<ProductType> AllProductTypes
		{
			get
			{
				return this.dbContext.ProductTypes.Where(pt => pt.IsDeleted == false).ToList();
			}
		}

		public ICommand GenerateReportForCategory
		{
			get
			{
				if (this.generateReport == null)
				{
					this.generateReport = new RelayCommand(this.HandleProductTypeReportGeneration);
				}

				return this.generateReport;
			}
		}

		private void HandleProductTypeReportGeneration(object obj)
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf";
			saveFileDialog.ShowDialog();

			var category = obj as Category;

			if (category == null)
			{
				// Log in application logger
				throw new ArgumentException("Product type should be passed in order to generate report");
			}

			PDFReportGenerator.GenerateReportFileForCategory(category, saveFileDialog.FileName);
		}
	}
}

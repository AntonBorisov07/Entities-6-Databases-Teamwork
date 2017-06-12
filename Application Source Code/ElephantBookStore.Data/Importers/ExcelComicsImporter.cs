namespace ElephantBookStore.Data.Importers
{
	using System.Collections.Generic;
	using System.Linq;

	using Contracts;
	using Models;

	using ExcelLibrary.SpreadSheet;
	using System;
	using System.IO;

	public class ExcelComicsImporter : IExcelImporter
	{
		public void ImportDataToContext(IBookStoreContext bookStoreContext, string file)
		{
			if (bookStoreContext == null)
			{
				throw new ArgumentException("Context cannot be null");
			}

			if (string.IsNullOrEmpty(file))
			{
				throw new ArgumentException("File name cannot be null or empty");
			}

			try
			{
				Path.IsPathRooted(file);
			}
			catch (ArgumentException e)
			{
				throw new ArgumentException("File name is not valid");
			}

			if (!File.Exists(file))
			{
				throw new FileNotFoundException("File does not exist");
			}

			var comics = this.ConvertDataToComicsObjects(bookStoreContext, file);

			bookStoreContext.Items.AddRange(comics);
			bookStoreContext.SaveChanges();
		}

		private IEnumerable<Comic> ConvertDataToComicsObjects(IBookStoreContext sqlContext, string fileName)
		{
			var workbook = Workbook.Load(fileName);
			var workSheet = workbook.Worksheets[0];

			var result = new List<Comic>();
			var allCategories = sqlContext.Categories;

			var nameColumnIndex = 0;
			var categoryColumnIndex = 0;
			var priceColumnIndex = 0;
			var descriptionColumnIndex = 0;

			for (int i = 0; i <= workSheet.Cells.LastColIndex; i++)
			{
				var columnValue = workSheet.Cells[0, i].StringValue;

				if (columnValue == "Name")
				{
					nameColumnIndex = i;
					continue;
				}
				else if (columnValue == "Category")
				{
					categoryColumnIndex = i;
					continue;
				}
				else if (columnValue == "Price")
				{
					priceColumnIndex = i;
					continue;
				}
				else if (columnValue == "Description")
				{
					descriptionColumnIndex = i;
					continue;
				}
			}

			for (int i = 1; i <= workSheet.Cells.LastRowIndex; i++)
			{
				var newComicName = workSheet.Cells[i, nameColumnIndex].StringValue;
				var newComicCategory = workSheet.Cells[i, categoryColumnIndex].StringValue;
				var newComicPrice = workSheet.Cells[i, priceColumnIndex].StringValue;
				var newComicDescription = workSheet.Cells[i, descriptionColumnIndex].StringValue;

				var newComic = new Comic()
				{
					Name = newComicName,
					Category = allCategories.First(c => c.CategoryName == newComicCategory),
					Price = decimal.Parse(newComicPrice),
					Description = newComicDescription,
					IsDeleted = false
				};

				result.Add(newComic);
			}

			return result;
		}
	}
}

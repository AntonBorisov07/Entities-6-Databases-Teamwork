﻿namespace ElephantBookStore.Data.Importers
{
	using System.Collections.Generic;
	using System.Linq;

	using Contracts;
	using Models;

	using ExcelLibrary.SpreadSheet;

	public class ExcelMagazinesImporter : IExcelImporter
	{
		public void ImportDataToContext(BookStoreContext context, string fileName)
		{
			var magazines = this.ConvertExcelDataToMagazinesObjects(context, fileName);

			context.Items.AddRange(magazines);
			context.SaveChanges();
		}

		private IEnumerable<Magazine> ConvertExcelDataToMagazinesObjects(BookStoreContext sqlContext, string fileName)
		{
			var workbook = Workbook.Load(fileName);
			var workSheet = workbook.Worksheets[0];

			var result = new List<Magazine>();
			var allCategories = sqlContext.Categories;

			var nameColumnIndex = 0;
			var categoryColumnIndex = 0;
			var priceColumnIndex = 0;
			var descriptionColumnIndex = 0;

			for (int i = 0; i < workSheet.Cells.LastColIndex; i++)
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
				var newMagazineName = workSheet.Cells[i, nameColumnIndex].StringValue;
				var newMagazineCategory = workSheet.Cells[i, categoryColumnIndex].StringValue;
				var newMagazinePrice = workSheet.Cells[i, priceColumnIndex].StringValue;
				var newMagazineDescription = workSheet.Cells[i, descriptionColumnIndex].StringValue;

				var newMagazine = new Magazine()
				{
					Name = newMagazineName,
					Category = allCategories.First(c => c.CategoryName == newMagazineCategory),
					Price = decimal.Parse(newMagazinePrice),
					Description = newMagazineDescription,
					IsDeleted = false
				};

				result.Add(newMagazine);
			}

			return result;
		}
	}
}

namespace ElephantBookStore.Data.Importers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	using Contracts;
	using Models;

	public class XMLProductTypesImporter : IXMLImporter
	{
		public void ImportXMLToDBContext(IBookStoreContext context, string fileName)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be empty");
			}

			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException("File name cannot be null or empty");
			}

			if (fileName.Substring(fileName.Length - 4, 4) != ".xml")
			{
				throw new ArgumentException("File extension should be \".xml\"");
			}

			try
			{
				Path.IsPathRooted(fileName);
			}
			catch (ArgumentException e)
			{
				throw new ArgumentException("File name is not valid");
			}

			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException("File does not exist");
			}

			var xmlDoc = XDocument.Load(fileName);

			var productTypes = new List<ProductType>();
			var categories = new List<Category>();

			var productTypesFromXML = xmlDoc.Root.Elements("ProductType");

			foreach (var productTypeElement in productTypesFromXML)
			{
				var newProductType = new ProductType()
				{
					ProductTypeName = productTypeElement.Element("Name").Value
				};

				if (productTypes.FirstOrDefault(pt => pt.ProductTypeName == newProductType.ProductTypeName) == null)
				{
					productTypes.Add(newProductType);
				}
			}

			context.ProductTypes.AddRange(productTypes);
			context.SaveChanges();

			productTypes = context.ProductTypes.ToList();

			foreach (var pt in productTypes)
			{
				var categoriesForProductFromXML = productTypesFromXML.First(pr => pr.Element("Name").Value == pt.ProductTypeName).Element("Categories").Elements("Category").Select(e => e.Value).ToList();

				foreach (var category in categoriesForProductFromXML)
				{
					var newCategory = new Category()
					{
						CategoryName = category,
						ProductTypeID = pt.ID
					};

					if (context.Categories.FirstOrDefault(c => c.CategoryName == category && c.ProductTypeID == pt.ID) == null)
					{
						context.Categories.Add(newCategory);
					}
				}
			}

			context.SaveChanges();
		}
	}
}

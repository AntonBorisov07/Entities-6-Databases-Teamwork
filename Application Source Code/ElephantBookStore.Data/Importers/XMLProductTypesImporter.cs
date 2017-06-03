using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Models;

namespace ElephantBookStore.Data.Importers
{
	public class XMLProductTypesImporter : IXMLImporter
	{
		public void ImportXMLToDBContext(BookStoreContext context, string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return;
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

				if (context.ProductTypes.SingleOrDefault(pt => pt.ProductTypeName == newProductType.ProductTypeName) == null)
				{
					context.ProductTypes.Add(newProductType);
				}

				//foreach (var categoryName in categoriesElements)
				//{
				//	var newCategory = new Category()
				//	{
				//		CategoryName = 
				//	};
				//}
			}

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

					if (context.Categories.SingleOrDefault(c => c.CategoryName == category && c.ProductTypeID == pt.ID) == null)
					{
						context.Categories.Add(newCategory);
					}
				}
			}

			context.SaveChanges();
		}
	}
}

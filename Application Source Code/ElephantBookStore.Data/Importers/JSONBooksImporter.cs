using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Models;
using Newtonsoft.Json;

namespace ElephantBookStore.Data.Importers
{
	public class JSONBooksImporter : IJSONImporter
	{
		public void ImportJSONToDBContext(BookStoreContext context, string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return;
			}

			var jsonString = string.Empty;

			using (var sr = new StreamReader(fileName))
			{
				jsonString = sr.ReadToEnd();
			}

			var serializer = new JavaScriptSerializer();
			var booksFromJSON = serializer.Deserialize(jsonString, typeof(List<Book>)) as List<Book>;
			var allCategories = context.Categories.ToList();

			foreach (var book in booksFromJSON)
			{
				book.Category = allCategories.SingleOrDefault(c => c.CategoryName == book.Category.CategoryName);
				context.Books.Add(book);
			}

			context.SaveChanges();
		}
	}
}

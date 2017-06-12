namespace ElephantBookStore.Data.Importers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Web.Script.Serialization;

	using Contracts;
	using Models;

	public class JSONBooksImporter : IJSONImporter
	{
		public void ImportJSONToDBContext(IBookStoreContext context, string fileName)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be null");
			}

			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException("File name cannot be null or empty");
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

			var jsonString = string.Empty;

			using (var sr = new StreamReader(fileName))
			{
				jsonString = sr.ReadToEnd();
			}

			if (string.IsNullOrEmpty(jsonString))
			{
				throw new ArgumentException("JSON file cannot be empty");
			}

			var serializer = new JavaScriptSerializer();
			var booksFromJSON = serializer.Deserialize(jsonString, typeof(List<Book>)) as List<Book>;
			var allCategories = context.Categories.ToList();

			foreach (var book in booksFromJSON)
			{
				book.Category = allCategories.SingleOrDefault(c => c.CategoryName == book.Category.CategoryName);
				context.Items.Add(book);
			}

			context.SaveChanges();
		}
	}
}

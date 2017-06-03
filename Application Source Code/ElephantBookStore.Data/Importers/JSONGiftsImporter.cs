using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Models;

namespace ElephantBookStore.Data.Importers
{
	public class JSONGiftsImporter : IJSONImporter
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
			var giftsFromJson = serializer.Deserialize(jsonString, typeof(List<Gift>)) as List<Gift>;

			var allCategories = context.Categories.ToList();

			foreach (var gift in giftsFromJson)
			{
				gift.Category = allCategories.SingleOrDefault(c => c.CategoryName == gift.Category.CategoryName);
				context.Items.Add(gift);
			}

			context.SaveChanges();
			context.Dispose();
		}
	}
}

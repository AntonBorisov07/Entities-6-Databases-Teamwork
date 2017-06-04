using System.Collections.Generic;

namespace ElephantBookStore.Data.Models
{
	public class ProductType
	{
		public ProductType()
		{
			this.Categories = new List<Category>();
		}

		public int ID { get; set; }

		public string ProductTypeName { get; set; }

		public bool IsDeleted { get; set; }

		public virtual ICollection<Category> Categories { get; set; }
	}
}
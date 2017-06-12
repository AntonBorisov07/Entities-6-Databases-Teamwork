namespace ElephantBookStore.Data.Models
{
	using System.Collections.Generic;
	using ElephantBookStore.Data.Contracts;

	public class ProductType : IProductType
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
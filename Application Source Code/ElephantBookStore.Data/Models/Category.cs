namespace ElephantBookStore.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using ElephantBookStore.Data.Contracts;

	public class Category : ICategory
	{
		public Category()
		{
			this.Items = new List<Item>();
		}

		[Key]
		public int ID { get; set; }

		[MinLength(2)]
		[MaxLength(150)]
		[Index(IsUnique = true)]
		public string CategoryName { get; set; }

		public int ProductTypeID { get; set; }

		public bool IsDeleted { get; set; }

		[ForeignKey("ProductTypeID")]
		public virtual ProductType ProductType { get; set; }

		public virtual ICollection<Item> Items { get; set; }
	}
}

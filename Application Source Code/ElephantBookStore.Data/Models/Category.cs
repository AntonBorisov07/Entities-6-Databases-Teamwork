using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElephantBookStore.Data.Contracts;

namespace ElephantBookStore.Data.Models
{
	public class Category
	{
		public Category()
		{
			this.Items = new List<Item>();
		}

		[Key]
		public int ID { get; set; }

		[MinLength(2)]
		[MaxLength(50)]
		[Index(IsUnique =true)]
		public string CategoryName { get; set; }

		public int ProductTypeID { get; set; }

		[ForeignKey("ProductTypeID")]
		public virtual ProductType ProuctType { get; set; }

		public virtual ICollection<Item> Items { get; set; }
	}
}

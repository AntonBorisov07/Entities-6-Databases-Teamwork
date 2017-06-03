using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElephantBookStore.Data.Models
{
	public class Category
	{
		[Key]
		public int ID { get; set; }

		[MinLength(2)]
		[MaxLength(50)]
		[Index(IsUnique =true)]
		public string CategoryName { get; set; }

		public int ProductTypeID { get; set; }

		[ForeignKey("ProductTypeID")]
		public virtual ProductType ProuctType { get; set; }
	}
}

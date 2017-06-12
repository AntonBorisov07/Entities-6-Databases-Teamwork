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
	public abstract class Item : IItem
	{
		[Key]
		public int ID { get; set; }

		public int CategoryID { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(500)]
		public string Name { get; set; }

		[Required]
		[ForeignKey("CategoryID")]
		public Category Category { get; set; }

		[Required]
		[Range(typeof(decimal), "0.0", "10000.0")]
		public decimal Price { get; set; }

		public string Description { get; set; }

		public bool IsDeleted { get; set; }

		public string Distinguisher => this.GetType().Name;
	}
}

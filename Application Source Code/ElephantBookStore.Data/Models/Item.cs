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
		public int ID { get; set; }

		public int CategoryID { get; set; }

		[Required]
		public string Name { get; set; }

		[ForeignKey("CategoryID")]
		public Category Category { get; set; }

		public decimal Price { get; set; }

		public string Description { get; set; }

		public bool IsDeleted { get; set; }
	}
}

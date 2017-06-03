using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElephantBookStore.Data.Models
{
	public class Gift
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }
		
		public decimal Price { get; set; }

		public string Description { get; set; }

		public int CategoryID { get; set; }

		[ForeignKey("CategoryID")]
		public virtual Category Category { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElephantBookStore.Data.Models
{
	[Table("Books")]
	public class Book : Item
	{
		[Required]
		public string Author { get; set; }
		
	}
}

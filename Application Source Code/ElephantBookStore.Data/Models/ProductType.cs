using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public virtual ICollection<Category> Categories { get; set; }
	}
}

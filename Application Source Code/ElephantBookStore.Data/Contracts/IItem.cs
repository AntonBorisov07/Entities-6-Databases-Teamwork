using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElephantBookStore.Data.Models;

namespace ElephantBookStore.Data.Contracts
{
	public interface IItem
	{
		int ID { get; set; }

		int CategoryID { get; set; }

		Category Category { get; set; }

		decimal Price { get; set; }

		string Description { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElephantBookStore.Data.Models;

namespace ElephantBookStore.Data.Contracts
{
	public interface IBookStoreContext
	{
		DbSet<ProductType> ProductTypes { get; set; }

		DbSet<Category> Categories { get; set; }

		DbSet<Item> Items { get; set; }

		int SaveChanges();
	}
}

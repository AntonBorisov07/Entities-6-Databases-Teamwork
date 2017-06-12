namespace ElephantBookStore.Data.Contracts
{
	using System.Collections.Generic;
	using ElephantBookStore.Data.Models;

	public interface ICategory
	{
		string CategoryName { get; set; }

		bool IsDeleted { get; set; }

		ProductType ProductType { get; set; }

		ICollection<Item> Items { get; set; }
	}
}

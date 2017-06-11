namespace ElephantBookStore.Data.Contracts
{
	using Models;

	public interface IItem
	{
		int ID { get; set; }

		int CategoryID { get; set; }

		Category Category { get; set; }

		decimal Price { get; set; }

		string Description { get; set; }
		
		bool IsDeleted { get; set; }
	}
}

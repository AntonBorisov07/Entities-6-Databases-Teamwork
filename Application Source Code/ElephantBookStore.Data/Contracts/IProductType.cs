namespace ElephantBookStore.Data.Contracts
{
	public interface IProductType
	{
		string ProductTypeName { get; set; }

		bool IsDeleted { get; set; }
	}
}

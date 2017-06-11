namespace ElephantBookStore.Data.Contracts
{
	public interface IJSONImporter
	{
		void ImportJSONToDBContext(BookStoreContext context, string fileName);
	}
}

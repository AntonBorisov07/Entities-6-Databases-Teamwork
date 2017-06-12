namespace ElephantBookStore.Data.Contracts
{
	public interface IJSONImporter
	{
		void ImportJSONToDBContext(IBookStoreContext context, string fileName);
	}
}

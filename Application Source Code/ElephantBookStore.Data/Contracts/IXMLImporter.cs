namespace ElephantBookStore.Data.Contracts
{
	public interface IXMLImporter
	{
		void ImportXMLToDBContext(IBookStoreContext context, string fileName);
	}
}

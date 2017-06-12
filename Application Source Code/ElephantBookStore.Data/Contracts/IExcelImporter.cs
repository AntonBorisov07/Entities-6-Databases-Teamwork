namespace ElephantBookStore.Data.Contracts
{
	public interface IExcelImporter
	{
		void ImportDataToContext(IBookStoreContext context, string fileName);
	}
}

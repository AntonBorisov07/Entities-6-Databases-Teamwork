namespace ElephantBookStore.Data.Contracts
{
	public interface IExcelImporter
	{
		void ImportDataToContext(BookStoreContext context, string fileName);
	}
}

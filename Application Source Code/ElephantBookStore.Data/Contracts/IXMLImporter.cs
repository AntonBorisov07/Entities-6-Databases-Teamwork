using System.Data.Entity;

namespace ElephantBookStore.Data.Contracts
{
	public interface IXMLImporter
	{
		void ImportXMLToDBContext(BookStoreContext context, string fileName);
	}
}

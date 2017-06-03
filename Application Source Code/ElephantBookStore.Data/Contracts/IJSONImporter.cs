using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElephantBookStore.Data.Contracts
{
	public interface IJSONImporter
	{
		void ImportJSONToDBContext(BookStoreContext context, string fileName);
	}
}

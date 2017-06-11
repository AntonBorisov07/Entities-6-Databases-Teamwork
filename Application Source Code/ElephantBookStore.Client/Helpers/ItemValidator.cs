namespace ElephantBookStore.Client.Helpers
{
	public static class ItemValidator
	{
		public static bool ValidateBookAuthor(string author)
		{
			if (string.IsNullOrEmpty(author) || author.Length < 3 || !char.IsUpper(author[0]) || !author.ContainsOnlyLetters())
			{
				return false;
			}

			return true;
		}

		internal static bool ValidateItemName(string newItemName)
		{
			if (string.IsNullOrEmpty(newItemName) || newItemName.Length < 3 || !char.IsUpper(newItemName[0]) || !newItemName.ContainsOnlyLetters())
			{
				return false;
			}

			return true;
		}

		internal static bool ValidateItemPrice(string newItemPrice)
		{
			var newPriceAsDecimal = 0m;

			if (string.IsNullOrEmpty(newItemPrice) || !decimal.TryParse(newItemPrice, out newPriceAsDecimal) || newPriceAsDecimal < 0 || newPriceAsDecimal > 10000)
			{
				return false;
			}

			return true;
		}

		internal static bool ValidateCategoryName(string newItemCategoryName)
		{
			if (string.IsNullOrEmpty(newItemCategoryName))
			{
				return false;
			}

			return true;
		}
	}
}

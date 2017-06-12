namespace ElephantBookStore.Client.Helpers
{
	public static class StringExtensions
	{
		public static bool ContainsOnlyLetters(this string str)
		{
			var trimmedString = str.Trim();

			if (string.IsNullOrEmpty(trimmedString))
			{
				return false;
			}

			foreach (var symbol in trimmedString)
			{
				if (!char.IsLetter(symbol) && !char.IsWhiteSpace(symbol))
				{
					return false;
				}
			}

			return true;
		}
	}
}

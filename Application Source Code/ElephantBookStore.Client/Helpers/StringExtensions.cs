namespace ElephantBookStore.Client.Helpers
{
	public static class StringExtensions
	{
		public static bool ContainsOnlyLetters(this string str)
		{
			foreach (var symbol in str)
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

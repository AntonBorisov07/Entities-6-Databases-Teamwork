using System;
using ElephantBookStore.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.ItemValidatorTests
{
	[TestClass]
	public class ValidateBookAuthorShould
	{
		[TestMethod]
		public void ReturnFalse_WhenAuthorIsNull()
		{
			var result = ItemValidator.ValidateBookAuthor(null);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorIsEmptyString()
		{
			var result = ItemValidator.ValidateBookAuthor(string.Empty);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorLengthIsLessThan3Symbols()
		{
			var result = ItemValidator.ValidateBookAuthor("No");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorIsMoreThan250Symbols()
		{
			var result = ItemValidator.ValidateBookAuthor(new string('A',251));

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorDoesntStartWithCapitalLetter()
		{
			var result = ItemValidator.ValidateBookAuthor("gosho");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorContainsNonLetterSymbols()
		{
			var result = ItemValidator.ValidateBookAuthor("Gosho2");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenAuthorContainsNonLetterSymbols2()
		{
			var result = ItemValidator.ValidateBookAuthor("Gosho/");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenAuthorIsValid()
		{
			var result = ItemValidator.ValidateBookAuthor("Gosho");

			Assert.IsTrue(result);
		}
	}
}

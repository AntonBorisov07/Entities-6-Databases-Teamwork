using System;
using ElephantBookStore.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.ItemValidatorTests
{
	[TestClass]
	public class ValidateItemNameShould
	{
		[TestMethod]
		public void ReturnFalse_WhenItemNameIsNull()
		{
			var result = ItemValidator.ValidateItemName(null);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemNameIsEmptyString()
		{
			var result = ItemValidator.ValidateItemName(string.Empty);

			Assert.IsFalse(result);
		}
		
		[TestMethod]
		public void ReturnFalse_WhenItemNameLengthIsLessThan3Symbols()
		{
			var result = ItemValidator.ValidateItemName("No");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemNameIsMoreThan500Symbols()
		{
			var result = ItemValidator.ValidateItemName(new string('A', 501));

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemNameDoesntStartWithCapitalLetter()
		{
			var result = ItemValidator.ValidateItemName("gosho");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemNameContainsNonLetterSymbols()
		{
			var result = ItemValidator.ValidateItemName("Gosho2");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemNameContainsNonLetterSymbols2()
		{
			var result = ItemValidator.ValidateItemName("Gosho/");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenItemNameIsValid()
		{
			var result = ItemValidator.ValidateItemName("Gosho");

			Assert.IsTrue(result);
		}
	}
}

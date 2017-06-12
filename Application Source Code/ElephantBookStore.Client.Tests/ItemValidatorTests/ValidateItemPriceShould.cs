using System;
using ElephantBookStore.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.ItemValidatorTests
{
	[TestClass]
	public class ValidateItemPriceShould
	{
		[TestMethod]
		public void ReturnFalse_WhenItemPriceIsNull()
		{
			var result = ItemValidator.ValidateItemPrice(null);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemPriceIsEmptyString()
		{
			var result = ItemValidator.ValidateItemPrice(string.Empty);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenItemPriceStringIsNotCastableToDecimal()
		{
			var result = ItemValidator.ValidateItemPrice(new string('1', 25));

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenNewPriceIsLessThanZero()
		{
			var result = ItemValidator.ValidateItemPrice("-50");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenNewPriceIsGreaterThanTenThousand()
		{
			var result = ItemValidator.ValidateItemPrice("10001");

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenNewPriceIsValid()
		{
			var result = ItemValidator.ValidateItemPrice("55.35");

			Assert.IsTrue(result);
		}
	}
}

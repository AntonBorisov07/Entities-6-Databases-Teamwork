using System;
using ElephantBookStore.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.ItemValidatorTests
{
	[TestClass]
	public class ValidateCategoryNameShould
	{
		[TestMethod]
		public void ReturnFalse_WhenCategoryNameIsNull()
		{
			var result = ItemValidator.ValidateCategoryName(null);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenCategoryNameIsEmptyString()
		{
			var result = ItemValidator.ValidateCategoryName(string.Empty);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenCategoryNameIsValid()
		{
			var result = ItemValidator.ValidateCategoryName("Category");

			Assert.IsTrue(result);
		}
	}
}

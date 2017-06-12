using System;
using ElephantBookStore.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.StringExtensionsTests
{
	[TestClass]
	public class ContainsOnlyLettersShould
	{
		[TestMethod]
		public void ReturnFalse_WhenStringIsEmpty()
		{
			var result = string.Empty.ContainsOnlyLetters();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenStringContainsOnlyWhiteSpaces()
		{
			var result = "        ".ContainsOnlyLetters();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenStringContainsDigits()
		{
			var result = "asfas2safasg".ContainsOnlyLetters();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenStringContainsAsterisk()
		{
			var result = "asfas*safasg".ContainsOnlyLetters();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnFalse_WhenStringContainsSlash()
		{
			var result = "asfas/safasg".ContainsOnlyLetters();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenStringContainsOnlyLetters()
		{
			var result = "asfassafasg".ContainsOnlyLetters();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void ReturnTrue_WhenStringContainsTwoWords()
		{
			var result = "asfass afasg".ContainsOnlyLetters();

			Assert.IsTrue(result);
		}
	}
}

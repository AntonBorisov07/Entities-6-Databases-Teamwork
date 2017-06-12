using System;
using System.Globalization;
using System.Windows;
using ElephantBookStore.Client.Converters;
using ElephantBookStore.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElephantBookStore.Client.UnitTests.InvertedBooleanToVisibilityConverterTests
{
	[TestClass]
	public class ConvertShould
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "No exception was thrown")]
		public void ThrowArgumentException_IfNullValueIsPassed()
		{
			var converter = new InvertedBooleanToVisibilityConverter();

			converter.Convert(null, null, null, CultureInfo.CurrentCulture);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "No exception was thrown")]
		public void ThrowArgumentException_IfIntegerValueIsPassed()
		{
			var converter = new InvertedBooleanToVisibilityConverter();

			converter.Convert(5, null, null, CultureInfo.CurrentCulture);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "No exception was thrown")]
		public void ThrowArgumentException_IfObjectValueIsPassed()
		{
			var converter = new InvertedBooleanToVisibilityConverter();
			var item = new Mock<IItem>();

			converter.Convert(item.Object, null, null, CultureInfo.CurrentCulture);
		}

		[TestMethod]
		public void NotThrow_WhenBooleanValueIsPassed()
		{
			var converter = new InvertedBooleanToVisibilityConverter();

			converter.Convert(true, null, null, CultureInfo.CurrentCulture);
		}

		[TestMethod]
		public void Return_VisibilityCollapsed_WhenValueIsTrue()
		{
			var converter = new InvertedBooleanToVisibilityConverter();
			var result = converter.Convert(true, null, null, CultureInfo.CurrentCulture);

			Assert.AreEqual(result, Visibility.Collapsed);
		}

		[TestMethod]
		public void Return_VisibilityVisible_WhenValueIsFalse()
		{
			var converter = new InvertedBooleanToVisibilityConverter();
			var result = converter.Convert(false, null, null, CultureInfo.CurrentCulture);

			Assert.AreEqual(result, Visibility.Visible);
		}
	}
}

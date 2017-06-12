using System;
using System.Windows;
using ElephantBookStore.Client.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElephantBookStore.Client.UnitTests.InvertedBooleanToVisibilityConverterTests
{
	[TestClass]
	public class ConvertBackShould
	{
		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void Throw_NotSupportedException_WhenCalled()
		{
			var converter = new InvertedBooleanToVisibilityConverter();

			converter.ConvertBack(Visibility.Visible, null, null, null);
		}
	}
}

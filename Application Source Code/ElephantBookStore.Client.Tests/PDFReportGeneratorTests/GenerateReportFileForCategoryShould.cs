using System;
using System.Collections.Generic;
using System.IO;
using ElephantBookStore.Client.Helpers;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElephantBookStore.Client.UnitTests.PDFReportGeneratorTests
{
	[TestClass]
	public class GenerateReportFileForCategoryShould
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Category is null but no exception is thrown")]
		public void ThrowArgumentException_IfCategoryIsNull_WithMessageContainingCategory()
		{
			try
			{
				PDFReportGenerator.GenerateReportFileForCategory(null, "./../../Test files/Georgi.pdf");
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("Category"));
				throw e;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "File name is null but no exception is thrown")]
		public void ThrowArgumentException_IfFileNameIsNull_WithMessageContainingFileName()
		{
			var mockedCategory = new Mock<ICategory>();

			try
			{
				PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, null);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("File name"));
				throw e;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "File name is invalid but no exception is thrown")]
		public void ThrowArgumentException_IfFileExtensionIsNotPDF()
		{
			var mockedCategory = new Mock<ICategory>();
			var xlsFileName = "./../../Test files/Georgi.xls";

			PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, xlsFileName);
		}

		[TestMethod]
		public void ThrowArgumentExceptionContainingExtension_IfFileExtensionIsNotPDF()
		{
			var mockedCategory = new Mock<ICategory>();
			var xlsFileName = "./../../Test files/Georgi.xls";

			try
			{
				PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, xlsFileName);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("File extension"));
				Assert.IsTrue(e.Message.Contains(".pdf"));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowArgumentException_IfFileNameContainsInvalidPathCharacters()
		{
			var mockedCategory = new Mock<ICategory>();
			var invalidSymbols = Path.GetInvalidPathChars();
			var fileName = "./../../Test files/Georgi.pdf";
			fileName = fileName.Insert(15, invalidSymbols[0].ToString());

			PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, fileName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowArgumentException_IfFileNameContainsInvalidFileNameCharacters()
		{
			var mockedCategory = new Mock<ICategory>();
			var invalidSymbols = Path.GetInvalidFileNameChars();
			var fileName = "./../../Test files/Georgi.pdf";
			fileName = fileName.Insert(15, invalidSymbols[0].ToString());

			PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, fileName);
		}

		[TestMethod]
		public void ThrowArgumentException_Containing_InvalidSymbols_WhenFileNameIsInvalid()
		{
			var mockedCategory = new Mock<ICategory>();
			var invalidSymbols = Path.GetInvalidFileNameChars();
			var fileName = "./../../Test files/Georgi.pdf";
			fileName = fileName.Insert(10, invalidSymbols[0].ToString());

			try
			{
				PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, fileName);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("invalid symbols"));
			}
		}

		[TestMethod]
		public void NotThrow_WhenParametersAreBothValid()
		{
			var mockedCategory = new Mock<ICategory>();
			mockedCategory.SetupGet(c => c.Items).Returns(new List<Item>());
			mockedCategory.SetupGet(c => c.ProductType).Returns(new ProductType()
			{
				ProductTypeName = "Gosho"
			});

			var fileName = "./../../Test files/Georgi.pdf";

			PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, fileName);
		}

		[TestMethod]
		public void TraverseCategorysItems()
		{
			var mockedCategory = new Mock<ICategory>();
			mockedCategory.SetupGet(c => c.Items).Returns(new List<Item>()).Verifiable();
			mockedCategory.SetupGet(c => c.ProductType).Returns(new ProductType()
			{
				ProductTypeName = "Gosho"
			});

			var fileName = "./../../Test files/Georgi.pdf";
			PDFReportGenerator.GenerateReportFileForCategory(mockedCategory.Object, fileName);

			Mock.Verify(mockedCategory);
		}
	}
}

using System;
using System.IO;
using ElephantBookStore.Data.Contracts;
using ElephantBookStore.Data.Importers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElephantBookStore.Client.UnitTests.ImportersTests.ExcelMagazinesImporterTests
{
	[TestClass]
	public class ImportDataToContextShould
	{
		private const string ValidFileName = "./../../Test files/Georgi.xls";

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Context is null, but no exception was thrown")]
		public void ThrowArgumentException_WhenContextIsNull()
		{
			var importer = new ExcelMagazinesImporter();
			importer.ImportDataToContext(null, ValidFileName);
		}

		[TestMethod]
		public void ThrowArgumentException_WithMessageContainingContext_WhenContextIsNull()
		{
			var importer = new ExcelMagazinesImporter();
			try
			{
				importer.ImportDataToContext(null, ValidFileName);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("Context"));
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowArgumentException_WhenFileIsNull()
		{
			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();
			importer.ImportDataToContext(mockedContext.Object, null);
		}

		[TestMethod]
		public void ThrowArgumentException_WithMessageContainingFileNameCannotBeNull_WhenFileIsNull()
		{
			var mockedContext = new Mock<IBookStoreContext>();

			var importer = new ExcelMagazinesImporter();
			try
			{
				importer.ImportDataToContext(mockedContext.Object, null);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("File name cannot be null"));
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowArgumentException_WhenFileIsEmpty()
		{
			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();
			importer.ImportDataToContext(mockedContext.Object, string.Empty);
		}

		[TestMethod]
		public void ThrowArgumentException_WithMessageContainingFileNameCannotBeNull_WhenFileNameIsEmpty()
		{
			var mockedContext = new Mock<IBookStoreContext>();

			var importer = new ExcelMagazinesImporter();
			try
			{
				importer.ImportDataToContext(mockedContext.Object, string.Empty);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("File name cannot be"));
				Assert.IsTrue(e.Message.Contains("empty"));
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowArgumentException_WhenFileNameIsInvalid()
		{
			var invalidSymbols = Path.GetInvalidPathChars();
			var invalidFileName = ValidFileName.Insert(10, invalidSymbols[0].ToString());

			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();
			importer.ImportDataToContext(mockedContext.Object, invalidFileName);
		}

		[TestMethod]
		public void ThrowArgumentException_WithMessageThatNameIsNotValid_WhenFileNameIsInvalid()
		{
			var invalidSymbols = Path.GetInvalidFileNameChars();
			var invalidFileName = ValidFileName.Insert(10, invalidSymbols[0].ToString());

			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();

			try
			{
				importer.ImportDataToContext(mockedContext.Object, invalidFileName);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("File name"));
				Assert.IsTrue(e.Message.Contains("not valid"));
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void ThrowFileNotFoundException_WhenFileDoesntExist()
		{
			var fileName = "./../../Test files/Alabala.pdf";
			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();

			importer.ImportDataToContext(mockedContext.Object, fileName);
		}

		[TestMethod]
		public void ThrowDescriptiveFileNotFoundException()
		{
			var fileName = "./../../Test files/Alabala.pdf";
			var mockedContext = new Mock<IBookStoreContext>();
			var importer = new ExcelMagazinesImporter();

			try
			{
				importer.ImportDataToContext(mockedContext.Object, fileName);
			}
			catch (FileNotFoundException e)
			{
				Assert.IsTrue(e.Message.Contains("File"));
				Assert.IsTrue(e.Message.Contains("not"));
				Assert.IsTrue(e.Message.Contains("exist"));
				return;
			}

			Assert.Fail();
		}
	}
}

namespace ElephantBookStore.Client.Helpers
{
	using System.Collections.Generic;
	using System.IO;
	using ElephantBookStore.Data.Models;
	using iTextSharp.text;
	using iTextSharp.text.pdf;
	using static iTextSharp.text.Font;

	public class PDFReportGenerator
	{
		public static void GenerateReportFileForCategory(Category category, string fileName)
		{
			var generatingBooksReport = false;
			var doc = new Document();
			PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
			doc.Open();

			var header = new Paragraph(string.Format("{0} from category \"{1}\"", category.ProuctType.ProductTypeName, category.CategoryName));
			header.Alignment = Element.ALIGN_CENTER;

			doc.Add(header);

			var table = new PdfPTable(2);
			table.SpacingBefore = 20f;

			if (category.ProuctType.ProductTypeName.Contains(typeof(Book).Name))
			{
				table.ResetColumnCount(3);
				generatingBooksReport = true;
			}

			var headerCells = new List<PdfPHeaderCell>();

			var nameCell = new PdfPHeaderCell();
			var nameParagraph = new Paragraph("Name");
			nameParagraph.Alignment = Element.ALIGN_CENTER;
			nameParagraph.Font = new Font(FontFamily.COURIER, 14);
			nameCell.AddElement(nameParagraph);

			var authorCell = new PdfPHeaderCell();
			var authorParagraph = new Paragraph("Author");
			authorParagraph.Alignment = Element.ALIGN_CENTER;
			authorParagraph.Font = new Font(FontFamily.COURIER, 14);
			authorCell.AddElement(authorParagraph);

			var priceCell = new PdfPHeaderCell();
			var priceParagraph = new Paragraph("Price");
			priceParagraph.Alignment = Element.ALIGN_CENTER;
			priceParagraph.Font = new Font(FontFamily.COURIER, 14);
			priceCell.AddElement(priceParagraph);

			//var descriptionCell = new PdfPHeaderCell();
			//var descriptionParagraph = new Paragraph("Description");
			//descriptionParagraph.Alignment = Element.ALIGN_CENTER;
			//descriptionParagraph.Font = new Font(FontFamily.COURIER, 14);
			//descriptionCell.AddElement(descriptionParagraph);

			headerCells.Add(nameCell);

			if (generatingBooksReport)
			{
				headerCells.Add(authorCell);
			}

			headerCells.Add(priceCell);
			//headerCells.Add(descriptionCell);

			foreach (var cell in headerCells)
			{
				table.AddCell(cell);
			}

			foreach (var item in category.Items)
			{
				if (item.IsDeleted)
				{
					continue;
				}

				for (int i = 0; i < table.NumberOfColumns; i++)
				{
					var newCell = new PdfPCell();
					var headerCellValue = headerCells[i].Column.CompositeElements[0].Chunks[0].Content;

					var valueForCell = item.GetType().GetProperty(headerCellValue).GetValue(item);

					if (valueForCell == null)
					{
						valueForCell = "Not available";
					}

					newCell.AddElement(new Paragraph(valueForCell.ToString()));
					table.AddCell(newCell);
				}
			}

			doc.Add(table);
			doc.Close();
		}
	}
}

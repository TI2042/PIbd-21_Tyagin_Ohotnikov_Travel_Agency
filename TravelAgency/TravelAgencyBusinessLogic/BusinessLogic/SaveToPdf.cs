using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.HelperModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);

            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            Paragraph paragraph1 = section.AddParagraph("C " + info.DateFrom.ToString("dd.MM.yyyy") + " по " + info.DateTo.ToString("dd.MM.yyyy"));
            paragraph1.Format.SpaceAfter = "1cm";
            paragraph1.Format.Alignment = ParagraphAlignment.Center;
            paragraph1.Style = "NormalTitle";

            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "4cm", "4cm", "4cm", "3cm", "3cm" };

            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }

            if (info.Guides != null)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { "Дата заказа", "Гиды", "Состояние", "Кол-во", "Стоимость" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });

                foreach (var pc in info.Guides)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = new List<string>
                    {
                        pc.CreationDate.ToString(),
                        pc.GuideThemeName,
                        pc.Status,
                        pc.Count.ToString(),
                        pc.Price.ToString()
                    },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always) { Document = document };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }

        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }

        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();

            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }

        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);

            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }

            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using TravelAgencyBusinessLogic.HelperModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    static class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                if (info.Tours != null)
                {
                    foreach (var product in info.Tours)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { product.TourName, " - " + product.Price.ToString() },
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = true,
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                    }
                }
                else if (info.Hotels != null)
                {
                    Table table = new Table();

                    TableProperties props = new TableProperties(
                        new TableBorders(
                            new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                            new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                    ));

                    table.AppendChild(props);

                    foreach (var warehouse in info.Hotels)
                    {
                        var tr = new TableRow();
                        var tc = new TableCell();

                        tc.Append(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { warehouse.HotelName },
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = false,
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));

                        tr.AppendChild(tc);
                        table.AppendChild(tr);
                    }

                    docBody.AppendChild(table);
                }

                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize { Orient = PageOrientationValues.Portrait };

            properties.AppendChild(pageSize);

            return properties;
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));

                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });

                    if (!run.StartsWith(" - ") && paragraph.TextProperties.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }

                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text { Text = run, Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }

                return docParagraph;
            }

            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();

                properties.AppendChild(new Justification() { Val = paragraphProperties.JustificationValues });
                properties.AppendChild(new SpacingBetweenLines { LineRule = LineSpacingRuleValues.Auto });
                properties.AppendChild(new Indentation());

                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();

                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }

                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }

                properties.AppendChild(paragraphMarkRunProperties);

                return properties;
            }

            return null;
        }
    }
}


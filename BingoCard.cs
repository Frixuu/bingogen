using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frixu.BingoGen
{
    /// <summary>Represents a 5x5 bingo card.</summary>
    public class BingoCard
    {
        static BingoCard()
        {
            GlobalFontSettings.FontResolver = new BingoFontResolver();
        }

        private PdfDocument InternalDocument { get; }
        private PdfPage Page { get; }
        private XGraphics Gfx { get; }
        private XFont Font { get; }
        private XFont TitleFont { get; }
        private readonly Dictionary<string, int> Words;
        private readonly Random RNG;

        private const double FontRegularSize = 16;
        private const double FontTitleSize = 40;
        private const double LineHeight = 1.15;
        private const double TableBorder = 3.0;

        /// <summary>Creates a new bingo card.</summary>
        /// <param name="title">Card title. Will display at the top of the card.</param>
        /// <param name="author">Card author. Will appear in PDF metadata.</param>
        /// <param name="creator">Card creator. Will appear in PDF metadata.</param>
        public BingoCard(string title = "Bingo!", string author = "BingoGen", string creator = "BingoGen")
        {
            RNG = new Random();
            Words = new Dictionary<string, int>();
            InternalDocument = new PdfDocument();
            InternalDocument.Info.Title = title;
            InternalDocument.Info.Author = author;
            InternalDocument.Info.Creator = creator;
            Page = InternalDocument.AddPage();
            Page.TrimMargins.All = XUnit.FromCentimeter(2);
            Gfx = XGraphics.FromPdfPage(Page);
            Font = new XFont("OpenSans", FontRegularSize, XFontStyle.Regular);
            TitleFont = new XFont("OpenSans", FontTitleSize, XFontStyle.Bold);
        }

        /// <summary>Adds word to this card.</summary>
        public void AddWord(string word) => Words.Add(word, RNG.Next(10000));

        /// <summary>Compiles word list into a PDF document.</summary>
        public BingoCard Create()
        {
            var shuffledWords = Words.OrderBy(w => w.Value).Select(w => w.Key).ToList();

            Gfx.DrawString
            (
                text: InternalDocument.Info.Title,
                font: TitleFont,
                brush: XBrushes.Black,
                layoutRectangle: new XRect(0, 0, Page.Width, Page.Height.Value / 10),
                format: XStringFormats.Center
            );

            //Which word (from the shuffled list) should we use now?
            var index = 0;

            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    //Don't print anything in the middle cell
                    if(i == 2 && j == 2) continue;

                    var parts = shuffledWords[index++].Split(@"\n");

                    for(var partIndex = 0; partIndex < parts.Length; partIndex++)
                    {
                        Gfx.DrawString
                        (
                            text: parts[partIndex],
                            font: Font,
                            brush: XBrushes.Black,
                            layoutRectangle: new XRect
                            (
                                Page.Width.Value / 5 * i,
                                Page.Height.Value / 8 * (2 + j)
                                - XUnit.FromPoint(FontRegularSize) * ((parts.Length - 1) * 0.5 - partIndex) * LineHeight,
                                Page.Width.Value / 5,
                                Page.Height.Value / 8
                            ),
                            format: XStringFormats.Center
                        );
                    }
                }
            }

            //Draw table borders
            var pen = new XPen(XColors.Black, TableBorder);
            for (var i = 0; i <= 5; i++)
            {
                Gfx.DrawLine
                (
                    pen,
                    Page.Width.Value / 5 * i,
                    Page.Height.Value / 8 * 2,
                    Page.Width.Value / 5 * i,
                    Page.Height.Value / 8 * 7
                );
                Gfx.DrawLine
                (
                    pen,
                    0,
                    Page.Height.Value / 8 * (2 + i),
                    Page.Width,
                    Page.Height.Value / 8 * (2 + i)
                );
            }

            return this;
        }

        /// <summary>Saves resulting PDF document to disk.</summary>
        /// <param name="filename">Output file.</param>
        public void Save(string filename = "bingo.pdf")
        {
            try
            {
                InternalDocument.Save(filename);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Cannot save to {filename}.");
                Console.Error.WriteLine(e.ToString());
            }
        }
    }
}

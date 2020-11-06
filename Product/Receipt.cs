using InventoryControl.UserControls;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Product
{
    public class Receipt
    {
        //Meta
        readonly int number = 0;
        readonly DateTime dateTime = DateTime.Now;
        readonly String pointOfSales = "";

        public Receipt(int receiptNumber, DateTime dateTime, String pointOfSales, List<SaleProductData> saleProducts)
        {
            PdfDocument document = new PdfDocument();
            this.number = receiptNumber;
            this.dateTime = dateTime;
            this.pointOfSales = pointOfSales;

            //Add pages
            const int maxRowsOnPage = 64;
            foreach(var pageContent in 
                saleProducts
                    .Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / maxRowsOnPage)
                    .Select(x => x.Select(v => v.Value).ToList())
                    .ToList())
            {
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                page.Orientation = PdfSharp.PageOrientation.Portrait;
                this.DrawPage(page, pageContent.ToArray());
            }

            //Save
            string filename = "Invoice.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
        void DrawPage(PdfPage page, SaleProductData[] saleProducts)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontMain = new XFont("Times New Roman", 12);
            XFont fontHeader = new XFont("Times New Roman", 16, XFontStyle.Bold);


            double marginX = 20;
            double x = marginX;
            double y = fontHeader.Height * 1.5;

            String headerString = "Накладная № " + this.number + " от " + dateTime.ToShortDateString();
            gfx.DrawString(headerString, fontHeader, XBrushes.Black, marginX + 5, y);
            y += 5;
            gfx.DrawLine(new XPen(XColors.Black, 3), marginX, y, page.Width - marginX, y);
            y += 15;
            gfx.DrawString("Получатель: " + pointOfSales, fontMain, XBrushes.Black, marginX + 5, y, XStringFormats.CenterLeft);


            //Drawing table
            y += 15;
            Dictionary<string, List<string>> tableContent = new Dictionary<string, List<string>>
            {
                { "Id", new List<string>() },
                { "Title", new List<string>() },
                { "Number", new List<string>() },
                { "Measurement", new List<string>() },
                { "Weight", new List<string>() },
                { "Price", new List<string>() },
                { "Sum", new List<string>() },
            };

            saleProducts = saleProducts.OrderBy((p) => p.Title).ToArray();
            foreach (var row in saleProducts)
            {
                ProductData data = ProductDatabase.GetProductData(row.Id);
                tableContent["Id"].Add(row.Id.ToString());
                tableContent["Title"].Add(data.Title);
                tableContent["Number"].Add(row.NumberToSell.ToString());
                tableContent["Measurement"].Add(data.Measurement);
                tableContent["Weight"].Add(data.Weight);
                tableContent["Price"].Add(data.SalePrice);
                tableContent["Sum"].Add((data.salePrice*row.NumberToSell).ToString());
            }

            var numberColumn = new PdfColumn(gfx, "№", Enumerable.Range(1, saleProducts.Length).Select(n => n.ToString()).ToList());

            double columnOffset = 20;
            columnOffset += numberColumn
                .Draw(columnOffset, y, 20, XStringFormats.Center);
            columnOffset += new PdfColumn(gfx, "Наименование", tableContent["Title"])
                .Draw(columnOffset, y, page.Width - columnOffset - 180 - 20, XStringFormats.CenterLeft);
            columnOffset += new PdfColumn(gfx, "Кол", tableContent["Number"])
                .Draw(columnOffset, y, 25, XStringFormats.Center);
            columnOffset += new PdfColumn(gfx, "Вес", tableContent["Weight"])
                .Draw(columnOffset, y, 35, XStringFormats.Center);
            columnOffset += new PdfColumn(gfx, "Ед", tableContent["Measurement"])
                .Draw(columnOffset, y, 20, XStringFormats.Center);
            columnOffset += new PdfColumn(gfx, "Цена", tableContent["Price"])
                .Draw(columnOffset, y, 50, XStringFormats.CenterRight);
            columnOffset += new PdfColumn(gfx, "Сумма", tableContent["Sum"])
                .Draw(columnOffset, y, 50, XStringFormats.CenterRight);

            y += numberColumn.Height + 5;
            double sum = 0.0;
            foreach (var sumStr in tableContent["Sum"])
            {
                sum += Double.Parse(sumStr, System.Globalization.CultureInfo.InvariantCulture);
            }
            gfx.DrawString("Всего наименований " + tableContent["Id"].Count + " на сумму " + sum + "\u20BD",
                    new XFont("Times New Roman", 12, XFontStyle.Bold),
                    XBrushes.Black, page.Width-20, y, XStringFormats.TopRight);
        }
    }
    class PdfColumn
    {
        public double Height
        { 
            get { return CellHeight * (content.Count+1); } 
        }
        public double CellHeight { set; get; }

        private readonly string header;
        private readonly ImmutableList<string> content;
        private XGraphics gfx;

        public PdfColumn(XGraphics gfx, string header, List<string> content)
        {
            this.gfx = gfx;
            this.header = header;
            this.content = content.ToImmutableList();
        }

        public double Draw(double x, double y, double width, XStringFormat cellContentAlignment, int paddingH = 4)
        {
            var content = this.content.Insert(0,header);
            //Draw
            for (int i = 0; i < content.Count; i++)
            {
                XFont font = i==0 ? new XFont("Times New Roman", 9, XFontStyle.Bold) : new XFont("Times New Roman", 9);
                XStringFormat contentAlignment = i==0 ? XStringFormats.Center : cellContentAlignment;
                CellHeight = font.Height; 

                gfx.DrawString(content[i], font, XBrushes.Black,
                new XRect(x+paddingH/2, y + CellHeight * i, width-paddingH, CellHeight), contentAlignment);
                gfx.DrawRectangle(XPens.Black, XBrushes.Transparent, x, y + CellHeight * i, width, CellHeight);
            }
            return width;
        }
    }
}

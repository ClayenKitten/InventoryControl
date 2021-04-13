using InventoryControl.ORM;
using InventoryControl.ViewModel;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace InventoryControl.Model
{
    public class ProductInvoice : IEntity
    {
        // TODO: Create new JoinTable
        public static JoinTable ProductsTable { get; }
            = new JoinTable("InvoiceProducts", typeof(long), typeof(long), SqlType.BOOLEAN);
        public static ConstructorEntityTable<ProductInvoice> Table { get; } = new ConstructorEntityTable<ProductInvoice>
        (
            new Column<ProductInvoice>("Number", SqlType.LONG, x => x.Number),
            new Column<ProductInvoice>("CreationDateTime", SqlType.DATETIME, x => x.CreationDateTime),
            new Column<ProductInvoice>("Type", SqlType.INT, x => (int)x.Type),
            new Column<ProductInvoice>("Sender", SqlType.TEXT, x => x.Sender),
            new Column<ProductInvoice>("Receiver", SqlType.TEXT, x => x.Receiver),
            new Column<ProductInvoice>("Payer", SqlType.TEXT, x => x.Payer),
            new Column<ProductInvoice>("Cause", SqlType.TEXT, x => x.Cause)
        );

        public long Id { get; set; }
        public long Number { get; set; }
        public DateTime CreationDateTime { get; set; }
        public TransferType Type { get; set; }
        public string Sender { get; set; } = "";
        public string Receiver { get; set; } = "";
        public string Payer { get; set; } = "";
        public string Cause { get; set; } = "";
        public IList<InvoiceProduct> Products { get; set; } = new List<InvoiceProduct>();

        public ProductInvoice(long id, long number, DateTime creationDateTime, int type, string sender, string receiver, string payer, string cause)
        {
            Id = id;
            Number = number;
            CreationDateTime = creationDateTime;
            Type = new TransferType(type);
            Sender = sender;
            Receiver = receiver;
            Payer = payer;
            Cause = cause;
            Products = ProductsTable
                            .ReadAll()
                            .Where(x => x.Item1 == Id)
                            .Select(x => InvoiceProduct.Table.Read(x.Item2))
                            .ToList();
        }

        #region Document generation
        private Paragraph AddParagraph(Cell cell, string text, ParagraphAlignment alignment = ParagraphAlignment.Center)
        {
            var par = new Paragraph();
            par.AddText(text);
            par.Format.Alignment = alignment;
            par.Format.Font = new Font("Arial", "10");
            cell.Add(par);
            return par;
        }
        private Table AddProductTable(Section section)
        {
            void AddHeader(Cell cell, string text)
            {
                var par = new Paragraph();
                par.AddFormattedText(text, TextFormat.Bold);
                par.Format.Font = new Font("Arial", "10");
                par.Format.Alignment = ParagraphAlignment.Center;
                cell.Add(par);
            }
            Row AddRow(Table table)
            {
                var row = table.AddRow();
                row.Borders.Visible = true;
                row.Borders.Color = Colors.Black;
                row.Borders.Width = 0.25;
                row.VerticalAlignment = VerticalAlignment.Center;
                return row;
            }
            Row AddProductRow(Table table, int num, InvoiceProduct product)
            {
                var row = AddRow(table);
                AddParagraph(row.Cells[0], num.ToString());
                AddParagraph(row.Cells[1], product.Name, ParagraphAlignment.Left);
                AddParagraph(row.Cells[2], product.NumberOfPackages.ToString(), ParagraphAlignment.Right);
                AddParagraph(row.Cells[3], product.NumberInPackage.ToString(), ParagraphAlignment.Right);
                AddParagraph(row.Cells[4], product.Number.ToString(), ParagraphAlignment.Right);
                AddParagraph(row.Cells[5], product.Measurement);
                AddParagraph(row.Cells[6], product.Price.ToString("0.00"), ParagraphAlignment.Right);
                AddParagraph(row.Cells[7], product.Cost.ToString("0.00"), ParagraphAlignment.Right);
                return row;
            }

            var table = section.AddTable();
            var colWidth = (section.PageSetup.PageWidth
                - section.PageSetup.LeftMargin
                - section.PageSetup.RightMargin) / 32;
            table.AddColumn().Width = colWidth * 2;
            table.AddColumn().Width = colWidth * 13;
            table.AddColumn().Width = colWidth * 3;
            table.AddColumn().Width = colWidth * 3;
            table.AddColumn().Width = colWidth * 3;
            table.AddColumn().Width = colWidth * 2;
            table.AddColumn().Width = colWidth * 3;
            table.AddColumn().Width = colWidth * 3;

            var headerRow = AddRow(table);
            AddHeader(headerRow.Cells[0], "№");
            AddHeader(headerRow.Cells[1], "Наименование");
            AddHeader(headerRow.Cells[2], "Кол-во мест");
            AddHeader(headerRow.Cells[3], "Кол-во в месте");
            AddHeader(headerRow.Cells[4], "Кол-во");
            AddHeader(headerRow.Cells[5], "Ед.");
            AddHeader(headerRow.Cells[6], "Цена");
            AddHeader(headerRow.Cells[7], "Сумма");

            for (int num = 0; num < Products.Count; num++)
            {
                AddProductRow(table, num+1, Products[num]);
            }
            return table;
        }
        public Document GetDocument()
        {
            var doc = new Document();
            var section = doc.AddSection();
            section.PageSetup = new PageSetup()
            {
                PageWidth = doc.DefaultPageSetup.PageWidth,
                PageHeight = doc.DefaultPageSetup.PageHeight,
                TopMargin = "1cm",
                BottomMargin = "1cm",
                LeftMargin = "1cm",
                RightMargin = "1cm",
            };
            #region Header
            var headerPar = new Paragraph();
            headerPar.AddFormattedText(
                $"Накладная №{Number} от {CreationDateTime.ToShortDateString()}",
                new Font("Arial", "14") { Bold = true });
            headerPar.Format.SpaceAfter = 10;
            section.Add(headerPar);
            #endregion
            #region Counterparties
            var rPar = section.AddParagraph($"Грузополучатель: {Receiver}");
            var sPar = section.AddParagraph($"Поставщик: {Sender}");
            var pPar = section.AddParagraph($"Плательщик: {Payer}");
            var cPar = section.AddParagraph($"Покупатель: {Cause}");
            cPar.Format.SpaceAfter = 10;
            #endregion
            #region Table
            var table = AddProductTable(section);
            var sumRow = table.AddRow();
            sumRow.VerticalAlignment = VerticalAlignment.Center;
            var par2 = AddParagraph(sumRow.Cells[2], Products
                .Select(x => x.NumberOfPackages).Sum().ToString(), ParagraphAlignment.Right);
            sumRow.Cells[2].Borders = new Borders() { Width = 0.25, Color = Colors.Black, Visible = true };
            par2.Format.Font.Bold = true;

            var par3 = AddParagraph(sumRow.Cells[7], Products
                .Select(x=>x.Cost).Sum().ToString("0.00"), ParagraphAlignment.Right);
            par3.Format.Font.Bold = true;
            sumRow.Cells[7].Borders = new Borders() { Width = 0.25, Color = Colors.Black, Visible = true };
            #endregion
            #region Bottom
            var totalPar = new Paragraph();
            totalPar.AddText($"Всего наименований {Products.Count} " +
                             $"на сумму {Products.Select(x => x.Cost).Sum().ToString("0.00")} руб.");

            var linePar = new Paragraph();
            linePar.Format.Borders.Bottom = new Border() { Color = Colors.Black, Width = 2, Visible = true };

            var signsPar = new Paragraph();
            signsPar.AddText("Отпустил" + new string(Chars.NonBreakableSpace, 10) + new string('_', 30));
            signsPar.AddSpace(23);
            signsPar.AddText("Получил" + new string(Chars.NonBreakableSpace, 10) + new string('_', 30));
            signsPar.Format.SpaceBefore = 10;
            var frame = new TextFrame
            {
                RelativeVertical = RelativeVertical.Page,
                Top = ShapePosition.Bottom,
                Width = section.PageSetup.PageWidth
                            - section.PageSetup.LeftMargin
                            - section.PageSetup.RightMargin
            };
            frame.Add(totalPar);
            frame.Add(linePar);
            frame.Add(signsPar);
            section.Add(frame);
            #endregion
            return doc;
        }
        #endregion
    }
}

using InventoryControl.Model;
using InventoryControl.Util;
using InventoryControl.View.Controls;
using MigraDoc.Rendering;
using MigraDoc.RtfRendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for TransferHistoryViewer.xaml
    /// </summary>
    public partial class TransferInvoiceViewer : ControlPanel
    {
        private List<string> cleanup = new List<string>();

        private ProductInvoice invoice;
        public ProductInvoice Invoice
        {
            get => invoice;
            private set
            {
                invoice = value;
                UpdateViewer();
            }
        }
        public TransferInvoiceViewer()
        {
            InitializeComponent();
        }
        private void UpdateViewer()
        {
            var path = Path.GetTempFileName();
            var renderer = new PdfDocumentRenderer(true);
            renderer.Document = Invoice.GetDocument();
            renderer.RenderDocument();
            renderer.Save(path);
            cleanup.Add(path);
            Viewer.Source = new Uri(path);
        }
        public override void ReceiveMessage(object sender, object message)
        {
            if (sender is TransferHistoryViewer)
            {
                Invoice = (message as ProductInvoice);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            Viewer.Dispose();
            foreach (var path in cleanup)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch { }
            }
        }
    }
}
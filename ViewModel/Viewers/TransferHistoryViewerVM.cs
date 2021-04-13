using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.ObjectModel;

namespace InventoryControl.ViewModel
{
    public class TransferHistoryViewerVM
    {
        public class ProductInvoicePresenter
        {
            public ProductInvoice Item { get; set; }
            private string InvoiceType
            {
                get
                {
                    if (Item.Type == TransferType.Buy)
                    {
                        return "Закупка";
                    }
                    else if (Item.Type == TransferType.Sell)
                    {
                        return "Продажа";
                    }
                    else if (Item.Type == TransferType.Return)
                    {
                        return "Возврат";
                    }
                    else if (Item.Type == TransferType.Supply)
                    {
                        return "Поставка";
                    }
                    else if (Item.Type == TransferType.Transport)
                    {
                        return "Перевозка";
                    }
                    else
                    {
                        return "Накладная";
                    }
                }
            }
            public string Header => $"{InvoiceType} №{Item.Number} от {Item.CreationDateTime.ToShortDateString()}";
            private double Sum
            {
                get
                {
                    var res = 0.0;
                    foreach (var product in Item.Products)
                    {
                        res += product.Cost;
                    }
                    return res;
                }
            }
            public string Content => $"{Item.Products.Count} наименований на сумму {Sum.ToString("0.00")}₽";
            public string Footer => $"По основанию {Item.Cause}";

            public ProductInvoicePresenter(ProductInvoice item) => Item = item;
        }
        public ObservableCollection<ProductInvoicePresenter> Content { get; }
            = new ObservableCollection<ProductInvoicePresenter>()
            {
                new ProductInvoicePresenter(new ProductInvoice(0, DateTime.Now.AddDays(-2), TransferType.Buy, "SENDER1", "RECEIVER1", "PAYER1", "CAUSE1")),
                new ProductInvoicePresenter(new ProductInvoice(1, DateTime.Now.AddDays(-1), TransferType.Supply, "SENDER2", "RECEIVER2", "PAYER2", "CAUSE2")),
                new ProductInvoicePresenter(new ProductInvoice(2, DateTime.Now, TransferType.Transport, "SENDER3", "RECEIVER3", "PAYER3", "CAUSE3"))
            };
    }
}

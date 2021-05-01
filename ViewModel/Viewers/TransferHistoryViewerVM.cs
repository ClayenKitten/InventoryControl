using InventoryControl.Model;
using MahApps.Metro.IconPacks;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
            public PackIconFontAwesomeKind IconKind
            {
                get
                {
                    if (Item.Type == TransferType.Buy)
                        return PackIconFontAwesomeKind.TruckLoadingSolid;
                    else if (Item.Type == TransferType.Sell)
                        return PackIconFontAwesomeKind.TruckSolid;
                    else if (Item.Type == TransferType.Supply)
                        return PackIconFontAwesomeKind.ShippingFastSolid;
                    else if (Item.Type == TransferType.Transport)
                        return PackIconFontAwesomeKind.PeopleCarrySolid;
                    else
                        return PackIconFontAwesomeKind.None;
                }
            }

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
            public string Footer => $"По основанию \"{Item.Cause}\"";
            public Visibility FooterVisibility => Item.Cause == "" ? Visibility.Collapsed : Visibility.Visible;

            public ProductInvoicePresenter(ProductInvoice item) => Item = item;
        }
        public ObservableCollection<ProductInvoicePresenter> Content { get; } = new ObservableCollection<ProductInvoicePresenter>();

        public TransferHistoryViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += (_) => UpdateContent();
            UpdateContent();
        }

        public void UpdateContent()
        {
            Content.Clear();
            var presenters = ProductInvoice.Table.ReadAll().Select(x => new ProductInvoicePresenter(x));
            foreach (var presenter in presenters)
            {
                Content.Add(presenter);
            }
        }
    }
}

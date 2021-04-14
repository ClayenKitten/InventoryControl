using InventoryControl.Model;
using InventoryControl.View;
using InventoryControl.View.Controls;
using System;
using System.Windows;
using System.Windows.Media;

namespace InventoryControl.ViewModel
{
    public class PanelManager
    {
        #region Commands
        //Storage
        public OpenPanelCommand OpenStorageView
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new StorageViewer(-1));
                });
            }
        }
        public OpenPanelCommand OpenStorageEdit
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new StorageListViewer());
                });
            }
        }
        //Product
        public OpenPanelCommand OpenProductView
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new ProductDictionaryViewer(), 
                        new EditProductPanel(-1),
                        0.5f
                    );
                });
            }
        }
        //Transfer
        public OpenPanelCommand OpenTransactionProductsViewerBuy
        {
            get
            {
                return new OpenPanelCommand(() =>
                {                
                    if (CounterpartyMapper.GetSuppliers().Count < 1)
                    {
                        MessageBox.Show("Для оформления закупки необходим как минимум один поставщик", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else if (Storage.Table.ReadAll().Count < 1)
                    {
                        MessageBox.Show("Для оформления закупки необходим как минимум один склад", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else
                    {
                        return new DualControlPanelContainer(
                            new ProductDictionaryViewer(),
                            new TransactionProductsViewer(TransferType.Buy)
                        );
                    }
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerSell
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    if (CounterpartyMapper.GetPurchasers().Count < 1)
                    {
                        MessageBox.Show("Для оформления продажи необходим как минимум один покупатель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else if (Storage.Table.ReadAll().Count < 1)
                    {
                        MessageBox.Show("Для оформления продажи необходим как минимум один склад", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else
                    {
                        return new DualControlPanelContainer(
                            new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                            new TransactionProductsViewer(TransferType.Sell));
                    }
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerSupply
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    if (PointOfSales.Table.ReadAll().Count < 1)
                    {
                        MessageBox.Show("Для оформления поставки необходима как минимум одна точка продаж", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else if (Storage.Table.ReadAll().Count < 1)
                    {
                        MessageBox.Show("Для оформления поставки необходим как минимум один склад", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else
                    {
                        return new DualControlPanelContainer(
                            new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                            new TransactionProductsViewer(TransferType.Supply)
                        );
                    }
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerTransport
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    if (Storage.Table.ReadAll().Count < 2)
                    {
                        MessageBox.Show("Для оформления перевозки необходимо как минимум два склада", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    else
                    {
                        return new DualControlPanelContainer(
                            new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                            new TransactionProductsViewer(TransferType.Transport)
                        );
                    }
                });
            }
        }
        public OpenPanelCommand OpenTransferHistoryViewer
            => new OpenPanelCommand(() => new DualControlPanelContainer
                                          (
                                            new TransferHistoryViewer(),
                                            new TransferInvoiceViewer())
                                          );
        // Organizations
        public OpenPanelCommand OpenManagedOrg
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new EditOrganizationPanel(CounterpartyMapper.GetManaged()));
                });
            }
        }
        public OpenPanelCommand OpenSuppliers
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new CounterpartyViewer(false),
                        new EditOrganizationPanel(),
                        0.5f);
                });
            }
        }
        public OpenPanelCommand OpenPurchasers
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new CounterpartyViewer(true),
                        new EditOrganizationPanel(),
                        0.5f);
                });
            }
        }
        public OpenPanelCommand OpenPointsOfSales
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new PointsOfSalesViewer());
                });
            }
        }
        //Other
        public OpenPanelCommand OpenSettings
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new SettingsViewer());
                });
            }
        }
        #endregion
    }
}

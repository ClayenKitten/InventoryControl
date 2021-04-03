﻿using InventoryControl.Model;
using InventoryControl.View;
using InventoryControl.View.Controls;
using System;
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
        //Product
        public OpenPanelCommand OpenProductView
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new ProductDictionaryViewer());
                });
            }
        }
        public OpenPanelCommand OpenProductViewAdd
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new ProductDictionaryViewer(), 
                        new EditProductPanel(),
                        0.5f
                    );
                });
            }
        }
        //Transaction
        public OpenPanelCommand OpenTransactionProductsViewerBuy
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new ProductDictionaryViewer(),
                        new TransactionProductsViewer(TransferType.Buy)
                    );
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerSell
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                        new TransactionProductsViewer(TransferType.Sell)
                    );
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerReturn
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                        new TransactionProductsViewer(TransferType.Return)
                    );
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerSupply
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                        new TransactionProductsViewer(TransferType.Supply)
                    );
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerTransport
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new DualControlPanelContainer(
                        new StorageViewer(-1, new ViewOptions() { HideStorageSelector = true }),
                        new TransactionProductsViewer(TransferType.Transport)
                    );
                });
            }
        }
        public OpenPanelCommand OpenSuppliers
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new CounterpartyViewer(false));
                });
            }
        }
        public OpenPanelCommand OpenPurchasers
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new SingleControlPanelContainer(new CounterpartyViewer(true));
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

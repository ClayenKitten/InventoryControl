using InventoryControl.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryControl.ViewModel
{
    public class PanelManager
    {
        #region Names consts
        //Storage
        const string STORAGEVIEW = "StorageView";
        const string STORAGEEDIT = "StorageEdit";
        //Transaction
        const string BUYTRANSACTION = "BuyTransaction";
        const string SELLTRANSACTION = "SellTransaction";
        const string INVOICEEDIT = "InvoiceEditor";
        //Product
        const string PRODUCTVIEW = "ProductView";
        const string PRODUCTVIEWADD = "ProductViewAdd";
        const string RECEIPEDIT = "ReceiptEditor";
        //COUNTERPARTY
        const string SUPPLIERSEDIT = "Suppliers";
        const string BUYERSEDIT = "Buyers";
        const string POINTSOFSALESEDIT = "PointsOfSales";
        //OTHER
        const string SETTINGS = "Settings";
        #endregion
        #region Commands
        //Storage
        public OpenPanelCommand OpenStorageView { get; } = new OpenPanelCommand(STORAGEVIEW);
        public OpenPanelCommand OpenStorageEdit { get; } = new OpenPanelCommand(STORAGEEDIT);
        //Transaction
        public OpenPanelCommand OpenBuy { get; } = new OpenPanelCommand(BUYTRANSACTION);
        public OpenPanelCommand OpenSell { get; } = new OpenPanelCommand(SELLTRANSACTION);
        public OpenPanelCommand OpenInvoiceEditor { get; } = new OpenPanelCommand(INVOICEEDIT);
        //Product
        public OpenPanelCommand OpenProductView { get; } = new OpenPanelCommand(PRODUCTVIEW);
        public OpenPanelCommand OpenProductViewAdd { get; } = new OpenPanelCommand(PRODUCTVIEWADD);
        public OpenPanelCommand OpenReceiptEditor { get; } = new OpenPanelCommand(RECEIPEDIT);
        //Counterparty
        public OpenPanelCommand OpenSuppliers { get; } = new OpenPanelCommand(SUPPLIERSEDIT);
        public OpenPanelCommand OpenBuyers { get; } = new OpenPanelCommand(BUYERSEDIT);
        public OpenPanelCommand OpenPointsOfSales { get; } = new OpenPanelCommand(POINTSOFSALESEDIT);
        //Other
        public OpenPanelCommand OpenSettings { get; } = new OpenPanelCommand(SETTINGS);
        #endregion
        public static void OpenPanel(string PanelName)
        {
            MainWindow window = (MainWindow)App.Current.MainWindow;
            switch(PanelName)
            {
                case (PanelManager.STORAGEVIEW):
                    window.SetPanel(
                        new AdaptiveStackControl(AdaptiveStackScheme.SINGLE,
                        new StorageViewer(0))
                    );
                    break;
                case (PanelManager.STORAGEEDIT):
                    break;
                case (PanelManager.PRODUCTVIEW):
                    window.SetPanel(
                        new AdaptiveStackControl(AdaptiveStackScheme.SINGLE,
                        new ProductDictionaryViewer())
                    );
                    break;
                case (PanelManager.PRODUCTVIEWADD):
                    window.SetPanel(
                        new AdaptiveStackControl(AdaptiveStackScheme.PRIORITIZED, 
                        new ProductDictionaryViewer(), new EditProductPanel())
                    );
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

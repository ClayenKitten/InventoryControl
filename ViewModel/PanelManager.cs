using InventoryControl.View;
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
                    return new AdaptiveStackControl(AdaptiveStackScheme.SINGLE,
                    new StorageViewer());
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
                    return new AdaptiveStackControl(AdaptiveStackScheme.SINGLE,
                    new ProductDictionaryViewer());
                });
            }
        }
        public OpenPanelCommand OpenProductViewAdd
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new AdaptiveStackControl(AdaptiveStackScheme.PRIORITIZED, 
                    new ProductDictionaryViewer(), new EditProductPanel());
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
                    return new AdaptiveStackControl(AdaptiveStackScheme.DIVIDED,
                    new StorageViewer(), new TransactionProductsViewer(TransactionType.Buy));
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerSell
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new AdaptiveStackControl(AdaptiveStackScheme.DIVIDED,
                    new StorageViewer(), new TransactionProductsViewer(TransactionType.Sell));
                });
            }
        }
        public OpenPanelCommand OpenTransactionProductsViewerReturn
        {
            get
            {
                return new OpenPanelCommand(() =>
                {
                    return new AdaptiveStackControl(AdaptiveStackScheme.DIVIDED,
                    new StorageViewer(), new TransactionProductsViewer(TransactionType.Return));
                });
            }
        }
        #endregion
    }
}

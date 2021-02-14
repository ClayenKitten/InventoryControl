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
        #endregion
    }
}

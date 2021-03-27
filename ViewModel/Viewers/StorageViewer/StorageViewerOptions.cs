using System;

namespace InventoryControl.ViewModel
{
    [Flags]
    public enum StorageViewerOptions
    {
        None = 0,
        HideStorageSelector = 1,
        HideOutOfStockProducts = 2,
        GroupOutOfStockProducts = 4,
        ShowOptionsSettings = 8
    }
}

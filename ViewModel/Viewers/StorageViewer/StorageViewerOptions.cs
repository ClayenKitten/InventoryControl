namespace InventoryControl.ViewModel
{
    public class StorageViewerOptions
    {
        // Filtering
        public bool HideOutOfStockProducts { get; set; } = true;
        public bool HideDeletedProducts { get; set; } = true;
        // Grouping
        public bool GroupByCategory { get; set; } = true;
        public bool GroupByManufacturer { get; set; } = false;
        public bool GroupOutOfStockProducts { get; set; } = false;
        // Limiting control over StorageViewer
        public bool HideStorageSelector { get; set; } = false;
        public bool HideFilteringSettings { get; set; } = false;
    }
}

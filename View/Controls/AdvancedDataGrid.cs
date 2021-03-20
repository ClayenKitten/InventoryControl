using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace InventoryControl.View.Controls
{
    public class AdvancedDataGrid : DataGrid
    {
        private string searchString;
        //TODO
        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                Items.Filter = (x) => { return true; };
            }
        }
        
        public AdvancedDataGrid() : base()
        {
            
            new CollectionViewSource() { Source = this.ItemsSource };
            
        }
    }
}

using InventoryControl.Model;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace InventoryControl.View.Controls
{
    public class AdvancedDataGrid : DataGrid
    {
        private string searchString;
        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                Items.Filter = (x) =>
                {
                    bool hasPassed = true;
                    if (x is INamed)
                        hasPassed = (x as INamed).Name == SearchString;
                    else
                        hasPassed = false;
                    return hasPassed && this.Filter(x);
                };
            }
        }
        public Predicate<object> Filter { get; set; }      

        public AdvancedDataGrid() : base() { }
    }
}

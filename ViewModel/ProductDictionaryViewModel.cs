using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryControl.ViewModel
{
    class ProductDictionaryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProductPresenter> Content { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

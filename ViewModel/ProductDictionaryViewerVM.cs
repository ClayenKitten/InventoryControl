using InventoryControl.Model.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace InventoryControl.ViewModel
{
    class ProductDictionaryViewerVM : INotifyPropertyChanged
    {
        public ObservableCollection<ProductPresenter> Content
        {
            get
            {
                ObservableCollection<ProductPresenter> res = new ObservableCollection<ProductPresenter>();
                foreach (var product in ProductDataMapper.GetFullDictionary())
                {
                    res.Add(new ProductPresenter(product));
                }
                return res;
            }
        }
        public ProductDictionaryViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += (_) => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content")); };
        }

        private string searchString;
        public string SearchString 
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged();
                OnPropertyChanged("Content");
            }
        }

        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

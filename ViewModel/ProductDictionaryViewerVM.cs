using InventoryControl.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InventoryControl.ViewModel
{
    class ProductDictionaryViewerVM : INotifyPropertyChanged
    {
        public ObservableCollection<ProductPresenter> Content
        {
            get
            {
                ObservableCollection<ProductPresenter> res = new ObservableCollection<ProductPresenter>();
                foreach (var product in ProductMapper.GetFullDictionary())
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

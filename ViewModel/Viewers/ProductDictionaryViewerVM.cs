using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
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
                foreach (var product in Product.Table.ReadAll())
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

        public ActionCommand CreateNewProductCommand { get; }
            = new ActionCommand(() =>
                {
                    Product.Table.Create(new Product());
                    GlobalCommands.ModelUpdated.Execute(null);
                });

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

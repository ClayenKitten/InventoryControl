using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InventoryControl.ViewModel
{
    class ProductDictionaryViewerVM : INotifyPropertyChanged
    {
        public ObservableCollection<ProductPresenter> Content { get; set; }
        public ProductDictionaryViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += (_) => UpdateContent();
            UpdateContent();
        }

        public ActionCommand CreateNewProductCommand => new ActionCommand(() =>
        {
            UpdateContent();
            Content.Add(new Product());
        });
        public ActionCommand DeleteProductCommand => new ActionCommand((id) =>
        {
            Product.Table.TryDelete((long)id);
            UpdateContent();
        });
        
        private void UpdateContent()
        {
            Content = new ObservableCollection<ProductPresenter>();
            foreach (var product in Product.Table.ReadAll())
            {
                Content.Add(new ProductPresenter(product));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

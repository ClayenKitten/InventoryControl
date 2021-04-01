using InventoryControl.Model;
using InventoryControl.View.Controls;
using InventoryControl.ViewModel;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for StorageViewer.xaml
    /// </summary>
    public partial class StorageViewer : ControlPanel
    {
        public StorageViewer()
        {
            InitializeComponent();
        }
        public StorageViewer(int storageId, StorageViewerOptions options) : this()
        {
            var dataContext = (StorageViewerVM)DataContext;
            if(storageId == -1)
            {
                dataContext.StorageId = StorageMapper.GetAllStorages().FirstOrDefault().Id;
            }
            else
            {
                dataContext.StorageId = storageId;
            }
            dataContext.Options = options;
            GlobalCommands.ModelUpdated.Execute(null);
        }
        public StorageViewer(int storageId) : this(storageId, new StorageViewerOptions()) {}

        private void ShowHeaderContextMenu()
        {
            var builder = new ContextMenuBuilder()
                .BeginGroup("Группировать...")
                    .AddCheckable("По категории", null)
                    .AddCheckable("По производителю", null)
                    .AddCheckable("По наличию", null)
                .EndGroup();
            if (!((StorageViewerVM)DataContext).Options.HideFilteringSettings)
            {
                builder
                    .BeginGroup("Фильтровать...")
                        .AddCheckable("Закончившиеся", null)
                        .AddCheckable("Удалённые", null)
                    .EndGroup();
            }
            builder
                .AddSeparator()
                .BeginGroup("Показывать поле...")
                    .AddCheckable("Артикула", null)
                    .AddCheckable("Наименования", null)
                    .AddCheckable("Категории", null)
                    .AddCheckable("Производителя", null)
                    .AddCheckable("Количества", null)
                    .AddCheckable("Единиц измерения", null)
                    .AddCheckable("Закупочной цены", null)
                    .AddCheckable("Продажной цены", null)
                    .AddCheckable("Остатка", null)
                .EndGroup();
            builder.Build().IsOpen = true;
        }
        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            if(e.ClickCount == 2)
            {
                var panels = ((MainWindow)App.Current.MainWindow).Panel.ControlPanels;
                panels.Remove(this);
                foreach(var panel in panels)
                {
                    if (panel.DataContext is TransactionProductsViewerVM vm)
                    {
                        vm.AddProduct(((StockProductPresenter)row.Item).Id);
                    }
                }
            }
        }
        private void MainDataGrid_HeaderClicked(object sender, MouseButtonEventArgs e, System.Windows.Controls.Primitives.DataGridColumnHeader header)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                ShowHeaderContextMenu();
            }
        }
    }
}

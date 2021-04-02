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
        public StorageViewer(int storageId, ViewOptions options) : this()
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
        public StorageViewer(int storageId) : this(storageId, new ViewOptions()) {}

        private void ShowHeaderContextMenu()
        {
            var VM = (StorageViewerVM)DataContext;
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
                        .AddCheckable("Закончившиеся", x => VM.Options.SetFilter(x, "IsInStock", true))
                        .AddCheckable("Удалённые", null)
                    .EndGroup();
            }
            builder
                .AddSeparator()
                .BeginGroup("Показывать поле...")
                    .AddCheckable("Артикула", x => VM.Options.SetCollapsedColumn(!x, "Article"), isEnabled: false)
                    .AddCheckable("Наименования", x => VM.Options.SetCollapsedColumn(!x, "Name"), isEnabled: false)
                    .AddCheckable("Категории", x => VM.Options.SetCollapsedColumn(!x, "Category"))
                    .AddCheckable("Производителя", x => VM.Options.SetCollapsedColumn(!x, "Manufacturer"))
                    .AddCheckable("Количества", x => VM.Options.SetCollapsedColumn(!x, "Packing"))
                    .AddCheckable("Единиц измерения", x => VM.Options.SetCollapsedColumn(!x, "Measurement"))
                    .AddCheckable("Закупочной цены", x => VM.Options.SetCollapsedColumn(!x, "PurchasePrice"))
                    .AddCheckable("Продажной цены", x => VM.Options.SetCollapsedColumn(!x, "SalePrice"))
                    .AddCheckable("Остатка", x => VM.Options.SetCollapsedColumn(!x, "Remain"))
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

using InventoryControl.Model;
using InventoryControl.ORM;
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
            var o = ((StorageViewerVM)DataContext).Options;
            var builder = new ContextMenuBuilder();
            if (!((StorageViewerVM)DataContext).Options.HideFilteringSettings)
            {
                builder.AddCheckable("Скрывать закончившиеся", x => o.SetFilter(x, "IsInStock", true))
                    .Check(o.DoesFilter("IsInStock", true));
            }
            builder
                .BeginGroup("Группировать...")
                    .AddRadio("По категории", "grouping", x => o.SetGroup(x, "Category"))
                        .Check(o.DoesGroup("Category"))
                    .AddRadio("По наличию", "grouping", x => o.SetGroup(x, "IsInStock"))
                        .Check(o.DoesGroup("IsInStock"))
                    .AsRadioGroup()
                .EndGroup()
                .AddSeparator()
                .BeginGroup("Показывать поле...")
                    .AddCheckable("Категории", x => o.SetCollapsedColumn(!x, "Category"))
                        .Check(!o.IsColumnCollapsed("Category"))
                    .AddCheckable("Количества", x => o.SetCollapsedColumn(!x, "Packing"))
                        .Check(!o.IsColumnCollapsed("Packing"))
                    .AddCheckable("Единиц измерения", x => o.SetCollapsedColumn(!x, "Measurement"))
                        .Check(!o.IsColumnCollapsed("Measurement"))
                    .AddCheckable("Закупочной цены", x => o.SetCollapsedColumn(!x, "PurchasePrice"))
                        .Check(!o.IsColumnCollapsed("PurchasePrice"))
                    .AddCheckable("Продажной цены", x => o.SetCollapsedColumn(!x, "SalePrice"))
                        .Check(!o.IsColumnCollapsed("SalePrice"))
                .EndGroup();
            builder.Build().IsOpen = true;
        }
        private void MainDataGrid_RowClicked(object sender, MouseButtonEventArgs e, DataGridRow row)
        {
            if(e.ClickCount == 2)
            {
                SendMessage(typeof(TransactionProductsViewer), ((StockProductPresenter)row.Item).Id);
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

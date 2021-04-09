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
                .BeginGroup("Группировка...")
                    .AddRadio("Отсутствует", "grouping", x => { if (x) o.Group = string.Empty; })
                        .Check(o.Group == string.Empty)
                    .AddRadio("По категории", "grouping", x => { if (x) o.Group = "Category"; })
                        .Check(o.Group == "Category")
                    .AddRadio("По наличию", "grouping", x => { if (x) o.Group = "IsInStock"; })
                        .Check(o.Group == "IsInStock")
                    .AsRadioGroup()
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

using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace InventoryControl.ViewModel
{
    public class PointsOfSalesViewerVM : INotifyPropertyChanged
    {
        public PointsOfSalesViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += _ =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            };
        }
        public List<PointOfSales> Content
            => (List<PointOfSales>)PointOfSales.Table.ReadAll();
        public ActionCommand DeletePointOfSales
            => new ActionCommand((x) => 
            {
                if (MessageBox.Show
                (
                    "Вы действительно хотите удалить точку сбыта? Эта операция необратима.",
                    "Удалить точку сбыта",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No
                ) == MessageBoxResult.No) 
                {
                    return;
                }
                PointOfSales.Table.TryDelete((int)x);
                GlobalCommands.ModelUpdated.Execute(null);
            });

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
                    "�� ������������� ������ ������� ����� �����? ��� �������� ����������.",
                    "������� ����� �����",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No
                ) == MessageBoxResult.No) 
                {
                    return;
                }
                PointOfSales.Table.TryDelete((long)x);
                GlobalCommands.ModelUpdated.Execute(null);
            });

        public event PropertyChangedEventHandler PropertyChanged;

        public Action<DataGridRowEditEndingEventArgs> OnRowEditEnded => (e) =>
        {
            PointOfSales.Table.Update((PointOfSales)e.Row.Item);
        };
    }
}

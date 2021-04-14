using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    public class StorageListViewerVM : INotifyPropertyChanged
    {
        public StorageListViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += _ =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            };
        }
        public List<Storage> Content
            => (List<Storage>)Storage.Table.ReadAll();
        public ActionCommand DeleteStorage
            => new ActionCommand((x) => 
            {
                if (!Storage.ProductsNumberTable.ReadAll()
                    .Where(s => s.Item2 == (long)x)
                    .All(x => (int)x.Item3 == 0))
                {
                    MessageBox.Show("Склад должен быть пуст перед удалением",
                                    "Невозможно удалить склад",
                                    MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    return;
                }
                if (MessageBox.Show
                (
                    "Вы действительно хотите удалить склад? Эта операция необратима.",
                    "Удалить склад",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No
                ) == MessageBoxResult.No) 
                {
                    return;
                }
                Storage.Table.TryDelete((long)x);
                GlobalCommands.ModelUpdated.Execute(null);
            });

        public event PropertyChangedEventHandler PropertyChanged;

        public Action<DataGridRowEditEndingEventArgs> OnRowEditEnded => (e) =>
        {
            Storage.Table.Update((Storage)e.Row.Item);
        };
    }
}

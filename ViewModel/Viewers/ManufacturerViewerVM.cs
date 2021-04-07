using InventoryControl.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace InventoryControl.ViewModel
{
    public class ManufacturerViewerVM : INotifyPropertyChanged
    {
        public ManufacturerViewerVM()
        {
            GlobalCommands.ModelUpdated.Executed += _ =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            };
        }
        public IList<Manufacturer> Content
            => Manufacturer.Table.ReadAll();
        public ActionCommand DeleteRecord
            => new ActionCommand((x) => 
            {
                if (MessageBox.Show
                (
                    "¬ы действительно хотите удалить производител€? Ёта операци€ необратима.",
                    "”далить производител€",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No
                ) == MessageBoxResult.No) 
                {
                    return;
                }
                Manufacturer.Table.TryDelete((long)x);
                GlobalCommands.ModelUpdated.Execute(null);
            });

        public Action<DataGridRowEditEndingEventArgs> OnRowEditEnded => (e) =>
        {
            Manufacturer.Table.Update((Manufacturer)e.Row.Item);
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

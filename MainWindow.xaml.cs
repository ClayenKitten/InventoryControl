﻿using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.View;
using System.Windows.Shapes;
using System.Windows.Media;
using InventoryControl.ViewModel;
using InventoryControl.View.Controls;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var initPanel = new AdaptiveStackControl(AdaptiveStackScheme.SINGLE, new StorageViewer(0));
            this.SetPanel(initPanel);

            GlobalCommands.EditProduct.Executed += (productId) =>
            {
                this.SetPanel
                (
                    new AdaptiveStackControl
                    (
                        AdaptiveStackScheme.PRIORITIZED,
                        new ProductDictionaryViewer(),
                        new EditProductPanel((int)productId)
                    )
                );
            };
        }
        public void SetPanel(AdaptiveStackControl content)
        {
            //Dispose if ASC already set
            var cur = MainWindowGrid.Children[MainWindowGrid.Children.Count - 1];
            if (cur is AdaptiveStackControl adaptiveStackControl)
            {
                adaptiveStackControl.Dispose();
            }
            //Set new ASC
            content.SetValue(Grid.RowProperty, 1);
            foreach (var child in MainWindowGrid.Children)
            {
                if (child is AdaptiveStackControl)
                {
                    MainWindowGrid.Children.Remove((UIElement)child);
                    MainWindowGrid.Children.Add(content);
                    return;
                }
            }
            MainWindowGrid.Children.Add(content);
        }
    }
}
﻿using System;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using InventoryControl.UserControls;
using InventoryControl.UserControls.OrderControl;
using InventoryControl.Panel;

namespace InventoryControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public IPanel OpenedPanel { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int OrderControlColumnIndex = 3;
        
        public void SetOrderControl(bool? isBuying) {}
        public OrderControl GetOrderControl() 
        {
            return null;
        }

        public void SetPanel<T>(T panel) where T : UIElement, IPanel
        {
            throw new NotImplementedException();
        }
    }
}
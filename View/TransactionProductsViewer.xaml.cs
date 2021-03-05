﻿using InventoryControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for TransactionProductsViewer.xaml
    /// </summary>
    public partial class TransactionProductsViewer : UserControl
    {
        public TransactionProductsViewer(TransactionType type)
        {
            InitializeComponent();
            ((TransactionProductsViewerVM)DataContext).Type = type;
        }
    }
}
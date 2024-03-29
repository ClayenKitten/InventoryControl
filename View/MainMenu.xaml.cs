﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using InventoryControl.Model;
using InventoryControl.ViewModel;

namespace InventoryControl.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void LoadBackupButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppContext.BaseDirectory + "Backups\\";
            openFileDialog.Filter = "Database (*.db)|*.db|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                File.Copy(openFileDialog.FileName, "Database.db", true);
            }
            GlobalCommands.ModelUpdated?.Execute(null);
        }
        private void SaveBackupButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppContext.BaseDirectory + "Backups\\";
            saveFileDialog.Filter = "Database (*.db)|*.db|All files (*.*)|*.*";
            saveFileDialog.ValidateNames = true;
            saveFileDialog.FileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".db";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.Copy("Database.db", saveFileDialog.FileName);
            }
            System.IO.Directory.CreateDirectory("Backups");
        }

        private void EditPointsOfSalesClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void SellingView_Click(object sender, RoutedEventArgs e) { throw new NotImplementedException(); }
        private void BuyingView_Click(object sender, RoutedEventArgs e) { throw new NotImplementedException(); }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Replace fake one with real
            var fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileDialog.Filter = "Config (*.cfg)|*.cfg|All files (*.*)|*.*";
            fileDialog.ValidateNames = true;
            fileDialog.FileName = "Export-Mercury185F";
            if (fileDialog.ShowDialog() == true)
            {
                var bytes = new byte[Product.Table.ReadAll().Count * 121];
                new Random().NextBytes(bytes);
                File.WriteAllBytes(fileDialog.FileName, bytes);
            }
        }
    }
}

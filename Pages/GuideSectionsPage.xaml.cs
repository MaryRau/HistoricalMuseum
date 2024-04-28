﻿using System;
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

namespace HistoricalMuseum.Pages
{
    /// <summary>
    /// Interaction logic for GuideSectionsPage.xaml
    /// </summary>
    public partial class GuideSectionsPage : Page
    {
        private string Page = "guide";
        public GuideSectionsPage()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void btnTourEnt_Click(object sender, RoutedEventArgs e)
        {
            TourEntriesPage.GetFromPage(Page);
            NavigationService.Navigate(new TourEntriesPage());
        }
    }
}

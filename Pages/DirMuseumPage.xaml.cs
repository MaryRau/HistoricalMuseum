﻿using HistoricalMuseum.Pages;
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

namespace HistoricalMuseum
{
    /// <summary>
    /// Interaction logic for AdminHallsPage.xaml
    /// </summary>
    public partial class DirMuseumPage : Page
    {
        private string Page = "dir";
        public DirMuseumPage()
        {
            InitializeComponent();
        }

        private void btnExh_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsPage());
        }

        private void btnAuthors_Click(object sender, RoutedEventArgs e)
        {
            AuthorsPage.GetFromPage(Page);
            NavigationService.Navigate(new AuthorsPage());
        }

        private void btnEpoches_Click(object sender, RoutedEventArgs e)
        {
            HistoricalEpochsPage.GetFromPage(Page);
            NavigationService.Navigate(new HistoricalEpochsPage());
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsTypesPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsTypesPage());
        }

        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            CountriesPage.GetFromPage(Page);
            NavigationService.Navigate(new CountriesPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirSectionsPage());
        }

        private void btnHalls_Click(object sender, RoutedEventArgs e)
        {
            HallsPage.GetFromPage(Page);
            NavigationService.Navigate(new HallsPage());
        }

        private void btnExhInHalls_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsInHallsPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsInHallsPage());
        }

        private void btnExhInStoreroom_Click(object sender, RoutedEventArgs e)
        {
            ExhibitsInStoreroomPage.GetFromPage(Page);
            NavigationService.Navigate(new ExhibitsInStoreroomPage());
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaffPage());
        }

        private void btnPosts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PostsPage());
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            (sender as Border).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            (sender as Border).BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }

        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF313131");
            (sender as TextBlock).TextDecorations = TextDecorations.Baseline;
        }

        private void btnExit_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF6A6A6A");
            (sender as TextBlock).TextDecorations = TextDecorations.Underline;
        }
    }
}

using HistoricalMuseum.Pages;
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
            CountriesPage.GetFromPage(Page);
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
    }
}

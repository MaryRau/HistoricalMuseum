using HistoricalMuseum.Pages.Tables;
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
    /// Interaction logic for AdminSectionsPage.xaml
    /// </summary>
    public partial class DirSectionsPage : Page
    {
        public DirSectionsPage()
        {
            InitializeComponent();
        }

        private void btnMuseum_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirMuseumPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuidedToursPage());
        }

        private void Report_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PopularityOfProgramPage());
        }

        private void btnTours_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirToursPage());
        }
    }
}

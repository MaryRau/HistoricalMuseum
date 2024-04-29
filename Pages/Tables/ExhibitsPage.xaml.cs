using HistoricalMuseum.Pages;
using HistoricalMuseum.Pages.AddToTables;
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
    /// Interaction logic for ExhibitsPage.xaml
    /// </summary>
    public partial class ExhibitsPage : Page
    {
        private static string fromPage;
        public ExhibitsPage()
        {
            InitializeComponent();
            DataGridExhibits.ItemsSource = MuseumEntities.GetContext().Exhibits.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (fromPage == "researcher")
                NavigationService.Navigate(new ResearcherSectionsPage());
            else if (fromPage == "dir")
                NavigationService.Navigate(new DirMuseumPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddExhibitsPage((sender as Button).DataContext as Exhibits));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddExhibitsPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridExhibits.ItemsSource = MuseumEntities.GetContext().Exhibits.ToList();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridExhibits.SelectedItems.Cast<Exhibits>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().Exhibits.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridExhibits.ItemsSource = MuseumEntities.GetContext().Exhibits.ToList();
                }
                catch
                {
                    return;
                }
            }
        }

        public static string GetFromPage(string from)
        {
            fromPage = from;
            return fromPage;
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Поиск")
            {
                txtSearch.Clear();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string s = txtSearch.Text.Trim();
            if (txtSearch.Text != "Поиск" || !string.IsNullOrWhiteSpace(s))
                DataGridExhibits.ItemsSource = MuseumEntities.GetContext().Exhibits.Where(x => x.Exhibit.Contains(txtSearch.Text) || x.ExhibitsTypes.Type.Contains(txtSearch.Text) ||
                x.HistoricalEpochs.Epoch.Contains(txtSearch.Text) || x.Authors.FIOAuthor.Contains(txtSearch.Text) || x.Countries.Country.Contains(txtSearch.Text) ||
                x.Description.Contains(txtSearch.Text)).ToList();
            else
            {
                DataGridExhibits.ItemsSource = MuseumEntities.GetContext().Exhibits.ToList();
                txtSearch.Text = "Поиск";
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Поиск";
        }

        private void btnAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            borderBtnAdd.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            borderBtnAdd.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btnAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            borderBtnAdd.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            borderBtnAdd.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }

        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            borderBtnBack.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            borderBtnBack.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            borderBtnBack.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            borderBtnBack.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }

        private void btnDel_MouseEnter(object sender, MouseEventArgs e)
        {
            borderBtnDel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            borderBtnDel.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btnDel_MouseLeave(object sender, MouseEventArgs e)
        {
            borderBtnDel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            borderBtnDel.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }

        private void btnSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            borderBtnSearch.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFCFBDAB");
            borderBtnSearch.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7A6653");
        }

        private void btnSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            borderBtnSearch.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFEEDCCA");
            borderBtnSearch.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF98826C");
        }
    }
}

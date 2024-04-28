﻿using HistoricalMuseum.Pages.AddToTables;
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

namespace HistoricalMuseum.Pages
{
    /// <summary>
    /// Interaction logic for CountriesPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        public PostsPage()
        {
            InitializeComponent();
            DataGridPosts.ItemsSource = MuseumEntities.GetContext().Posts.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirMuseumPage());
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPostPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridPosts.ItemsSource = MuseumEntities.GetContext().Posts.ToList();
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var elemForRemoving = DataGridPosts.SelectedItems.Cast<Posts>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {elemForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MuseumEntities.GetContext().Posts.RemoveRange(elemForRemoving);
                    MuseumEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridPosts.ItemsSource = MuseumEntities.GetContext().Posts.ToList();
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPostPage((sender as Button).DataContext as Posts));
        }
    }
}

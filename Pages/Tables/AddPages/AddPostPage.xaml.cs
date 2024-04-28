using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace HistoricalMuseum.Pages.AddToTables
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddPostPage : Page
    {
        private Posts _currentPost = new Posts();
        public AddPostPage(Posts selected)
        {
            InitializeComponent();

            if (selected != null) 
                _currentPost = selected;

            DataContext = _currentPost;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtPost.Text))
                errors.AppendLine("Укажите должность!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

                Posts postObject = new Posts
                {
                    Post = txtPost.Text,
                };

                var posts = MuseumEntities.GetContext().Posts.AsNoTracking().FirstOrDefault(f => f.Post == txtPost.Text);

            if (_currentPost.id == 0)
            {
                if (posts == null)
                {
                    MuseumEntities.GetContext().Posts.Add(_currentPost);
                }

                else
                {
                    MessageBox.Show("Такая должность уже существует!");
                    return;
                }
            }

            else
            {
                if (posts != null)
                {
                    MessageBox.Show("Такая должность уже существует!");
                    return;
                }
            }

            try
            {
                MuseumEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

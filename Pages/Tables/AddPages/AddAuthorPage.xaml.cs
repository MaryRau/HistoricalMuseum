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
    public partial class AddAuthorPage : Page
    {
        private Authors _currentAuthor = new Authors();
        public AddAuthorPage(Authors selected)
        {
            InitializeComponent();

            if (selected != null)
            {
                _currentAuthor = selected;
            }

            DataContext = _currentAuthor;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(FIO.Text))
                errors.AppendLine("Укажите автора!");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            Authors authorObject = new Authors
            {
                FIOAuthor = FIO.Text
            };

            var author = MuseumEntities.GetContext().Authors.AsNoTracking().FirstOrDefault(f => f.FIOAuthor.ToLower() == FIO.Text.ToLower());

            if (_currentAuthor.id == 0)
            {
                if (author == null)
                {
                    MuseumEntities.GetContext().Authors.Add(_currentAuthor);
                }

                else
                {
                    MessageBox.Show("Такой автор уже существует!");
                    return;
                }
            }

            else
            {
                if (author != null)
                { 
                    MessageBox.Show("Такой автор уже существует!");
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

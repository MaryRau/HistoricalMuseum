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

namespace HistoricalMuseum.Pages.AddToTables
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddHallsPage : Page
    {
        private Halls _currentHall = new Halls();
        public AddHallsPage(Halls selected)
        {
            InitializeComponent();

            if (selected != null )
                _currentHall = selected;

            DataContext = _currentHall;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtTheme.Text))
                errors.AppendLine("Укажите зал!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            Halls hallObject = new Halls
            {
                Theme = txtTheme.Text,
            };

            var halls = MuseumEntities.GetContext().Halls.AsNoTracking().FirstOrDefault(f => f.Theme == txtTheme.Text);

            if (_currentHall.id == 0)
            {
                if (halls == null)
                {
                    MuseumEntities.GetContext().Halls.Add(_currentHall);
                }

                else
                {
                    MessageBox.Show("Такой зал уже существует!");
                    return;
                }
            }

            else
            {
                if (halls != null)
                {
                    MessageBox.Show("Такой зал уже существует!");
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

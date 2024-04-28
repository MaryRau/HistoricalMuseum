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
    public partial class AddCountryPage : Page
    {
        private Countries _currentCountry = new Countries();
        public AddCountryPage( Countries selected)
        {
            InitializeComponent();

            if (selected != null)
                _currentCountry = selected;

            DataContext = _currentCountry;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtCountry.Text))
                errors.AppendLine("Укажите страну!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            Countries countryObject = new Countries
            {
                Country = txtCountry.Text
            };

            var country = MuseumEntities.GetContext().Countries.AsNoTracking().FirstOrDefault(f => f.Country.ToLower() == txtCountry.Text.ToLower());

            if (_currentCountry.id == 0)
            {
                if (country == null)
                {
                    MuseumEntities.GetContext().Countries.Add(_currentCountry);
                }

                else
                {
                    MessageBox.Show("Такая страна уже существует!");
                    return;
                }
            }

            else
            {
                if (country != null)
                {
                    MessageBox.Show("Такая страна уже существует!");
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

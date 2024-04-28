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
    public partial class AddEpochPage : Page
    {
        private HistoricalEpochs _currentEpoch = new HistoricalEpochs();
        public AddEpochPage(HistoricalEpochs selected)
        {
            InitializeComponent();

            if (selected != null)
                _currentEpoch = selected;

            DataContext = _currentEpoch;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtEpoch.Text))
                errors.AppendLine("Укажите историческую эпоху!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            HistoricalEpochs authorObject = new HistoricalEpochs
            {
                Epoch = txtEpoch.Text
            };

            var epoch = MuseumEntities.GetContext().HistoricalEpochs.AsNoTracking().FirstOrDefault(f => f.Epoch.ToLower() == txtEpoch.Text.ToLower());

            if (_currentEpoch.id == 0)
            {
                if (epoch == null)
                {
                    MuseumEntities.GetContext().HistoricalEpochs.Add(_currentEpoch);
                }

                else
                {
                    MessageBox.Show("Такая эпоха уже существует!");
                    return;
                }
            }

            else
            {
                if (epoch != null)
                {
                    MessageBox.Show("Такая эпоха уже существует!");
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

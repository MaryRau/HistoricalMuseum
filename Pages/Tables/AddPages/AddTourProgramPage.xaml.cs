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
    public partial class AddTourProgramPage : Page
    {
        private TourPrograms _currentTour = new TourPrograms();
        public AddTourProgramPage(TourPrograms selected)
        {
            InitializeComponent();

            if (selected != null) 
                _currentTour = selected;

            DataContext = _currentTour;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtTour.Text))
                errors.AppendLine("Укажите экскурсию!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            TourPrograms tourObject = new TourPrograms
            {
                TourTheme = txtTour.Text
            };

            var tour = MuseumEntities.GetContext().TourPrograms.AsNoTracking().FirstOrDefault(f => f.TourTheme == txtTour.Text);

            if (_currentTour.id == 0)
            {
                if (tour == null)
                {
                    MuseumEntities.GetContext().TourPrograms.Add(_currentTour);
                }

                else
                {
                    MessageBox.Show("Такой сотрудник уже существует!");
                    return;
                }
            }

            else
            {
                if (tour != null)
                {
                    MessageBox.Show("Такой сотрудник уже существует!");
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

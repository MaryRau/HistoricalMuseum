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
    public partial class AddHallsInTourProgramsPage : Page
    {
        private HallsInTourPrograms _current = new HallsInTourPrograms();
        public AddHallsInTourProgramsPage(HallsInTourPrograms selected)
        {
            InitializeComponent();

            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.Select(x => x.Theme).ToList();
            cmbTourProg.ItemsSource = MuseumEntities.GetContext().TourPrograms.Select(x => x.TourTheme).ToList();

            if (selected != null)
                _current = selected;

            DataContext = _current;
            cmbTourProg.ItemsSource = MuseumEntities.GetContext().TourPrograms.Select(x => x.TourTheme).ToList();
            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.Select(x => x.Theme).ToList();

            if (cmbHall.SelectedItem == null)
                cmbHall.Text = "Обязательный";
            if (cmbTourProg.SelectedItem == null)
                cmbTourProg.Text = "Обязательный";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(cmbHall.Text) || cmbHall.Text == "Обязательный")
                errors.AppendLine("Укажите зал!");
            if (string.IsNullOrWhiteSpace(cmbTourProg.Text) || cmbTourProg.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсию!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            int hall = MuseumEntities.GetContext().Halls.Where(x => x.Theme == cmbHall.Text).Select(x => x.id).First();
            int tour = MuseumEntities.GetContext().TourPrograms.Where(x => x.TourTheme == cmbTourProg.Text).Select(x => x.id).First();

            HallsInTourPrograms exInStoreObject = new HallsInTourPrograms
            {
                Hall = hall,
                TourProgram = tour
            };

            var hallsInTours = MuseumEntities.GetContext().HallsInTourPrograms.AsNoTracking().FirstOrDefault(f => f.Hall == hall && f.TourProgram == tour);

            if (_current.id == 0)
            {
                if (hallsInTours == null)
                {
                    MuseumEntities.GetContext().HallsInTourPrograms.Add(_current);
                }

                else
                {
                    MessageBox.Show("Такая запись уже существует!");
                    return;
                }
            }

            else
            {
                if (hallsInTours != null)
                {
                    MessageBox.Show("Такая запись уже существует!");
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

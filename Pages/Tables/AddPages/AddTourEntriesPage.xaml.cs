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
    public partial class AddTourEntriesPage : Page
    {
        private TourEntries _currentEntry = new TourEntries();
        private DateTime dateAndTime;
        public AddTourEntriesPage(TourEntries selected)
        {
            InitializeComponent();

            cmbGuide.ItemsSource = MuseumEntities.GetContext().Staff.ToList();
            cmbGuide.DisplayMemberPath = "FIOStaff";
            cmbTourProgram.ItemsSource = MuseumEntities.GetContext().TourPrograms.ToList();
            cmbTourProgram.DisplayMemberPath = "TourTheme";

            if (selected != null)
            {
                _currentEntry = selected;
                cmbGuide.SelectedItem = MuseumEntities.GetContext().TourPrograms.FirstOrDefault(x => x.id == _currentEntry.Guide);
                cmbTourProgram.SelectedItem = MuseumEntities.GetContext().TourPrograms.FirstOrDefault(x => x.id == _currentEntry.TourProgram);
                txtDate.SelectedDate = Convert.ToDateTime(_currentEntry.DateAndTime.ToString().Split(' ')[0]);
                txtTime.Text = _currentEntry.DateAndTime.ToString().Split(' ')[1];
            }

            else
            {
                cmbGuide.Text = "Обязательный";
                cmbTourProgram.Text = "Обязательный";
            }

            DataContext = _currentEntry;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtDate.Text))
                errors.AppendLine("Укажите дату!");
            if (string.IsNullOrWhiteSpace(txtTime.Text))
                try
                {
                    DateTime date = Convert.ToDateTime(txtDate.Text);
                    TimeSpan time = TimeSpan.Parse(txtTime.Text);
                    dateAndTime = date.Add(time);
                }
                catch (Exception)
                {
                    errors.AppendLine("Введите время в формате ЧЧ:ММ");
                }
            if (string.IsNullOrWhiteSpace(cmbTourProgram.Text) || cmbTourProgram.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсию!");
            if (string.IsNullOrWhiteSpace(cmbGuide.Text) || cmbGuide.Text == "Обязательный")
                errors.AppendLine("Укажите экскурсовода!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedGuide = (Staff)cmbGuide.SelectedItem;
            var selectedTour = (TourPrograms)cmbTourProgram.SelectedItem;

            int guideId = selectedGuide.id;
            int tourId = selectedTour.id;

            TourEntries tourEntrObject = new TourEntries
            {
                DateAndTime = dateAndTime,
                Guide = guideId,
                TourProgram = tourId
            };

            var entries = MuseumEntities.GetContext().TourEntries.AsNoTracking().FirstOrDefault(f => f.Guide == guideId && f.TourProgram == tourId && f.DateAndTime == dateAndTime);

            if (_currentEntry.id == 0)
            {
                if (entries == null)
                {
                    _currentEntry = tourEntrObject;
                    MuseumEntities.GetContext().TourEntries.Add(_currentEntry);
                }

                else
                {
                    MessageBox.Show("Такая запись уже существует!");
                    return;
                }
            }

            else
            {
                if (entries != null)
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

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

            cmbGuide.ItemsSource = MuseumEntities.GetContext().Staff.Select(x => x.FIOStaff).ToList();
            cmbTourProgram.ItemsSource = MuseumEntities.GetContext().TourPrograms.Select(x => x.TourTheme).ToList();

            if (selected != null)
            {
                _currentEntry = selected;
                txtDate.SelectedDate = Convert.ToDateTime(_currentEntry.DateAndTime.ToString().Split(' ')[0]);
                txtTime.Text = _currentEntry.DateAndTime.ToString().Split(' ')[1];
            }

            DataContext = _currentEntry;
            cmbTourProgram.ItemsSource = MuseumEntities.GetContext().TourPrograms.Select(x => x.TourTheme).ToList();
            cmbGuide.ItemsSource = MuseumEntities.GetContext().Staff.Where(y => y.Posts.Post == "Экскурсовод").Select(x => x.FIOStaff).ToList();

            if (cmbGuide.SelectedItem == null)
                cmbGuide.Text = "Обязательный";
            if (cmbTourProgram.SelectedItem == null)
                cmbTourProgram.Text = "Обязательный";
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

            int guide = MuseumEntities.GetContext().Staff.Where(x => x.FIOStaff == cmbGuide.Text).Select(x => x.id).First();
            int tour = MuseumEntities.GetContext().TourPrograms.Where(x => x.TourTheme == cmbTourProgram.Text).Select(x => x.id).First();
            TourEntries tourEntrObject = new TourEntries
            {
                DateAndTime = dateAndTime,
                Guide = guide,
                TourProgram = tour
            };

            var entries = MuseumEntities.GetContext().TourEntries.AsNoTracking().FirstOrDefault(f => f.Guide == guide && f.TourProgram == tour && f.DateAndTime == dateAndTime);

            if (_currentEntry.id == 0)
            {
                if (entries == null)
                {
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

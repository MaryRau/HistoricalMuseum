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
using System.Xml;
using System.Xml.Linq;

namespace HistoricalMuseum.Pages.AddToTables
{
    /// <summary>
    /// Interaction logic for AddAuthorPage.xaml
    /// </summary>
    public partial class AddExhInHallsPage : Page
    {
        private ExhibitsInHalls _current = new ExhibitsInHalls();
        public AddExhInHallsPage(ExhibitsInHalls selected)
        {
            InitializeComponent();

            var exhibitsInHallsIds = MuseumEntities.GetContext().ExhibitsInHalls.Select(x => x.Exhibit).ToList();
            var exhibitsInStoreIds = MuseumEntities.GetContext().ExhibitsInStoreroom.Select(x => x.Exhibit).ToList();

            int length = MuseumEntities.GetContext().Exhibits
                .Where(x => !exhibitsInHallsIds.Contains(x.id) && !exhibitsInStoreIds.Contains(x.id))
                .ToList().Count();

            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits
                .Where(x => !exhibitsInHallsIds.Contains(x.id) && !exhibitsInStoreIds.Contains(x.id))
                .ToList();
            cmbExh.DisplayMemberPath = "Exhibit";
            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.ToList();
            cmbHall.DisplayMemberPath = "Theme";

            if (length == 0)
            {
                cmbExh.Text = "Невозможно разместить ни один экспонат!";
                // настроить кнопку сохранения невидимой
            }


            if (selected != null)
            {
                _current = selected;
                cmbExh.SelectedItem = MuseumEntities.GetContext().Exhibits.FirstOrDefault(x => x.id == _current.Exhibit);
                cmbHall.SelectedItem = MuseumEntities.GetContext().Halls.FirstOrDefault(x => x.id == _current.Hall);
            }

            else
            {
                
                if (length == 0)
                    cmbExh.Text = "Невозможно разместить ни один экспонат!";
                else cmbExh.Text = "Обязательный";
                cmbHall.Text = "Обязательный";
            }

            DataContext = _current;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(cmbExh.Text) || cmbExh.Text == "Обязательный")
                errors.AppendLine("Укажите экспонат!");
            if (string.IsNullOrWhiteSpace(cmbHall.Text) || cmbHall.Text == "Обязательный")
                errors.AppendLine("Укажите зал!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var selectedExh = (Exhibits)cmbExh.SelectedItem;
            var selectedHall = (Halls)cmbHall.SelectedItem;

            int exhId = selectedExh.id;
            int hallId = selectedHall.id;

            ExhibitsInHalls exInHallsObject = new ExhibitsInHalls
            {
                Exhibit = exhId,
                Hall = hallId
            };

            var exhInHalls = MuseumEntities.GetContext().ExhibitsInHalls.AsNoTracking().FirstOrDefault(f => f.Exhibit == exhId && f.Hall == hallId);


            if (_current.id == 0)
            {
                if (exhInHalls == null)
                {
                    _current = exInHallsObject;
                    MuseumEntities.GetContext().ExhibitsInHalls.Add(_current);}

                else
                {
                    MessageBox.Show("Такая запись уже существует!");
                    return;
                }
            }

            else
            {
                if (exhInHalls != null)
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

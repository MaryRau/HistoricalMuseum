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

            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits.Select(x => x.Exhibit).ToList();
            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.Select(x => x.Theme).ToList();

            if (selected != null)
                _current = selected;

            DataContext = _current;
            cmbHall.ItemsSource = MuseumEntities.GetContext().Halls.Select(x => x.Theme).ToList();
            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits.Select(x => x.Exhibit).ToList();

            if (cmbExh.SelectedItem == null)
                cmbExh.Text = "Обязательный";
            if (cmbHall.SelectedItem == null)
                cmbHall.Text = "Обязательный";
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

            int exh = MuseumEntities.GetContext().Exhibits.Where(x => x.Exhibit == cmbExh.Text).Select(x => x.id).First();
            int hall = MuseumEntities.GetContext().Halls.Where(x => x.Theme == cmbHall.Text).Select(x => x.id).First();
            ExhibitsInHalls exInHallsObject = new ExhibitsInHalls
            {
                Exhibit = exh,
                Hall = hall
            };

            var exhInHalls = MuseumEntities.GetContext().ExhibitsInHalls.AsNoTracking().FirstOrDefault(f => f.Exhibit == exh && f.Hall == hall);


            if (_current.id == 0)
            {
                if (exhInHalls == null)
                {
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

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
    public partial class AddExhInStoreroomPage : Page
    {
        private ExhibitsInStoreroom _current = new ExhibitsInStoreroom();
        public AddExhInStoreroomPage(ExhibitsInStoreroom selected)
        {
            InitializeComponent();

            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits.Select(x => x.Exhibit).ToList();

            if (selected != null) 
                _current = selected;

            DataContext = _current;
            cmbExh.ItemsSource = MuseumEntities.GetContext().Exhibits.Select(x => x.Exhibit).ToList();

            if (cmbExh.SelectedItem == null)
                cmbExh.Text = "Обязательный";
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

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            int exh = MuseumEntities.GetContext().Exhibits.Where(x => x.Exhibit == cmbExh.Text).Select(x => x.id).First();
            ExhibitsInStoreroom exInStoreObject = new ExhibitsInStoreroom
            {
                Exhibit = exh,
            };

            var exhInHalls = MuseumEntities.GetContext().ExhibitsInStoreroom.AsNoTracking().FirstOrDefault(f => f.Exhibit == exh);

            if (_current.id == 0)
            {
                if (exhInHalls == null)
                {
                    MuseumEntities.GetContext().ExhibitsInStoreroom.Add(_current);
                }

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

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
    public partial class AddExhibitsPage : Page
    {
        private Exhibits _currentExh = new Exhibits();
        public AddExhibitsPage(Exhibits selected)
        {
            InitializeComponent();

            cmbType.ItemsSource = MuseumEntities.GetContext().ExhibitsTypes.Select(x => x.Type).ToList();
            cmbAuthor.ItemsSource = MuseumEntities.GetContext().Authors.Select(x => x.FIOAuthor).ToList();
            cmbEpoch.ItemsSource = MuseumEntities.GetContext().HistoricalEpochs.Select(x => x.Epoch).ToList();
            cmbCountry.ItemsSource = MuseumEntities.GetContext().Countries.Select(x => x.Country).ToList();

            if (selected != null)
            {
                _currentExh = selected;

                cmbType.SelectedItem = MuseumEntities.GetContext().ExhibitsTypes.Where(x => x.id == _currentExh.ExhibitType).Select(x => x.Type);
                cmbAuthor.SelectedItem = MuseumEntities.GetContext().Authors.Where(x => x.id == _currentExh.Author).Select(x => x.FIOAuthor);
                cmbEpoch.SelectedItem = MuseumEntities.GetContext().HistoricalEpochs.Where(x => x.id == _currentExh.HistoricalEpoch).Select(x => x.Epoch);
                cmbCountry.SelectedItem = MuseumEntities.GetContext().Countries.Where(x => x.id == _currentExh.CreationCountry).Select(x => x.Country);
            }

            if (cmbType.SelectedItem == null)
                cmbType.Text = "Обязательный";
            if (cmbAuthor.SelectedItem == null)
                cmbAuthor.Text = "Необязательный";
            if (cmbEpoch.SelectedItem == null)
                cmbEpoch.Text = "Обязательный";
            if (cmbCountry.SelectedItem == null)
                cmbCountry.Text = "Обязательный";


            DataContext = _currentExh;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtExh.Text))
                errors.AppendLine("Укажите название экспоната!");
            if (string.IsNullOrWhiteSpace(cmbType.Text) || cmbType.Text == "Обязательный")
                errors.AppendLine("Укажите тип экспоната!");
            if (string.IsNullOrWhiteSpace(cmbEpoch.Text) || cmbEpoch.Text == "Обязательный")
                errors.AppendLine("Укажите историческую эпоху!");
            if (string.IsNullOrWhiteSpace(cmbCountry.Text) || cmbCountry.Text == "Обязательный")
                errors.AppendLine("Укажите место создания!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            int type = MuseumEntities.GetContext().ExhibitsTypes.Where(x => x.Type == cmbType.SelectedItem.ToString()).Select(x => x.id).First();
            int? author = MuseumEntities.GetContext().Authors.Where(x => x.FIOAuthor == cmbAuthor.SelectedItem.ToString()).Select(x => x.id).FirstOrDefault();
            int epoch = MuseumEntities.GetContext().HistoricalEpochs.Where(x => x.Epoch == cmbEpoch.SelectedItem.ToString()).Select(x => x.id).First();
            int country = MuseumEntities.GetContext().Countries.Where(x => x.Country == cmbCountry.SelectedItem.ToString()).Select(x => x.id).First();

            Exhibits exhibitObject = new Exhibits
            {
                Exhibit = txtExh.Text,
                ExhibitType = type,
                Author = author,
                HistoricalEpoch = epoch,
                CreationCountry = country,
                Description = txtDiscription.Text ?? null
            };

            var exhibit = MuseumEntities.GetContext().Exhibits.AsNoTracking().FirstOrDefault(f => f.Exhibit.ToLower() == txtExh.Text.ToLower() && f.ExhibitType == type
            && f.Author == author && f.HistoricalEpoch == epoch && f.CreationCountry == country && f.Description == txtDiscription.Text);

            if (_currentExh.id == 0)
            {
                if (exhibit == null)
                {
                    MuseumEntities.GetContext().Exhibits.Add(_currentExh);
                }

                else
                {
                    MessageBox.Show("Такой экспонат уже существует!");
                    return;
                }
            }

            else
            {
                if (exhibit != null)
                {
                    MessageBox.Show("Такой экспонат уже существует!");
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

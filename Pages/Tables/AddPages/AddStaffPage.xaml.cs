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
    public partial class AddStaffPage : Page
    {
        private Staff _currentStaff = new Staff();
        public AddStaffPage(Staff selected)
        {
            InitializeComponent();

            cmbPost.ItemsSource = MuseumEntities.GetContext().Posts.Select(x => x.Post).ToList();

            if (selected != null) 
                _currentStaff = selected;

            DataContext = _currentStaff;
            cmbPost.ItemsSource = MuseumEntities.GetContext().Posts.Select(x => x.Post).ToList();

            if (cmbPost.SelectedItem == null)
                cmbPost.Text = "Обязательный";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtStaff.Text))
                errors.AppendLine("Укажите сотрудника!");
            if (string.IsNullOrWhiteSpace(cmbPost.Text) || cmbPost.Text == "Обязательный")
                errors.AppendLine("Укажите должность!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            int post = MuseumEntities.GetContext().Posts.Where(x => x.Post == cmbPost.Text).Select(x => x.id).First();
            Staff staffObject = new Staff
            {
                FIOStaff = txtStaff.Text,
                Post = post
            };

            var staff = MuseumEntities.GetContext().Staff.AsNoTracking().FirstOrDefault(f => f.FIOStaff == txtStaff.Text && f.Post == post);

            if (_currentStaff.id == 0)
            {
                if (staff == null)
                {
                    MuseumEntities.GetContext().Staff.Add(_currentStaff);
                }

                else
                {
                    MessageBox.Show("Такой сотрудник уже существует!");
                    return;
                }
            }

            else
            {
                if (staff != null)
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

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
    public partial class AddTypePage : Page
    {
        private ExhibitsTypes _currentType = new ExhibitsTypes();
        public AddTypePage( ExhibitsTypes selected)
        {
            InitializeComponent();

            if (selected != null ) 
                _currentType = selected;

            DataContext = _currentType;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtType.Text))
                errors.AppendLine("Укажите тип экспоната!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            ExhibitsTypes staffObject = new ExhibitsTypes
            {
                Type = txtType.Text
            };

            var types = MuseumEntities.GetContext().ExhibitsTypes.AsNoTracking().FirstOrDefault(f => f.Type == txtType.Text);

            if (_currentType.id == 0)
            {
                if (types == null)
                {
                    MuseumEntities.GetContext().ExhibitsTypes.Add(_currentType);
                }

                else
                {
                    MessageBox.Show("Такой тип уже существует!");
                    return;
                }
            }

            else
            {
                if (types != null)
                {
                    MessageBox.Show("Такой тип уже существует!");
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

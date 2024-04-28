﻿using System;
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

namespace HistoricalMuseum.Pages.Tables
{
    /// <summary>
    /// Interaction logic for GuidedToursPage.xaml
    /// </summary>
    public partial class PopularityOfProgramPage : Page
    {
        public PopularityOfProgramPage()
        {
            InitializeComponent();
            DataGridPopulOfTours.ItemsSource = MuseumEntities.GetContext().PopulariryPfProgram.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                MuseumEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridPopulOfTours.ItemsSource = MuseumEntities.GetContext().PopulariryPfProgram.ToList();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ProductivityTracker.Client.Interfaces;
using ProductivityTracker.Client.Models;

namespace ProductivityTracker.Client.Views
{
    /// <summary>
    /// Interaction logic for AddClientView.xaml
    /// </summary>
    public partial class AddClientView
    {
        private readonly IList<PositionModel> _itemsToRemoveFromLeft = new List<PositionModel>();
        private readonly IList<PositionModel> _itemsToRemoveFromRight = new List<PositionModel>();

        public AddClientView()
        {
            InitializeComponent();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            if (AllProductsList.SelectedItems.Count > 0)
            {
                foreach(var item in AllProductsList.SelectedItems.Cast<PositionModel>())
                {
                    _itemsToRemoveFromRight.Add(item);
                    Model.Positions.Add(item);
                }
            }

            foreach (var item in _itemsToRemoveFromRight)
            {
                Model.AllPositions.Remove(item);
            }
        }

        private void OnRemoveClick(object sender, RoutedEventArgs e)
        {
            if (SelectedProductsList.SelectedItems.Count > 0)
            {
                foreach (var item in SelectedProductsList.SelectedItems.Cast<PositionModel>())
                {
                    _itemsToRemoveFromLeft.Add(item);
                    Model.AllPositions.Add(item);
                }
            }

            foreach (var item in _itemsToRemoveFromLeft)
            {
                Model.Positions.Remove(item);
            }
        }

        public IAddClientViewModel Model
        {
            get { return (IAddClientViewModel) ((Grid)Content).DataContext; }
        }
    }
}

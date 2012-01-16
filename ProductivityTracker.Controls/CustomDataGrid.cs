using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProductivityTracker.Controls
{
    public class CustomDataGrid : DataGrid
    {
        public CustomDataGrid()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
                                                 {
                                                     Source = new Uri("/ProductivityTracker.Controls;component/Themes/CustomDataGridResources.xaml", UriKind.Relative)
                                                 });
            Background = new SolidColorBrush(Colors.LightGray);
            BorderThickness = new Thickness(1);
            BorderBrush = new SolidColorBrush(Colors.LightGray);
            AutoGenerateColumns = false;
            CanUserSortColumns = false;
            CanUserReorderColumns = false;
            CanUserAddRows = false;
            CanUserDeleteRows = false;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            HorizontalGridLinesBrush = new SolidColorBrush(Colors.LightGray);
            VerticalGridLinesBrush = new SolidColorBrush(Colors.Transparent);
        }
    }
}
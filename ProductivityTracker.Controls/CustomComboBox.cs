using System;
using System.Windows;
using System.Windows.Controls;

namespace ProductivityTracker.Controls
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
                                                 {
                                                     Source = new Uri("/ProductivityTracker.Controls;component/Themes/CustomComboBox.xaml", UriKind.Relative)
                                                 });
            Style = (Style)Resources["CustomComboBoxStyle"];
        }

        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register("SelectedItemTemplate", typeof(DataTemplate), typeof(CustomComboBox), new UIPropertyMetadata(null));
    }
}
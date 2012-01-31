using System;
using System.Windows;
using Visifire.Charts;

namespace ProductivityTracker.Analyse
{
    public class CustomChart : Chart
    {
        public DataSeriesCollection BindableSeries
        {
            get { return (DataSeriesCollection)GetValue(BindableSeriesProperty); }
            set { SetValue(BindableSeriesProperty, value); }
        }

        public static readonly DependencyProperty BindableSeriesProperty =
            DependencyProperty.Register("BindableSeries", typeof(DataSeriesCollection), typeof(CustomChart), new UIPropertyMetadata(OnBindableSeriesChanged));

        private static void OnBindableSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (CustomChart) d;
            if (e.NewValue != null)
            {
                source.Series.Clear();
                var items = (DataSeriesCollection) e.NewValue;
                foreach(var item in items) source.Series.Add(item);
            }
        }
    }
}
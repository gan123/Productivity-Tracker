using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ProductivityTracker
{
    [ContentProperty("Items")]
    public class TabNavigator : Control
    {
        private StackPanel _hostPanel;

        static TabNavigator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabNavigator), new FrameworkPropertyMetadata(typeof(TabNavigator)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _hostPanel = (StackPanel) GetTemplateChild("HostPanel");
        }

        public IEnumerable<TabNavigationItem> Items
        {
            get { return (IEnumerable<TabNavigationItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable<TabNavigationItem>), typeof(TabNavigator), new UIPropertyMetadata(OnItemsChanged));

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (TabNavigator) d;
            if (e.NewValue != null)
            {
                foreach(var item in (IEnumerable<TabNavigationItem>)e.NewValue)
                {
                    item.Style = (Style)Application.Current.Resources["TabNavigationItemStyle"];
                    item.Click += (s, ex) => source.OnItemClick((TabNavigationItem)s);
                    if (item.Parent == null)
                        source._hostPanel.Children.Add(item);
                }
            }
        }

        private void OnItemClick(TabNavigationItem clickedItem)
        {
            foreach(var item in Items)
            {
                item.IsChecked = false;
            }
            clickedItem.IsChecked = true;
        }
    }

    public class TabNavigationItem : ToggleButton
    {
    }
}

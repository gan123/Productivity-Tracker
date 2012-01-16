using System;
using System.Collections;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ProductivityTracker.Controls
{
    public abstract class CustomAutocompleteBox : Control
    {
        private TextBox _searchTextBox;
        private ListBox _searchListBox;
        private Popup _popup;
        private bool _isItemSelecting;

        static CustomAutocompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomAutocompleteBox), new FrameworkPropertyMetadata(typeof(CustomAutocompleteBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _searchTextBox = (TextBox)GetTemplateChild("SearchTextBox");
            Observable.FromEventPattern(_searchTextBox, "TextChanged")
                .Select(t => ((TextBox)t.Sender).Text)
                .Where(t => t.Length >= 3 && !_isItemSelecting)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .ObserveOnDispatcher()
                .Subscribe(BeginSearch);

            _searchListBox = (ListBox)GetTemplateChild("SearchListBox");
            Observable.FromEventPattern(_searchListBox, "SelectionChanged")
                .ObserveOnDispatcher()
                .SubscribeOnDispatcher()
                .Subscribe(s =>
                               {
                                   if (_searchListBox.SelectedIndex == -1) return;
                                   var selectedItem = _searchListBox.SelectedItem;
                                   _isItemSelecting = true;
                                   _searchTextBox.Text = selectedItem.ToString();
                                   VisualStateManager.GoToState(this, "Hide", true);
                                   _isItemSelecting = false;
                               });
            _popup = (Popup)GetTemplateChild("SearchPopup");
            LostFocus += OnDropDownFocusChanged;

            var button = (Button) GetTemplateChild("SearchButton");
            button.Click += (s, e) => BeginSearch(_searchTextBox.Text);

            KeyUp += (s, e) =>
                         {
                             if (e.Key == Key.Escape)
                             {
                                 _popup.IsOpen = false;
                                 SelectedItem = null;
                             }
                         };
        }

        private void BeginSearch(string query)
        {
            _searchTextBox.IsEnabled = false;
            VisualStateManager.GoToState(this, "Hide", true);
            Search(query);
        }

        public abstract void Search(string query);

        private void OnDropDownFocusChanged(object sender, EventArgs e)
        {
            FocusChanged(HasFocus());
        }

        private void FocusChanged(bool hasFocus)
        {
           if (hasFocus)
            {
                if (_searchTextBox != null && _searchTextBox.SelectionLength == 0)
                {
                    _searchTextBox.SelectAll();
                }
            }
            else
            {
                _popup.IsOpen = false;
                if (_searchTextBox != null)
                {
                    _searchTextBox.Select(_searchTextBox.Text.Length, 0);
                }
            }
        }

        protected bool HasFocus()
        {
            var focused = FocusManager.GetFocusedElement(this) as DependencyObject;
            while (focused != null)
            {
                if (ReferenceEquals(focused, this))
                {
                    return true;
                }

                // This helps deal with popups that may not be in the same 
                // visual tree
                var parent = VisualTreeHelper.GetParent(focused);
                if (parent == null)
                {
                    // Try the logical parent.
                    var element = focused as FrameworkElement;
                    if (element != null)
                    {
                        parent = element.Parent;
                    }
                }
                focused = parent;
            }
            return false;
        }

        public void PopulateComplete()
        {
            _searchTextBox.IsEnabled = true;
            SelectedItem = null;
            if (!_popup.IsOpen) _popup.IsOpen = true;
            VisualStateManager.GoToState(this, "Show", true);
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomAutocompleteBox), new UIPropertyMetadata(OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                ((CustomAutocompleteBox) d)._searchTextBox.Text = string.Empty;
        }

        public IEnumerable SearchResults
        {
            get { return (IEnumerable)GetValue(SearchResultsProperty); }
            set { SetValue(SearchResultsProperty, value); }
        }

        public static readonly DependencyProperty SearchResultsProperty =
            DependencyProperty.Register("SearchResults", typeof(IEnumerable), typeof(CustomAutocompleteBox), new UIPropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CustomAutocompleteBox), new UIPropertyMetadata(null));

        public double DropDownWidth
        {
            get { return (double)GetValue(DropDownWidthProperty); }
            set { SetValue(DropDownWidthProperty, value); }
        }

        public static readonly DependencyProperty DropDownWidthProperty =
            DependencyProperty.Register("DropDownWidth", typeof(double), typeof(CustomAutocompleteBox), new UIPropertyMetadata(300.0));
    }
}

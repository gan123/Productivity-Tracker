using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProductivityTracker.Controls
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog
    {
        public Dialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Dialog(bool allowCancel, DialogType dialogType) : this()
        {
            CancelButton.Visibility = allowCancel
                                          ? System.Windows.Visibility.Visible
                                          : System.Windows.Visibility.Collapsed;
            SetImage(dialogType);
        }

        public Action<bool> CloseAction { get; set; }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(Dialog), new UIPropertyMetadata(null));

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SetImage(DialogType dialogType)
        {
            switch (dialogType)
            {
                case DialogType.Warning :
                    ActionImage.Source = new BitmapImage(new Uri("/ProductivityTracker.Controls;component/Icons/warning.png",
                                                 UriKind.Relative));
                    break;
                case DialogType.Error:
                    ActionImage.Source = new BitmapImage(new Uri("/ProductivityTracker.Controls;component/Icons/error.png",
                                                 UriKind.Relative));
                    break;
                case DialogType.Question:
                    ActionImage.Source = new BitmapImage(new Uri("/ProductivityTracker.Controls;component/Icons/question.png",
                                                 UriKind.Relative));
                    break;
                case DialogType.Information:
                    ActionImage.Source = new BitmapImage(new Uri("/ProductivityTracker.Controls;component/Icons/info.png",
                                                 UriKind.Relative));
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace ProductivityTracker.Ui.Common
{
    /// <summary>
    /// Interaction logic for NotificationChildWindow.xaml
    /// </summary>
    public partial class NotificationChildWindow
    {
        public NotificationChildWindow()
        {
            InitializeComponent();
        }

        ///<summary>
        /// The <see cref="DataTemplate"/> to apply when displaying <see cref="Notification"/> data.
        ///</summary>
        public static readonly DependencyProperty NotificationTemplateProperty =
            DependencyProperty.Register(
                "NotificationTemplate",
                typeof(DataTemplate),
                typeof(NotificationChildWindow),
                new PropertyMetadata(null));

        
        ///<summary>
        /// The <see cref="DataTemplate"/> to apply when displaying <see cref="Notification"/> data.
        ///</summary>
        public DataTemplate NotificationTemplate
        {
            get { return (DataTemplate)GetValue(NotificationTemplateProperty); }
            set { SetValue(NotificationTemplateProperty, value); }
        }
    }
}

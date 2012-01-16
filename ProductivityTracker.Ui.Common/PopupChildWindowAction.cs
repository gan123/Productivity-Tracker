using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Windows.Controls;

namespace ProductivityTracker.Ui.Common
{
    public class PopupChildWindowAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var childWindow = ((ResponseNotification)args.Context).ChildWindow;
            childWindow.DataContext = args.Context;
            Parent.Children.Add(childWindow);
            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                    {
                        childWindow.Closed -= handler;
                        Parent.Children.Remove(childWindow);
                        Dispatcher.BeginInvoke(new Action(callback));
                    };
            childWindow.Closed += handler;
            childWindow.Show();
        }

        /// <summary>
        /// The child window to display as part of the popup.
        /// </summary>
        public static readonly DependencyProperty ChildWindowProperty =
            DependencyProperty.Register(
                "ChildWindow",
                typeof(ChildWindow),
                typeof(PopupChildWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// The <see cref="DataTemplate"/> to apply to the popup content.
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(PopupChildWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the child window to pop up.
        /// </summary>
        /// <remarks>
        /// If not specified, a default child window is used instead.
        /// </remarks>
        public ChildWindow ChildWindow
        {
            get { return (ChildWindow)GetValue(ChildWindowProperty); }
            set { SetValue(ChildWindowProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content template for a default child window.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public Grid Parent
        {
            get { return (Grid)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }

        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register("Parent", typeof(Grid), typeof(PopupChildWindowAction), new UIPropertyMetadata(null));
        
        /// <summary>
        /// Returns the child window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The notification to display in the child window.</param>
        /// <returns></returns>
        protected ChildWindow GetChildWindow(Notification notification)
        {
            var childWindow = this.ChildWindow ?? this.CreateDefaultWindow(notification);
            childWindow.DataContext = notification;

            return childWindow;
        }

        private ChildWindow CreateDefaultWindow(Notification notification)
        {
            return new NotificationChildWindow { NotificationTemplate = this.ContentTemplate };
        }
    }
}
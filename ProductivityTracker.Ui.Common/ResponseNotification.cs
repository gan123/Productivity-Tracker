using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Windows.Controls;

namespace ProductivityTracker.Ui.Common
{
    public class ResponseNotification : Notification
    {
        public bool? Result
        {
            get { return ((ChildWindowViewModelBase) Content).Result; }
        }

        public ChildWindow ChildWindow { get; set; }
    }
}
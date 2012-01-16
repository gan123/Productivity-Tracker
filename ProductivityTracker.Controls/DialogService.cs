using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ProductivityTracker.Controls
{
    [Export(typeof(IDialogService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DialogService : IDialogService
    {
        public void Show(string caption, string message, DialogType type, bool showOk, bool showCancel, Action<bool> closeAction)
        {
            var dialog = new Dialog(showCancel, type) {Caption = caption, Message = message, CloseAction = closeAction};
            dialog.Closed += new EventHandler(OnDialogClosed);
            ParentGrid.Children.Add(dialog);
            dialog.Show();
        }

        void OnDialogClosed(object sender, EventArgs e)
        {
            var dialog = sender as Dialog;
            if (dialog != null)
            {
                dialog.Closed -= OnDialogClosed;
                dialog.CloseAction(dialog.DialogResult == true);
                ParentGrid.Children.Remove(dialog);
            }
        }

        public void Show(string caption, string message, DialogType type, bool showOk, bool showCancel)
        {
            var dialog = new Dialog(showCancel, type) { Caption = caption, Message = message };
            ParentGrid.Children.Add(dialog);
            dialog.Show();
        }

        public Grid ParentGrid { get; set; }
    }
}
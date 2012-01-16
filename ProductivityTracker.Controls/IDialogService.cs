using System;
using System.Windows.Controls;

namespace ProductivityTracker.Controls
{
    public interface IDialogService
    {
        void Show(string caption, string message, DialogType type, bool showOk, bool showCancel, Action<bool> closeAction);
        void Show(string caption, string message, DialogType type, bool showOk, bool showCancel);
        Grid ParentGrid { get; set; }
    }

    public enum DialogType
    {
        Error,
        Question,
        Warning,
        Information
    }
}
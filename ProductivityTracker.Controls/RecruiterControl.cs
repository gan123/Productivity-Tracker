using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductivityTracker.Controls
{
    public class RecruiterControl : Control
    {
        private IEditableObject _context;
        private ContentControl _hostContent;

        static RecruiterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RecruiterControl), new FrameworkPropertyMetadata(typeof(RecruiterControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DataContext is IEditableObject)
                _context = (IEditableObject)DataContext;

            var edit = (Button)GetTemplateChild("EditButton");
            edit.Click += (s, e) =>
                              {
                                  if (_context != null) _context.BeginEdit();
                                  _hostContent.ContentTemplate = EditTemplate;
                                  VisualStateManager.GoToState(this, "Edit", true);
                              };

            var cancelEdit = (Button)GetTemplateChild("CancelButton");
            cancelEdit.Click += (s, e) =>
                                    {
                                        if (_context != null) _context.CancelEdit();
                                        _hostContent.ContentTemplate = ReadOnlyTemplate;
                                        VisualStateManager.GoToState(this, "ReadOnly", true);
                                    };

            var saveButton = (Button)GetTemplateChild("SaveButton");
            saveButton.Click += (s, e) =>
                                    {
                                        if (_context != null)
                                        {
                                            if (_context is ICanSave)
                                            {
                                                if (((ICanSave) _context).CanSave())
                                                {
                                                    _context.EndEdit();
                                                    _hostContent.ContentTemplate = ReadOnlyTemplate;
                                                    VisualStateManager.GoToState(this, "ReadOnly", true);
                                                }
                                            }
                                        }
                                    };

            _hostContent = (ContentControl)GetTemplateChild("HostContent");
            _hostContent.Content = DataContext;
            _hostContent.ContentTemplate = ReadOnlyTemplate;
        }

        public bool IsAdmin
        {
            get { return (bool)GetValue(IsAdminProperty); }
            set { SetValue(IsAdminProperty, value); }
        }

        public static readonly DependencyProperty IsAdminProperty =
            DependencyProperty.Register("IsAdmin", typeof(bool), typeof(RecruiterControl), new UIPropertyMetadata(false));

        public DataTemplate EditTemplate
        {
            get { return (DataTemplate)GetValue(EditTemplateProperty); }
            set { SetValue(EditTemplateProperty, value); }
        }

        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(RecruiterControl), new UIPropertyMetadata(null));

        public DataTemplate ReadOnlyTemplate
        {
            get { return (DataTemplate)GetValue(ReadOnlyTemplateProperty); }
            set { SetValue(ReadOnlyTemplateProperty, value); }
        }

        public static readonly DependencyProperty ReadOnlyTemplateProperty =
            DependencyProperty.Register("ReadOnlyTemplate", typeof(DataTemplate), typeof(RecruiterControl), new UIPropertyMetadata(null));

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(RecruiterControl), new UIPropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(RecruiterControl), new UIPropertyMetadata(null));

        public ICommand MaximizeCommand
        {
            get { return (ICommand)GetValue(MaximizeCommandProperty); }
            set { SetValue(MaximizeCommandProperty, value); }
        }

        public static readonly DependencyProperty MaximizeCommandProperty =
            DependencyProperty.Register("MaximizeCommand", typeof(ICommand), typeof(RecruiterControl), new UIPropertyMetadata(null));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(RecruiterControl), new UIPropertyMetadata(false));
    }

    public interface ICanSave
    {
        bool CanSave();
    }
}

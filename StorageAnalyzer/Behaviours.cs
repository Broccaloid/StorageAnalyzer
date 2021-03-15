using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StorageAnalyzer
{
    public static class Behaviours
    {
        #region ExpandingBehaviour (Attached DependencyProperty)
        public static readonly DependencyProperty ExpandingBehaviourProperty =
            DependencyProperty.RegisterAttached("ExpandingBehaviour", typeof(ICommand), typeof(Behaviours),
                new PropertyMetadata(OnExpandingBehaviourChanged));
        public static void SetExpandingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(ExpandingBehaviourProperty, value);
        }
        public static ICommand GetExpandingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(ExpandingBehaviourProperty);
        }
        private static void OnExpandingBehaviourChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem item = o as TreeViewItem;
            if (item != null)
            {
                if (e.NewValue is ICommand command)
                {
                    item.Expanded += (s, a) =>
                    {
                        if (command.CanExecute(a))
                        {
                            command.Execute(a);
                        }
                        a.Handled = true;
                    };
                }
            }
        }
        #endregion
    }
}

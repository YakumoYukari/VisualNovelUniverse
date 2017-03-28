using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Visual_Novel_Universe.ThirdParty
{
    public class ScrollIntoViewBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        private static void AssociatedObject_SelectionChanged(object Sender, SelectionChangedEventArgs EventArgs)
        {
            var ListBox = Sender as ListBox;
            if (ListBox?.SelectedItem != null)
            {
                ListBox.Dispatcher.BeginInvoke(
                    (Action) (() =>
                    {
                        ListBox.UpdateLayout();
                        if (ListBox.SelectedItem !=
                            null)
                            ListBox.ScrollIntoView(
                                ListBox.SelectedItem);
                    }));
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -=
                AssociatedObject_SelectionChanged;
        }
    }
}

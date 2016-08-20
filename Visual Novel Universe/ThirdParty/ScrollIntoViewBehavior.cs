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

        private static void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox?.SelectedItem != null)
            {
                listBox.Dispatcher.BeginInvoke(
                    (Action) (() =>
                    {
                        listBox.UpdateLayout();
                        if (listBox.SelectedItem !=
                            null)
                            listBox.ScrollIntoView(
                                listBox.SelectedItem);
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

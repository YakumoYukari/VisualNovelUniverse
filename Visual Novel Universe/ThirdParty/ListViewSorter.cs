using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Visual_Novel_Universe.ThirdParty
{
    public class GridViewSort
    {
        #region Attached properties

        public static ICommand GetCommand(DependencyObject Obj)
        {
            return (ICommand)Obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject Obj, ICommand Value)
        {
            Obj.SetValue(CommandProperty, Value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(GridViewSort),
                new UIPropertyMetadata(
                    null,
                    (O, E) =>
                    {
                        var ListView = O as ItemsControl;

                        if (ListView == null) return;
                        if (GetAutoSort(ListView)) return;

                        if (E.OldValue != null && E.NewValue == null)
                        {
                            ListView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                        }
                        if (E.OldValue == null && E.NewValue != null)
                        {
                            ListView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                        }
                    }
                )
            );

        public static bool GetAutoSort(DependencyObject Obj)
        {
            return (bool)Obj.GetValue(AutoSortProperty);
        }

        public static void SetAutoSort(DependencyObject Obj, bool Value)
        {
            Obj.SetValue(AutoSortProperty, Value);
        }

        // Using a DependencyProperty as the backing store for AutoSort.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoSortProperty =
            DependencyProperty.RegisterAttached(
                "AutoSort",
                typeof(bool),
                typeof(GridViewSort),
                new UIPropertyMetadata(
                    false,
                    (O, E) =>
                    {
                        var ListView = O as ListView;

                        if (ListView == null) return;
                        if (GetCommand(ListView) != null) return;

                        bool OldValue = (bool)E.OldValue;
                        bool NewValue = (bool)E.NewValue;
                        if (OldValue && !NewValue)
                        {
                            ListView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                        }
                        if (!OldValue && NewValue)
                        {
                            ListView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                        }
                    }
                )
            );

        public static string GetPropertyName(DependencyObject Obj)
        {
            return (string)Obj.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(DependencyObject Obj, string Value)
        {
            Obj.SetValue(PropertyNameProperty, Value);
        }

        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached(
                "PropertyName",
                typeof(string),
                typeof(GridViewSort),
                new UIPropertyMetadata(null)
            );

        #endregion

        #region Column header click event handler

        private static void ColumnHeader_Click(object Sender, RoutedEventArgs E)
        {
            var HeaderClicked = E.OriginalSource as GridViewColumnHeader;
            if (HeaderClicked == null) return;

            string PropertyName = GetPropertyName(HeaderClicked.Column);
            if (string.IsNullOrEmpty(PropertyName)) return;

            var ListView = GetAncestor<ListView>(HeaderClicked);
            if (ListView == null) return;

            var command = GetCommand(ListView);
            if (command != null)
            {
                if (command.CanExecute(PropertyName))
                {
                    command.Execute(PropertyName);
                }
            }
            else if (GetAutoSort(ListView))
            {
                ApplySort(ListView.Items, PropertyName);
            }
        }

        #endregion

        #region Helper methods

        public static T GetAncestor<T>(DependencyObject Reference) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(Reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (T)parent;
        }

        public static void ApplySort(ICollectionView View, string PropertyName)
        {
            var direction = ListSortDirection.Descending;
            if (View.SortDescriptions.Count > 0)
            {
                var CurrentSort = View.SortDescriptions[0];
                if (CurrentSort.PropertyName == PropertyName)
                {
                    direction = CurrentSort.Direction == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                }
                View.SortDescriptions.Clear();
            }
            if (!string.IsNullOrEmpty(PropertyName))
            {
                View.SortDescriptions.Add(new SortDescription(PropertyName, direction));
            }
        }

        #endregion
    }
}
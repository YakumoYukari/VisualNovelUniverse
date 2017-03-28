using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Visual_Novel_Universe.ThirdParty
{
    public class GridViewColumnVisibilityManager
    {
        private static readonly Dictionary<GridViewColumn, double> OriginalColumnWidths = new Dictionary<GridViewColumn, double>();

        public static bool GetIsVisible(DependencyObject Obj)
        {
            return (bool)Obj.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject Obj, bool Value)
        {
            Obj.SetValue(IsVisibleProperty, Value);
        }

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(GridViewColumnVisibilityManager), new UIPropertyMetadata(true, OnIsVisibleChanged));

        private static void OnIsVisibleChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {
            var gc = D as GridViewColumn;
            if (gc == null)
                return;

            if (GetIsVisible(gc) == false)
            {
                OriginalColumnWidths[gc] = gc.Width;
                gc.Width = 0;
            }
            else
            {
                if (Math.Abs(gc.Width) < 0.001)
                    gc.Width = OriginalColumnWidths[gc];
            }
        }
    }
}

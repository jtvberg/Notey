namespace Notey
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    public static class TreeHelpers
    {
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
            { return null; }

            T parent = parentObject as T;
            if (parent != null)
            { return parent; }
            else
            { return FindParent<T>(parentObject); }
        }

        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            DependencyObject childObject = VisualTreeHelper.GetChild(parent, 0);
            if (childObject == null)
            { return null; }

            T child = childObject as T;
            if (child != null)
            { return child; }
            else
            { return FindChild<T>(childObject); }
        }

        public static void ShallowConvert<T, U>(this T parent, U child)
        {
            foreach (PropertyInfo property in parent.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(parent, null));
                }
            }
        }
    }
}

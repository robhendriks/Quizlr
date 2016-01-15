using System.Windows;

namespace Quizlr.Lib.Utility
{
    public static class WindowHelper
    {
        public static T Show<T>() where T : Window, new()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (!(window is T)) continue;
                window.Focus();
                return (T) window;
            }
            var instance = new T();
            instance.Show();
            return instance;
        }
    }
}
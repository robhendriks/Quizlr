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
                window.Show();
                return (T) window;
            }
            var instance = new T();
            instance.Show();
            return instance;
        }

        public static T Hide<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (!(window is T)) continue;
                window.Hide();
                return (T) window;
            }
            return null;
        }

        public static T Switch<T>() where T : Window, new()
        {
            foreach (Window window in Application.Current.Windows)
                window.Hide();
            return Show<T>();
        }

        public static T2 Switch<T1, T2>()
            where T1 : Window
            where T2 : Window, new()
        {
            Hide<T1>();
            return Show<T2>();
        }
    }
}
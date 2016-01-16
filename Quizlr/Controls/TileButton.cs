using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Quizlr.Lib.Utility;
using Quizlr.Utility;

namespace Quizlr.Controls
{
    public class TileButton : Button
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource),
            typeof(TileButton), new PropertyMetadata());

        public static readonly DependencyProperty TintProperty = DependencyProperty.Register("Tint", typeof (Color),
            typeof (TileButton), new PropertyMetadata(Colors.CornflowerBlue, TintPropertyChanged));

        public static readonly DependencyProperty LightTintProperty = DependencyProperty.Register("LightTint",
            typeof (Color), typeof (TileButton), new PropertyMetadata());

        public static readonly DependencyProperty DarkTintProperty = DependencyProperty.Register("DarkTint",
            typeof (Color), typeof (TileButton), new PropertyMetadata());

        static TileButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (TileButton),
                new FrameworkPropertyMetadata(typeof (TileButton)));
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public Color Tint
        {
            get { return (Color) GetValue(TintProperty); }
            set { SetValue(TintProperty, value); }
        }

        public Color LightTint
        {
            get { return (Color) GetValue(LightTintProperty); }
            set { SetValue(LightTintProperty, value); }
        }

        public Color DarkTint
        {
            get { return (Color) GetValue(DarkTintProperty); }
            set { SetValue(DarkTintProperty, value); }
        }

        private static void TintPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var value = obj?.GetValue(TintProperty);
            if (!(value is Color)) return;
            var tint = (Color) value;
            obj.SetCurrentValue(LightTintProperty, tint.Lighten(0.1f));
            obj.SetCurrentValue(DarkTintProperty, tint.Darken(0.1f));
        }
    }
}
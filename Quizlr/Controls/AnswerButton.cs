using System.Windows;
using System.Windows.Controls;

namespace Quizlr.Controls
{
    public class AnswerButton : Button
    {
        static AnswerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (AnswerButton),
                new FrameworkPropertyMetadata(typeof (AnswerButton)));
        }
    }
}
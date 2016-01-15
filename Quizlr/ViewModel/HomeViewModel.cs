using System;
using GalaSoft.MvvmLight.Command;
using Quizlr.Lib.Utility;
using Quizlr.View;

namespace Quizlr.ViewModel
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Initialize();
        }

        public RelayCommand OpenQuizzesCommand { get; set; }
        public RelayCommand OpenQuestionsCommand { get; set; }

        private void Initialize()
        {
            OpenQuizzesCommand = new RelayCommand(OpenQuizzes);
            OpenQuestionsCommand = new RelayCommand(OpenQuestions);
        }

        private void OpenQuizzes()
        {
            WindowHelper.Show<QuizCrudWindow>();
        }

        private void OpenQuestions()
        {
            WindowHelper.Show<QuestionCrudWindow>();
        }
    }
}
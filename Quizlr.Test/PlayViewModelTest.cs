using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;
using Quizlr.Test.Utility;
using Quizlr.ViewModel;

namespace Quizlr.Test
{
    [TestClass]
    public class PlayViewModelTest
    {
        private Mock<IAnswerInstanceRepository> _answerMock;
        private Mock<IViewModelLocator> _locatorMock;
        private Mock<IQuestionInstanceRepository> _questionMock;
        private int _quizIndex = 1, _questionIndex = 1;
        private Mock<IQuizInstanceRepository> _quizMock;

        [TestInitialize]
        public void Initialize()
        {
            // Setup Locator Mock
            _locatorMock = new Mock<IViewModelLocator>();
            _locatorMock.Setup(l => l.Get<object>()).Returns(() => null);

            // Setup Quiz Mock
            _quizMock = new Mock<IQuizInstanceRepository>();
            _quizMock.Setup(q => q.CreateInstance(It.IsAny<QuizInstance>()))
                .Returns((QuizInstance instance) =>
                {
                    instance.QuizInstanceId = _quizIndex++;
                    return instance;
                });

            // Setup Question Mock
            _questionMock = new Mock<IQuestionInstanceRepository>();
            _questionMock.Setup(q => q.CreateInstance(It.IsAny<QuestionInstance>()))
                .Returns((QuestionInstance instance) =>
                {
                    instance.QuestionInstanceId = _questionIndex++;
                    return instance;
                });

            // Setup Answer Mock
            _answerMock = new Mock<IAnswerInstanceRepository>();
        }

        [TestMethod]
        public void PlayTest()
        {
            int answerCount = 0;
            var quiz = new QuizViewModel(QuizGenerator.Generate());
            var vm = new PlayViewModel(_locatorMock.Object, _quizMock.Object, _questionMock.Object, _answerMock.Object)
            {
                CurrentQuiz = quiz
            };

            // Verify that the quiz instance has been saved
            _quizMock.Verify(q => q.CreateInstance(It.IsAny<QuizInstance>()), Times.Exactly(1));

            // Iterate available questions
            foreach (var quizQuestion in quiz.QuizQuestions)
            {
                var question = quizQuestion.Question;
                answerCount += question.Answers.Count;

                Assert.AreEqual(question.QuestionId, vm.CurrentQuestion.QuestionId, $"QuestionID should be \"{question.QuestionId}\"");

                vm.SelectedAnswer = new AnswerViewModel(question.Answers.FirstOrDefault());
                vm.NextCommand.Execute(null);
            }

            // Verify that the quiz has ended
            Assert.AreEqual(true, vm.IsComplete, "Quiz should have ended by now");

            // Verify that all the questions have been saved
            _questionMock.Verify(q => q.CreateInstance(It.IsAny<QuestionInstance>()), Times.Exactly(quiz.QuizQuestionCount));

            // Verify that all the answers have been saved
            _answerMock.Verify(a => a.CreateInstance(It.IsAny<AnswerInstance>()), Times.Exactly(answerCount));
        }
    }
}
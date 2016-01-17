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
            var answerCount = 0;
            var vm = new PlayViewModel(_locatorMock.Object, _quizMock.Object, _questionMock.Object, _answerMock.Object)
            {
                CurrentQuiz = new QuizViewModel(QuizGenerator.Generate())
            };

            // Verify that the CreateInstance method was called once
            _quizMock.Verify(q => q.CreateInstance(It.IsAny<QuizInstance>()), Times.Exactly(1));

            // Verify that the ID of the current question is '1'
            Assert.AreEqual(1, vm.CurrentQuestion.QuestionId, "First question should be the current question");

            // Select answer, navigate to the next question
            answerCount += vm.CurrentQuestion.AnswerCount;
            vm.SelectedAnswer = new AnswerViewModel(vm.CurrentQuestion.Answers.ElementAt(0));
            vm.NextCommand.Execute(null);

            // Verify that the ID of the current question is '2'
            Assert.AreEqual(2, vm.CurrentQuestion.QuestionId, "Second question should be the current question");

            // Select answer, navigate to the next question
            answerCount += vm.CurrentQuestion.AnswerCount;
            vm.SelectedAnswer = new AnswerViewModel(vm.CurrentQuestion.Answers.ElementAt(0));
            vm.NextCommand.Execute(null);

            // Verify that the quiz has been comleted
            Assert.AreEqual(true, vm.IsComplete, "Quiz should be completed.");

            // Verify that exactly $answerCount answers have been saved
            _answerMock.Verify(a => a.CreateInstance(It.IsAny<AnswerInstance>()), Times.Exactly(answerCount));
        }
    }
}
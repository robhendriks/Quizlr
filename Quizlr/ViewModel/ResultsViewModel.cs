using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Repository;

namespace Quizlr.ViewModel
{
    public class ResultsViewModel : ViewModelBase
    {
        private static readonly Uri
            NotFoundLocation = new Uri("pack://application:,,,/View/NoResultPage.xaml"),
            FoundLocation = new Uri("pack://application:,,,/View/ResultPage.xaml");

        private readonly IQuizInstanceRepository _quizInstanceRepository;
        private Uri _location;

        private QuizInstanceViewModel _selectedQuizInstance;

        public ResultsViewModel(IQuizInstanceRepository quizInstanceRepository)
        {
            if (quizInstanceRepository == null)
                throw new ArgumentNullException(nameof(quizInstanceRepository));
            _quizInstanceRepository = quizInstanceRepository;
            Initialize();
        }

        public ObservableCollection<QuizInstanceViewModel> QuizInstances { get; set; }

        public RelayCommand DeleteQuizInstanceCommand { get; set; }

        public QuizInstanceViewModel SelectedQuizInstance
        {
            get { return _selectedQuizInstance; }
            set
            {
                _selectedQuizInstance = value;
                RaisePropertyChanged(() => SelectedQuizInstance);
                Invalidate();
                Switch();
            }
        }

        public Uri Location
        {
            get { return _location; }
            set
            {
                _location = value ?? NotFoundLocation;
                RaisePropertyChanged(() => Location);
            }
        }

        private void Initialize()
        {
            Location = null;
            DeleteQuizInstanceCommand = new RelayCommand(DeleteQuizInstance, CanDeleteQuizInstance);
            Populate();
        }

        private void Invalidate()
        {
            DeleteQuizInstanceCommand.RaiseCanExecuteChanged();
        }

        private bool CanDeleteQuizInstance()
        {
            return SelectedQuizInstance != null;
        }

        private void DeleteQuizInstance()
        {
            if (SelectedQuizInstance == null) return;
            _quizInstanceRepository.DeleteInstance(SelectedQuizInstance);
            QuizInstances.Remove(SelectedQuizInstance);
            SelectedQuizInstance = null;
        }

        private void Populate()
        {
            var quizInstances = _quizInstanceRepository.GetInstances()
                .Where(q => q.Completed != null)
                .Where(q => q.QuestionInstances.Count != 0)
                .OrderByDescending(q => q.Completed)
                .Select(qi => new QuizInstanceViewModel(qi));
            QuizInstances = new ObservableCollection<QuizInstanceViewModel>(quizInstances);
        }

        private void Switch()
        {
            if (SelectedQuizInstance == null)
            {
                Location = null;
                return;
            }
            var vm = NinjectServiceLocator.GetInstance<ResultViewModel>();
            vm.QuizInstance = _selectedQuizInstance;
            Location = FoundLocation;
        }
    }
}
using SubnettingCalculator.ViewModels.Core;

namespace SubnettingCalculator.ViewModels
{
    public class MainViewModel: ObservableObject
    {
        public RelayCommand IpCalculatorViewCommand { get; set; }
        public RelayCommand SubnettingViewCommand { get; set; }                
        public IPCalculatorViewModel IPCalculatorViewModel { get; set; }
        public SubnettingViewModel SubnettingViewModel { get; set; }
        
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set 
            { 
                currentView = value;
                OnPropertyChanged();
            }
        }

        
        public MainViewModel()
        {
            IPCalculatorViewModel = new IPCalculatorViewModel();
            SubnettingViewModel = new SubnettingViewModel();

            currentView = IPCalculatorViewModel;

            IpCalculatorViewCommand = new RelayCommand(x =>
            {
                currentView = IPCalculatorViewModel;
            });

            SubnettingViewCommand = new RelayCommand(x =>
            {
                currentView = SubnettingViewModel;
            });
        } 
    }
}
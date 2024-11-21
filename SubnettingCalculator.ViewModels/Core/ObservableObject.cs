using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SubnettingCalculator.ViewModels.Core
{
    //macht updates
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

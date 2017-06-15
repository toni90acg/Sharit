using Sharit.ViewModels.Base;

namespace Sharit.ViewModels.Modals
{
    public class SharitModalViewModel : ViewModelBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public SharitModalViewModel()
        {
            IsLoading = true;
        }
    }
}

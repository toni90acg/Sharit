using Sharit.Models;
using Sharit.Services;
using Sharit.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sharit.ViewModels
{
    public class SharitListViewModel : ViewModelBase
    {
        public SharitListViewModel()
        {
            //IsBusy = true;
            //LoadSharitItemsAsync();
        }

        private ObservableCollection<SharitItem> _items;
        public ObservableCollection<SharitItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        private SharitItem _selectedItem;
        public SharitItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if(_selectedItem != null)
                {
                    NavigationService.Instance.NavigateTo<SharitDetailViewModel>(_selectedItem);
                    _selectedItem = null;
                }


                OnPropertyChanged("SelectedItem");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshAsync());
        public ICommand NewCommand => new Command(New);
        public ICommand SearchCommand => new Command(Search);
        
        public override async void OnAppearing(object navigationContext)
        {
            _selectedItem = null;
            base.OnAppearing(navigationContext);

            if(Items == null)
            {
                await LoadSharitItemsAsync();
            }
        }

        private async Task RefreshAsync()
        {
            Items = null;
            await LoadSharitItemsAsync();
        }

        private void New()
        {
            //await LoadSharitItemsAsync();
            NavigationService.Instance.NavigateTo<AddSharitItemViewModel>();
        }

        private async Task LoadSharitItemsAsync()
        {
            IsBusy = true;

            var result = await ClientHttpService.Instance.GetItems<SharitItem>();

            if (result != null)
            {
                Items = new ObservableCollection<SharitItem>(result);
            }

            IsBusy = false;
        }

        private void Search()
        {
            // await LoadSharitItemsAsync();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new AddSharitItemView(null));
            NavigationService.Instance.NavigateTo<SearchSharitViewModel>(this);
        }
    }
}

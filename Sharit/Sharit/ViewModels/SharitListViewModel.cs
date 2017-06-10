﻿using Sharit.Models;
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
            IsBusy = true;
            LoadSharitItemsAsync();
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

                NavigationService.Instance.NavigateTo<SharitDetailViewModel>(_selectedItem);
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

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            await LoadSharitItemsAsync();
        }

        private async Task RefreshAsync()
        {
            await LoadSharitItemsAsync();
        }

        private async void New()
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
    }
}

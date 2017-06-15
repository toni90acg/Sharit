using Sharit.Models;
using Sharit.Services;
using Sharit.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sharit.ViewModels
{
    public class SearchSharitViewModel : ViewModelBase
    {
        private SharitListViewModel _context;
        public ICommand SearchCommand => new Command(Search);

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

        private string _textToSearch;
        public string TextToSearch
        {
            get { return _textToSearch; }
            set
            {
                _textToSearch = value;
                OnPropertyChanged("TextToSearch");
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var context = navigationContext as SharitListViewModel;

            if (context != null)
            {
                _context = context;
                Items = context.Items;
            }
        }

        private void Search()
        {
            if(Items != null && !string.IsNullOrEmpty(TextToSearch))
            {
                // await LoadSharitItemsAsync();
                var results = Items.Where(s =>
                {
                    if (!string.IsNullOrEmpty(s.Title))
                    {
                        if (s.Title.ToUpper().Contains(TextToSearch.ToUpper())) return true;
                    }
                    else if (!string.IsNullOrEmpty(s.Description))
                    {
                        if (s.Description.ToUpper().Contains(TextToSearch.ToUpper())) return true;
                    }
                    return false;
                }).ToList();

                _context.Items = new ObservableCollection<SharitItem>(results.OrderBy(i=>i.Date));

                NavigationService.Instance.NavigateBack();
            }

            NavigationService.Instance.NavigateBack();
        }
    }
}

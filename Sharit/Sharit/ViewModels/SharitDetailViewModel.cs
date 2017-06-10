using Sharit.Models;
using Sharit.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sharit.ViewModels
{
    public class SharitDetailViewModel : ViewModelBase
    {
        private SharitItem _item;
        public SharitItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public ICommand EditCommand => new Command(Edit);
        public ICommand DeleteCommand => new Command(async () => await DeleteAsync());

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if (navigationContext is SharitItem)
            {
                Item = (SharitItem)navigationContext;
            }
        }

        private void Edit()
        {
            //NavigationService.Instance.NavigateTo<NewXamagramItemViewModel>(Item);
        }

        private async Task DeleteAsync()
        {
            //if (Item.Id != null)
            //{
            //    await XamagramMobileService.Instance.DeleteXamagramItemAsync(Item);

            //    NavigationService.Instance.NavigateBack();
            //}
        }
    }
}

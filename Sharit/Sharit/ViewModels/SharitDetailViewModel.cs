using Sharit.Configs;
using Sharit.Models;
using Sharit.ViewModels.Base;

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

        private string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if (navigationContext is SharitItem)
            {
                Item = (SharitItem)navigationContext;
                Price = Item.Price.ToString() + GlobalSettings.CurrencySimbol;
            }
        }
    }
}

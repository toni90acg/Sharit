using Sharit.Models;
using Sharit.Services;
using Sharit.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sharit.ViewModels
{
    public class AddSharitItemViewModel : ViewModelBase
    {
        private string _id;



        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        public ICommand CancelCommand => new Command(Cancel);

        public override void OnAppearing(object navigationContext)
        {
            //if (navigationContext is SharitItem)
            //{
            //    var xamagramItem = (SharitItem)navigationContext;

            //    Id = xamagramItem.Id;
            //    ImageUrl = xamagramItem.Image;
            //    Name = xamagramItem.Name;
            //    Description = xamagramItem.Description;
            //}

            base.OnAppearing(navigationContext);
        }


        private async Task SaveAsync()
        {
            var sharitItem = new SharitItem
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Date = Date,
                Price = Price
            };

            await ClientHttpService.Instance.AddSharitItem(sharitItem);

            NavigationService.Instance.NavigateBack();
        }

        private void Cancel()
        {
            NavigationService.Instance.NavigateBack();
        }
    }
}

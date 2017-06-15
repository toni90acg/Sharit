using Sharit.Models;
using Sharit.Services;
using Sharit.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sharit.ViewModels
{
    public class AddSharitItemViewModel : ViewModelBase
    {
        public AddSharitItemViewModel(List<Entry> requiredElements, Entry priceEntry)
        {
            ShowRequiredFields = false;
            RequiredElements = requiredElements;
            PriceEntry = priceEntry;
        }

        public List<Entry> RequiredElements { get; set; }
        public Entry PriceEntry { get; set; }
        //public bool ShowRequiredFields { get; set; }
        //private string _id;
        //public string Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        private bool _showRequiredFields;
        public bool ShowRequiredFields
        {
            get { return _showRequiredFields; }
            set
            {
                _showRequiredFields = value;
                OnPropertyChanged("ShowRequiredFields");
            }
        }

        private bool _isFree;
        public bool IsFree
        {
            get { return _isFree; }
            set
            {
                _isFree = value;
                if (value)
                {
                    Price = 0;
                    PriceEntry.IsEnabled = false;
                }
                else
                {
                    PriceEntry.IsEnabled = true;
                }
                OnPropertyChanged("IsFree");
            }
        }

        

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                ClearRequiredElements();
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
            Date = DateTime.Now;

            base.OnAppearing(navigationContext);
        }


        private async Task SaveAsync()
        {
            if (RequiredElements.Any(e => string.IsNullOrEmpty(e.Text)))
            {
                MarkRequiredElements();
            }
            else
            {
                NavigationService.Instance.PushSharitModal();
                var sharitItem = new SharitItem
                {
                    //Id = Id,
                    Title = Title,
                    Description = Description,
                    Date = Date,
                    Price = Price
                };

                await ClientHttpService.Instance.AddSharitItem(sharitItem);

                NavigationService.Instance.PopSharitModal();
                NavigationService.Instance.NavigateBack();
            }
        }

        private void Cancel()
        {
            NavigationService.Instance.NavigateBack();
        }

        private void MarkRequiredElements()
        {
            ShowRequiredFields = true;
            foreach (var element in RequiredElements)
            {
                if (string.IsNullOrEmpty(element.Text))
                {
                    element.BackgroundColor = Color.Red;
                }
            }
        }

        private void ClearRequiredElements()
        {
            ShowRequiredFields = false;
            foreach (var element in RequiredElements)
            {
                element.BackgroundColor = Color.Default;
            }
        }
    }
}

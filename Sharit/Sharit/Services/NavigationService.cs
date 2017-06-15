using Sharit.ViewModels;
using Sharit.Views;
using Sharit.Views.Modals;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sharit.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance
        {
            get
            {
                if (_instance == null) _instance = new NavigationService();                
                return _instance;
            }
        }

        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(SharitListViewModel), typeof(SharitListView) },
            { typeof(SharitDetailViewModel), typeof(SharitDetailView) },
            { typeof(AddSharitItemViewModel), typeof(AddSharitItemView) },
            { typeof(SearchSharitViewModel), typeof(SearchSharitView) }
        };

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void PushSharitModal()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new SharitModalView());
        }

        public void PopSharitModal()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}

using Sharit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sharit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchSharitView : ContentPage
    {
        public SearchSharitView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new SearchSharitViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as SearchSharitViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
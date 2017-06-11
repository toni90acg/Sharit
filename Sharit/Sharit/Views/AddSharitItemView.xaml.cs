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
    public partial class AddSharitItemView : ContentPage
    {
        public AddSharitItemView(object parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            BindingContext = new AddSharitItemViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as AddSharitItemViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
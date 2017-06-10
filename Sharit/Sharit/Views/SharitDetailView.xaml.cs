using Sharit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sharit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharitDetailView : ContentPage
    {
        public SharitDetailView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new SharitDetailViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as SharitDetailViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
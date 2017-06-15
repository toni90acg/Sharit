using Sharit.ViewModels.Modals;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sharit.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharitModalView : ContentPage
    {
        public SharitModalView()
        {
            InitializeComponent();

            BindingContext = new SharitModalViewModel();
        }
    }
}
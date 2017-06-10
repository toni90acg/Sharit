using Sharit.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sharit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharitListView : ContentPage
    {
        public SharitListView()
        {
            InitializeComponent();

            BindingContext = new SharitListViewModel();
        }
    }
}
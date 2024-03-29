﻿using Sharit.ViewModels;
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
        public SharitListView(object parameter = null)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new SharitListViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as SharitListViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
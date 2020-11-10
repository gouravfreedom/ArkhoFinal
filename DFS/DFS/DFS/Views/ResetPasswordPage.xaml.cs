using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DFS.Views
{
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();

            BindingContext = new ViewModels.ResetPasswordViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ViewModels.ResetPasswordViewModel, String>(this, "Alert", async (sender, message) =>
            {

                await DisplayAlert("Alert", message, "OK");
            });

            MessagingCenter.Subscribe<ViewModels.ResetPasswordViewModel>(this, "Success", async (sender) =>
            {

                await this.Navigation.PopAsync();
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ViewModels.ResetPasswordViewModel, String>(this, "Alert");
            MessagingCenter.Unsubscribe<ViewModels.ResetPasswordViewModel>(this, "Success");
        }
    }
}

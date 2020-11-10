using System;
using System.Collections.Generic;
using DFS.Utils;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class TrainerProfilePage : ContentPage
    {
        double xTransition;
        ViewModels.ProfileViewModel profileViewModel;
        public TrainerProfilePage()
        {
            InitializeComponent();

            BindingContext = profileViewModel = new ViewModels.ProfileViewModel();


        }

        private async void GalleryListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as string;
            await PopupNavigation.Instance.PushAsync(new ImagePopupPage(item));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            //this.Navigation.PushAsync(new UserInformationPage(new ViewModels.SignupViewModel()));
            App.Current.MainPage = new HanselmanNavigationPage(new UserInformationPage(new ViewModels.SignupViewModel()));
        }

        async void Handle_Calender(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new TraineeCalanderPage());
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // don't do anything if we just de-selected the row.
            if (e.SelectedItem == null) return;

            // Deselect the item.
            if (sender is ListView lv)
            {
                Models.LoginResponse.Services service = (Models.LoginResponse.Services)e.SelectedItem;
                profileViewModel.ServiceDesc = service.ChargingPeriod;
                ServiceLabel.IsVisible = true;
                lv.SelectedItem = null;

                await DisplayAlert("Service Description", service.ChargingPeriod, "OK");
                await this.Navigation.PushAsync(new CalenderPage(service));

            }

        }

        void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed)
            {
                if (xTransition > 50)
                {
                    if (profileViewModel.SelectedIndex != 0)
                    {
                        profileViewModel.SelectedIndex -= 1;
                        profileViewModel.HandleSwipe();
                    }

                }
                else if (xTransition < -50)
                {
                    if (profileViewModel.SelectedIndex < 2)
                    {
                        profileViewModel.SelectedIndex += 1;
                        profileViewModel.HandleSwipe();
                    }

                }

            }
            else
            {
                xTransition = e.TotalX;
            }
        }

        async void Insta_Disconnect(object sender, System.EventArgs e)
        {
            profileViewModel.IsServiceInProgress = true;
            var response = await App.TodoManager.DeleteInstaImages();

            if (response == "Success")
            {
                App.InstagramMedia = null;
                App.InstaAccessToken = null;
                //traineeProfileViewModel.UpdateInstagramMedia();
                profileViewModel.GalleryVisible = false;
                profileViewModel.InstaVisible = true;
                profileViewModel.DisconnectVisible = false;
            }
            else
            {
                await DisplayAlert("Alert", "Something went wrong. Please try again later.", "Ok");
            }

            profileViewModel.IsServiceInProgress = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.SelectedView == "Trainer")
            {

                profileViewModel.UpdateInstagramMedia();

                MessagingCenter.Subscribe<Object, string>(this, "InstagramMedia", async (arg1, arg2) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(arg2))
                        {
                            var instaResult = await App.TodoManager.GetInstaToken(arg2);

                            if (instaResult == "Success")
                            {
                                var result = await App.TodoManager.GetInstagramMedia(App.InstaAccessToken);

                                CredentialsService.SaveCredentials(instagramMedia: App.InstagramMedia);
                                Application.Current.MainPage = new RootPage(App.SelectedView);
                            }
                            else
                            {
                                await DisplayAlert("Alert", instaResult, "Ok");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        await DisplayAlert("Alert", "something went wrong", "Ok");
                    }
                });
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<InstagramLoginPage, string>(this, "InstagramMedia");
        }

        async void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new InstagramLoginPage(false));
        }
    }
}

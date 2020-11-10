using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DFS.Utils;
using Xamarin.Forms;

namespace DFS
{
    public class RootPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> Pages { get; set; }
        String selectedView;

        public RootPage(String _selectedView)
        {

            NavigationPage.SetHasNavigationBar(this, false);
            Pages = new Dictionary<int, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel
            {
                Title = "Hanselman",
                Icon = "slideout.png"
            };

            if (_selectedView == "Trainee")
            {

                Pages.Add((int)MenuType.TraineeProfile, new HanselmanNavigationPage(new Views.TraineeProfilePage(this)));
                Detail = Pages[(int)MenuType.TraineeProfile];
                Detail.BackgroundColor = Color.Purple;
            }
            else
            {
                Pages.Add((int)MenuType.TrainerProfile, new HanselmanNavigationPage(new Views.TrainerProfilePage()));
                Detail = Pages[(int)MenuType.TrainerProfile];
            }

            InvalidateMeasure();

            selectedView = _selectedView;
        }



        public async void NavigateAsync(int id)
        {


            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case (int)MenuType.TraineeProfile:
                        Pages.Add(id, new HanselmanNavigationPage(new Views.TraineeProfilePage(this)));
                        break;

                    case (int)MenuType.TrainerProfile:
                        Pages.Add(id, new HanselmanNavigationPage(new Views.TrainerProfilePage()));
                        break;

                    case (int)MenuType.CoachList:
                        Pages.Add(id, new HanselmanNavigationPage(new Views.CoachListPage()));
                        break;

                    case (int)MenuType.Contact:
                        Pages.Add(id, new HanselmanNavigationPage(new Views.ScheduleList()));
                        break;

                    case (int)MenuType.Settings:
                        ViewModels.SignupViewModel signupViewModel = new ViewModels.SignupViewModel();
                        signupViewModel.UserIcon = App.LoginResponse.basicInfo.ImageUrl;

                        App.Current.MainPage = new HanselmanNavigationPage(new Views.UserInformationPage(signupViewModel));
                        return;


                    case (int)MenuType.Logout:
                        App.LoginResponse = new Models.LoginResponse.Member();
                        App.TrainerData = new Models.LoginResponse.Member();
                        App.FacebookUser = null;
                        App.InstagramUser = null;
                        App.InstagramMedia = null;
                        //App.FacebookProfile = new Models.FacebookProfile();
                        App.SelectedView = null;
                        App.access_code = null;
                        App.InstaAccessToken = null;
                        await CredentialsService.DeleteCredentials();

                        Application.Current.MainPage = new NavigationPage(new Views.SelectionPage());

                        //await this.Navigation.PushAsync(new NavigationPage(new LoginPage(selectedView)));
                        return;

                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;



            Detail = newPage;
            this.IsPresented = false;
        }
    }
}


using System;
using System.Threading.Tasks;
using DFS.Models;
using DFS.Service;
using DFS.Utils;
//using DFS.Views;
using DLToolkit.Forms.Controls;
using FFImageLoading.Transformations;
using Newtonsoft.Json;
using Xamarin.Forms;
using static DFS.Models.LoginResponse;

namespace DFS
{
    public partial class App : Application
    {
        public static TodoItemManager TodoManager { get; private set; }

        public static Service.DatabaseManager DatabaseManager { get; private set; }

        public static string SelectedView { get; set; }

        public static string TrainerStripeUrl { get; set; }

        public static Models.LoginResponse.Member LoginResponse { get; set; }

        public static Models.LoginResponse.Member TrainerData { get; set; }

        public static string access_code { get; set; }
        public static string AppName { get { return "Fitness"; } }
        //public static Models.FacebookProfile FacebookProfile { get; set; }

        public static Models.FacebookUser FacebookUser { get; set; }

        public static Models.InstagramUser InstagramUser { get; set; }

        public static Models.InstagramMedia InstagramMedia { get; set; }

        public static string InstaAccessToken { get; set; }

        public Task Initialization { get; private set; }

        private async Task InitializeAsync()
        {
            try
            {
                var isExist = await CredentialsService.DoCredentialsExist();
                if (isExist)
                {
                    var account = await CredentialsService.GetAccount();
                    var data = JsonConvert.DeserializeObject<Member>(account.Properties["Member"]);
                    App.LoginResponse = data;

                    if (App.LoginResponse == null)
                    {
                        LoginResponse = new Models.LoginResponse.Member();
                        TrainerData = new Models.LoginResponse.Member();
                        await CredentialsService.DeleteCredentials();
                        MainPage = new HanselmanNavigationPage(new Views.SelectionPage());

                        return;
                    }

                    if (App.LoginResponse.Email == null)
                    {
                        LoginResponse = new Models.LoginResponse.Member();
                        TrainerData = new Models.LoginResponse.Member();
                        await CredentialsService.DeleteCredentials();
                        MainPage = new HanselmanNavigationPage(new Views.SelectionPage());

                        return;
                    }

                    App.SelectedView = account.Properties["UserType"];
                    if (account.Properties.ContainsKey("FacebookUser"))
                    {
                        var fbData = JsonConvert.DeserializeObject<FacebookUser>(account.Properties["FacebookUser"]);
                        App.FacebookUser = fbData;
                    }
                    if (account.Properties.ContainsKey("InstagramUser"))
                    {
                        var instaData = JsonConvert.DeserializeObject<InstagramUser>(account.Properties["InstagramUser"]);
                        App.InstagramUser = instaData;
                    }
                    //if (account.Properties.ContainsKey("InstgramMedia"))
                    //{
                    //    var instaMedia = JsonConvert.DeserializeObject<InstagramMedia>(account.Properties["InstgramMedia"]);
                    //    App.InstagramMedia = instaMedia;
                    //}
                    MainPage = new NavigationPage(new RootPage(App.SelectedView));
                }
                else
                {
                    LoginResponse = new Models.LoginResponse.Member();
                    TrainerData = new Models.LoginResponse.Member();
                }

            }
            catch (Exception ex)
            {
                await CredentialsService.DeleteCredentials();
                MainPage = new HanselmanNavigationPage(new Views.SelectionPage());
            }
        }

        public App()
        {
            try
            {
                InitializeComponent();
                //var ignore = new CircleTransformation();
                FlowListView.Init();

                TodoManager = new TodoItemManager(new HTTPService());

                DatabaseManager = new Service.DatabaseManager(new DatabaseService());

                MainPage = new HanselmanNavigationPage(new Views.SelectionPage());
                TrainerStripeUrl = "";

                Initialization = InitializeAsync();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

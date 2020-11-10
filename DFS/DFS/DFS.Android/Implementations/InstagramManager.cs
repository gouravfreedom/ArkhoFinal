using System;
//using Com.Cloudrail.SI;
//using Com.Cloudrail.SI.Services;
using DFS.Dependency;
using DFS.Droid;
using DFS.Droid.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstagramManager))]
namespace DFS.Droid.Implementations
{
    public class InstagramManager: IInstagramManager
    {
        public void Login()
        {
            //Context context = ;
            //var cRInstagram = new Instagram(Android.App.Application.Context, "821083700e44427d9f50832f4a47bf87", "07b4baabfb6549a19ab938088a998de7");
            //cRInstagram.Login();
        }
    }
}

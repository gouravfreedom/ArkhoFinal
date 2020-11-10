using System;
//using CloudRailSI;
using DFS.Dependency;
using DFS.iOS;
using DFS.iOS.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstagramManager))]
namespace DFS.iOS.Implementations
{
    public class InstagramManager: IInstagramManager
    {
        public void Login()
        {
            //var cRInstagram = new CRInstagram("33c8f1v15f9d499c891305d9bf12ef8e", "3b93f768d948405984fd8ad3e94512d7");
            //cRInstagram.Login();
        }
    }
}

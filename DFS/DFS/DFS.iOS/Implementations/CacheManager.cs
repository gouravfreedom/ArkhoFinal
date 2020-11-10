using System;
using DFS.Dependency;
using DFS.iOS.Implementations;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(CacheManager))]
namespace DFS.iOS.Implementations
{
    public class CacheManager : ICacheManager
    {

        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
}
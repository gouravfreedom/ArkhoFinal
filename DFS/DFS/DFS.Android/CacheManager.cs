using System;
using Android.Webkit;
using DFS.Dependency;
using DFS.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DFS.Droid.CacheManager))]
namespace DFS.Droid
{
    public class CacheManager : ICacheManager
    {
        public CacheManager()
        {
        }

        public void Clear()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}

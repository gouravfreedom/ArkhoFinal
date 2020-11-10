using System;
using DFS.Models;

namespace DFS
{
    public interface IFacebookManager
    {
        void Login(Action<FacebookUser, string> onLoginComplete);

        void Logout();
    }
}

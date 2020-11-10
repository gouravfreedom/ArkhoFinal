using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DFS.Models;
using Newtonsoft.Json;
using Xamarin.Auth;
using static DFS.Models.LoginResponse;
using Xamarin.Essentials;

namespace DFS.Utils
{
    public class CredentialsService
    {
        public static async Task SaveAsync(Account account, string serviceId)
        {
            // Find existing accounts for the service
            var accounts = await FindAccountsForServiceAsync(serviceId);

            // Remove existing account with Id if exists
            accounts.RemoveAll(a => a.Username == account.Username);

            // Add account we are saving
            accounts.Add(account);

            // Serialize all the accounts to javascript
            var json = JsonConvert.SerializeObject(accounts);

            // Securely save the accounts for the given service
            await SecureStorage.SetAsync(serviceId, json);
        }

        public static async Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
        {
            // Get the json for accounts for the service
            var json = await SecureStorage.GetAsync(serviceId);

            try
            {
                // Try to return deserialized list of accounts
                return JsonConvert.DeserializeObject<List<Account>>(json);
            }
            catch { }

            // If this fails, return an empty list
            return new List<Account>();
        }

        public static async Task MigrateAllAccountsAsync(string serviceId, IEnumerable<Account> accountStoreAccounts)
        {
            var wasMigrated = await SecureStorage.GetAsync("XamarinAuthAccountStoreMigrated");

            if (wasMigrated == "1")
                return;

            await SecureStorage.SetAsync("XamarinAuthAccountStoreMigrated", "1");

            // Just in case, look at existing 'new' accounts
            var accounts = await FindAccountsForServiceAsync(serviceId);

            foreach (var account in accountStoreAccounts)
            {

                // Check if the new storage already has this account
                // We don't want to overwrite it if it does
                if (accounts.Any(a => a.Username == account.Username))
                    continue;

                // Add the account we are migrating
                accounts.Add(account);
            }

            // Serialize all the accounts to javascript
            var json = JsonConvert.SerializeObject(accounts);

            // Securely save the accounts for the given service
            await SecureStorage.SetAsync(serviceId, json);
        }

        public static async Task SaveCredentials(string userName=null, string password=null,Member member=null,FacebookUser fbUser=null,InstagramUser instagramUser=null,InstagramMedia instagramMedia=null, string userType=null)
        {
            var isExist = await CredentialsService.DoCredentialsExist();
            if (isExist && instagramMedia != null)
            {
                var account = await CredentialsService.GetAccount();
                var data = JsonConvert.SerializeObject(instagramMedia);
                account.Properties.Add("InstgramMedia", data);
                //AccountStore.Create().Save(account, "Fitness");
                await SaveAsync(account, "Fitness");

            }
            else
            {
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
                {
                    Account account = new Account
                    {
                        Username = userName,
                    };
                    account.Properties.Add("Password", password);
                    account.Properties.Add("UserType", userType);
                    var data = JsonConvert.SerializeObject(member);
                    account.Properties.Add("Member", data);

                    if (fbUser != null)
                    {
                        var fbData = JsonConvert.SerializeObject(fbUser);
                        account.Properties.Add("FacebookUser", fbData);
                    }

                    if (instagramUser != null)
                    {
                        var instData = JsonConvert.SerializeObject(instagramUser);
                        account.Properties.Add("InstagramUser", instData);
                    }

                    if (instagramMedia != null)
                    {
                        var mediaData = JsonConvert.SerializeObject(instagramMedia);
                        account.Properties.Add("InstgramMedia", mediaData);
                    }

                    await SaveAsync(account, "Fitness");
                }
            }
        }

        public static async Task DeleteCredentials()
        {
            var account = await FindAccountsForServiceAsync("Fitness");
            if (account.Any())
            {

                SecureStorage.RemoveAll();
            }
        }

        public static async Task<bool> DoCredentialsExist()
        {
            try
            {
                var accounts = await FindAccountsForServiceAsync("Fitness");
                if (accounts.Any())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;                   
            }
        }

        public static async Task<Account> GetAccount()
        {
            try
            {

                var accounts = await FindAccountsForServiceAsync("Fitness");
                if (accounts.Any())
                    return accounts.FirstOrDefault();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
            return null;
        }
    }
}

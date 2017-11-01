/************************************************************
 *https://components.xamarin.com/gettingstarted/xamarin.auth*
 ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using TinPet_Projeto.APIS;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TinPet_Projeto.Droid.APIS
{
    public class Facebook : IFacebook
    {
        public Semaphore semaforo = new Semaphore(0, 1);
        public OAuth2Authenticator auth = null;
        string accessToken;
        double ExpiraEm;
        DateTime DataExpirar;
        public string Id;
        public string Nome;

        public Facebook()
        {
            Id = "";
            Nome = "";
            accessToken = "";
            ExpiraEm = 0;

            auth = new OAuth2Authenticator(
            clientId: FacebookID.ID,
            scope: "",
            authorizeUrl: new Uri(FacebookID.End_Autorizacao),
            redirectUrl: new Uri(FacebookID.End_Redirecionamento));
        }

        public async Task Login()
        {
            auth.Completed += async (sender, eventArgs) =>
            {

                if (eventArgs.IsAuthenticated)
                {
                    accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    ExpiraEm = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    DataExpirar = DateTime.Now + TimeSpan.FromSeconds(ExpiraEm);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    Id = obj["id"].ToString().Replace("\"", "");
                    Nome = obj["name"].ToString().Replace("\"", "");
                    semaforo.Release();
                }
                else
                {
                    // o usuario cancelou o login
                    semaforo.Release();
                }
            };
        }

        public string GetNome()
        {
            return Nome;
        }
        public string GetId()
        {
            return Id;
        }
    }
}
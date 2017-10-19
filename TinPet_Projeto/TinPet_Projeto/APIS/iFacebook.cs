using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;
using Newtonsoft.Json.Linq;

namespace TinPet_Projeto.APIS
{
    public class PCLFacebook
    {
        public Semaphore semaforo = new Semaphore(0, 1);
        public OAuth2Authenticator auth = null;
        string accessToken;
        double ExpiraEm;
        DateTime DataExpirar;
        public string Id;
        public string Nome;

        /*https://components.xamarin.com/gettingstarted/xamarin.auth*/
        public PCLFacebook()
        {
            Id = "";
            Nome = "";
            accessToken = "";
            ExpiraEm = 0;

            auth = new OAuth2Authenticator(
            clientId: "537904446553253",//513f5038d6c8721421782ce592e5cec7
            scope: "",
            authorizeUrl: new Uri("https://m.facebook.com/v2.10/dialog/oauth/"),
            redirectUrl: new Uri("http://www.facebook.com/connect/login_success.htm"));
        }

        public void GetID()
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
    }
}

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

using TinPet_Projeto;
using Newtonsoft.Json.Linq;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "Tela_SemLogin")]
    public class Tela_SemLogin : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Tela_SemLogin);

            /*Pega um ponteiro da interface*/
            Button BotaoProxTela = FindViewById<Button>(Resource.Id.Botao_Login);
            BotaoProxTela.Click += delegate
            {

                var auth = new OAuth2Authenticator(
            clientId: "537904446553253",//513f5038d6c8721421782ce592e5cec7
            scope: "",
            authorizeUrl: new Uri("https://m.facebook.com/v2.10/dialog/oauth/"),
            redirectUrl: new Uri("http://www.facebook.com/connect/login_success.htm"));

             //   var auth = PCLFacebook.GetAutenticador();
                var ui = auth.GetUI(this);


                auth.Completed += async (sender, eventArgs) => {
                    if (eventArgs.IsAuthenticated)
                    {
                        var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                        var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                        var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                        var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                        var response = await request.GetResponseAsync();
                        var obj = JObject.Parse(response.GetResponseText());

                        var id = obj["id"].ToString().Replace("\"", "");
                        var name = obj["name"].ToString().Replace("\"", "");
                    }
                    else
                    {
                        // o usuario cancelou o login
                    }
                };

                StartActivity(auth.GetUI(this));
                //StartActivity(typeof(Tela_SelecionaAnimais));
            };
        }
    };

}
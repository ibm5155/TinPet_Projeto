using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace TinPet_Projeto.APIS
{
    public class PCLFacebook
    {
        /*https://components.xamarin.com/gettingstarted/xamarin.auth*/
        public static OAuth2Authenticator GetAutenticador()
        {
            var auth = new OAuth2Authenticator(
            clientId: "537904446553253",//513f5038d6c8721421782ce592e5cec7
            scope: "",
            authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
            redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            return auth;
        }
    }
}

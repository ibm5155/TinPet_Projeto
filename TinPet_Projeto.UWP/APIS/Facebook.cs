using Microsoft.Toolkit.Uwp.Services.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinPet_Projeto.APIS;

/* https://docs.microsoft.com/en-us/windows/uwpcommunitytoolkit/services/facebook */

namespace TinPet_Projeto.UWP.APIS
{
    public class Facebook : IFacebook
    {
        public string Id;
        public string Nome;


        public Facebook()
        {
            Id = "";
            Nome = "";
            // Initialize service
            FacebookService.Instance.Initialize(FacebookID.ID,0);
                /*
                 *             authorizeUrl: new Uri(),
            redirectUrl: new Uri());

                 */
        }

        public async Task Login()
        {
            // Login to Facebook
            if (!await FacebookService.Instance.LoginAsync())
            {
                return;
            }
            Id = FacebookService.Instance.Provider.User.Id;
            Nome = FacebookService.Instance.LoggedUser;        
        }

        public string GetId()
        {
            return Id;
        }

        public string GetNome()
        {
            return Nome;
        }
    }
}

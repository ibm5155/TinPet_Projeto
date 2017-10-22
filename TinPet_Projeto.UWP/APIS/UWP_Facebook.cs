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
    public class UWP_Facebook
    {
        public string Id;
        public string Nome;


        public UWP_Facebook()
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

        public async Task GetIDAsync()
        {
            // Login to Facebook
            if (!await FacebookService.Instance.LoginAsync())
            {
                return;
            }
            Id = FacebookService.Instance.Provider.User.Id;
            Nome = FacebookService.Instance.LoggedUser;        
        }
    }
}

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
using TinPet_Projeto.APIS.Secret;
using TinPet_Projeto.APIS;

using Quickblox.Sdk;

namespace TinPet_Projeto.Droid.APIS
{
    public class QuickBlox_Android : iQuickBlox
    {
        private int UserId;
        private int DestinatarioId;
        QuickbloxClient quickbloxClient;
        public QuickBlox_Android()
        {
            quickbloxClient = new QuickbloxClient(QuickBlox_S.ID, QuickBlox_S.Authorize, QuickBlox_S.Secret,"","");


        }

        public void Conectar()
        {
            switch(Globais.MeusDados.IdDono)
            {
                case "100003019488049":
                    UserId = 1;
                    /*Alexandre*/
                    break;
                case "100000216193356":
                    /*Edson*/
                    UserId = 2;
                    break;
                case "123312":
                    UserId = 3;
                    /*Lucas*/
                    break;
            }
            quickbloxClient.ChatXmppClient.Connect(UserId, "1234" + Globais.MeusDados.IdDono);

        }

        public void Desconectar()
        {
            quickbloxClient.ChatXmppClient.Close();
        }

    }
}
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

using TinPet_Projeto;


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
                StartActivity(typeof(Tela_SelecionaAnimais));
            };
        }
    };

}
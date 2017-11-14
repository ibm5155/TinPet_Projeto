using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TinPet_Projeto;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform;
using TinPet_Projeto.Models;
using TinPet_Projeto.Droid.Models;
using Android.Content.Res;
using TinPet_Projeto.APIS;

namespace TinPet_Projeto.Droid
{
	[Activity (Label = "TinPet_Projeto.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            /*Ver http://arteksoftware.com/ioc-containers-with-xamarin/*/
            /*Resumidamente o Autofac linka um codigo a outro assim poderá ser trabalhado
             todo o código a partir do projet o PCL, fazendo um bom reaproveitamento de código
             pois todas as plataformas terão uma parte de código em comum
             ...
             https://www.mvvmcross.com/
             */
            MvxSimpleIoCContainer.Initialize();
            Mvx.RegisterSingleton<IDataBase>(new DataBase_Android());

            APIS.FileHelper_Assets.assets = this.Assets;

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            /*Pega um ponteiro da interface*/
            Button BotaoProxTela = FindViewById<Button>(Resource.Id.botaoProximaTela);
            Button Botao = FindViewById<Button> (Resource.Id.BotaoSomar);
            TextView Numero1_UI = FindViewById<TextView>(Resource.Id.Numero1);
            TextView Numero2_UI = FindViewById<TextView>(Resource.Id.Numero2);
            TextView NumeroResultado_UI = FindViewById<TextView>(Resource.Id.Resultado);

            /*Adiciona um evento ao clicar esse botão*/
            /*ou seja, ele executa o codigo abaixo ao clicar o botão*/
            Botao.Click += delegate {
                float Numero1;
                float Numero2;
                float Somatorio;
                if(Numero1_UI.Text != "" && Numero2_UI.Text != "")
                {
                    //temos dois números
                    Numero1 = float.Parse(Numero1_UI.Text);/*Transforma string em um float*/
                                                           /*a UI sendo numerica só filtra a entrada do texto sendo campos numéricos, mas ao final será uma string*/
                    Numero2 = float.Parse(Numero2_UI.Text);
                    Somatorio = TinPet_Projeto.LogicaSomar.Somar(Numero1, Numero2);
                    NumeroResultado_UI.Text = "Resultado: " + Somatorio.ToString();
                }
                else
                {
                    NumeroResultado_UI.Text = "";
                }
            };

            BotaoProxTela.Click += delegate
            {
                StartActivity(typeof(Tela_SemLogin));
            };

        }
	}
}



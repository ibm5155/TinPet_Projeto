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

using TinPet_Projeto;
using TinPet_Projeto.Droid.APIS;
using System.Threading.Tasks;
using TinPet_Projeto.Database;
using Android.Support.V7.App;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaSemLogin", Theme = "@style/Theme.DesignDemo", MainLauncher = false)]
    public class Tela_SemLogin : AppCompatActivity
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
                Facebook Usuario = new Facebook();
                var ui = Usuario.auth.GetUI(this);
                Task task = new Task( ()=>
                {
                    //Roda em paralelo a UI principal o código para pegar a ID/NOME do usuário
                    Usuario.Login();
                    StartActivity(ui);
                    Usuario.semaforo.WaitOne(); //espera a tela do facebook terminar de ser processada
                    //não botar o wait one fora da thread senão você vai travar a main thread que lida com a ui em geral
                                                //terminado o processo, checar o que aconteceu
                    if (Usuario.Id != "")
                    {
                        //Temos um usuário válido

                        //Inicia o banco de dados local
                        Globais.BancoDeDados = new DataAccess();
                        if(Globais.BancoDeDados.PrimeiroAcesso(Usuario.Id) == true)
                        {
                            var ActivityComArg = new Intent(this, typeof(Tela_PrimeiroLogin));
                            ActivityComArg.PutExtra("MyData", Usuario.Id);
                            StartActivity(ActivityComArg);
                        }
                        else
                        {
                            Globais.BancoDeDados.GetFiltroUsuario();
                            StartActivity(typeof(Tela_SelecionaAnimais));
                        }


                    }
                    else
                    {
                        //Usuario cancelou
                    }

                });
                task.Start();
            };
        }
    };

}
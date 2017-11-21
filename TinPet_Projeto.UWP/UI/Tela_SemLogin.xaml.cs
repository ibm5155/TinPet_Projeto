using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TinPet_Projeto.UWP.APIS;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.Foundation.Metadata;
using TinPet_Projeto.Database;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Tela_SemLogin : Page
    {

        public Tela_SemLogin()
        {
            this.InitializeComponent();

            Botao_Login.Click += async delegate
            {
                Facebook Usuario = new Facebook();
                await Usuario.Login();
                if (Usuario.Id != "")
                {
                    //Temos um usuário válido

                    //Inicia o banco de dados local
                    Globais.BancoDeDados = new DataAccess();

                    Usuario = null;

                    this.Frame.Navigate(typeof(UI.Tela_SelecionaAnimais));
                }
            };
        }

  
    }
}

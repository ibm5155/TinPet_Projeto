using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TinPet_Projeto.Models;
using TinPet_Projeto.UWP.ImageHandler;
using TinPet_Projeto.UWP.Models;
using Windows.Storage;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Tela_SelecionaAnimais : Page
    {
        private List<Cachorro> Filtro;

        public Tela_SelecionaAnimais()
        {
            Task.Delay(1000);
            this.InitializeComponent();

            /*nova Thread separada da UI*/
            Task FiltrarDadosDB = new Task(
                async ()=>
                {
                    var x = ApplicationData.Current.LocalFolder.TryGetItemAsync("Cachorros.db");
                    File.Exists(Path.Combine(ApplicationData.Current.LocalFolder.Path, "Cachorros.db"));
                    DataBase DB = new DataBase();
                    await DB.GetConnectionAsync();
                    Filtro = DB.GetCachorros((int)TipoGenero.Feminino);
                    ImgAPI imgAPI = new ImgAPI();
                    imgAPI.CarregaImagemMemoria(ref CachorroAtual_IMG, Filtro[0].Imagem, false);

                }
                );


            #region menu flyout comandos
            Botao_Menu.Click += delegate
            {
                MenuOpcoes.IsPaneOpen = !MenuOpcoes.IsPaneOpen;
            };
            Botao_Configura_Animais.Click += delegate
            {
                this.Frame.Navigate(typeof(UI.Tela_MeuPet));
            };
            Botao_Filtro.Click += delegate
            {
                this.Frame.Navigate(typeof(UI.Tela_Filtrar));
            };
            #endregion


            FiltrarDadosDB.Start();

        }
    }
}

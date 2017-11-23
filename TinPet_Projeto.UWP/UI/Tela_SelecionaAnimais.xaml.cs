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
using TinPet_Projeto.Database;
using TinPet_Projeto.TypeConv;


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Tela_SelecionaAnimais : Page
    {
        private List<Cachorro> Filtro;
        private int CachorroAtual = 0;
        private bool CachorroAtualCarregado = true;
        private ImgAPI imgAPI = new ImgAPI();

        public Tela_SelecionaAnimais()
        {
            Task.Delay(1000);
            this.InitializeComponent();

            /*nova Thread separada da UI*/
            Task FiltrarDadosDB = new Task(
                ()=>
                {
                    Filtro = Globais.BancoDeDados.GetCachorros(TipoGenero.Feminino);
                    CarregaProximo();
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

            #region botão like dislike

            botao_dislike.Click += delegate
            {
                if (Filtro.Count() != 0)
                {
                    CarregaProximo();
                }
            };

            botao_like.Click += delegate
            {
                if (Filtro.Count() != 0)
                {
                    CarregaProximo();
                }
            };
            #endregion


            FiltrarDadosDB.Start();

        }

        private void CarregaProximo()
        {
            lock (Filtro)
            {
                CachorroAtualCarregado = false;
                CachorroAtual++;
                if (CachorroAtual == Filtro.Count())
                {
                    CachorroAtual = 0;
                }

                imgAPI.CarregaImagemMemoria(ref CachorroAtual_IMG, Filtro[CachorroAtual].Imagem, false);
                Cachorro_Nome.Text = Filtro[CachorroAtual].Nome;
                Cachorro_Raca.Text = Filtro[CachorroAtual].Raca.ToString();
                Idade.Text = (DateTime.Now.Year -  Filtro[CachorroAtual].AnoNascimento).ToString();
                Distancia.Text = DistanceAlgorithm.DistanceBetweenPlaces(Filtro[CachorroAtual].Regiao_Longitude, Filtro[CachorroAtual].Regiao_Longitude, Globais.MeusDados.CachorroLongitude, Globais.MeusDados.CachorroLatitude).ToString();

                CachorroAtualCarregado = true;
            }
        }
    }
}

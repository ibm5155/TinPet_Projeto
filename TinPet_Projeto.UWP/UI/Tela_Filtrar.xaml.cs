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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Tela_Filtrar : Page
    {
        public Tela_Filtrar()
        {
            this.InitializeComponent();

            Slider_Distancia.Value =Globais.MeuFiltro.DistanciaMaxima;
            InputBox_Idade_Maxima.Text = Globais.MeuFiltro.IdadeMaxima.ToString();
            InputBox_Idade_Minima.Text = Globais.MeuFiltro.IdadeMinima.ToString();


            Botao_Menu.Click += delegate
            {
                MenuOpcoes.IsPaneOpen = !MenuOpcoes.IsPaneOpen;
            };
            Botao_Escolher_Animais.Click += delegate
            {
                AtualizaBancodeDados();
                this.Frame.Navigate(typeof(UI.Tela_SelecionaAnimais));
            };
            Botao_Configura_Animais.Click += delegate
            {
                AtualizaBancodeDados();
                this.Frame.Navigate(typeof(UI.Tela_MeuPet));
            };

        }

        void AtualizaBancodeDados()
        {
            Globais.MeuFiltro.DistanciaMaxima = (int)Slider_Distancia.Value;
            Globais.MeuFiltro.IdadeMaxima = int.Parse(InputBox_Idade_Maxima.Text);
            Globais.MeuFiltro.IdadeMinima = int.Parse(InputBox_Idade_Minima.Text);
            Globais.BancoDeDados.UpdateFiltrousuario();
        }

    }
}

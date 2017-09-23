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
using TinPet_Projeto;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace TinPet_Projeto.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            /*em UWP não precisamos criar o ponteiro da interface, basta chamar pelo nome dado da interface e ele estará aqui...*/

            /*Adiciona um evento ao clicar esse botão*/
            /*ou seja, ele executa o codigo abaixo ao clicar o botão*/
            Bsomar.Click += delegate {
                float Numero1;
                float Numero2;
                float Somatorio;
                if (Numero1_UI.Text != "" && Numero2_UI.Text != "")
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

            bProximaTela.Click += delegate
            {
                Frame.Navigate(typeof(UI.TesteCachorro));
            };
        }
    }
}

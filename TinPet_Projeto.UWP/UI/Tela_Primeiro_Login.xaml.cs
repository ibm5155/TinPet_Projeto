using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TinPet_Projeto.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TinPet_Projeto.APIS;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Core.ViewModels;
using TinPet_Projeto.UWP.ImageHandler;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{

    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Primeiro_Login : Page
    {
        private int EtapaAtual = 0;
        private PicturePicker GaleriaDeImagens = new PicturePicker();
        private ImgAPI imgAPI = new ImgAPI();


        public Primeiro_Login()
        {
            this.InitializeComponent();
            Globais.MeusDados = new DadosPessoais();

            CriaListaRacas();
            Proximo_Botao.Click += async delegate
            {
                Proximo_Botao.IsEnabled = false;
                switch (EtapaAtual)
                {
                    case 0:
                        if(ValidaNome() == true)
                        {
                            EtapaAtual++;
                            Grid_Nome_Cachorro.Visibility = Visibility.Collapsed;
                            Grid_Ano_Cachorro.Visibility = Visibility.Visible;
                        }
                        break;
                    case 1:
                        if (ValidaIdade() == true)
                        {
                            EtapaAtual++;
                            Grid_Ano_Cachorro.Visibility = Visibility.Collapsed;
                            Grid_Genero_Cachorro.Visibility = Visibility.Visible;
                        }
                        break;
                    case 2:
                        EtapaAtual++;
                        Grid_Genero_Cachorro.Visibility = Visibility.Collapsed;
                        Grid_Localizacao.Visibility = Visibility.Visible;
                        Proximo_Botao.IsEnabled = true;
                        break;
                    case 3:
                        if (await ValidaLocalizacaoAsync() == true)
                        {
                            EtapaAtual++;
                            Grid_Localizacao.Visibility = Visibility.Collapsed;
                            Grid_Raca_Cachorro.Visibility = Visibility.Visible;
                        }
                        break;
                    case 4:
                        int id = Lista_Racas_Cachorro.SelectedIndex;
                        if(id > 0 && id < 11)
                        {
                            EtapaAtual++;
                            Globais.MeusDados.CachorroRaca = (Raca)id;
                            Grid_Raca_Cachorro.Visibility = Visibility.Collapsed;
                            Grid_ImagemCachorro.Visibility = Visibility.Visible;
                            Proximo_Botao.IsEnabled = true;
                            Proximo_Botao.Background = new SolidColorBrush(Color.FromArgb(255, 58, 255, 58));
                            Simbolo_Botao.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Okpng.png"));

                        }
                        break;
                    case 5:
                        //dados coletados, partiu proxima tela
                        this.Frame.Navigate(typeof(UI.Tela_SelecionaAnimais));
                        break;
                }
            };

            Genero_Masculino.Click += delegate
            {
                Genero_Feminino.BorderThickness = new Thickness(0);
                Genero_Masculino.BorderThickness = new Thickness(5);
                Globais.MeusDados.CachorroGenero = TipoGenero.Masculino;
            };

            Genero_Feminino.Click += delegate
            {
                Genero_Masculino.BorderThickness = new Thickness(0);
                Genero_Feminino.BorderThickness = new Thickness(5);
                Globais.MeusDados.CachorroGenero = TipoGenero.Feminino;
            };

            Botao_Cachorro_Imagem.Click += async delegate
            {
                //Carrega um Stream de uma imagem selecionada da galeria de imagens
                Stream Temp = await GaleriaDeImagens.GaleriaDeImagens.GetImageStreamAsync();
                if (Temp != null)
                {
                    byte[] resultado;
                    var memoryStream = new MemoryStream();
                    Temp.CopyTo(memoryStream);
                    resultado = memoryStream.ToArray();
                    Temp.CopyTo(memoryStream);
                    Globais.MeusDados.Foto = resultado;
                    imgAPI.CarregaImagemMemoria(ref Cachorro_Imagem, resultado, false);
                }
            };

        }

        private void CriaListaRacas()
        {
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Vira_Lata),FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Husky_Siberiano), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Shih_Tzu), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Chow_Chow), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Golden_Retriever), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Dalmatas), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Beagle), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Sao_Bernardo), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Samoieda), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Luluda_Pomerania), FontSize = 18 }));
            Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Akita), FontSize = 18 }));
        }

        bool ValidaNome()
        {
            bool NomeValido = false;
            if(InputBox_Nome_Cachorro.Text != "")
            {
                NomeValido = true;
                Globais.MeusDados.CachorroNome = InputBox_Nome_Cachorro.Text;
            }
            Proximo_Botao.IsEnabled = true;
            return NomeValido;
        }

        private bool ValidaIdade()
        {
            bool IdadeValida = false;
            if(InputBox_Ano_Cachorro.Text != "")
            {
                if(InputBox_Ano_Cachorro.Text.Length == 4)
                {
                    try
                    {
                        Globais.MeusDados.CachorroAnoNascimento =  int.Parse(InputBox_Ano_Cachorro.Text);
                        IdadeValida = true;
                    }
                    catch (FormatException)
                    {
                        // faz nada
                    }
                        
                }
            }
            Proximo_Botao.IsEnabled = true;
            return IdadeValida;
        }


        private async System.Threading.Tasks.Task<bool> ValidaLocalizacaoAsync()
        {
            bool Valido = false;
            GoogleMaps GetEndereco = new GoogleMaps();
            if(InputBox_Endereco.Text.Length > 5)
            {
                GeoCode geo = await GetEndereco.GetGeocodeAsync(InputBox_Endereco.Text);
                if(geo.Latitude != 0 && geo.Longitude  != 0)
                {
                    Globais.MeusDados.CachorroLatitude = geo.Latitude;
                    Globais.MeusDados.CachorroLongitude = geo.Longitude;
                    Valido = true;
                }
            }
            Proximo_Botao.IsEnabled = true;
            return Valido;
        }


    }
}

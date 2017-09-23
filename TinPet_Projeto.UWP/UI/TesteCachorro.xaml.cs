using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TinPet_Projeto.Models;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FFImageLoading;
using System.Threading.Tasks;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class TesteCachorro : Page
    {
        private Cachorro MeuCachorro = new Cachorro();

        private async void EscondeAnimacaoCarregando()
        {

            
        }

        private async void CarregaImagemPrincipal()
        {
            ImageService.Instance.LoadUrl(MeuCachorro.Imagem)
            .DownSample(300, 300)
            .Finish(workScheduled =>
            {
                CarregandoAnimacao.Visibility = Visibility.Collapsed;
            })
            .Into(Imagem)
            ;

        }

        public TesteCachorro()
        {
            this.InitializeComponent();

            MeuCachorro.AddImagem("https://i.imgur.com/hZ3AlAn.jpg");/*Exemplo de carregar imagem*/

            CarregaImagemPrincipal();

            bAtualizar.Click += delegate
            {
                bool DadosValidos = true;
                if (iNome.Text == "")
                {
                    DadosValidos = false;
                }
                if (iSexo.Text == "")
                {
                    DadosValidos = false;
                }
                if (iRegiao.Text == "")
                {
                    DadosValidos = false;
                }
                if (DadosValidos == true)
                {
                    MeuCachorro.SetNome(iNome.Text);
                    MeuCachorro.SetRegiao(double.Parse(iRegiao.Text), 0);
                }
            };
        }
    }
}

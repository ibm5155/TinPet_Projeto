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
using TinPet_Projeto.UWP.ImageHandler;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class TesteCachorro : Page
    {
        private Cachorro MeuCachorro = new Cachorro();

        public TesteCachorro()
        {
            Task x = new Task(() => {
                ImgAPI cachorroimg = new ImgAPI();
                cachorroimg.CarregaImagemURL("https://i.imgur.com/hZ3AlAn.jpg", "loading.png", "", ref Imagem, false);
            });
            this.InitializeComponent();
            x.Start();

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
                    MeuCachorro.Nome = iNome.Text;
//                    MeuCachorro.SetRegiao(double.Parse(iRegiao.Text), 0);
                }
            };
        }
    }
}

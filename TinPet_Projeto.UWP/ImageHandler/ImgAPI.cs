using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace TinPet_Projeto.UWP.ImageHandler
{
    public class ImgAPI
    {
        public bool Finalizou;
        public bool Carregando;
        public bool Falha;

        public void CarregaImagemURL(string URL, string ImagemCarregando, string ImagemErro, ref Image Img, bool MostrarErro)
        {
            /*Requer Chamada por Thread*/
            Semaphore S = new Semaphore(0, 1);
            Finalizou = false;
            Carregando = true;
            Falha = false;
            /*Já que a !#(*$ do servico dos caras tem um delay legal ao mostrar a imagem de carregando então vamos fazer o nosso na mão*/
            ImageService.Instance.LoadUrl(URL)
            .LoadingPlaceholder("loading.png", FFImageLoading.Work.ImageSource.CompiledResource)
            .DownSample(300, 300)
            .Error(exception =>
            {
                Finalizou = true;
                Falha = true;
                if (MostrarErro == true)
                {
                    new MessageDialog("Ocorreu um erro ao carregar a imagem").ShowAsync();
                }
            })
            .Finish(workScheduled =>
            {
                Finalizou = true;
                S.Release();
            })
            .Into(Img);
            S.WaitOne();
        }

        public void CarregaImagemResources(ref Image Destino, string Path, bool MostrarErro)
        {
            Finalizou = false;
            Carregando = true;
            Falha = false;
            ImageService.Instance.LoadCompiledResource(Path)
            .WithPriority(FFImageLoading.Work.LoadingPriority.High)
            .DownSample(300, 300)
            .Error(exception =>
            {
                Finalizou = true;
                Falha = true;
                if (MostrarErro == true)
                {
                    new MessageDialog("Ocorreu um erro ao carregar a imagem").ShowAsync();
                }
            })
            .Finish(workScheduled =>
            {
                Finalizou = true;
            })
            .IntoAsync(Destino);
        }


        public async void AdicionaCache(string URLouPATH, FFImageLoading.Work.ImageSource Source)
        {
            if (Source == FFImageLoading.Work.ImageSource.Url)
            {
                await ImageService.Instance.LoadUrl(URLouPATH)
                    .DownSample(300, 300)
                    .PreloadAsync();
            }
            else
            {
                await ImageService.Instance.LoadCompiledResource(URLouPATH)
                    .DownSample(300, 300)
                    .PreloadAsync();
            }
        }

    }
}

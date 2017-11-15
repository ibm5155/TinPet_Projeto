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
using System.Threading;
using FFImageLoading;
using Android.Media;
using FFImageLoading.Views;
using System.Threading.Tasks;
using System.IO;

namespace TinPet_Projeto.Droid.ImageHandler
{
    public class ImgAPI
    {
        public bool Finalizou;
        public bool Carregando;
        public bool Falha;

        public void CarregaImagemURL(string URL, string ImagemCarregando, string ImagemErro, ref ImageViewAsync Img, bool MostrarErro)
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
                    new AlertDialog.Builder(null)
                       .SetMessage("Ocorreu um erro ao carregar a imagem")
                       .Show();
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

        public void CarregaImagemResources(ref ImageViewAsync Destino, string Path, bool MostrarErro)
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
                    new AlertDialog.Builder(null)
                       .SetMessage("Ocorreu um erro ao carregar a imagem")
                       .Show();
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

        public void CarregaImagemMemoria(ref ImageViewAsync Destino, byte[] ImagemEmBytes, bool MostraErro)
        {
            Finalizou = false;
            Carregando = true;
            Falha = false;


            ImageService.Instance.LoadStream( (token) => { return GetStreamDeBytes(token, ImagemEmBytes); })
            .WithPriority(FFImageLoading.Work.LoadingPriority.High)
            .DownSample(300, 300)
            .Error(exception =>
            {
                Finalizou = true;
                Falha = true;
                if (MostraErro == true)
                {
                    //new MessageDialog("Ocorreu um erro ao carregar a imagem").ShowAsync();
                }
            })
            .Finish(workScheduled =>
            {
                Finalizou = true;
            })
            .IntoAsync(Destino);

        }

        public Task<System.IO.Stream> GetStreamDeBytes(CancellationToken ct, byte[] ImagemEmBytes)
        {

            //Since we need to return a Task<Stream> we will use a TaskCompletionSource>
            TaskCompletionSource<System.IO.Stream> tcs = new TaskCompletionSource<System.IO.Stream>();

            tcs.TrySetResult(new MemoryStream(ImagemEmBytes));

            return tcs.Task;

        }

    }
}
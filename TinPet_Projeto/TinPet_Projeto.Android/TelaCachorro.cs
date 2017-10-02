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
using FFImageLoading.Views;
using TinPet_Projeto.Droid.ImageHandler;
using System.Threading.Tasks;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaCachorro")]
    public class TelaCachorro : Activity
    {
        Task x;

        void carregaimagem()
        {


        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.TelaCachorro);
            carregaimagem();
            ImageViewAsync Tela = FindViewById<ImageViewAsync>(Resource.Id.Meudogepng);

            x = new Task(()=> {
                ImgAPI cachorroimg = new ImgAPI();
                cachorroimg.CarregaImagemURL("https://i.imgur.com/hZ3AlAn.jpg", "", "", ref Tela, false);
            });
            x.Start();
        }
    }
}
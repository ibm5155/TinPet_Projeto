using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

using FFImageLoading.Views;
using TinPet_Projeto.Droid.ImageHandler;
using System.Threading.Tasks;
using TinPet_Projeto.Database;
using TinPet_Projeto.Models;
using System.Collections.Generic;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaCachorro", Theme = "@style/Theme.DesignDemo", MainLauncher = true)]
    public class Tela_SelecionaAnimais : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        private List<Cachorro> Filtro;


        void carregaimagem()
        {


        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Tela_SelecionaAnimais);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            /*            carregaimagem();
                        ImageViewAsync Tela = FindViewById<ImageViewAsync>(Resource.Id.Meudogepng);

                        x = new Task(()=> {
                            ImgAPI cachorroimg = new ImgAPI();
                            cachorroimg.CarregaImagemURL("https://i.imgur.com/hZ3AlAn.jpg", "loading.png", "", ref Tela, false);
                        });
                        x.Start();
                        */

            /*nova Thread separada da UI*/
            Task FiltrarDadosDB = new Task(
                async () =>
                {
                    DataAccess DB = new DataAccess();
                    //await DB.GetConnectionAsync();
                    Filtro = DB.GetCachorros(TipoGenero.Feminino);
                   // ImgAPI imgAPI = new ImgAPI();
                    //imgAPI.CarregaImagemMemoria(ref CachorroAtual_IMG, Filtro[0].Imagem, false);

                }
                );



            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            FiltrarDadosDB.Start();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_filtrarbusca):
                    StartActivity(typeof(Tela_Filtrar));
                    break;
            }
        }

    }
}
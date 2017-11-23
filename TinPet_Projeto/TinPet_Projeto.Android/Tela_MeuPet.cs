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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using TinPet_Projeto.Droid.ImageHandler;
using TinPet_Projeto.APIS;
using TinPet_Projeto.Models;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using FFImageLoading.Views;
using System.Threading.Tasks;
using TinPet_Projeto.Droid.APIS;
using System.IO;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaMeuPet", Theme = "@style/Theme.DesignDemo", MainLauncher = false)]
    public class Tela_MeuPet : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        ImgAPI API_Imagem = new ImgAPI();
        ImgAPI API_Imagem3 = new ImgAPI();


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Tela_MeuPet);


            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar_Tela_MeuPet);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_Tela_MeuPet);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;


            // Create your application here
            var Pnt_Nome = FindViewById<TextView>(Resource.Id.Tela_MeuPet_Nome);
            var Pnt_Idade = FindViewById<TextView>(Resource.Id.Tela_MeuPet_Ano);
            var Pnt_Raca = FindViewById<TextView>(Resource.Id.Tela_MeuPet_Raca);
            var Pnt_Foto = FindViewById<ImageViewAsync>(Resource.Id.Tela_MeuPet_Imagem);
            var Pnt_Mapa = FindViewById<ImageViewAsync>(Resource.Id.Tela_MeuPet_Mapa);
            var Pnt_Genero = FindViewById<ImageViewAsync>(Resource.Id.Tela_MeuPet_Genero);

            Pnt_Nome.Text = Globais.MeusDados.CachorroNome;
            Pnt_Idade.Text = (DateTime.Now.Year - Globais.MeusDados.CachorroAnoNascimento).ToString();
            Pnt_Raca.Text = Cachorro.GetRacaNome(Globais.MeusDados.CachorroRaca);
            API_Imagem.CarregaImagemMemoria(ref Pnt_Foto, Globais.MeusDados.Foto, false);
            API_Imagem3.CarregaImagemResources(ref Pnt_Genero, (Globais.MeusDados.CachorroGenero == TipoGenero.Masculino ? "Masculino.png" : "Feminino.png"), false);


            new Task(async ()=>
            {
                ImgAPI API_Imagem2 = new ImgAPI();
                if (FileHelper_Assets.Temp_ImagemMapa_MeuCachorro == null)
                {
                    //carrega imagem
                    GoogleMaps Gmaps = new GoogleMaps();
                    FileHelper_Assets.Temp_ImagemMapa_MeuCachorro = await Gmaps.GetImagemMapa(Globais.MeusDados.CachorroLatitude, Globais.MeusDados.CachorroLongitude);
                }
                byte[] resultado;
                var memoryStream = new MemoryStream();
                FileHelper_Assets.Temp_ImagemMapa_MeuCachorro.CopyTo(memoryStream);
                resultado = memoryStream.ToArray();
                API_Imagem2.CarregaImagemMemoria(ref Pnt_Mapa, resultado, false);
            }
                ).Start();
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
                case (Resource.Id.nav_buscapets):
                    StartActivity(typeof(Tela_SelecionaAnimais));
                    break;
                case (Resource.Id.nav_filtrarbusca):
                    StartActivity(typeof(Tela_Filtrar));
                    break;
            }
        }

    }
}
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
    [Activity(Label = "TelaCachorro", Theme = "@style/Theme.DesignDemo", MainLauncher = false)]
    public class Tela_SelecionaAnimais : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        private Android.Support.Design.Widget.FloatingActionButton botao_dislike;
        private Android.Support.Design.Widget.FloatingActionButton botao_like;
        private TextView Cachorro_Nome;
        private TextView Cachorro_Raca;
        private TextView Idade;
        private ImageViewAsync CachorroAtual_IMG;

        private List<Cachorro> Filtro;

        private int CachorroAtual = 0;
        private bool CachorroAtualCarregado = true;
        private ImgAPI imgAPI = new ImgAPI();

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;

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

            Cachorro_Nome = FindViewById<TextView>(Resource.Id.Cachorro_Nome);
            Cachorro_Raca = FindViewById<TextView>(Resource.Id.Cachorro_Raca);
            Idade = FindViewById<TextView>(Resource.Id.Cachorro_Idade);
            CachorroAtual_IMG = FindViewById< ImageViewAsync>(Resource.Id.CachorroAtual_IMG);

            /*nova Thread separada da UI*/
            Task FiltrarDadosDB = new Task(
                async () =>
                {
                    Filtro = Globais.BancoDeDados.GetCachorros(TipoGenero.Feminino);
                    imgAPI = new ImgAPI();
                    CarregaProximo();
                }
                );

            #region botão like dislike
            botao_dislike = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.botao_dislike);
            botao_dislike.Click += delegate
            {
                if (Filtro.Count != 0)
                {
                    CarregaProximo();
                }
            };

            botao_like = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.botao_like);
            botao_like.Click += delegate
            {
                if (Filtro.Count != 0)
                {
                    CarregaProximo();
                }
            };
            #endregion

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
                case (Resource.Id.nav_meuspets):
                    StartActivity(typeof(Tela_MeuPet));
                    break;
            }
        }

        private void CarregaProximo()
        {
            lock (Filtro)
            {
                CachorroAtualCarregado = false;
                CachorroAtual++;
                if (CachorroAtual == Filtro.Count)
                {
                    CachorroAtual = 0;
                }

                imgAPI.CarregaImagemMemoria(ref CachorroAtual_IMG, Filtro[CachorroAtual].Imagem, false);
                RunOnUiThread(() =>
                {
                    Cachorro_Nome.Text = Filtro[CachorroAtual].Nome;
                    Cachorro_Raca.Text = Filtro[CachorroAtual].Raca.ToString();
                    Idade.Text = "10";
                });
                CachorroAtualCarregado = true;
            }
        }


    }
}
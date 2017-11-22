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


namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaFiltrar", Theme = "@style/Theme.DesignDemo", MainLauncher = true)]
    public class Tela_Filtrar : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Tela_Filtrar);
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;


            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar_Tela_Filtrar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_Tela_Filtrar);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

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
                case (Resource.Id.nav_meuspets):
                    StartActivity(typeof(Tela_MeuPet));
                    break;

            }
        }


    }
}
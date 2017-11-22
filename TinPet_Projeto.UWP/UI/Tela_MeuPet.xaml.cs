using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TinPet_Projeto.UWP.ImageHandler;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace TinPet_Projeto.UWP.UI
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Tela_MeuPet : Page
    {
        public Tela_MeuPet()
        {
            this.InitializeComponent();
            Nome.Text = Globais.MeusDados.CachorroNome;
            Idade.Text = (DateTime.Now.Year - Globais.MeusDados.CachorroAnoNascimento).ToString();
            Genero.Text = (Globais.MeusDados.CachorroGenero == TinPet_Projeto.Models.TipoGenero.Masculino ? "♂" : "♀");
            Raca_tela.Text = Globais.MeusDados.CachorroRaca.ToString().Replace('_',' ');


            ImgAPI iAPI = new ImgAPI();
            iAPI.CarregaImagemMemoria(ref Cachorro_imagem, Globais.MeusDados.Foto, false);

            

            Botao_Menu.Click += delegate
            {
                MenuOpcoes.IsPaneOpen = !MenuOpcoes.IsPaneOpen;
            };
            Botao_Escolher_Animais.Click += delegate
            {
                ClearMap();
                this.Frame.Navigate(typeof(UI.Tela_SelecionaAnimais));
            };
            Botao_Filtro.Click += delegate
            {
                ClearMap();
                this.Frame.Navigate(typeof(UI.Tela_Filtrar));
            };


            AddSpaceNeedleIcon();

        }

        public void AddSpaceNeedleIcon()
        {
            // get position
            Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = Globais.MeusDados.CachorroLatitude, Longitude = Globais.MeusDados.CachorroLongitude});
            //create POI
            MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = Globais.MeusDados.CachorroNome, ZIndex = 0 };
            // add to map and center it
            MapaCachorro.MapElements.Add(myPOI);
            MapaCachorro.Center = myPoint;
            MapaCachorro.ZoomLevel = 20;
        }

        void ClearMap()
        {
            MapaCachorro.MapElements.Clear();
            MapaCachorro.Resources.Clear();
            MapaCachorro = null;
            GC.Collect();
        }
    }
}

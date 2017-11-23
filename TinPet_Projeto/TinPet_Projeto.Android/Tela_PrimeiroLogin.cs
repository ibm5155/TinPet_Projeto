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
using Android.Support.Design.Widget;
using Android.Graphics.Drawables;

using FFImageLoading.Views;
using TinPet_Projeto.APIS;
using TinPet_Projeto.Models;
using TinPet_Projeto.Droid.ImageHandler;

using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace TinPet_Projeto.Droid
{
    [Activity(Label = "TelaPrimeiroLogin", Theme = "@style/Theme.DesignDemo", MainLauncher = false)]
    public class Tela_PrimeiroLogin : AppCompatActivity
    {
        private int EtapaAtual = 0;
        private PicturePicker GaleriaDeImagens = new PicturePicker();
        private ImgAPI imgAPI = new ImgAPI();

        EditText InputBox_Nome;
        EditText InputBox_Ano;
        EditText InputBox_Endereco;
        ImageViewAsync Foto_Cachorro_Tela;
        FloatingActionButton Botao_Proximo;
        Button Botao_Masculino;
        Button Botao_Feminino;
        GridLayout Grid1;
        GridLayout Grid2;
        GridLayout Grid3;
        GridLayout Grid4;
        GridLayout Grid5;
        GridLayout Grid6;
        LinearLayout LL;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Tela_PrimeiroLogin);

            InputBox_Nome = FindViewById<EditText>(Resource.Id.Tela_PC_Nome_Cachorro);
            InputBox_Ano = FindViewById<EditText>(Resource.Id.Tela_PC_Ano_Cachorro);
            InputBox_Endereco = FindViewById<EditText>(Resource.Id.Tela_PC_Endereco_Cachorro);
            Foto_Cachorro_Tela = FindViewById<ImageViewAsync>(Resource.Id.Tela_PC_Foto);
            Botao_Proximo = FindViewById<FloatingActionButton>(Resource.Id.Tela_PC_Proximo);
            Botao_Masculino = FindViewById<Button>(Resource.Id.Tela_PC_Btn_Masculino);
            Botao_Feminino = FindViewById<Button>(Resource.Id.Tela_PC_Btn_Feminino);
            Grid1 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid01);
            Grid2 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid02);
            Grid3 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid03);
            Grid4 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid04);
            Grid5 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid05);
            Grid6 = FindViewById<GridLayout>(Resource.Id.Tela_PC_Grid06);
            LL = FindViewById<LinearLayout>(Resource.Id.Tela_PC_LL);
            Globais.MeusDados = new DadosPessoais();
            Globais.MeusDados.IdDono = Intent.GetStringExtra("MyData") ?? null;
            CriaListaRacas();

            Botao_Proximo.Click += async delegate
            {
                Botao_Proximo.Enabled = false;
                switch (EtapaAtual)
                {
                    case 0:
                        if (ValidaNome() == true)
                        {
                            EtapaAtual++;
                            LL.RemoveViewAt(0);
                        }
                        break;
                    case 1:
                        if (ValidaIdade() == true)
                        {
                            EtapaAtual++;
                            LL.RemoveViewAt(0);
                        }
                        break;
                    case 2:
                        EtapaAtual++;
                        LL.RemoveViewAt(0);
                        Botao_Proximo.Enabled = true;
                        break;
                    case 3:
                        if (await ValidaLocalizacaoAsync() == true)
                        {
                            EtapaAtual++;
                            LL.RemoveViewAt(0);
                            Botao_Proximo.Enabled = true;
                        }
                        break;
                    case 4:
                        //int id = Lista_Racas_Cachorro.SelectedIndex;
                        // if (id > 0 && id < 11)
                        // {
                        EtapaAtual++;
                        //                            Globais.MeusDados.CachorroRaca = (Raca)
                        LL.RemoveViewAt(0);
                        Botao_Proximo.Enabled = true;
                        //                            Botao_Proximo.SetBackgroundColor(Colorgb(255, 58, 255, 58));
#warning Arruma essas cores depois
                        Botao_Proximo.SetImageResource(Resource.Drawable.Okpng);

                        // }
                        break;
                    case 5:
                        //dados coletados, partiu proxima tela
                        Globais.BancoDeDados.InsertInto_DadosPessoais();
                        Globais.MeuFiltro = new FiltroUsuario();
                        Globais.MeuFiltro.CachorroGenero = Globais.MeusDados.CachorroGenero == TipoGenero.Feminino ? TipoGenero.Masculino : TipoGenero.Feminino;
                        Globais.MeuFiltro.DistanciaMaxima = 30;//Km
                        Globais.MeuFiltro.IdadeMaxima = 12;
                        Globais.MeuFiltro.IdadeMinima = 5;
                        Globais.MeuFiltro.IdDono = Globais.MeusDados.IdDono;
                        Globais.BancoDeDados.InsertInto_FiltroUsuario();

                        StartActivity(typeof(Tela_Filtrar));
                        break;
                }
            };


            Botao_Masculino.Click += delegate
            {
                Globais.MeusDados.CachorroGenero = TipoGenero.Masculino;
            };

            Botao_Feminino.Click += delegate
            {
                Globais.MeusDados.CachorroGenero = TipoGenero.Feminino;
            };


            Foto_Cachorro_Tela.Click += async delegate
            {
                //Carrega um Stream de uma imagem selecionada da galeria de imagens
                APIS.FileHelper_Assets.Instance = this;
                Stream Temp = await GaleriaDeImagens.GaleriaDeImagens.GetImageStreamAsync();
                if (Temp != null)
                {
                    byte[] resultado;
                    var memoryStream = new MemoryStream();
                    Temp.CopyTo(memoryStream);
                    resultado = memoryStream.ToArray();
                    Temp.CopyTo(memoryStream);
                    Globais.MeusDados.Foto = resultado;
                    imgAPI.CarregaImagemMemoria(ref Foto_Cachorro_Tela, resultado, false);
                }
            };


        }

        //https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/photo-picker/

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == 100)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    APIS.FileHelper_Assets.PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    APIS.FileHelper_Assets.PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }



        private void CriaListaRacas()
        {
            /*  Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Vira_Lata), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Husky_Siberiano), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Shih_Tzu), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Chow_Chow), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Golden_Retriever), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Dalmatas), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Beagle), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Sao_Bernardo), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Samoieda), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Luluda_Pomerania), FontSize = 18 }));
              Lista_Racas_Cachorro.Items.Add((new TextBlock() { Text = Cachorro.GetRacaNome(Raca.Akita), FontSize = 18 }));
    */
        }

        bool ValidaNome()
        {
            bool NomeValido = false;
            if (InputBox_Nome.Text != "")
            {
                NomeValido = true;
                Globais.MeusDados.CachorroNome = InputBox_Nome.Text;
            }
            Botao_Proximo.Enabled = true;
            return NomeValido;
        }

        private bool ValidaIdade()
        {
            bool IdadeValida = false;
            if (InputBox_Ano.Text != "")
            {
                if (InputBox_Ano.Text.Length == 4)
                {
                    try
                    {
                        Globais.MeusDados.CachorroAnoNascimento = int.Parse(InputBox_Ano.Text);
                        IdadeValida = true;
                    }
                    catch (FormatException)
                    {
                        // faz nada
                    }

                }
            }
            Botao_Proximo.Enabled = true;
            return IdadeValida;
        }


        private async System.Threading.Tasks.Task<bool> ValidaLocalizacaoAsync()
        {
            bool Valido = false;
            GoogleMaps GetEndereco = new GoogleMaps();
            if (InputBox_Endereco.Text.Length > 5)
            {
                GeoCode geo = await GetEndereco.GetGeocodeAsync(InputBox_Endereco.Text);
                if (geo.Latitude != 0 && geo.Longitude != 0)
                {
                    Globais.MeusDados.CachorroLatitude = geo.Latitude;
                    Globais.MeusDados.CachorroLongitude = geo.Longitude;
                    Valido = true;
                }
            }
            Botao_Proximo.Enabled = true;
            return Valido;
        }

    }
}
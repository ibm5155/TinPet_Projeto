namespace TinPet_Projeto.Models
{
    public enum Genero
    {
        GeneroNaoDefinido,
        Feminino,
        Masculino
    }


    public class Cachorro
    {
        /*Modelo generico de classe cachorro, cada projeto vai extender ele para suportar peculiaridades de cada uma como imagens*/
        #region variaveis globais
        public int ID { get; private set; }
        public string Nome { get; private set; }
        public Genero genero { get; private set; }
        public double[] Regiao { get; private set; } = new double[2];/*Coordenadas XY de onde o cachorro foi registrado*/
        public string Imagem { get; private set; } /*URL ou URI de onde se encontra a foto do cachorro*/

        /*Variavel foto está extendida em cadas projeto*/
        #endregion

        public Cachorro()
        {
            /*Inicializador*/
            Nome = "";
            ID = GeradorID.GetNovaID();
            genero = Genero.GeneroNaoDefinido;
            Regiao[0] = 0;
            Regiao[1] = 0;
        }

        public void SetRegiao(double X, double Y)
        {
            Regiao[0] = X;
            Regiao[1] = Y;
        }

        public void SetGenero(Genero genero)
        {
            this.genero = genero;
        }

        public void SetNome(string Nome)
        {
            this.Nome = Nome;
        }

        public async void AddImagem(string EndInternet)
        {
            Imagem = EndInternet;
        }



    }
}

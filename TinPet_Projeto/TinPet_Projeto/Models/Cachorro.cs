using SQLite;

namespace TinPet_Projeto.Models
{
    public enum TipoGenero
    {
        GeneroNaoDefinido,
        Feminino,
        Masculino
    }

    public enum Raca
    {
        Nao_Definida,
        Vira_Lata,
        Husky_Siberiano,
        Shih_Tzu,
        Chow_Chow,
        Golden_Retriever,
        Dalmatas,
        Beagle,
        Sao_Bernardo,
        Samoieda,
        Luluda_Pomerania,
        Akita
    }



    [Table("Cachorro")]
    public class Cachorro
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public TipoGenero Genero { get; set; }
        public double Regiao_Latitude { get; set; }
        public double Regiao_Longitude { get; set; }
        public byte[] Imagem { get; set; } /*Arquivo Imagem*/
        public Raca Raca { get; set; }
        public int AnoNascimento { get; set; }

        public static string GetRacaNome(Raca raca)
        {
            return raca.ToString().Replace('_', ' ');
        }
    }
}

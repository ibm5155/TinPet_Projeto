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
        NaoDefinida,
        ViraLata,
        HuskySiberiano,
        ShihTzu,
        ChowChow,
        GoldenRetriever,
        Dalmatas,
        Beagle,
        SaoBernardo,
        Samoieda,
        LuludaPomerania,
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
    }
}

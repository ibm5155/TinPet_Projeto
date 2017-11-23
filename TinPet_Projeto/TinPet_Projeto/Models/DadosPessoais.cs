using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.Models
{
    [Table("DadosPessoais")]
    public class DadosPessoais
    {
        [PrimaryKey]
        public string IdDono { get; set; }
        public int CachorroId { get; set; }
        public string CachorroNome { get; set; }
        public TipoGenero CachorroGenero { get; set; }
        public Raca CachorroRaca { get; set; }
        public double CachorroLatitude { get; set; }
        public double CachorroLongitude { get; set; }
        public byte[] Foto { get; set; }//cachorro
        public int CachorroAnoNascimento { get; set; }
    }
}

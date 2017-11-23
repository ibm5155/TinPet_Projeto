using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinPet_Projeto.Models
{
    [Table("FiltroUsuario")]
    public class FiltroUsuario
    {
        [PrimaryKey]
        public string IdDono { get; set; }
        public TipoGenero CachorroGenero { get; set; }
        public int DistanciaMaxima { get; set; } /*Km*/
        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }
    }
}

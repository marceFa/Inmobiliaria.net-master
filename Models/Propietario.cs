using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models
{
    public class Propietario
    {
        
        [Key]
        [Display(Name = "Código Propietario")]
        public int id { get; set; }
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        [Required, DataType(DataType.Password)]
        public string Clave { get; set; }


    }
}


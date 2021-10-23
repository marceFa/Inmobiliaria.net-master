using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {
        [Key]
        [Display(Name = "Código Inquilino")]
        public int idInquilino { get; set; }
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }

        [Display(Name = "Lugar de Trabajo")]
        public string LugarDeTrabajo { get; set; }

        [Display(Name = "Nombre Garante")]
        public string nombreGarante { get; set; }

        [Display(Name = "Tel Garante")]
        public string telefonoGarante { get; set; }
    }
}

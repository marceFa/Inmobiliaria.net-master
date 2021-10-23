using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        [Key]
        [Display(Name = "Código Inmueble")]
        public int idInmueble{ get; set; }
        
        public int id { get; set; }
        [ForeignKey("id")]

        public Propietario Propietarios { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }
        public string Uso { get; set; }
        public int Ambientes { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        [NotMapped]
        public string Imagen { get; set; }
    }
}

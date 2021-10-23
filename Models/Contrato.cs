using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models
{
    public class Contrato
    {
        [Key]
        [Display(Name = "Código Contrato")]
        public int idContrato { get; set; }
                
        public int idInquilino { get; set; }
        [ForeignKey("idInquilino")]

        public Inquilino Inquilinos { get; set; }

        public int idInmueble { get; set; }
        [ForeignKey("idInmueble")]


        public Inmueble Inmuebles { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Final")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "$ Alquiler")]
        public decimal MontoAlquiler { get; set; }
        public bool Vigente { get; set; }
    }
}

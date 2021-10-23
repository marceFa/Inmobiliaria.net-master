using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria.Models
{
    public class Pago
    {
        [Key]
        [Display(Name = "Código Pago")]
        public int idPago { get; set; }
        
        public int Numero { get; set; }

        public int idContrato { get; set; }
        [ForeignKey("idContrato")]

        public Contrato Contratos { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        public DateTime FechaDePago { get; set; }
        public decimal Importe { get; set; }
    }
}

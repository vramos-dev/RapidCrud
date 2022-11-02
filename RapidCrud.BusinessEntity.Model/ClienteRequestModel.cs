using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.BusinessEntity.Model
{
    public class ClienteRequestModel
    {
        [Required]        
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Dni ingresado es incorrecto.")]
        public string Dni { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Telefono ingresado es incorrecto.")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email ingresado es incorrecto.")]
        public string Email { get; set; }
    }
}

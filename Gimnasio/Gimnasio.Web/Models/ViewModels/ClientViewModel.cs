using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gimnasio.Web.Models.ViewModels
{
    public class ClientViewModel
    {
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        [Display(Name = "Edad")]
        public int Age { get; set; }
        [Display(Name = "Tipo")]
        public string Type { get; set; }
        [Display(Name = "Correo")]
        public string Email { get; set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public DateTime Admission { get; set; }
        [Display(Name = "Entrenador")]
        public int CoachId { get; set; }
        [Display(Name = "Nutriólogo")]
        public int NutritionistId { get; set; }
    }
}
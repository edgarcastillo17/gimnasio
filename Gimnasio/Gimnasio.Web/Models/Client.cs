using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gimnasio.Web.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Edad")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime Admission { get; set; }
        public int CoachId { get; set; }
        [ForeignKey("CoachId")]
        public Coach Coach { get; set; }
        public int NutritionistId { get; set; }
        [ForeignKey("NutritionistId")]
        public Nutritionist Nutritionist { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
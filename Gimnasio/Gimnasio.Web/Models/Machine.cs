using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gimnasio.Web.Models
{
    public class Machine
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        [Display(Name = "Fecha de Adquisición")]
        public DateTime Purchase { get; set; }
        [Required]
        [Display(Name = "Imagen")]
        public string Image { get; set; }
    }
}
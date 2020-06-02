using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gimnasio.Web.Models
{
    public class Coach
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Edad")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Especialidad")]
        public string Specialty { get; set; }
        [Required]
        [Display(Name = "Imagen")]
        public string Image { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
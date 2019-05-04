using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class Worker
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Worker Name")]
        public string worker_name { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public int salary { get; set; }
        [Required]
        [Display(Name = "Worker Status")]
        public string worker_status { get; set; }
        
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Cell Number")]
        [Required]
        public string CellNumber { get; set; }


        public Order Order { get; set; }
        public int? OrderId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
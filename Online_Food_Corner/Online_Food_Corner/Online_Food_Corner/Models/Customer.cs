using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class Customer
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string customer_name { get; set; }
        [Required]
        [Display(Name = "Customer Address")]
        public string customer_address { get; set; }
        [Required]
        [Display(Name = "Cell Number")]
        public string cell_no { get; set; }
        [Required]
        [Display(Name = "Customer Email")]
        public string customer_email { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
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

        public Order Order { get; set; }
        public int? OrderId { get; set; }
           
    }
}
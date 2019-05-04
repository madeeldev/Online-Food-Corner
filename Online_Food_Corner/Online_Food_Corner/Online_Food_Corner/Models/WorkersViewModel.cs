using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class WorkersViewModel
    {
        public int worker_id { get; set; }
        [Required]
        [Display(Name = "Worker Name")]
        public string worker_name { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public int salary { get; set; }
        [Required]
        [Display(Name = "Worker Status")]
        public string worker_status { get; set; }
        [Required]
        [Display(Name = "Order Id")]
        public int order_id { get; set; }
    }
}
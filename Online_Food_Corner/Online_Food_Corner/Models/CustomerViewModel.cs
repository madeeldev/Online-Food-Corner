using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class CustomerViewModel
    {
        public int customer_id { get; set; }
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
        [Required]
        [Display(Name = "Customer Order Id")]
        public int customer_order_id { get; set; }
        //[Required]
        [Display(Name = "Product Id")]
        public Nullable<int> product_id { get; set; }
    }
}
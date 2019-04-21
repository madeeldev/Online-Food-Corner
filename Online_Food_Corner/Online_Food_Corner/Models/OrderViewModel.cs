using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class OrderViewModel
    {
        public int order_id { get; set; }
        [Required]
        [Display(Name = "Customer Id")]
        public int customer_id { get; set; }
        [Required]
        [Display(Name = "Total Price")]
        public int total_price { get; set; }
        [Required]
        [Display(Name = "Timing")]
        public System.DateTime timing { get; set; }
        [Required]
        [Display(Name = "Delivery Mode")]
        public string delivery_mode { get; set; }
        //[Required]
        [Display(Name = "Product Id")]
        public Nullable<int> product_id { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public System.DateTime order_date { get; set; }
    }
}
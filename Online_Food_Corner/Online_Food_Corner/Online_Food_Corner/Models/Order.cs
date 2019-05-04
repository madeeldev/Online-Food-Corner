using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class Order
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Total Price")]
        public int total_price { get; set; }
        [Required]
        [Display(Name = "Timing")]
        public System.DateTime timing { get; set; }
        [Required]
        [Display(Name = "Delivery Mode")]
        public string delivery_mode { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public System.DateTime order_date { get; set; }
        [Required]
        [Display(Name = "Order Status")]
        public String Status { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
}
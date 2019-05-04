using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class DeliveryViewModel
    {
        public int delivery_id { get; set; }
        [Required]
        [Display(Name = "Order Id")]
        public int order_id { get; set; }
        [Required]
        [Display(Name = "Delivery Status")]
        public int delivery_status { get; set; }
    }
}
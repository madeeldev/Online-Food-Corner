using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Food_Corner.Models
{
    public class ProductViewModel
    {
        //[Display(Name = "Product Id")]
        public int product_id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string product_name { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public string product_type { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public int product_Price { get; set; }
        [Required]
        [Display(Name = "Product Picture")]
        public byte[] product_picture { get; set; }
        [Required]
        [Display(Name = "Product Quantity")]
        public int product_quantity { get; set; }
    }
}
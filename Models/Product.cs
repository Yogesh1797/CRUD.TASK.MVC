using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CRUD.TASK.MVC.Models
{
    public class Product
    {
        public Product()
        {

        }
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        // reference navigation property
        public virtual Category Category { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
    }
}
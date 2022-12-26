using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.TASK.MVC.Models
{
    public class Category
    {
        public Category()
        {

        }
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public virtual ICollection<Product> Products { get; set; } // Navigation property 
    }
}
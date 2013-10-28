using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdvancedWebForms.ModelBinding
{
    public class Widget
    {
        [Required, StringLength(100)] 
        public string Name { get; set; }

        [StringLength(10000), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
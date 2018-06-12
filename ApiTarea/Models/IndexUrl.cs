using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class IndexUrl { 

        [Required]
        public string Url { get; set; }
    }

}


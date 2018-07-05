using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Le champ nom est obligatoire")]
        [MinLength(5, ErrorMessage = "Le nom entré est trop court")]
        //[RegularExpression("^[a-z]+$", ErrorMessage ="Caractère invalide")]
        public string Name { get; set; }

        //public ICollection<ToDo> ToDos { get; set; }
    }
}
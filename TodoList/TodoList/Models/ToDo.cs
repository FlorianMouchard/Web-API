using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    [Table(name:"ToDos")]
    public class ToDo : BaseModel
    {
        //public int ID { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class CategoriesController : ApiController
    {
        //ouverture de la connexion à la base de données
        private TodoListDbContext db = new TodoListDbContext();

        public List<Category> GetCategories()
        {
            return (from x in db.Categories
                    select x).ToList();
                    
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var cat = db.Categories.SingleOrDefault(x => x.ID == id);
            if(cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }
            var updateCat = db.Categories.SingleOrDefault(x => x.ID == id);
            if (updateCat == null)
            {
                return NotFound();
            }
            updateCat.Name = category.Name;
            db.SaveChanges();
            return Ok(category);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var deletingCat = db.Categories.SingleOrDefault(x => x.ID == id);
            if(deletingCat == null)
            {
                return NotFound();
            }
            db.Categories.Remove(deletingCat);
            db.SaveChanges();
            return Ok("Element supprimé");
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if(! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        //réécriture de la méthode dispose pour libérer en mémoire le DbContext
        //et donc la connexion
        //méthode dispose appelé lorsque IIS n'utilise plus le controller
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose(); //libère le db context
            base.Dispose(disposing);
        }
    }
}

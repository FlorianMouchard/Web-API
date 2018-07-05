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

        //retourne la liste des catégories
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;                    
        }

        //retourne la catégorie suivant l'ID
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var cat = db.Categories.Find(id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var deletingCat = db.Categories.Find(id);
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

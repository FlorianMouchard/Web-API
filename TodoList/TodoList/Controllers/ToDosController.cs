using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    [RoutePrefix("api/todos")]
    public class ToDosController : ApiController
    {
        private TodoListDbContext db = new TodoListDbContext();
        /// <summary>
        /// Retourne la liste des Todos
        /// </summary>
        /// <remarks>
        /// Il fait chaud!!!
        /// </remarks>        
        /// <returns></returns>
        // GET: api/ToDos
        public IQueryable<ToDo> GetToDos()
        {
            return db.ToDos.Where(x => !x.Deleted).Include(x => x.Category);
        }

        //GET: api/Todos/search
        [Route("search")]
        public IQueryable<ToDo> GetSearch(string name = "", int? categoryID = null, bool? done = null, DateTime? deadline = null)
        {
            var query = db.ToDos.Where(x => !x.Deleted);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name)).Include(x =>x.Category);
            if (categoryID != null)
                query = query.Where(x => x.CategoryID == categoryID);
            if (done != null)
                query = query.Where(x => x.Done == done);
            if (deadline != null)
                query = query.Where(x => x.DeadLine == deadline);

            return query;
        }


        // GET: api/ToDos/5
        [Route("{id:int}")]
        [ResponseType(typeof(ToDo))]
        public IHttpActionResult GetToDo(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        [Route("{name}")]
        [ResponseType(typeof(ToDo))]
        public IQueryable<ToDo> GetToDo(string name)
        {
            return db.ToDos.Where(x => !x.Deleted && x.Name.Contains(name));
        }

        // PUT: api/ToDos/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToDo(int id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.ID)
            {
                return BadRequest();
            }

            db.Entry(toDo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ToDos
        [ResponseType(typeof(ToDo))]
        public IHttpActionResult PostToDo(ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToDos.Add(toDo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = toDo.ID }, toDo);
        }

        // DELETE: api/ToDos/5
        [Route("{id:int}")]
        [ResponseType(typeof(ToDo))]
        public IHttpActionResult DeleteToDo(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return NotFound();
            }

            //db.ToDos.Remove(toDo);
            toDo.Deleted = true;
            toDo.DeletedAt = DateTime.Now;
            db.Entry(toDo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(toDo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoExists(int id)
        {
            return db.ToDos.Count(e => e.ID == id) > 0;
        }
    }
}
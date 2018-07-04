using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace TodoList.Controllers
{
    public class TestModel
    {
        public int ID { get; set; }
        public string Commentaire { get; set; }
    }
    public class TestController : ApiController
    {
        //GET: api/test
        public List<TestModel> GetTests()
        {
           
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            return (from x in doc.Descendants("test")
                    select new TestModel
                    {
                        ID = int.Parse(x.Element("ID").Value),
                        Commentaire = x.Element("Commentaire").Value
                    }).ToList();

            /*return doc.Descendants("Test").Select(x => new TestModel
            {
                ID = int.Parse(x.Element("ID").Value),
                Commentaire = x.Element("Commentaire").Value
            }).ToList();
            */

        }

        //GET: api/test/42
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult GetTest(int id)
        {
            /*if (id == 42)
            {
                return NotFound();
            }
            return Ok(new TestModel { ID = id, Commentaire = "Bravo" });*/
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));            
            //var test = doc.Descendants("Test").SingleOrDefault(
            //    x => int.Parse(x.Element("ID").Value) == id);

            TestModel test = null;

            var elements = doc.Root.Elements();
            foreach (var item in elements)
            {
                if (int.Parse(item.Element("ID").Value) == id)
                {
                    test = new TestModel
                    {
                        ID = int.Parse(item.Element("ID").Value),
                        Commentaire = item.Element("Commentaire").Value
                    };
                }
            }
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        //POST: api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PostTest(TestModel test)
        {
            if (test.ID != 0)
            {
                return BadRequest();
            }
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            int lastID = doc.Descendants("test").Max(x => int.Parse(x.Element("ID").Value));
            //lastID++;
            test.ID = lastID + 1;

            XElement element = new XElement("test",
                new XElement("ID", test.ID),
                new XElement("Commentaire", test.Commentaire)
                );
            doc.Element("tests").Add(element);
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            return CreatedAtRoute("DefaultApi", new { id = test.ID }, test);
        }
        //PUT: api/test        
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PutTest(int id, TestModel test)
        {
            
            if (id != test.ID)
            {
                return BadRequest();
            }
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            var element = doc.Descendants("test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (element == null)
            {
                return NotFound();
            }            
            element.Element("Commentaire").SetValue(test.Commentaire);
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            return Ok(test);
        }
        //DELETE: api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult DeleteTest(int id)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            var deletingElement = doc.Descendants("test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (deletingElement == null)
            {
                return NotFound();
            }
            deletingElement.Remove();
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            return Ok("élément supprimer");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Mirnes.Models;

namespace Mirnes.Controllers
{
    public class KorisnikController : ApiController
    {
        private dbmirnesEntities db = new dbmirnesEntities();

        // GET api/Korisnik
        public IEnumerable<Korisnik> GetKorisniks()
        {
            return db.Korisnik.AsEnumerable();
        }

        // GET api/Korisnik/5
        public Korisnik GetKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return korisnik;
        }

        // PUT api/Korisnik/5
        public HttpResponseMessage PutKorisnik(int id, Korisnik korisnik)
        {
            if (ModelState.IsValid && id == korisnik.Id)
            {
                db.Entry(korisnik).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Korisnik
        public HttpResponseMessage PostKorisnik(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                db.Korisnik.Add(korisnik);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, korisnik);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = korisnik.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Korisnik/5
        public HttpResponseMessage DeleteKorisnik(int id)
        {
            Korisnik korisnik = db.Korisnik.Find(id);
            if (korisnik == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Korisnik.Remove(korisnik);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
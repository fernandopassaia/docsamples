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
using CRUDAPI.Models;

namespace CRUDAPI.fonts
{
    public class EmaployeeMastersController : ApiController
    {
        private WebApiDbEntities db = new WebApiDbEntities();

        //// GET: api/EmaployeeMasters
        //public IQueryable<EmaployeeMaster> GetEmaployeeMasters()
        //{
        //    return db.EmaployeeMasters;
        //}

        //// GET: api/EmaployeeMasters/5
        //[ResponseType(typeof(EmaployeeMaster))]
        //public IHttpActionResult GetEmaployeeMaster(string id)
        //{
        //    int ID = Convert.ToInt32(id);
        //    EmaployeeMaster emaployeeMaster = db.EmaployeeMasters.Find(ID);
        //    if (emaployeeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(emaployeeMaster);
        //}

        //// PUT: api/EmaployeeMasters/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEmaployeeMaster(int id, EmaployeeMaster emaployeeMaster)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != emaployeeMaster.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(emaployeeMaster).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmaployeeMasterExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/EmaployeeMasters
        //[ResponseType(typeof(EmaployeeMaster))]
        //public IHttpActionResult PostEmaployeeMaster(EmaployeeMaster emaployeeMaster)
        //{
        //    var error = ModelState.Values.SelectMany(a => a.Errors);


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.EmaployeeMasters.Add(emaployeeMaster);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = emaployeeMaster.ID }, emaployeeMaster);
        //}

        //// DELETE: api/EmaployeeMasters/5
        //[ResponseType(typeof(EmaployeeMaster))]
        //public IHttpActionResult DeleteEmaployeeMaster(int id)
        //{
        //    EmaployeeMaster emaployeeMaster = db.EmaployeeMasters.Find(id);
        //    if (emaployeeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    db.EmaployeeMasters.Remove(emaployeeMaster);
        //    db.SaveChanges();

        //    return Ok(emaployeeMaster);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EmaployeeMasterExists(int id)
        //{
        //    return db.EmaployeeMasters.Count(e => e.ID == id) > 0;
        //}
    }
}
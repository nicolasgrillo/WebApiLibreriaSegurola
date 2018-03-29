using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiLibreriaSegurola.DL.Entities;
using WebApiLibreriaSegurola.DL.Mappers;

namespace WebApiLibreriaSegurola.Controllers
{
    public class SheetsController : ApiController
    {
        private LibreriaSegurolaContext db = new LibreriaSegurolaContext();

        // GET: api/Sheets
        public IQueryable<Sheet> GetSheets()
        {
            return db.Sheets;
        }

        // GET: api/Sheets/5
        [ResponseType(typeof(Sheet))]
        public async Task<IHttpActionResult> GetSheet(int id)
        {
            Sheet sheet = await db.Sheets.FindAsync(id);
            if (sheet == null)
            {
                return NotFound();
            }

            return Ok(sheet);
        }

        // PUT: api/Sheets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSheet(int id, Sheet sheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sheet.SheetID)
            {
                return BadRequest();
            }

            db.Entry(sheet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SheetExists(id))
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

        // POST: api/Sheets
        [ResponseType(typeof(Sheet))]
        public async Task<IHttpActionResult> PostSheet(Sheet sheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sheets.Add(sheet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sheet.SheetID }, sheet);
        }

        // DELETE: api/Sheets/5
        [ResponseType(typeof(Sheet))]
        public async Task<IHttpActionResult> DeleteSheet(int id)
        {
            Sheet sheet = await db.Sheets.FindAsync(id);
            if (sheet == null)
            {
                return NotFound();
            }

            db.Sheets.Remove(sheet);
            await db.SaveChangesAsync();

            return Ok(sheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SheetExists(int id)
        {
            return db.Sheets.Count(e => e.SheetID == id) > 0;
        }
    }
}
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
    public class ReceiptsController : ApiController
    {
        private LibreriaSegurolaContext db = new LibreriaSegurolaContext();

        // GET: api/Receipts
        public IQueryable<Receipt> GetReceipts()
        {
            return db.Receipts;
        }

        // GET: api/Receipts/5
        [ResponseType(typeof(Receipt))]
        public async Task<IHttpActionResult> GetReceipt(int id)
        {
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            return Ok(receipt);
        }

        // PUT: api/Receipts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReceipt(int id, Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receipt.ReceiptID)
            {
                return BadRequest();
            }

            db.Entry(receipt).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        [ResponseType(typeof(Receipt))]
        public async Task<IHttpActionResult> PostReceipt(Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Receipts.Add(receipt);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = receipt.ReceiptID }, receipt);
        }

        // DELETE: api/Receipts/5
        [ResponseType(typeof(Receipt))]
        public async Task<IHttpActionResult> DeleteReceipt(int id)
        {
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            db.Receipts.Remove(receipt);
            await db.SaveChangesAsync();

            return Ok(receipt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceiptExists(int id)
        {
            return db.Receipts.Count(e => e.ReceiptID == id) > 0;
        }
    }
}
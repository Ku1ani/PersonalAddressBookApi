using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalAddressBook.Data;
using PersonalAddressBook.Model;

namespace PersonalAddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactTable(int currentPage, int pageSize, string Orderby, string Search, DateTime StartDate, DateTime EndDate)
        {
            var query = from s in _context.ContactTable
                        select s;

            if (!String.IsNullOrEmpty(Search))
            {
                query = query.Where(s => s.FirstName.Contains(Search)
                                       || s.Surname.Contains(Search)
                                       || s.Tel.Contains(Search));
            }

            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                query = query.Where(s => s.UpdatedDate >= StartDate && s.UpdatedDate <= EndDate);
            }

            switch (Orderby)
            {
                case "FirstName desc":
                    query = query.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName asc":
                    query = query.OrderBy(s => s.FirstName);
                    break;
                case "Surname desc":
                    query = query.OrderByDescending(s => s.Surname);
                    break;
                case "Surname asc":
                    query = query.OrderBy(s => s.Surname);
                    break;    
            }

            var entries = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();
            return entries;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.ContactTable.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.ContactTable.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.ContactTable.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.ContactTable.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.ContactTable.Any(e => e.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PersonalAddressBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalAddressBook.Data
{
    public class ContactDbContext: DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> ContactTable { get; set; }
    }
}

using Contact.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Contact.Core
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ContactCustomer> ContactCustomers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}

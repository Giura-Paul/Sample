using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data
{
    public class PersonsContext : DbContext
    {
        public PersonsContext(DbContextOptions<PersonsContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SCQLV87\\SQLEXPRESS;Database=PersonsDb;Trusted_Connection=True;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(person => person.Id);
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(personEntity => personEntity.FirstName)
                      .HasMaxLength(255);

                entity.Property(personEntity => personEntity.LastName)
                      .HasMaxLength(255);
            });
        }
    }
}

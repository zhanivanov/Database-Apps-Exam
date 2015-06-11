using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_First_Phonebook.Phonebook.Models;

namespace Code_First_Phonebook.Phonebook.Data
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext()
            : base("PhonebookDb")
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Phone> Phones { get; set; }
    }
}

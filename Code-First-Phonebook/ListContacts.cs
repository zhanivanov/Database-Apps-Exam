using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_First_Phonebook.Migrations;
using Code_First_Phonebook.Phonebook.Data;
using Code_First_Phonebook.Phonebook.Models;

namespace Code_First_Phonebook
{
    class ListContacts
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookContext, Configuration>());

            var context = new PhonebookContext();

            var contacts = context.Contacts.Select(c => new
            {
                Name = c.Name,
                Emails = c.Emails,
                Phones = c.Phones
            }).ToList();

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.Name);
                foreach (var email in contact.Emails)
                {
                    Console.WriteLine(email.EmailAdress);
                }
                foreach (var phone in contact.Phones)
                {
                    Console.WriteLine(phone.PhoneNumber);
                }
            }
        }
    }
}

using Code_First_Phonebook.Phonebook.Data;
using Code_First_Phonebook.Phonebook.Models;

namespace Code_First_Phonebook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhonebookContext context)
        {
            if (!context.Database.Exists())
            {
                var peter = new Contact
                {
                    Name = "Peter Ivanov",
                    Position = "CTO",
                    Company = "Smart Ideas",
                    Emails =
                    {
                        new Email() {EmailAdress = "peter@gmail.com"},
                        new Email() {EmailAdress = "peter_ivanov@yahoo.com"}
                    },
                    Phones =
                    {
                        new Phone() {PhoneNumber = "+359 2 22 22 22"},
                        new Phone() {PhoneNumber = "+359 88 77 88 99"}
                    },
                    Url = "http://blog.peter.com",
                    Notes = "Friend from school"
                };

                var maria = new Contact
                {
                    Name = "Maria",
                    Phones =
                    {
                        new Phone() {PhoneNumber = "+359 22 33 44 55"}
                    }
                };

                var angie = new Contact
                {
                    Name = "Angie Stanton",
                    Emails =
                    {
                        new Email() {EmailAdress = "info@angiestanton.com"}
                    },
                    Url = "http://angiestanton.com"
                };

                context.Contacts.Add(peter);
                context.Contacts.Add(maria);
                context.Contacts.Add(angie);

                context.SaveChanges();
            }
        }
    }
}

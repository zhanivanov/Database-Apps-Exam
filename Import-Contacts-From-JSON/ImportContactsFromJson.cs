using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Code_First_Phonebook.Phonebook.Data;
using Code_First_Phonebook.Phonebook.Models;

namespace Import_Contacts_From_JSON
{
    class ImportContactsFromJson
    {
        static void Main()
        {
            var context = new PhonebookContext();

            var json = File.ReadAllText("../../contacts.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var contacts = serializer.Deserialize<ContactDTO[]>(json);


            foreach (var contactDto in contacts)
            {
                ICollection<Phone> phones = new List<Phone>();
                if (contactDto.phones != null)
                {
                    foreach (var phone in contactDto.phones)
                    {
                        var phoneToAdd = new Phone
                        {
                            PhoneNumber = phone
                        };

                        phones.Add(phoneToAdd);
                    }
                }

                ICollection<Email> emails = new List<Email>();
                if (contactDto.emails != null)
                {
                    foreach (var email in contactDto.emails)
                    {
                        var emailToAdd = new Email
                        {
                            EmailAdress = email
                        };

                        emails.Add(emailToAdd);
                    }
                }

                if (contactDto.name != null)
                {
                    var contact = new Contact
                    {
                        Name = contactDto.name,
                        Phones = phones,
                        Emails = emails,
                        Notes = contactDto.notes,
                        Company = contactDto.company,
                        Position = contactDto.position,
                        Url = contactDto.site
                    };

                    context.Contacts.Add(contact);
                }
            }

            context.SaveChanges();
        }
    }
}

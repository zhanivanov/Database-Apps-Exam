using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Phonebook.Phonebook.Models
{
    public class Email
    {
        public int Id { get; set; }

        public string EmailAdress { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}

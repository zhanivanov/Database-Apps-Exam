﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Phonebook.Phonebook.Models
{
    public class Phone
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}

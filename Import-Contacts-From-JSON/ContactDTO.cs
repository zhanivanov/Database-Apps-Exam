using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Import_Contacts_From_JSON
{
    public class ContactDTO
    {
        public string name { get; set; }

        public string[] emails { get; set; }

        public string[] phones { get; set; }

        public string company { get; set; }

        public string position { get; set; }

        public string site { get; set; }

        public string notes { get; set; }
    }
}

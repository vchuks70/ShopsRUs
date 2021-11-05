﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Customer : BaseClass
    {
        public string Name { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }

         public ICollection<Invoice> Invoices { get; set; }
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }
         

    }
}

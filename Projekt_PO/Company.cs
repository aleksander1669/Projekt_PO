using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_PO
{
    public class Company : Customer
    {
        private string nip_number;
        public Company(string Name, string Phone, string Nip_Number) : base(Name, Phone)
        {
            nip_number = Nip_Number;
        }
    }
}

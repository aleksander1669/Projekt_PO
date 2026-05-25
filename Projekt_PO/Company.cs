using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_PO
{
    public class Company : Customer
    {
        private string nip;
        public Company(string Name, string Phone, string Nip) : base(Name, Phone)
        {
            nip = Nip;
        }
    }
}

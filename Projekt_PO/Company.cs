using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_PO
{
    public class Company : Customer
    {
        public string nip {  get; private set; }
        public Company(string Name, string Phone, string Nip) : base(Name, Phone)
        {
            nip = Nip;
        }
    }
}

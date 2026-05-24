using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_PO
{
    public class Private : Customer
    {
        private string surename;
        private string identification;

        public Private(string Name, string Phone, string Surename, string Identification) : base(Name, Phone)
        {
            surename = Surename;
            identification = Identification;
        }
    }
}

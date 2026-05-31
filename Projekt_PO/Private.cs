using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_PO
{
    public class Private : Customer
    {
        public string surename {  get; private set; }
        public string identification { get; private set; }

        public Private(string Name, string Phone, string Surename, string Identification) : base(Name, Phone)
        {
            surename = Surename;
            identification = Identification;
        }
        public override void Display()
        {
            Console.WriteLine($"|| Name: {name}");
            Console.WriteLine($"|| Surename: {surename}");
            Console.WriteLine($"|| Phone: {phone}");
            Console.WriteLine($"|| Identification: {identification}");
        }
    }
}

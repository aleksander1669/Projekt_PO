using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Customer
    {
        public string Name { get; private set; }
        public string Surename { get; private set; }
        public int Phone { get; private set; }
        public string Identification { get; private set; }

        [JsonConstructor]
        public Customer(string name, string surename, int phone, string identification)
        {
            Name = name;
            Surename = surename;
            Phone = phone;
            Identification = identification;
        }
        public void Display()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Surename: " + Surename);
            Console.WriteLine("Phone number: " + Phone);
            Console.WriteLine("Identification number: " + Identification);
        }
    }
}

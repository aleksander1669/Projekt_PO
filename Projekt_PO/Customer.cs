using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public interface ICustomer
    {
        void Display();
        void Rent();
    }
    public class Customer : ICustomer
    {
        public string name { get; private set; }
        public string phone { get; private set; }

        [JsonConstructor]
        public Customer(string Name, string Phone)
        {
            name = Name;
            phone = Phone;
        }
        public void Display()
        {
            Console.WriteLine($"Customer: {name} | Phone: {phone}");
        }

        public void Rent()
        {

        }
    }
}

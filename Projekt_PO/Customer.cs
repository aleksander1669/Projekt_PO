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
    public class Customer
    {
        public string name { get; private set; }
        public string phone { get; private set; }

        [JsonConstructor]
        public Customer(string Name, string Phone)
        {
            name = Name;
            phone = Phone;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Rent
    {
        public int Id { get; private set; }
        public Customer Renter { get; private set; }
        public Equipment Rented_Item { get; private set; }

        public DateTime Rental_Date { get; private set; }
        public DateTime Rental_Till { get; private set; }
        [JsonConstructor]
        public Rent(int id, Customer renter, Equipment rented_item, DateTime rental_date, DateTime rental_till)
        {
            Id = id;
            Renter = renter;
            Rented_Item = rented_item;
            Rental_Date = rental_date;
            Rental_Till = rental_till;
        }
        public void Return()
        {
            Rented_Item.Lendable(true);
        }
        public void Display_Rent()
        {
            Console.WriteLine("-----------------------------------------------Customer Information-----------------------------------------------");
            Console.WriteLine("ID: " + Id);
            Renter.Display();
            Console.WriteLine("Rented at : " + Rental_Date);
            Console.WriteLine("Rent till: " + Rental_Till);
            Console.WriteLine();
            Console.WriteLine("Rented item:");
            Rented_Item.Info_Short();
            Console.WriteLine("==================================================================================================================");
        }
    }
}

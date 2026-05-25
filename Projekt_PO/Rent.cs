using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public interface IRent
    {
        void Display();
        void Settle();
    }
    public class Rent : IRent
    {
        public int id { get; private set; }
        public Customer renter { get; private set; }
        public Equipment rented_item { get; private set; }

        public DateTime rental_date { get; private set; }
        public DateTime rental_till { get; private set; }
        [JsonConstructor]
        public Rent(int Id, Customer Renter, Equipment Rented_Item, DateTime Rental_Date, DateTime Rental_Till)
        {
            id = Id;
            renter = Renter;
            rented_item = Rented_Item;
            rental_date = Rental_Date;
            rental_till = Rental_Till;
        }
        public void Display()
        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine($"|| ID: {id} | Customer: {renter.name} ||");
            Console.WriteLine($"|| Equipment: {rented_item.name} ||");
            Console.WriteLine($"|| Rent date: {rental_date} | Rented till: {rental_till} ||");
            Console.WriteLine("==============================================================================================================");
        }
        public void Settle()
        {
            int days = (rental_till - rental_date).Days;

            if (days < 1)
            {
                days = 1;
            }

            Console.WriteLine("==========================================================================");
            Console.WriteLine("Rent settled:");
            Console.WriteLine($"Price per days rented: {rented_item.price} zł x {days} days = {rented_item.price * days} zł");
            Console.WriteLine($"Deposit: {rented_item.deposit} zł");
            Console.WriteLine($"Total cost: {rented_item.price * days + rented_item.deposit} zł");
            Console.WriteLine("==========================================================================");
            rented_item.Status_Change(false);
        }
    }
}

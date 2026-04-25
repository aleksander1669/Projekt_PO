using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Xml.Linq;

namespace Projekt_PO
{
    public class Equipment
    {
        protected int id;
        protected string type;
        protected DateTime time;
        protected bool lend;
        protected double price;
        public Equipment(int Id, string Type, DateTime Time, bool Lend, double Price)
        {
            id = Id;
            type = Type;
            time = Time;
            lend = Lend;
            price = Price;
        }
    }
    public class Bike : Equipment
    {
        private string maintenance;
    
    public Bike(int Id, string Type, DateTime Time, bool Lend, double Price, string Maintenance) : base(Id, Type, Time, Lend, Price)
        {
            maintenance = Maintenance;
        }
    public void Show_Info_Short_Bike()
        {
            string x = string.Empty;
            if (lend)
            {
                x = "Yes";
            } else {
                x = "No";
            }
            Console.WriteLine("=======================================================================");
            Console.WriteLine("ID: " + id + "|| Bike: " + type + "|| Lendable: " + x + "|| Price: " + price + " zł");
            Console.WriteLine("=======================================================================");
        }
    }
    public class Motorcycle : Equipment
    {
        private string maintenance;
        private DateTime inspection;
        private string plate;
        private int oli;
    public Motorcycle(int Id, string Type, DateTime Time, bool Lend, double Price, string Maintenance, DateTime Inspection, string Plate, int Oil) : base(Id, Type, Time, Lend, Price)
        {
            maintenance = Maintenance;
            inspection = Inspection;
            plate = Plate;
            oli = Oil;
        }
        public void Show_Info_Short_Motorcycle()
        {
            string x = string.Empty;
            if (lend)
            {
                x = "Yes";
            }
            else
            {
                x = "No";
            }
            Console.WriteLine("=======================================================================");
            Console.WriteLine("ID: " + id + "|| Motorcycle: " + type + "|| Lendable: " + x + "|| Price: " + price + " zł");
            Console.WriteLine("=======================================================================");
        }
    }
    public class Customer
    {
        private string name;
        private string surename;
        private string phone;
        private string identification;

        public Customer(string Name, string Surename, string Phone, string Identification)
        {
            name = Name;
            surename = Surename;
            phone = Phone;
            identification = Identification;
        }
    }
    public class Rental
    {
        public Customer renter;
        public Equipment rented_item;

        public DateTime rental_date;
        public string rental_till;
    public Rental(Customer Renter,  Equipment Rented_Item, string Rental_Till)
        {
            renter = Renter;
            rented_item = Rented_Item;
            rental_date = DateTime.Now;
            rental_till = Rental_Till;
        }
    }
}

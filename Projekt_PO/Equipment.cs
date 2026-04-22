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
        private string id;
        private string type;
        private string time;
        private string lend;
        private string price;

        private string name;
        private string surename;
        private string phone;
        private string identification;

        public Equipment(string Id, string Type, string Time, string Lend, string Price)
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
    
    public Bike(string Id, string Type, string Time, string Lend, string Price, string Maintenance) : base(Id, Type, Time, Lend, Price)
        {
            maintenance = Maintenance;
        }
    }
    public class Motorcycle : Equipment
    {
        private string maintenance;
        private string inspection;
        private string plate;
        private string oli;
    public Motorcycle(string Id, string Type, string Time, string Lend, string Price, string Maintenance, string Inspection, string Plate, string Oil) : base(Id, Type, Time, Lend, Price)
        {
            maintenance = Maintenance;
            inspection = Inspection;
            plate = Plate;
            oli = Oil;
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

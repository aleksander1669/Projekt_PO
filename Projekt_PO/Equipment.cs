using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Equipment
    {
        public int Id { get; private set; }
        public string Type { get; private set; }
        public DateTime Time { get; private set; }
        public bool Lend { get; private set; }
        public double Price { get; private set; }
        public double Deposit { get; private set; }
        [JsonConstructor]
        public Equipment(int id, string type, DateTime time, bool lend, double price, double deposit)
        {
            Id = id;
            Type = type;
            Time = time;
            Lend = lend;
            Price = price;
            Deposit = deposit;
        }
        public virtual void Info_Short()
        {
            string x = string.Empty;
            if (Lend)
            {
                x = "Yes";
            }
            else
            {
                x = "No";
            }
            Console.WriteLine("=====================================================================================================");
            Console.WriteLine("|| ID: " + Id + "|| Type/Name of Equipment: " + Type + "|| Lendable: " + x + "|| Price per day: " + Price + " zł + Deposit: " + Deposit + " zł ||");
        }
        public virtual void Info_All()
        {

        }
        public virtual double Count_Cost(int days)
        {
            double final_cost;

            final_cost = (Price * days) + Deposit;
            return final_cost;
        }
    }
    public class Bike : Equipment
    {
        public string Maintenance {  get; private set; }
        [JsonConstructor]
    public Bike(int Id, string Type, DateTime Time, bool Lend, double Price, double Deposit, string maintenance) : base(Id, Type, Time, Lend, Price, Deposit)
        {
            Maintenance = maintenance;
        }
        public override double Count_Cost(int days)
        {
            return base.Count_Cost(days);
        }
        public override void Info_Short()
        {
            base.Info_Short();
        }
    }
    public class Motorcycle : Equipment
    {
        public string Maintenance { get; private set; }
        public DateTime Inspection {  get; private set; }
        public string Plate { get; private set; }
        public int Oil { get; private set; }
        [JsonConstructor]
    public Motorcycle(int id, string type, DateTime time, bool lend, double price, double deposit, string maintenance, DateTime inspection, string plate, int oil) : base(id, type, time, lend, price, deposit)
        {
            Maintenance = maintenance;
            Inspection = inspection;
            Plate = plate;
            Oil = oil;
        }
        public override void Info_Short()
        {
            base.Info_Short();
            Console.WriteLine("|| Inspection: " + Inspection + " || Plate number: " + Plate + " || Oil life: " + Oil + " kilometers ||");
        }
        public override double Count_Cost(int days)
        {
            return base.Count_Cost(days);
        }
    }
    public class Customer
    {
        private string Name;
        private string Surename;
        private string Phone;
        private string Identification;

        public Customer(string name, string surename, string phone, string identification)
        {
            Name = name;
            Surename = surename;
            Phone = phone;
            Identification = identification;
        }
    }
    public class Rental
    {
        public Customer Renter;
        public Equipment Rented_Item;

        public DateTime Rental_Date;
        public string Rental_Till;
    public Rental(Customer renter,  Equipment rented_item, string rental_till)
        {
            Renter = renter;
            Rented_Item = rented_item;
            Rental_Date = DateTime.Now;
            Rental_Till = rental_till;
        }
    }
}

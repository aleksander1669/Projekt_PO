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
    [JsonDerivedType(typeof(Bike), typeDiscriminator: "rower")]
    [JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "motocykl")]
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
        public bool Can_be_Removed()
        {
            return Lend;
        }
        public virtual void Lendable(bool x)
        {
            Lend = x;
        }
        public virtual void Info_Short()
        {
            string x = string.Empty;
            if (Lend)
            {
                x = "Can be rented";
            }
            else
            {
                x = "Already rented";
            }
            Console.WriteLine("==================================================================================================================");
            Console.WriteLine("|| ID: " + Id + "|| Type/Name of Equipment: " + Type + "|| Rent status: " + x + "|| Price per day: " + Price + " zł + Deposit: " + Deposit + " zł ||");
        }
        public virtual void Info_All()
        {
            string x = string.Empty;
            if (Lend)
            {
                x = "Can be rented";
            } else
            {
                x = "Already rented";
            }
            Console.WriteLine("==================================================================================================================");
            Console.WriteLine("* ID: " + Id);
            Console.WriteLine("* Name/Type: " + Type);
            Console.WriteLine("* Added: " + Time);
            Console.WriteLine("* Lendable: " + x);
            Console.WriteLine("* Price: " + Price + " + Deposit: " + Deposit);

        }
        public virtual double Count_Cost(int days)
        {
            double final_cost;

            final_cost = (Price * days) + Deposit;
            return final_cost;
        }
    }
}

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
    public interface IEquipment
    {
        void Info_Short();
        void Info_Full();
        bool Can_Be_Removed();
    }
    [JsonDerivedType(typeof(Bike), typeDiscriminator: "bike")]
    [JsonDerivedType(typeof(Combustion_Vehicle), typeDiscriminator: "combustion")]
    [JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "motorcycle")]
    [JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
    public class Equipment : IEquipment
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public DateTime time { get; private set; }
        public bool rented { get; private set; }
        public double price { get; private set; }
        public double deposit { get; private set; }
        [JsonConstructor]
        public Equipment(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit)
        {
            id = Id;
            name = Name;
            time = Time;
            rented = Rented;
            price = Price;
            deposit = Deposit;
        }
        public virtual void Info_Short()
        {
            string x = string.Empty;
            if (rented)
            {
                x = "rented";
            } else
            {
                x = "not rented";
            }
            Console.WriteLine($"|| ID: {id} | Name: {name} | Status: {x} | Added: {time}");
        }
        public virtual void Info_Full()
        {
            string x = string.Empty;
            if (rented)
            {
                x = "rented";
            } else
            {
                x = "not rented";
            }
            Console.WriteLine("====================================================================================");
            Console.WriteLine($"|| ID: {id} | Name: {name} ||");
            Console.WriteLine($"|| Price: {price} | Deposit: {deposit} ||");
            Console.WriteLine($"|| Status: {x} | Added: {time} ||");
        }
        public virtual bool Can_Be_Removed()
        {
            if (rented)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

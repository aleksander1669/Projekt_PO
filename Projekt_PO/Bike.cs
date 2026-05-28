using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Bike : Equipment, IEquipment
    {
        public string maintenance { get; private set; }
        [JsonConstructor]
        public Bike(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit, string Maintenance) : base(Id, Name, Time, Rented, Price, Deposit)
        {
            maintenance = Maintenance;
        }
        public override void Info_Full()
        {
            base.Info_Full();
            Console.WriteLine($"|| Maintenance: {maintenance}");
            Console.WriteLine("====================================================================================");
        }
    }
}

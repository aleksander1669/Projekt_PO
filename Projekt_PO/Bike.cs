using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Bike : Equipment
    {
        public string Maintenance { get; private set; }
        [JsonConstructor]
        public Bike(int Id, string Type, DateTime Time, bool Lend, double Price, double Deposit, string maintenance) : base(Id, Type, Time, Lend, Price, Deposit)
        {
            Maintenance = maintenance;
        }
    }
}

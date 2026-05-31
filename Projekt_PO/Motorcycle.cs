using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Motorcycle : Combustion_Vehicle, IEquipment
    {
        public string style {  get; private set; }
        [JsonConstructor]
        public Motorcycle(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit, string Maintenance, DateTime Inspection, string Plate, int Oil, int Hp, string Fuel, string Gearbox, string Style) : base(Id, Name, Time, Rented, Price, Deposit, Maintenance, Inspection, Plate, Oil, Hp, Fuel, Gearbox)
        {
            style = Style;
        }
        public override void Info_Full()
        {
            base.Info_Full();
            Console.WriteLine($"|| Style: {style}");
            Console.WriteLine("====================================================================================");
        }
    }
}

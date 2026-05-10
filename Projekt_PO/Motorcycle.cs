using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Motorcycle : Equipment
    {
        public string Maintenance { get; private set; }
        public DateTime Inspection { get; private set; }
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
        public override void Lendable(bool x)
        {
            base.Lendable(x);
        }
        public override void Info_Short()
        {
            base.Info_Short();
            Console.WriteLine("|| Inspection: " + Inspection + " || Plate number: " + Plate + " || Oil life: " + Oil + " kilometers ||");
        }
        public override void Info_All()
        {
            base.Info_All();
            Console.WriteLine("* Oil: " + Oil);
            Console.WriteLine("* Inspection: " + Inspection);
            Console.WriteLine("* Plate number: " + Plate);
            Console.WriteLine("* Maintenance: " + Maintenance);
        }
        public override double Count_Cost(int days)
        {
            return base.Count_Cost(days);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Combustion_Vehicle : Equipment
    {
        public string maintenance { get; private set; }
        public DateTime inspection {  get; private set; }
        public string plate { get; private set; }
        public int oil { get; private set; }
        public int hp { get; private set; }
        public string fuel { get; private set; }
        public string gearbox { get; private set; }
        [JsonConstructor]
        public Combustion_Vehicle(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit, string Maintenance, DateTime Inspection, string Plate, int Oil, int Hp, string Fuel, string Gearbox) : base(Id, Name, Time, Rented, Price, Deposit)
        {
            maintenance = Maintenance;
            inspection = Inspection;
            plate = Plate;
            oil = Oil;
            hp = Hp;
            fuel = Fuel;
            gearbox = Gearbox;
        }
    }
}

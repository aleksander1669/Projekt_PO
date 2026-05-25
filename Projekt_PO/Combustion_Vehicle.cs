using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    public class Combustion_Vehicle : Equipment
    {
        private string maintenance;
        private DateTime inspection;
        string plate;
        int oil;
        int hp;
        string fuel;
        string gearbox;
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

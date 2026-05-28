using System;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
   
    public class Car : Combustion_Vehicle
    {
        public int doors { get; private set; }
        public int seats { get; private set; }
        public string body_type { get; private set; }

        [JsonConstructor]
        public Car(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit, string Maintenance, DateTime Inspection, string Plate, int Oil, int Hp, string Fuel, string Gearbox, int Doors, int Seats, string Body_Type)
            : base(Id, Name, Time, Rented, Price, Deposit, Maintenance, Inspection, Plate, Oil, Hp, Fuel, Gearbox)
        {
            doors = Doors;
            seats = Seats;
            body_type = Body_Type;
        }
        public override void Info_Full()
        {
            base.Info_Full();
            Console.WriteLine($"|| Doors: {doors}");
            Console.WriteLine($"|| Seats: {seats}");
            Console.WriteLine($"|| Body type: {body_type}");
            Console.WriteLine("====================================================================================");

        }
    }
}
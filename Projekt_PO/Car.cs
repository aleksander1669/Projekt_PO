using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Projekt_PO
{
    public class Car
    {
        int doors;
        int seats;
        string body_type;
        public Car(int Id, string Type, DateTime Time, bool Lend, double Price, double Deposit, string Maintenance, DateTime Inspection, string Plate, int Oil, int Hp, string Fuel, string Gearbox, int Doors, int Seats, string Body_Type) : base(Id, Type, Time, Lend, Price, Deposit, Maintenance, Inspection, Plate, Oil, Hp, Fuel, Gearbox)
        {
            doors = Doors;
            seats = Seats;
            body_type = Body_Type;
        }
    }
}

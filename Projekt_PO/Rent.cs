using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace Projekt_PO
{
    /// <summary>
    /// Interfejs definiujący operacje dla wypożyczeń.
    /// </summary>
    public interface IRent
    {
        /// <summary>Wypisuje szczegóły wypożyczenia na ekranie.</summary>
        void Display();
        /// <summary>Rozlicza wypożyczenie, obliczając koszty i zwalniając sprzęt.</summary>
        void Settle();
    }

    /// <summary>
    /// Klasa reprezentująca umowę wypożyczenia sprzętu przez klienta.
    /// Łączy obiekt klienta z obiektem sprzętu na określony czas.
    /// </summary>
    public class Rent : IRent
    {
        /// <summary>Unikalny identyfikator wypożyczenia w bazie.</summary>
        public int id { get; private set; }

        /// <summary>Klient, który wypożyczył sprzęt.</summary>
        public Customer renter { get; private set; }

        /// <summary>Sprzęt, który został wypożyczony.</summary>
        public Equipment rented_item { get; private set; }

        /// <summary>Data rozpoczęcia wypożyczenia.</summary>
        public DateTime rental_date { get; private set; }

        /// <summary>Deklarowana data zwrotu sprzętu.</summary>
        public DateTime rental_till { get; private set; }

        /// <summary>
        /// Konstruktor tworzący nowe przypisanie (wypożyczenie).
        /// </summary>
        /// <param name="Id">ID wypożyczenia</param>
        /// <param name="Renter">Obiekt przypisanego klienta</param>
        /// <param name="Rented_Item">Obiekt przypisanego sprzętu</param>
        /// <param name="Rental_Date">Data startu</param>
        /// <param name="Rental_Till">Planowana data zakończenia</param>
        [JsonConstructor]
        public Rent(int Id, Customer Renter, Equipment Rented_Item, DateTime Rental_Date, DateTime Rental_Till)
        {
            id = Id;
            renter = Renter;
            rented_item = Rented_Item;
            rental_date = Rental_Date;
            rental_till = Rental_Till;
        }
        public void Display()

        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine($"|| ID: {id}");
            renter.Display();
            rented_item.Info_Short();
            Console.WriteLine("==============================================================================================================");
        }

        public void Settle()
        {
            int days = (rental_till - rental_date).Days;
            if (days < 1) { days = 1; }

            int free_days = days / 7;
            int payable_days = days - free_days;

            Console.WriteLine("==========================================================================");
            Console.WriteLine("Rent settled:");

            if (free_days > 0)
            {
                Console.WriteLine($"DISCOUNT APPLIED! You get {free_days} day(s) for free.");
            }

            Console.WriteLine($"Price per days rented: {rented_item.price.ToString("F2")} zł x {payable_days} paid days = {(rented_item.price * payable_days).ToString("F2")} zł");
            Console.WriteLine($"Deposit: {rented_item.deposit.ToString("F2")} zł");

            double total_cost = (rented_item.price * payable_days) + rented_item.deposit;
            Console.WriteLine($"Total cost: {total_cost.ToString("F2")} zł");
            Console.WriteLine("==========================================================================");

            rented_item.Status_Change(false);
        }
        public static double operator +(double aktualna_kwota, Rent wypozyczenie)
        {
            int days = Math.Max(1, (wypozyczenie.rental_till - wypozyczenie.rental_date).Days);
            int free_days = days / 7;
            int payable_days = days - free_days;
            double cost = (wypozyczenie.rented_item.price * payable_days) + wypozyczenie.rented_item.deposit;

            if (wypozyczenie.renter is Company)
            {
                cost = cost * 0.90;
            }

            return aktualna_kwota + cost;
        }
        public static double SettleMultiple(
        List<Rent> rentyKlienta,
        List<Rent> glownaListaRent,
        List<Rent> historiaRent,
        List<Bike> bikeList,
        List<Motorcycle> motorcycleList,
        List<Car> carList)
        {
            double koncowy_rachunek = 0;
            Console.WriteLine($"\nFound {rentyKlienta.Count} active rent(s) for this customer:");
            Console.WriteLine("--------------------------------------------------------------------------");
            foreach (Rent r in rentyKlienta)
            {
                Console.WriteLine($"- ID: {r.id} | Item: {r.rented_item.name} | Rented: {r.rental_date.ToShortDateString()}");
            }
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine($"\nFound {rentyKlienta.Count} active rent(s). Settling all...");
            foreach (Rent r in rentyKlienta)
            {
                int days = Math.Max(1, (r.rental_till - r.rental_date).Days);
                int free_days = days / 7;

                if (free_days > 0)
                {
                    Console.WriteLine($"> Item ID: {r.rented_item.id} | DISCOUNT APPLIED! You get {free_days} day(s) for free.");
                }

                if (r.renter is Company)
                {
                    Console.WriteLine($"> Item ID: {r.rented_item.id} | B2B PROMO: 10% corporate discount applied!");
                }

                koncowy_rachunek = koncowy_rachunek + r;
                Bike returned_bike = bikeList.FirstOrDefault(b => b.id == r.rented_item.id);
                Motorcycle returned_motorcycle = motorcycleList.FirstOrDefault(b => b.id == r.rented_item.id);
                Car returned_car = carList.FirstOrDefault(b => b.id == r.rented_item.id);

                if (returned_bike != null)

                {
                    returned_bike.Status_Change(false);
                }
                else if (returned_motorcycle != null)
                {
                    returned_motorcycle.Status_Change(false);
                }
                else if (returned_car != null)
                {
                    returned_car.Status_Change(false);
                }

                historiaRent.Add(r);
                glownaListaRent.Remove(r);
            }
            return koncowy_rachunek;
        }

    }

}
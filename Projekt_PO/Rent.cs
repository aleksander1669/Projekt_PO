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

        public void Display() //komentarz do tej metody: Wypisuje szczegóły wypożyczenia w czytelny sposób, pokazując ID, dane klienta, nazwę sprzętu oraz daty wypożyczenia i zwrotu. Służy do szybkiego przeglądu informacji o danym wypożyczeniu.
        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine($"|| ID: {id} | Customer: {renter.name} ||");
            Console.WriteLine($"|| Equipment: {rented_item.name} ||");
            Console.WriteLine($"|| Rent date: {rental_date.ToShortDateString()} | Rented till: {rental_till.ToShortDateString()} ||");
            Console.WriteLine("==============================================================================================================");
        }

        public void Settle()
        {
            int days = (rental_till - rental_date).Days;
            if (days < 1) { days = 1; }

            Console.WriteLine("==========================================================================");
            Console.WriteLine("Rent settled:");
            Console.WriteLine($"Price per days rented: {rented_item.price.ToString("F2")} zł x {days} days = {(rented_item.price * days).ToString("F2")} zł");
            Console.WriteLine($"Deposit: {rented_item.deposit.ToString("F2")} zł");
            Console.WriteLine($"Total cost: {(rented_item.price * days + rented_item.deposit).ToString("F2")} zł");
            Console.WriteLine("==========================================================================");
            rented_item.Status_Change(false);
        }
    }
}

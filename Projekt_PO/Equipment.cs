using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projekt_PO
{
    /// <summary>
    /// Interfejs określający podstawowe akcje dla każdego sprzętu w wypożyczalni.
    /// </summary>
    public interface IEquipment
    {
        void Info_Short();
        void Info_Full();
        bool Can_Be_Removed();
        void Status_Change(bool change);
    }

    /// <summary>
    /// Abstrakcyjna klasa bazowa dla wszystkich sprzętów (rowerów, motocykli, samochodów).
    /// Zawiera wspólne właściwości takie jak cena, kaucja i status dostępności.
    /// </summary>
    [JsonDerivedType(typeof(Bike), typeDiscriminator: "bike")]
    [JsonDerivedType(typeof(Combustion_Vehicle), typeDiscriminator: "combustion")]
    [JsonDerivedType(typeof(Motorcycle), typeDiscriminator: "motorcycle")]
    [JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
    public class Equipment : IEquipment
    {
        /// <summary>Unikalny numer ID sprzętu.</summary>
        public int id { get; private set; }

        /// <summary>Nazwa lub marka sprzętu.</summary>
        public string name { get; private set; }

        /// <summary>Data dodania sprzętu do bazy.</summary>
        public DateTime time { get; private set; }

        /// <summary>Status wypożyczenia (true = wypożyczony, false = wolny).</summary>
        public bool rented { get; private set; }

        /// <summary>Koszt wypożyczenia za jeden dzień (w PLN).</summary>
        public double price { get; private set; }

        /// <summary>Kaucja zwrotna pobierana przy wypożyczeniu (w PLN).</summary>
        public double deposit { get; private set; }

        /// <summary>
        /// Konstruktor inicjalizujący podstawowe parametry sprzętu.
        /// </summary>
        [JsonConstructor]
        public Equipment(int Id, string Name, DateTime Time, bool Rented, double Price, double Deposit)
        {
            id = Id;
            name = Name;
            time = Time;
            rented = Rented;
            price = Price;
            deposit = Deposit;
        }

        /// <summary>
        /// Wyświetla skrócone informacje o sprzęcie w jednej linii.
        /// </summary>
        public virtual void Info_Short()
        {
            string x = rented ? "rented" : "not rented";
            Console.WriteLine($"|| ID: {id} | Name: {name} | Status: {x} | Added: {time.ToShortDateString()} ||");
        }

        /// <summary>
        /// Wyświetla pełne, wielolinijkowe informacje o sprzęcie.
        /// Zastosowano metodę wirtualną, aby klasy pochodne mogły ją nadpisać.
        /// </summary>
        public virtual void Info_Full()
        {
            string x = rented ? "rented" : "not rented";
            Console.WriteLine("====================================================================================");
            Console.WriteLine($"|| ID: {id}");
            Console.WriteLine($"|| Name: {name}");
            Console.WriteLine($"|| Price: {price.ToString("F2")}");
            Console.WriteLine($"|| Deposit: {deposit.ToString("F2")}");
            Console.WriteLine($"|| Status: {x}");
            Console.WriteLine($"|| Added: {time.ToShortDateString()}");

        }

        /// <summary>
        /// Sprawdza, czy sprzęt może zostać bezpiecznie usunięty z bazy danych.
        /// </summary>
        /// <returns>Zwraca false, jeśli sprzęt jest obecnie wypożyczony.</returns>
        public virtual bool Can_Be_Removed()
        {
            return !rented;
        }

        /// <summary>
        /// Zmienia status wypożyczenia sprzętu.
        /// </summary>
        /// <param name="change">Nowy status (true/false)</param>
        public void Status_Change(bool change)
        {
            rented = change;
        }
    }
}
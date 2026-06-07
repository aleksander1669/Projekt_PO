using System;

namespace Projekt_PO
{
    public class RentalException : Exception
    {
        public RentalException() { }
        public RentalException(string message) : base(message) { }
        public RentalException(string message, Exception inner) : base(message, inner) { }
    }

    public class InvalidFormatException : RentalException
    {
        public InvalidFormatException(string message) : base(message) { }
        public InvalidFormatException(string message, Exception inner) : base(message, inner) { }
    }

    public class ValueOutOfRangeException : RentalException
    {
        public ValueOutOfRangeException(string message) : base(message) { }
    }

    public class EmptyInputException : RentalException
    {
        public EmptyInputException(string message) : base(message) { }
    }

    public class InvalidContentException : RentalException
    {
        public InvalidContentException(string message) : base(message) { }
    }

    public class InvalidDateException : RentalException
    {
        public InvalidDateException(string message) : base(message) { }
    }

    public class InvalidLengthException : RentalException
    {
        public InvalidLengthException(string message) : base(message) { }
    }

    public class EquipmentNotFoundException : RentalException
    {
        public int EquipmentId { get; }
        public EquipmentNotFoundException(int id) : base($"Nie znaleziono sprzętu o ID = {id}")
        {
            EquipmentId = id;
        }
    }

    public class EquipmentAlreadyRentedException : RentalException
    {
        public int EquipmentId { get; }
        public EquipmentAlreadyRentedException(int id) : base($"Sprzęt o ID = {id} jest już wypożyczony")
        {
            EquipmentId = id;
        }
    }

    public class RentNotFoundException : RentalException
    {
        public int RentId { get; }
        public RentNotFoundException(int id) : base($"Nie znaleziono wypożyczenia o ID = {id}")
        {
            RentId = id;
        }
    }

    public class FileOperationException : RentalException
    {
        public string FileName { get; }
        public FileOperationException(string fileName, string message) : base($"Błąd operacji na pliku '{fileName}': {message}")
        {
            FileName = fileName;
        }
        public FileOperationException(string fileName, string message, Exception inner) : base($"Błąd operacji na pliku '{fileName}': {message}", inner)
        {
            FileName = fileName;
        }
    }

    public class EmptyEquipmentListException : RentalException
    {
        public EmptyEquipmentListException() : base("Lista sprzętu jest pusta.") { }
    }
}
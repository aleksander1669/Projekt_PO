using Projekt_PO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Project_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            bool exit = false;
            DateTime teraz = DateTime.Now;
            List<Bike> Bike_List = new List<Bike>();
            List<Motorcycle> Motorcycle_List = new List<Motorcycle>();
            List<Car> Car_List = new List<Car>();
            List<Rent> Rent_List = new List<Rent>();
            var options = new JsonSerializerOptions { WriteIndented = true };

            List<Rent> Rent_History = new List<Rent>();

            try
            {
                if (File.Exists("history.json"))
                {
                    string loaded_history = File.ReadAllText("history.json");
                    Rent_History = JsonSerializer.Deserialize<List<Rent>>(loaded_history);
                }
                if (File.Exists("rent.json"))
                {
                    string loaded_rent = File.ReadAllText("rent.json");
                    Rent_List = JsonSerializer.Deserialize<List<Rent>>(loaded_rent);
                }
                if (File.Exists("bike.json"))
                {
                    string json_loaded = File.ReadAllText("bike.json");
                    Bike_List = JsonSerializer.Deserialize<List<Bike>>(json_loaded);
                }
                if (File.Exists("motorcycle.json"))
                {
                    string json_loaded = File.ReadAllText("motorcycle.json");
                    Motorcycle_List = JsonSerializer.Deserialize<List<Motorcycle>>(json_loaded);
                }
                if (File.Exists("car.json"))
                {
                    string json_loaded = File.ReadAllText("car.json");
                    Car_List = JsonSerializer.Deserialize<List<Car>>(json_loaded);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Krytyczny błąd podczas ładowania danych: {ex.Message}");
                return;
            }

            int Rent_Id = 0;
            int Max_Id = 0;
            if (Bike_List.Count > 0)
                Max_Id = Math.Max(Max_Id, Bike_List.Max(b => b.id));
            if (Motorcycle_List.Count > 0)
                Max_Id = Math.Max(Max_Id, Motorcycle_List.Max(b => b.id));
            if (Car_List.Count > 0)
                Max_Id = Math.Max(Max_Id, Car_List.Max(b => b.id));

            if (Rent_List.Count > 0)
                Rent_Id = Rent_List.Max(r => r.id);

            Console.WriteLine("Welcome to our program!");

            do
            {
                try
                {
                    choice = Int_Input("Choose interested option:\n1. Equipment management\n2. Rent management\n0. Exit\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            int choice_equipment_menager = Int_Input("Choose what you want to do:\n1. Add new equipment\n2. Show available equipment\n3. Remove equipment from database\n4. Filter/Search equipment\n0. Return\nChoice: ", 0, 4, "Invalid choice", "Invalid choice");
                            if (choice_equipment_menager == 1)
                            {
                                Console.Clear();
                                int choice_add = Int_Input("Choose what equipment you want to add:\n1. Bike\n2. Motorcycle\n3. Car\n0. Return\nChoice: ", 0, 3, "Invalid choice", "Invalid choice");
                                if (choice_add == 1)
                                {
                                    Console.Clear();
                                    string a = String_Input("Enter name/type of your bike: ");
                                    Console.Clear();
                                    double b = Double_Input("Enter price per day for your bike: ", 5, 500, "Bike cannot cost that low", "Price is too high for a bike");
                                    Console.Clear();
                                    double c = Double_Input("Enter deposit for your bike: ", 50, 1000, "Bike deposit cannot be that low", "Deposit is too high for a bike");
                                    Console.Clear();
                                    string d = String_Input("Enter some maintenance information for your bike (for example incoming chain conservation): ");

                                    Max_Id++;
                                    Bike nowy = new Bike(Max_Id, a, teraz, false, b, c, d);
                                    Bike_List.Add(nowy);
                                    Console.Clear();

                                    string json_string = JsonSerializer.Serialize(Bike_List, options);
                                    File.WriteAllText("bike.json", json_string);
                                    Console.WriteLine("Bike was succesfully added to database");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else if (choice_add == 2)
                                {
                                    Console.Clear();
                                    string a = String_Input("Enter name of your motorcycle: ");
                                    Console.Clear();
                                    double b = Double_Input("Enter price per day for your motorcycle: ", 100, 2000, "Motorcycle cannot cost that low", "Price is too high for a motorcycle");
                                    Console.Clear();
                                    double c = Double_Input("Enter deposit cost: ", 200, 2000, "Deposit for motorcycle cannot be that low", "Deposit is too high for a motorcycle");
                                    Console.Clear();
                                    string d = String_Input("Enter some maintenance information for your motorcycle (for example incoming oil change): ");
                                    Console.Clear();
                                    DateTime e = DateTime_Input("Enter date of inspection expiration : ");
                                    Console.Clear();
                                    string f = String_Input("Enter plate number: ");
                                    Console.Clear();
                                    int g = Int_Input("Enter how much kilometers have been driven on current oil (if unknown input -1): ", -1, 50000, "Selected oil range is invalid", "Either value is invalid or you should have not bought this motorcycle");
                                    Console.Clear();
                                    int h = Int_Input("Enter how much horse-power your motorcycle has: ", 10, 1000, "Hp cannot be that low", "Hp cannot be that high");
                                    Console.Clear();
                                    string i = String_Input("Enter fuel your motorcycle takes: ");
                                    Console.Clear();
                                    string j = String_Input("Enter gearbox your motorcycle has: ");
                                    Console.Clear();
                                    string k = String_Input("Specify style of your bike (for ex. chopper, cross): ");

                                    Max_Id++;
                                    Motorcycle nowy = new Motorcycle(Max_Id, a, teraz, false, b, c, d, e, f, g, h, i, j, k);
                                    Motorcycle_List.Add(nowy);
                                    Console.Clear();

                                    string json_string = JsonSerializer.Serialize(Motorcycle_List, options);
                                    File.WriteAllText("motorcycle.json", json_string);
                                    Console.WriteLine("Motorcycle was succesfully added to database");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else if (choice_add == 3)
                                {
                                    Console.Clear();
                                    string a = String_Input("Enter name of your car: ");
                                    Console.Clear();
                                    double b = Double_Input("Enter price per day for your car: ", 100, 2000, "Car cannot cost that low", "Price is too high for a car");
                                    Console.Clear();
                                    double c = Double_Input("Enter deposit cost: ", 200, 2000, "Deposit for a car cannot be that low", "Deposit is too high for a car");
                                    Console.Clear();
                                    string d = String_Input("Enter some maintenance information for your car (for example incoming oil change): ");
                                    Console.Clear();
                                    DateTime e = DateTime_Input("Enter date of inspection expiration : ");
                                    Console.Clear();
                                    string f = String_Input("Enter plate number: ");
                                    Console.Clear();
                                    int g = Int_Input("Enter how much kilometers have been driven on current oil (if unknown input -1): ", -1, 50000, "Selected oil range is invalid", "Either value is invalid or you should have not bought this motorcycle");
                                    Console.Clear();
                                    int h = Int_Input("Enter how much horse-power your motorcycle has: ", 10, 1000, "Hp cannot be that low", "Hp cannot be that high");
                                    Console.Clear();
                                    string i = String_Input("Enter fuel your car takes: ");
                                    Console.Clear();
                                    string j = String_Input("Enter gearbox your car has: ");
                                    Console.Clear();
                                    int k = Int_Input("Enter number of doors your car has: ", 2, 5, "Invalid number", "Invalid number");
                                    Console.Clear();
                                    int l = Int_Input("Enter ammount of seats for your car: ", 1, 7, "Invalid number", "Invalid number");
                                    Console.Clear();
                                    string m = String_Input("Specify body type(for ex. sedan, combi, coupe): ");

                                    Max_Id++;
                                    Car nowy = new Car(Max_Id, a, teraz, false, b, c, d, e, f, g, h, i, j, k, l, m);
                                    Car_List.Add(nowy);
                                    Console.Clear();

                                    string json_string = JsonSerializer.Serialize(Car_List, options);
                                    File.WriteAllText("car.json", json_string);
                                    Console.WriteLine("Car was succesfully added to database");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else if (choice_add == 0)
                                {
                                    Console.Clear();
                                    continue;
                                }
                            }
                            else if (choice_equipment_menager == 2)
                            {
                                bool exit_view = false;
                                bool showError = false;
                                string errorMessage = "";

                                do
                                {
                                    Console.Clear();

                                    if (showError)
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.WriteLine();
                                        showError = false;
                                    }

                                    if (Bike_List.Count == 0 && Motorcycle_List.Count == 0 && Car_List.Count == 0)
                                    {
                                        Console.WriteLine("There is no equipment added yet");
                                        exit_view = true;
                                    }
                                    else
                                    {
                                        if (Bike_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available bikes:");
                                            foreach (Bike bike in Bike_List)
                                            {
                                                bike.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }
                                        if (Motorcycle_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available motorcycles:");
                                            foreach (Motorcycle motor in Motorcycle_List)
                                            {
                                                motor.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }
                                        if (Car_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available Cars:");
                                            foreach (Car car in Car_List)
                                            {
                                                car.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }

                                        int choice_view = Int_Input_No_Max_Or_Low("Select ID of an item to see details or enter 0 to exit: ");

                                        if (choice_view == 0)
                                        {
                                            Console.Clear();
                                            exit_view = true;
                                            continue;
                                        }

                                        Bike bike_details = Bike_List.FirstOrDefault(b => b.id == choice_view);
                                        Motorcycle motorcycle_details = Motorcycle_List.FirstOrDefault(b => b.id == choice_view);
                                        Car car_details = Car_List.FirstOrDefault(b => b.id == choice_view);

                                        if (bike_details != null)
                                        {
                                            Console.Clear();
                                            bike_details.Info_Full();
                                            Console.WriteLine("\nPress any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_view = true;
                                        }
                                        else if (motorcycle_details != null)
                                        {
                                            Console.Clear();
                                            motorcycle_details.Info_Full();
                                            Console.WriteLine("\nPress any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_view = true;
                                        }
                                        else if (car_details != null)
                                        {
                                            Console.Clear();
                                            car_details.Info_Full();
                                            Console.WriteLine("\nPress any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_view = true;
                                        }
                                        else
                                        {
                                            showError = true;
                                            errorMessage = "Invalid ID - please try again";
                                        }
                                    }
                                } while (!exit_view);
                            }
                            else if (choice_equipment_menager == 3)
                            {
                                bool exit_del = false;
                                bool showError = false;
                                string errorMessage = "";

                                do
                                {
                                    Console.Clear();

                                    if (showError)
                                    {
                                        Console.WriteLine(errorMessage);
                                        Console.WriteLine();
                                        showError = false;
                                    }

                                    if (Bike_List.Count == 0 && Motorcycle_List.Count == 0 && Car_List.Count == 0)
                                    {
                                        Console.WriteLine("There is no equipment added yet");
                                        exit_del = true;
                                    }
                                    else
                                    {
                                        if (Bike_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available bikes:");
                                            foreach (Bike bike in Bike_List)
                                            {
                                                bike.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }
                                        if (Motorcycle_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available motorcycles:");
                                            foreach (Motorcycle motor in Motorcycle_List)
                                            {
                                                motor.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }
                                        if (Car_List.Count > 0)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("Available cars: ");
                                            foreach (Car car in Car_List)
                                            {
                                                car.Info_Short();
                                            }
                                            Console.WriteLine("==================================================================================================================\n");
                                        }

                                        int choice_del = Int_Input_No_Max_Or_Low("Choose ID of equipment you want to delete (enter 0 if you changed your mind): ");

                                        if (choice_del == 0)
                                        {
                                            Console.Clear();
                                            exit_del = true;
                                            continue;
                                        }

                                        Bike bike_to_remove = Bike_List.FirstOrDefault(b => b.id == choice_del);
                                        Motorcycle motorcycle_to_remove = Motorcycle_List.FirstOrDefault(b => b.id == choice_del);
                                        Car car_to_remove = Car_List.FirstOrDefault(b => b.id == choice_del);

                                        if (bike_to_remove != null)
                                        {
                                            if (!bike_to_remove.Can_Be_Removed())
                                            {
                                                throw new EquipmentAlreadyRentedException(bike_to_remove.id);
                                            }
                                            Bike_List.Remove(bike_to_remove);
                                            string json_string = JsonSerializer.Serialize(Bike_List, options);
                                            File.WriteAllText("bike.json", json_string);
                                            Console.Clear();
                                            Console.WriteLine("Bike with ID = " + choice_del + " is succesfully removed");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_del = true;
                                        }
                                        else if (motorcycle_to_remove != null)
                                        {
                                            if (!motorcycle_to_remove.Can_Be_Removed())
                                            {
                                                throw new EquipmentAlreadyRentedException(motorcycle_to_remove.id);
                                            }
                                            Motorcycle_List.Remove(motorcycle_to_remove);
                                            string json_string = JsonSerializer.Serialize(Motorcycle_List, options);
                                            File.WriteAllText("motorcycle.json", json_string);
                                            Console.Clear();
                                            Console.WriteLine("Motorcycle with ID = " + choice_del + " is succesfully removed");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_del = true;
                                        }
                                        else if (car_to_remove != null)
                                        {
                                            if (!car_to_remove.Can_Be_Removed())
                                            {
                                                throw new EquipmentAlreadyRentedException(car_to_remove.id);
                                            }
                                            Car_List.Remove(car_to_remove);
                                            string json_string = JsonSerializer.Serialize(Car_List, options);
                                            File.WriteAllText("car.json", json_string);
                                            Console.Clear();
                                            Console.WriteLine($"Car with ID = {choice_del} is succesfully removed");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_del = true;
                                        }
                                        else
                                        {
                                            showError = true;
                                            errorMessage = "Invalid ID - equipment not found";
                                        }
                                    }
                                } while (!exit_del);
                            }
                            else if (choice_equipment_menager == 4)
                            {
                                Console.Clear();
                                int filter_choice = Int_Input("Choose filter type:\n1. Available for rent only\n2. Filter by type (Bike/Motorcycle/Car)\n3. Search by name\n0. Return\nChoice: ", 0, 3, "Invalid choice", "Invalid choice");

                                if (filter_choice == 1)
                                {
                                    Console.Clear();
                                    bool any = false;
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("AVAILABLE FOR RENT:");
                                    Console.WriteLine("==================================================================================================================");

                                    foreach (Bike bike in Bike_List.Where(b => !b.rented))
                                    {
                                        bike.Info_Short();
                                        any = true;
                                    }
                                    foreach (Motorcycle m in Motorcycle_List.Where(m => !m.rented))
                                    {
                                        m.Info_Short();
                                        any = true;
                                    }
                                    foreach (Car c in Car_List.Where(c => !c.rented))
                                    {
                                        c.Info_Short();
                                        any = true;
                                    }

                                    Console.WriteLine("==================================================================================================================");
                                    if (!any)
                                        Console.WriteLine("No available equipment for rent");
                                    Console.WriteLine("\nPress any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else if (filter_choice == 2)
                                {
                                    Console.Clear();
                                    int type_choice = Int_Input("Choose equipment type:\n1. Bikes only\n2. Motorcycles only\n3. Cars only\n0. Return\nChoice: ", 0, 3, "Invalid choice", "Invalid choice");

                                    if (type_choice != 0)
                                    {
                                        Console.Clear();
                                        if (type_choice == 1)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("BIKES:");
                                            if (Bike_List.Count == 0)
                                                Console.WriteLine("No bikes in database");
                                            else
                                                foreach (Bike bike in Bike_List)
                                                    bike.Info_Short();
                                            Console.WriteLine("==================================================================================================================");
                                        }
                                        else if (type_choice == 2)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("MOTORCYCLES:");
                                            if (Motorcycle_List.Count == 0)
                                                Console.WriteLine("No motorcycles in database");
                                            else
                                                foreach (Motorcycle m in Motorcycle_List)
                                                    m.Info_Short();
                                            Console.WriteLine("==================================================================================================================");
                                        }
                                        else if (type_choice == 3)
                                        {
                                            Console.WriteLine("==================================================================================================================");
                                            Console.WriteLine("CARS:");
                                            if (Car_List.Count == 0)
                                                Console.WriteLine("No cars in database");
                                            else
                                                foreach (Car c in Car_List)
                                                    c.Info_Short();
                                            Console.WriteLine("==================================================================================================================");
                                        }
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                                else if (filter_choice == 3)
                                {
                                    Console.Clear();
                                    Console.Write("Enter name to search: ");
                                    string search_name = Console.ReadLine();

                                    bool found = false;
                                    Console.Clear();
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("SEARCH RESULTS:");

                                    foreach (Bike bike in Bike_List.Where(b => b.name.Contains(search_name, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        bike.Info_Short();
                                        found = true;
                                    }
                                    foreach (Motorcycle m in Motorcycle_List.Where(m => m.name.Contains(search_name, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        m.Info_Short();
                                        found = true;
                                    }
                                    foreach (Car c in Car_List.Where(c => c.name.Contains(search_name, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        c.Info_Short();
                                        found = true;
                                    }

                                    Console.WriteLine("==================================================================================================================");
                                    if (!found)
                                        Console.WriteLine("No equipment found matching: " + search_name);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            else if (choice_equipment_menager == 0)
                            {
                                Console.Clear();
                                continue;
                            }
                            break;

                        case 2:
                            Console.Clear();
                            int choice_rent = Int_Input("What action you want to do:\n1. Rent item\n2. Show all rents\n3. Settle rent\n4. Settle multiple rents\n5. Rent history\n0. Return\nChoice: ", 0, 5, "Invalid input", "Invalid input");
                            if (choice_rent == 0)
                            {
                                Console.Clear();
                                continue;
                            }
                            else if (choice_rent == 1)
                            {
                                Console.Clear();
                                var availableBikes = Bike_List.Where(b => !b.rented).ToList();
                                var availableMotorcycles = Motorcycle_List.Where(m => !m.rented).ToList();
                                var availableCars = Car_List.Where(c => !c.rented).ToList();

                                if (availableBikes.Count == 0 && availableMotorcycles.Count == 0 && availableCars.Count == 0)
                                {
                                    Console.WriteLine("There is no equipment available for rent");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    if (availableBikes.Count > 0)
                                    {
                                        Console.WriteLine("==================================================================================================================");
                                        Console.WriteLine("AVAILABLE bikes for rent:");
                                        foreach (Bike bike in availableBikes)
                                        {
                                            bike.Info_Short();
                                        }
                                        Console.WriteLine("==================================================================================================================");
                                    }
                                    if (availableMotorcycles.Count > 0)
                                    {
                                        Console.WriteLine("==================================================================================================================");
                                        Console.WriteLine("AVAILABLE motorcycles for rent:");
                                        foreach (Motorcycle motor in availableMotorcycles)
                                        {
                                            motor.Info_Short();
                                        }
                                        Console.WriteLine("==================================================================================================================");
                                    }
                                    if (availableCars.Count > 0)
                                    {
                                        Console.WriteLine("==================================================================================================================");
                                        Console.WriteLine("AVAILABLE cars for rent:");
                                        foreach (Car car in availableCars)
                                        {
                                            car.Info_Short();
                                        }
                                        Console.WriteLine("==================================================================================================================");
                                    }

                                    int choice_rent_id = Int_Input_No_Max_Or_Low("Select ID of an item to rent (0 to return): ");

                                    if (choice_rent_id == 0)
                                    {
                                        Console.Clear();
                                        continue;
                                    }

                                    Bike rent_bike = availableBikes.FirstOrDefault(b => b.id == choice_rent_id);
                                    Motorcycle rent_motorcycle = availableMotorcycles.FirstOrDefault(b => b.id == choice_rent_id);
                                    Car rent_car = availableCars.FirstOrDefault(b => b.id == choice_rent_id);

                                    if (rent_bike == null && rent_motorcycle == null && rent_car == null)
                                    {
                                        throw new EquipmentNotFoundException(choice_rent_id);
                                    }

                                    Console.Clear();
                                    int choice_rent_type = Int_Input("What type of rent your interested in:\n1. Private rent\n2. For a Company\n0. Return\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");

                                    if (choice_rent_type == 0)
                                    {
                                        Console.Clear();
                                        continue;
                                    }
                                    if (choice_rent_type == 1)
                                    {
                                        Console.Clear();
                                        string name = String_Input_No_Digits("Enter your name: ");
                                        Console.Clear();
                                        string surename = String_Input_No_Digits("Enter your surename: ");
                                        Console.Clear();
                                        string phone = String_Input_Lenght("Enter your phone number: ", 9, "Phone number needs to contain 9 digits");
                                        Console.Clear();
                                        string identification = String_Input_Lenght("Enter your identification number: ", 11, "Your identification number needs to contain 11 digits");
                                        Console.Clear();

                                        Private rent_customer = new Private(name, phone, surename, identification);

                                        if (rent_bike != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_bike.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_bike, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your bike is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_bike_list = JsonSerializer.Serialize(Bike_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("bike.json", json_bike_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (rent_motorcycle != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_motorcycle.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_motorcycle, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your motorcycle is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_motorcycle_list = JsonSerializer.Serialize(Motorcycle_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("motorcycle.json", json_motorcycle_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (rent_car != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_car.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_car, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your car is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_car_list = JsonSerializer.Serialize(Car_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("car.json", json_car_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                    }
                                    if (choice_rent_type == 2)
                                    {
                                        Console.Clear();
                                        string name = String_Input_No_Digits("Enter name of your company: ");
                                        Console.Clear();
                                        string phone = String_Input_Lenght("Enter your phone number: ", 9, "Phone number needs to contain 9 digits");
                                        Console.Clear();
                                        string nip = String_Input_Lenght("Enter your identification number: ", 10, "Your nip number needs to contain 10 digits");
                                        Console.Clear();
                                        Company rent_customer = new Company(name, phone, nip);

                                        if (rent_bike != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_bike.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_bike, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your bike is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_bike_list = JsonSerializer.Serialize(Bike_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("bike.json", json_bike_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (rent_motorcycle != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_motorcycle.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_motorcycle, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your motorcycle is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_motorcycle_list = JsonSerializer.Serialize(Motorcycle_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("motorcycle.json", json_motorcycle_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        if (rent_car != null)
                                        {
                                            Rent_Id++;
                                            DateTime rental_till = DateTime_Input("Enter date of return: ");
                                            rent_car.Status_Change(true);
                                            Rent new_rent = new Rent(Rent_Id, rent_customer, rent_car, teraz, rental_till);
                                            Console.Clear();
                                            Console.WriteLine("Your car is rented succesfully");
                                            Rent_List.Add(new_rent);
                                            string json_rent_list = JsonSerializer.Serialize(Rent_List, options);
                                            string json_car_list = JsonSerializer.Serialize(Car_List, options);
                                            File.WriteAllText("rent.json", json_rent_list);
                                            File.WriteAllText("car.json", json_car_list);
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                    }
                                }
                            }
                            else if (choice_rent == 2)
                            {
                                Console.Clear();
                                if (Rent_List.Count > 0)
                                {
                                    foreach (Rent x in Rent_List)
                                    {
                                        x.Display();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("There are no rents in database");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            else if (choice_rent == 3)
                            {
                                if (Rent_List.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There are no rents in database");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    bool exit_settle = false;
                                    bool showError = false;
                                    string errorMessage = "";

                                    do
                                    {
                                        Console.Clear();

                                        if (showError)
                                        {
                                            Console.WriteLine(errorMessage);
                                            Console.WriteLine();
                                            showError = false;
                                        }

                                        foreach (Rent x in Rent_List)
                                        {
                                            x.Display();
                                            Console.WriteLine();
                                        }

                                        int choice_settle = Int_Input_No_Max_Or_Low("Enter the Id of rent you want to settle or 0 to cancel: ");

                                        if (choice_settle == 0)
                                        {
                                            Console.Clear();
                                            exit_settle = true;
                                            continue;
                                        }

                                        Rent rent_settle = Rent_List.FirstOrDefault(b => b.id == choice_settle);

                                        if (rent_settle == null)
                                        {
                                            showError = true;
                                            errorMessage = "Invalid rent ID - please try again";
                                        }
                                        else
                                        {
                                            Bike returned_bike = Bike_List.FirstOrDefault(b => b.id == rent_settle.rented_item.id);
                                            Motorcycle returned_motorcycle = Motorcycle_List.FirstOrDefault(b => b.id == rent_settle.rented_item.id);
                                            Car returned_car = Car_List.FirstOrDefault(b => b.id == rent_settle.rented_item.id);

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

                                            Console.Clear();
                                            rent_settle.Settle();

                                            Rent_History.Add(rent_settle);
                                            File.WriteAllText("history.json", JsonSerializer.Serialize(Rent_History, options));

                                            Rent_List.Remove(rent_settle);

                                            string json_return = JsonSerializer.Serialize(Rent_List, options);
                                            File.WriteAllText("rent.json", json_return);

                                            string json_return_bike = JsonSerializer.Serialize(Bike_List, options);
                                            File.WriteAllText("bike.json", json_return_bike);

                                            string json_return_motorcycle = JsonSerializer.Serialize(Motorcycle_List, options);
                                            File.WriteAllText("motorcycle.json", json_return_motorcycle);

                                            string json_return_car = JsonSerializer.Serialize(Car_List, options);
                                            File.WriteAllText("car.json", json_return_car);

                                            Console.WriteLine("Rent settled successfully!");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            exit_settle = true;
                                        }
                                    } while (!exit_settle);
                                }
                            }
                            else if (choice_rent == 4)
                            {
                                if (Rent_List.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There are no rents in database");
                                    continue;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("=== CUSTOMER MULTI-SETTLE ===");
                                    string wpisany_telefon = "";
                                    do
                                    {
                                        wpisany_telefon = String_Input("Enter customer phone number to settle (or 0 to cancel): ");
                                        if (wpisany_telefon == "0")
                                        {
                                            break;
                                        }
                                        else if (wpisany_telefon.Length != 9 || !wpisany_telefon.All(char.IsDigit))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Invalid input! Phone number must consist of exactly 9 digits.");
                                            wpisany_telefon = "";
                                        }

                                    } while (wpisany_telefon == "");

                                    if (wpisany_telefon == "0")
                                    {
                                        Console.Clear();
                                        continue;
                                    }

                                    List<Rent> renty_klienta = Rent_List.FindAll(r => r.renter.phone == wpisany_telefon);

                                    if (renty_klienta.Count == 0)

                                    {
                                        Console.WriteLine("This customer has no active rents.");
                                        Console.WriteLine("\nPress any key to return...");
                                        Console.ReadKey();
                                        continue;
                                    }

                                    Console.WriteLine("--------------------------------------------------------------------------");
                                    Console.WriteLine($"\nSettling all...");
                                    double koncowy_rachunek = Rent.SettleMultiple(renty_klienta, Rent_List, Rent_History, Bike_List, Motorcycle_List, Car_List);

                                    Console.WriteLine($"TOTAL INVOICE COST TO PAY: {koncowy_rachunek.ToString("F2")} zł");

                                    File.WriteAllText("history.json", JsonSerializer.Serialize(Rent_History, options));
                                    File.WriteAllText("rent.json", JsonSerializer.Serialize(Rent_List, options));
                                    File.WriteAllText("bike.json", JsonSerializer.Serialize(Bike_List, options));
                                    File.WriteAllText("motorcycle.json", JsonSerializer.Serialize(Motorcycle_List, options));
                                    File.WriteAllText("car.json", JsonSerializer.Serialize(Car_List, options));

                                    Console.WriteLine("\nAll items returned successfully. Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                            }
                            else if (choice_rent == 5)
                            {
                                Console.Clear();
                                if (Rent_History.Count > 0)
                                {
                                    foreach (Rent x in Rent_History)
                                    {
                                        x.Display();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("No rental history yet");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            break;

                        case 0:
                            exit = true;

                            string json_Bike = JsonSerializer.Serialize(Bike_List, options);
                            File.WriteAllText("bike.json", json_Bike);

                            string json_Motorcycle = JsonSerializer.Serialize(Motorcycle_List, options);
                            File.WriteAllText("motorcycle.json", json_Motorcycle);

                            string json_car = JsonSerializer.Serialize(Car_List, options);
                            File.WriteAllText("car.json", json_car);

                            Console.Clear();
                            Console.WriteLine("Saving all changes...");
                            Console.WriteLine("Exiting now...");
                            break;
                    }
                }
                catch (EquipmentNotFoundException ex)
                {
                    Console.Clear();
                    Console.WriteLine($"BŁĄD: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (EquipmentAlreadyRentedException ex)
                {
                    Console.Clear();
                    Console.WriteLine($"BŁĄD: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"KRYTYCZNY BŁĄD: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!exit);
        }

        static int Int_Input(string message, int min_option, int max_option, string min_option_message, string max_option_message)
        {
            bool fine = true;
            int x = 0;
            do
            {
                fine = true;
                Console.Write(message);
                try
                {
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x < min_option)
                    {
                        Console.Clear();
                        Console.WriteLine(min_option_message);
                        fine = false;
                    }
                    else if (x > max_option)
                    {
                        Console.Clear();
                        Console.WriteLine(max_option_message);
                        fine = false;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }
            } while (!fine);
            return x;
        }

        static int Int_Input_No_Max_Or_Low(string message)
        {
            bool fine = true;
            int x = 0;
            do
            {
                fine = true;
                Console.Write(message);
                try
                {
                    x = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }
            } while (!fine);
            return x;
        }

        static int Int_Input_Lenght(string message, int lenght, string lenght_message)
        {
            bool fine = true;
            int x = 0;
            string y = string.Empty;
            do
            {
                fine = true;
                Console.Write(message);
                y = Console.ReadLine();
                int z = y.Length;
                if (z != lenght)
                {
                    Console.Clear();
                    Console.WriteLine(lenght_message);
                    fine = false;
                }
                else
                {
                    fine = true;
                    try
                    {
                        x = Convert.ToInt32(y);
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong format");
                        fine = false;
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Something went wrong");
                        fine = false;
                    }
                }
            } while (!fine);
            return x;
        }

        static double Double_Input(string message, double min_option, double max_option, string min_option_message, string max_option_message)
        {
            bool fine = true;
            double x = 0;

            do
            {
                fine = true;
                Console.Write(message);
                try
                {
                    x = Convert.ToDouble(Console.ReadLine());
                    if (x < min_option)
                    {
                        Console.Clear();
                        Console.WriteLine(min_option_message);
                        fine = false;
                    }
                    else if (x > max_option)
                    {
                        Console.Clear();
                        Console.WriteLine(max_option_message);
                        fine = false;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }
            } while (!fine);

            return x;
        }

        static string String_Input(string message)
        {
            bool fine = true;
            string x = string.Empty;
            do
            {
                fine = true;
                Console.Write(message);
                x = Console.ReadLine();
                if (x == string.Empty)
                {
                    Console.Clear();
                    Console.WriteLine("This blank cannot be empty");
                    fine = false;
                }
            } while (!fine);
            return x;
        }

        static string String_Input_No_Digits(string message)
        {
            bool fine = true;
            string x = null;
            do
            {
                fine = true;
                x = String_Input(message);
                if (x == string.Empty)
                {
                    Console.Clear();
                    fine = false;
                    Console.WriteLine("This blank cannot be empty");
                }
                try
                {
                    foreach (char c in x)
                    {
                        if (!char.IsLetter(c))
                        {
                            fine = false;
                            Console.Clear();
                            Console.WriteLine("This data cannot contain any digits");
                            break;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }

            } while (!fine);
            return x;
        }

        static string String_Input_No_Digits_Lenght(string message, int lenght, string lenght_message)
        {
            bool fine = true;
            string x = null;
            do
            {
                fine = true;
                x = String_Input(message);
                int z = x.Length;
                if (z != lenght)
                {
                    Console.Clear();
                    fine = false;
                    Console.WriteLine(lenght_message);
                }
                if (x == string.Empty)
                {
                    Console.Clear();
                    fine = false;
                    Console.WriteLine("This blank cannot be empty");
                }
                try
                {
                    foreach (char c in x)
                    {
                        if (!char.IsDigit(c))
                        {
                            fine = false;
                            Console.Clear();
                            Console.WriteLine("This data cannot contain any letters");
                            break;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }

            } while (!fine);
            return x;
        }

        static DateTime DateTime_Input(string message)
        {
            bool fine = true;
            DateTime x = default;
            do
            {
                fine = true;
                Console.Write(message);
                try
                {
                    x = Convert.ToDateTime(Console.ReadLine());
                    if (x <= DateTime.Now)
                    {
                        Console.Clear();
                        Console.WriteLine("Date must be in the future");
                        fine = false;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }
                if (x <= DateTime.Now)
                {
                    Console.Clear();
                    Console.WriteLine("Date must be in the future");
                    fine = false;
                }
            } while (!fine);
            return x;
        }

        static string String_Input_Lenght(string message, int lenght, string message_lenght)
        {
            bool fine = true;
            string x = null;

            do
            {
                fine = true;
                Console.Write(message);
                x = Console.ReadLine();
                int z = x.Length;
                if (z != lenght)
                {
                    Console.Clear();
                    fine = false;
                    Console.WriteLine(message_lenght);
                }
                else
                {
                    fine = true;
                }
            } while (!fine);
            return x;
        }
    }
}
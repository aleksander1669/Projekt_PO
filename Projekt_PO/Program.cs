using Projekt_PO;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using System.Timers;
using System.IO;

namespace Project_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            int item_count_bike = 0;
            int item_count_motorcycle = 0;
            int choice = 0;
            bool exit = false;
            int global_id = 0;
            DateTime teraz = DateTime.Now;
            List<Bike> Bike_List = new List<Bike>();
            List<Motorcycle> Motorcycle_List = new List<Motorcycle>();
            var options = new JsonSerializerOptions { WriteIndented = true };

            if (File.Exists("bike.json"))
            {
                string json_loaded = File.ReadAllText("bike.json");
                Bike_List = JsonSerializer.Deserialize<List<Bike>>(json_loaded);
                if (Bike_List.Count > 0)
                {
                    item_count_bike = Bike_List.Max(b => b.Id);
                }
            }
            if (File.Exists("motorcycle.json"))
            {
                string json_loaded = File.ReadAllText("motorcycle.json");
                Motorcycle_List = JsonSerializer.Deserialize<List<Motorcycle>>(json_loaded);
                if (Bike_List.Count > 0)
                {
                    item_count_motorcycle = Motorcycle_List.Max(b => b.Id);
                }
            }
            if (item_count_bike >= item_count_motorcycle)
            {
                global_id = item_count_bike;
            } else
            {
                global_id = item_count_motorcycle;
            }

            Console.WriteLine("Welcome to our program!");

            do
            {
                choice = Int_Input("Choose interested option:\n1. Equipment menagement\n0. Exit\nChoice: ", 0, 10, "Invalid choice", "Invalid choice");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        int choice_1 = Int_Input("Choose what you want to do:\n1. Add new equipment\n2. Show available equipment\n3. Remove equipment\n0. Return\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");
                        if (choice_1 == 1)
                        {
                            Console.Clear();
                            int choice_2 = Int_Input("Choose what equipment you want to add:\n1. Bike\n2. Motorcycle\n0. Return\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");
                            if (choice_2 == 1)
                            {
                                Console.Clear();
                                string a = String_Input("Enter name/type of your bike: ");

                                Console.Clear();
                                double b = Double_Input("Enter price per day for your bike: ", 5, 500, "Bike cannot cost that low", "Price is too high for a bike");

                                Console.Clear();
                                double c = Double_Input("Enter deposit for your bike: ", 50, 1000, "Bike deposit cannot be that low", "Deposit is too high for a bike");

                                Console.Clear();
                                string d = String_Input("Enter some maintenance information for your bike (for exmaple incoming chain conservation): ");

                                global_id++;
                                item_count_bike++;
                                Bike nowy = new Bike(global_id, a, teraz, true, b, c, d);
                                Bike_List.Add(nowy);
                                Console.Clear();
                                
                                string json_string = JsonSerializer.Serialize(Bike_List, options);

                                // Do zmiany na path "bike.json"

                                File.WriteAllText("bike.json",  json_string);

                                Console.WriteLine("Bike was succesfully added to database");
                            } 
                            else if (choice_2 == 2)
                            {
                                Console.Clear();
                                string a = String_Input("Enter name/type of your motorcycle: ");

                                Console.Clear();
                                double b = Double_Input("Enter price per day for your motorcycle: ", 100, 2000, "Motorcycle cannot cost that low", "Price is too high for a motorcycle");

                                Console.Clear();
                                double c = Double_Input("Enter deposit cost: ", 200, 2000, "Deposit for motorcycle cannot be that low", "Deposit is too high for a motorcycle");

                                Console.Clear();
                                string d = String_Input("Enter some maintenance information for your motorcycle (for exmaple incoming oil change): ");

                                Console.Clear();
                                DateTime e = DateTime_Input("Enter date of inspection expiration : ");

                                Console.Clear();
                                string f = String_Input("Enter plate number: ");

                                Console.Clear();
                                int g = Int_Input("Enter how much kilometers have been driven on current oil (if unknown input -1): ", -1, 50000, "Selected oil range is invalid", "Either value is invalid or you should have not bought this motorcycle");

                                global_id++;
                                item_count_motorcycle++;
                                Motorcycle nowy = new Motorcycle(global_id, a, teraz, false, b, c, d, e, f, g);
                                Motorcycle_List.Add(nowy);
                                Console.Clear();

                                string json_string = JsonSerializer.Serialize(Motorcycle_List, options);

                                // Do zmiany na path "motorcycle.json"

                                File.WriteAllText("motorcycle.json", json_string);

                                Console.WriteLine("Motorcycle was succesfully added to database");
                            } 
                            else if (choice_2 == 0)
                            {
                                Console.Clear();
                                continue;
                            }
                        } 
                        else if (choice_1 == 2)
                        {
                            bool exit_view = false;
                            int choice_view;
                            do
                            {
                                Console.Clear();
                                if (global_id == 0 && item_count_bike == 0 && item_count_motorcycle == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There is no equipment added yet");
                                    exit_view = true;
                                }
                                if (item_count_bike > 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("=====================================================================================================");
                                    Console.WriteLine("Available bikes:");
                                    foreach (Bike bike in Bike_List)
                                    {
                                        bike.Info_Short();
                                    }
                                    Console.WriteLine("=====================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (item_count_motorcycle > 0)
                                {
                                    Console.WriteLine("=====================================================================================================");
                                    Console.WriteLine("Available motorcycles:");
                                    foreach (Motorcycle motor in Motorcycle_List)
                                    {
                                        motor.Info_Short();
                                    }
                                    Console.WriteLine("=====================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (global_id > 0)
                                {
                                    Console.WriteLine("1. Show details");
                                    Console.WriteLine("0. Exit");
                                    choice_view = Int_Input("Choice: ", 0, 1, "Invalid choice", "Invalid choice");
                                    if (choice_view == 1)
                                    {

                                    }
                                    else if (choice_view == 0)
                                    {
                                        Console.Clear();
                                        exit_view = true;
                                    }
                                }
                            } while (!exit_view);
                        }
                        else if (choice_1 == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input");
                        break;
                    case 0:
                        exit = true;
                        Console.Clear();
                        Console.WriteLine("Exiting now...");
                        break;
                }
            } while (!exit);

        }
        static int Int_Input(string message,int min_option, int max_option, string min_option_message, string max_option_message)
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
                    } else if (x > max_option)
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
                    } else if (x > max_option) {
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
                if (x == string.Empty)
                {
                    Console.Clear();
                    fine = false;
                    Console.WriteLine("This blank cannot be empty");
                }
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
    }
}
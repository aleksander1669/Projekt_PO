using Projekt_PO;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Timers;
using static System.Net.WebRequestMethods;

namespace Project_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            int item_count = 0;
            int choice = 0;
            bool exit = false;
            DateTime teraz = DateTime.Now;
            List<Bike> Bike_List = new List<Bike>();
            List<Motorcycle> Motorcycle_List = new List<Motorcycle>();

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
                                string c = String_Input("Enter some maintenance information for your bike (for exmaple incoming chain conservation): ");

                                item_count++;
                                Bike nowy = new Bike(item_count, a, teraz, true, b, c);
                                Bike_List.Add(nowy);
                                Console.Clear();
                                Console.WriteLine("Bike was succesfully added to database");
                            } 
                            else if (choice_2 == 2)
                            {
                                Console.Clear();
                                string a = String_Input("Enter name/type of your motorcycle: ");

                                Console.Clear();
                                double b = Double_Input("Enter price per day for your motorcycle: ", 100, 2000, "Motorcycle cannot cost that low", "Price is too high for a motorcycle");

                                Console.Clear();
                                string c = String_Input("Enter some maintenance information for your motorcycle (for exmaple incoming oil change): ");

                                Console.Clear();
                                DateTime d = DateTime_Input("Enter date of inspection expiration : ");

                                Console.Clear();
                                string e = String_Input("Enter plate number: ");

                                Console.Clear();
                                int f = Int_Input("Enter how much kilometers have been driven on current oil to keep track of oil change (if unknown input -1): ", -1, 50000, "Selected oil range is invalid", "Either value is invalid or you should have not bought this motorcycle");

                                item_count++;
                                Motorcycle nowy = new Motorcycle(item_count, a, teraz, true, b, c, d, e, f);
                                Motorcycle_List.Add(nowy);
                                Console.Clear();
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
                            if (item_count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no equipment added yet");
                            } else
                            {
                                Console.Clear();
                                Console.WriteLine("============================================================================");
                                Console.WriteLine("Available bikes:");
                                foreach (Bike bike in Bike_List)
                                {
                                    bike.Show_Info_Short_Bike();
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("============================================================================");
                                Console.WriteLine("Available motorcycles:");
                                foreach (Motorcycle motor in Motorcycle_List)
                                {
                                    motor.Show_Info_Short_Motorcycle();
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                            }
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
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception ex)
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
                catch (FormatException ex)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception ex)
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
            string x = null;
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
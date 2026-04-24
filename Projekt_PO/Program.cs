using Projekt_PO;
using System;
using System.Data;
using System.Timers;

namespace Project_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            bool exit = false;
            DateTime teraz = DateTime.Now;

            Console.WriteLine("Welcome to our program!");

            do
            {
                choice = Int_Input("Choose interested option:\n1. Equipment menagement\n0. Exit\nChoice: ", 0, 10);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        int choice_1 = Int_Input("Choose what you want to do:\n1. Add new equipment\n2. Show available equipment\n3. Remove equipment\n4. Return\nChoice: ", 1, 4);
                        if (choice_1 == 1)
                        {
                            List<string> equipment = new List<string>();

                            Console.Clear();
                            int choice_2 = Int_Input("Choose what equipment you want to add:\n1. Bike\n2. Motorcycle\nChoice: ", 1, 2);
                            if (choice_2 == 1)
                            {
                                Console.Clear();
                                string a = String_Input("Enter name/type of your bike: ");

                                Console.Clear();
                                string b = String_Input("Enter price for one day of your bike: ");

                                Console.Clear();
                                string c = String_Input("Enter name for your bike: ");

                                Console.Clear();
                                string d = String_Input("Enter name for your bike: ");

                                Console.Clear();
                                string e = String_Input("Enter name for your bike: ");

                                Console.Clear();
                                string f = String_Input("Enter name for your bike: ");

                                Bike nowy = new Bike(string Id, a, teraz, false, string Price, string Maintenance);
                            }

                        }

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
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
        static int Int_Input(string message,int min_option, int max_option)
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
                    if (x < min_option || x > max_option)
                    {
                        Console.Clear();
                        Console.WriteLine("Option out of range");
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
    }
}
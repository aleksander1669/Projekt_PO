using System;

namespace Projekt_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            bool exit = false;

            Console.WriteLine("Welcome to our program!");

            do
            {
                Console.WriteLine("Choose interested option:");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                choice = Int_Input("Choice: ");

                switch (choice)
                {
                    case 1:
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
        static int Int_Input(string message)
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
                catch (FormatException ex)
                {
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception ex)
                {
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
                Console.WriteLine(message);
                try
                {
                    x = Console.ReadLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Wrong format");
                    fine = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    fine = false;
                }
                foreach (char c in x)
                {
                    if (!char.IsLetter(c))
                    {
                        fine = false;
                        Console.WriteLine("This data cannot contain any digits");
                        break;
                    }
                }

            } while (!fine);
            return x;
        }
    }
}
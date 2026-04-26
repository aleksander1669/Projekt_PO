using Projekt_PO;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using System.Timers;
using System.IO;
using System.ComponentModel;

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
            List<Rent> Rent_List = new List<Rent>();
            List<Customer> Customer_List = new List<Customer>();
            var options = new JsonSerializerOptions { WriteIndented = true };
            
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

            int Max_Id = 0;
            int Max_Id_Bike = 0;
            int Max_Id_Motorcycle = 0;
            int Bike_List_Counter = Bike_List.Count;
            int Motorcycle_List_Counter = Motorcycle_List.Count;
            global_id = Bike_List_Counter + Motorcycle_List_Counter;
            try
            {
                if (Bike_List.Max(b => b.Id) > Motorcycle_List.Max(b => b.Id))
                {
                    Max_Id = Max_Id_Bike;
                }
                else
                {
                    Max_Id = Max_Id_Motorcycle;
                }
            } catch (InvalidOperationException)
            {
                if (Bike_List_Counter == 0 && Motorcycle_List_Counter == 0)
                {
                    Max_Id = 0;
                } else if (Motorcycle_List_Counter == 0)
                {
                    Max_Id = Bike_List.Max(b => b.Id);
                } else
                {
                    Max_Id = Motorcycle_List.Max(b => b.Id);
                }
            }

            Console.WriteLine("Welcome to our program!");

            do
            {
                choice = Int_Input("Choose interested option:\n1. Equipment management\n2. Rent management\n0. Exit\nChoice: ", 0, 10, "Invalid choice", "Invalid choice");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        int choice_1 = Int_Input("Choose what you want to do:\n1. Add new equipment\n2. Show available equipment\n3. Remove equipment\n0. Return\nChoice: ", 0, 3, "Invalid choice", "Invalid choice");
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
                                Bike_List_Counter++;
                                Bike nowy = new Bike(global_id, a, teraz, true, b, c, d);
                                Bike_List.Add(nowy);
                                Console.Clear();
                                
                                string json_string = JsonSerializer.Serialize(Bike_List, options);

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
                                Motorcycle_List_Counter++;
                                Motorcycle nowy = new Motorcycle(global_id, a, teraz, false, b, c, d, e, f, g);
                                Motorcycle_List.Add(nowy);
                                Console.Clear();

                                string json_string = JsonSerializer.Serialize(Motorcycle_List, options);

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
                                if (global_id == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There is no equipment added yet");
                                    exit_view = true;
                                }
                                if (Bike_List_Counter > 0)
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
                                if (Motorcycle_List_Counter > 0)
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
                                    choice_view = Int_Input_No_Max_Or_Low("Select ID of an item to see details or enter 0 to exit: ");
                                    
                                    Bike bike_details = Bike_List.FirstOrDefault(b => b.Id == choice_view);
                                    Motorcycle motorcycle_details = Motorcycle_List.FirstOrDefault(b => b.Id == choice_view);

                                    if (choice_view == 0)
                                    {
                                        Console.Clear();
                                        exit_view = true;
                                        continue;
                                    } else if (bike_details != null)
                                    {
                                        Console.Clear();
                                        bike_details.Info_All();
                                        Console.WriteLine("=====================================================================================================");
                                        Console.WriteLine();
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                        exit_view= true;
                                        continue;
                                    } else if (motorcycle_details != null)
                                    {
                                        Console.Clear();
                                        motorcycle_details.Info_All();
                                        Console.WriteLine("=====================================================================================================");
                                        Console.WriteLine();
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                        exit_view = true;
                                        continue;
                                    } else
                                    {
                                        Console.Clear();
                                        exit_view = true;
                                        Console.WriteLine("Invalid ID");
                                    }
                                }
                            } while (!exit_view);
                        } else if (choice_1 == 3)
                        {
                            bool exit_del = false;
                            int choice_del;
                            do
                            {
                                Console.Clear();
                                if (global_id == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There is no equipment added yet");
                                    exit_del = true;
                                }
                                if (Bike_List_Counter > 0)
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
                                if (Motorcycle_List_Counter > 0)
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
                                    bool found = false;
                                    choice_del = Int_Input_No_Max_Or_Low("Choose ID of equipment you want to delete (enter 0 if you changed your mind): ");

                                    Bike bike_to_remove = Bike_List.FirstOrDefault(b => b.Id == choice_del);
                                    Motorcycle motorcycle_to_remove = Motorcycle_List.FirstOrDefault(b => b.Id == choice_del);

                                    if (choice_del == 0)
                                    {
                                        Console.Clear();
                                        exit_del = true;
                                        continue;
                                    }
                                    else if (bike_to_remove != null)
                                    {
                                        Bike_List.Remove(bike_to_remove);
                                        global_id--;
                                        Bike_List_Counter--;
                                        exit_del = true;
                                        Console.Clear();
                                        Console.WriteLine("Bike with ID = " + choice_del + " is succesfully removed");
                                    }
                                    else if (motorcycle_to_remove != null)
                                    {
                                        Motorcycle_List.Remove(motorcycle_to_remove);
                                        global_id--;
                                        exit_del = true;
                                        Bike_List_Counter--;
                                        Console.Clear();
                                        Console.WriteLine("Motorcycle with ID = " + choice_del + " is succesfully removed");
                                    } else
                                    {
                                        Console.Clear();
                                        exit_del = true;
                                        Console.WriteLine("Item with ID = " + choice_del + " not found");
                                    }
                                }
                            } while (!exit_del);
                        }
                        else if (choice_1 == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        int choice_rent = Int_Input("What action you want to do:\n1. Rent item\n2. Settle rented item\n0. Return\nChoice: ", 0, 2, "Invalid input", "Invalid input");

                        if (choice_rent == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        else if (choice_rent == 1)
                        {
                            Console.Clear();
                            if (global_id == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no equipment added yet");
                            }
                            if (Bike_List_Counter > 0)
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
                            if (Motorcycle_List_Counter > 0)
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
                                int choice_rent_id = Int_Input_No_Max_Or_Low("Select ID of an item to rent (0 to return): ");

                                Bike rent_bike = Bike_List.FirstOrDefault(b => b.Id == choice_rent_id);
                                Motorcycle rent_motorcycle = Motorcycle_List.FirstOrDefault(b => b.Id == choice_rent_id);

                                if (choice_rent_id == 0)
                                {
                                    Console.Clear();
                                    continue;
                                } else if (rent_bike != null)
                                {
                                    Console.Clear();
                                    string a = String_Input_No_Digits("Enter customer's name: ");

                                    Console.Clear();
                                    string b = String_Input_No_Digits("Enter customer's surename: ");

                                    Console.Clear();
                                    int c = Int_Input_Lenght("Enter customer's phone: ", 9, "Nuber needs to contain 9 digits");

                                    Console.Clear();
                                    string d = String_Input_No_Digits_Lenght("Enter customer's identification number: ", 11, "This number has to contain 11 digits");

                                    Customer new_customer = new Customer(a, b, c, d);
                                } else if (rent_motorcycle != null)
                                {

                                } else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid input");
                                    continue;
                                }
                            }
                        }
                            break;
                    case 0:
                        exit = true;

                        string json_Bike = JsonSerializer.Serialize(Bike_List, options);
                        File.WriteAllText("bike.json", json_Bike);

                        string json_Motorcycle = JsonSerializer.Serialize(Motorcycle_List, options);
                        File.WriteAllText("motorcycle.json", json_Motorcycle);

                        Console.Clear();
                        Console.WriteLine("Saving all changes...");
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
                } else
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
                } catch (NullReferenceException)
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
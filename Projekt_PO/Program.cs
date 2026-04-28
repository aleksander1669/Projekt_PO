using Projekt_PO;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
            List<Bike> Bike_List = new List<Bike>();
            List<Motorcycle> Motorcycle_List = new List<Motorcycle>();
            List<Rent> Rent_List = new List<Rent>();
            List<Customer> Customer_List = new List<Customer>();
            var options = new JsonSerializerOptions { WriteIndented = true };
            
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
            int Rent_Id = 0;
            int Max_Id = 0;
            int Max_Id_Bike = 0;
            int Max_Id_Motorcycle = 0;

            if (Bike_List.Count == 0)
            {
                Max_Id_Bike = 0;
            } else
            {
                Max_Id_Bike = Bike_List.Max(b => b.Id);
            }
            if (Motorcycle_List.Count == 0)
            {
                Max_Id_Motorcycle = 0;
            }
            else
            {
                Max_Id_Motorcycle = Motorcycle_List.Max(b => b.Id);
            }

            if (Max_Id_Bike > Max_Id_Motorcycle)
            {
                Max_Id = Max_Id_Bike;
            } else
            {
                Max_Id = Max_Id_Motorcycle;
            }

            if (Rent_List.Count == 0)
            {
                Rent_Id = 0;
            }
            else
            {
                Rent_Id = Rent_List.Max(b =>b.Id);
            }

            Console.WriteLine("Welcome to our program!");

            do
            {
                choice = Int_Input("Choose interested option:\n1. Equipment management\n2. Rent management\n0. Exit\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        int choice_equipment_menager = Int_Input("Choose what you want to do:\n1. Add new equipment\n2. Show available equipment\n3. Remove equipment from database\n0. Return\nChoice: ", 0, 3, "Invalid choice", "Invalid choice");
                        if (choice_equipment_menager == 1)
                        {
                            Console.Clear();
                            int choice_add = Int_Input("Choose what equipment you want to add:\n1. Bike\n2. Motorcycle\n0. Return\nChoice: ", 0, 2, "Invalid choice", "Invalid choice");
                            if (choice_add == 1)
                            {
                                Console.Clear();
                                string a = String_Input("Enter name/type of your bike: ");

                                Console.Clear();
                                double b = Double_Input("Enter price per day for your bike: ", 5, 500, "Bike cannot cost that low", "Price is too high for a bike");

                                Console.Clear();
                                double c = Double_Input("Enter deposit for your bike: ", 50, 1000, "Bike deposit cannot be that low", "Deposit is too high for a bike");

                                Console.Clear();
                                string d = String_Input("Enter some maintenance information for your bike (for exmaple incoming chain conservation): ");

                                Max_Id++;
                                Bike nowy = new Bike(Max_Id, a, teraz, true, b, c, d);
                                Bike_List.Add(nowy);
                                Console.Clear();
                                
                                string json_string = JsonSerializer.Serialize(Bike_List, options);

                                File.WriteAllText("bike.json",  json_string);

                                Console.WriteLine("Bike was succesfully added to database");
                            } 
                            else if (choice_add == 2)
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

                                Max_Id++;
                                Motorcycle nowy = new Motorcycle(Max_Id, a, teraz, true, b, c, d, e, f, g);
                                Motorcycle_List.Add(nowy);
                                Console.Clear();

                                string json_string = JsonSerializer.Serialize(Motorcycle_List, options);

                                File.WriteAllText("motorcycle.json", json_string);

                                Console.WriteLine("Motorcycle was succesfully added to database");
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
                            int choice_view;
                            do
                            {
                                Console.Clear();
                                if (Bike_List.Count == 0 && Motorcycle_List.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There is no equipment added yet");
                                    exit_view = true;
                                }
                                if (Bike_List.Count > 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("Available bikes:");
                                    foreach (Bike bike in Bike_List)
                                    {
                                        bike.Info_Short();
                                    }
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (Motorcycle_List.Count > 0)
                                {
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("Available motorcycles:");
                                    foreach (Motorcycle motor in Motorcycle_List)
                                    {
                                        motor.Info_Short();
                                    }
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (Bike_List.Count > 0 || Motorcycle_List.Count > 0)
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
                                        Console.WriteLine("==================================================================================================================");
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
                                        Console.WriteLine("==================================================================================================================");
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
                        } else if (choice_equipment_menager == 3)
                        {
                            bool exit_del = false;
                            int choice_del;
                            do
                            {
                                Console.Clear();
                                if (Bike_List.Count == 0 && Motorcycle_List.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There is no equipment added yet");
                                    exit_del = true;
                                }
                                if (Bike_List.Count > 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("Available bikes:");
                                    foreach (Bike bike in Bike_List)
                                    {
                                        bike.Info_Short();
                                    }
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (Motorcycle_List.Count > 0)
                                {
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine("Available motorcycles:");
                                    foreach (Motorcycle motor in Motorcycle_List)
                                    {
                                        motor.Info_Short();
                                    }
                                    Console.WriteLine("==================================================================================================================");
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                                if (Bike_List.Count > 0 || Motorcycle_List.Count > 0)
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
                                        exit_del = true;
                                        Console.Clear();
                                        if (!bike_to_remove.Can_be_Removed())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("This bike cannot be removed it is already rented");
                                            continue;
                                        }
                                        Console.WriteLine("Bike with ID = " + choice_del + " is succesfully removed");
                                    }
                                    else if (motorcycle_to_remove != null)
                                    {
                                        Motorcycle_List.Remove(motorcycle_to_remove);
                                        exit_del = true;
                                        Console.Clear();
                                        if (!motorcycle_to_remove.Can_be_Removed())
                                        {
                                            Console.Clear();
                                            Console.WriteLine("This Motorcycle cannot be removed it is already rented");
                                            continue;
                                        }
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
                        else if (choice_equipment_menager == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        
                        int choice_rent = Int_Input("What action you want to do:\n1. Rent item\n2. Show all rents\n3. Settle rent\n0. Return\nChoice: ", 0, 3, "Invalid input", "Invalid input");

                        if (choice_rent == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        else if (choice_rent == 1)
                        {
                            Console.Clear();
                            if (Motorcycle_List.Count == 0 && Bike_List.Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no equipment added yet");
                            }
                            if (Bike_List.Count > 0)
                            {
                                Console.Clear();
                                Console.WriteLine("==================================================================================================================");
                                Console.WriteLine("Available bikes:");
                                foreach (Bike bike in Bike_List)
                                {
                                    bike.Info_Short();
                                }
                                Console.WriteLine("==================================================================================================================");
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            if (Motorcycle_List.Count > 0)
                            {
                                Console.WriteLine("==================================================================================================================");
                                Console.WriteLine("Available motorcycles:");
                                foreach (Motorcycle motor in Motorcycle_List)
                                {
                                    motor.Info_Short();
                                }
                                Console.WriteLine("==================================================================================================================");
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            if (Motorcycle_List.Count > 0 || Bike_List.Count > 0)
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

                                    Console.Clear();
                                    DateTime e = DateTime_Input("Insert date of return (Input example \"2027-6-15\"): ");
                                    Console.Clear();
                                    Console.WriteLine("Bike succesfully rented");

                                    Rent_Id++;
                                    rent_bike.Lendable(false);
                                    Customer new_customer = new Customer(a, b, c, d);
                                    Customer_List.Add(new_customer);
                                    Rent new_rent = new Rent(Rent_Id, new_customer, rent_bike, e);
                                    Rent_List.Add(new_rent);

                                    string json_Bike_Update = JsonSerializer.Serialize(Bike_List, options);
                                    File.WriteAllText("bike.json", json_Bike_Update);

                                    string json_rent = JsonSerializer.Serialize(Rent_List, options);

                                    File.WriteAllText("rent.json", json_rent);

                                } else if (rent_motorcycle != null)
                                {
                                    Console.Clear();
                                    string a = String_Input_No_Digits("Enter customer's name: ");

                                    Console.Clear();
                                    string b = String_Input_No_Digits("Enter customer's surename: ");

                                    Console.Clear();
                                    int c = Int_Input_Lenght("Enter customer's phone: ", 9, "Nuber needs to contain 9 digits");

                                    Console.Clear();
                                    string d = String_Input_No_Digits_Lenght("Enter customer's identification number: ", 11, "This number has to contain 11 digits");

                                    Console.Clear();
                                    DateTime e = DateTime_Input("Insert date of return (Input example \"2027-6-15\"): ");

                                    Rent_Id++;
                                    rent_motorcycle.Lendable(false);
                                    Customer new_customer = new Customer(a, b, c, d);
                                    Customer_List.Add(new_customer);
                                    Rent new_rent = new Rent(Rent_Id, new_customer, rent_motorcycle, e);
                                    Rent_List.Add(new_rent);

                                    string json_rent = JsonSerializer.Serialize(Rent_List, options);

                                    string json_Bike_Update = JsonSerializer.Serialize(Bike_List, options);
                                    File.WriteAllText("bike.json", json_Bike_Update);

                                    Console.Clear();
                                    Console.WriteLine("Bike succesfully rented");

                                    File.WriteAllText("rent.json", json_rent);
                                } else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid input");
                                    continue;
                                }
                            }
                        } else if (choice_rent == 2)
                        {
                            if (Rent_List.Count > 0)
                            {
                                foreach (Rent x in Rent_List)
                                {
                                    Console.Clear();
                                    x.Display_Rent();
                                    Console.WriteLine();
                                }
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                            } else
                            {
                                Console.Clear();
                                Console.WriteLine("There are no rents in database");
                                continue;
                            }
                        } else if (choice_rent == 3)
                        {
                            if (Rent_List.Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("There are no rents in database");
                                continue;
                            } else
                            {
                                bool exit_settle = false;

                                do
                                {
                                    Console.Clear();
                                    foreach (Rent x in Rent_List)
                                    {
                                        Console.Clear();
                                        x.Display_Rent();
                                        Console.WriteLine();
                                    }
                                    int choice_settle = Int_Input_No_Max_Or_Low("Enter the Id of rent you want to settle or 0 to cancel: ");

                                    if (choice_settle == 0)
                                    {
                                        Console.Clear();
                                        exit_settle = true;
                                        continue;
                                    }

                                    Rent rent_settle = Rent_List.FirstOrDefault(b => b.Id == choice_settle);

                                    if (Rent_List.FirstOrDefault(b => b.Id == choice_settle) == null)
                                    {
                                        Console.Clear();
                                        exit_settle = true;
                                        Console.WriteLine("There are no rents with ID: " + choice_settle);
                                        continue;
                                    }
                                    else if (Rent_List.FirstOrDefault(b => b.Id == choice_settle) != null)
                                    {

                                        int id_of_settled_item = rent_settle.Rented_Item.Id;

                                        Bike a = Bike_List.FirstOrDefault(b => b.Id == id_of_settled_item);

                                        Motorcycle b = Motorcycle_List.FirstOrDefault(b => b.Id == id_of_settled_item);

                                        if (a != null)
                                        {
                                            a.Lendable(true);
                                        }
                                        if (b != null)
                                        {
                                            b.Lendable(true);
                                        }


                                        Rent_List.Remove(rent_settle);

                                        string json_return = JsonSerializer.Serialize(Rent_List, options);

                                        File.WriteAllText("rent.json", json_return);

                                        string json_return_bike = JsonSerializer.Serialize(Bike_List, options);

                                        File.WriteAllText("bike.json", json_return_bike);

                                        string json_return_motorcycle = JsonSerializer.Serialize(Motorcycle_List, options);

                                        File.WriteAllText("motorcycle.json", json_return_motorcycle);


                                        Console.Clear();
                                        Console.WriteLine("Rent succesfully settled");
                                        exit_settle = true;
                                    }
                                } while (!exit_settle);

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
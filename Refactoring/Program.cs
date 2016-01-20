using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load users from data file
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"..\..\Data\Users.json"));

            // Load products from data file
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"..\..\Data\Products.json"));

            // Show app info
            Console.WriteLine("Welcome to TUSC");
            Console.WriteLine("---------------");

            // Login
            bool loggedIn = false;
            Console.WriteLine("Enter Username:");
            string name = Console.ReadLine();

            // Validate Username
            bool validUser = false;
            if (name.Length > 0)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    User user = users[i];
                    if (user.Username == name)
                    {
                        validUser = true;
                    }
                }

                if (validUser)
                {
                    Console.WriteLine("Enter Password:");
                    string password = Console.ReadLine();

                    // Validate Password
                    bool validPassword = false;
                    for (int i = 0; i < users.Count; i++)
                    {
                        User user = users[i];
                        if (user.Username == name && user.Password == password)
                        {
                            validPassword = true;
                        }
                    }

                    if (validPassword == true)
                    {
                        Console.WriteLine("Login successful!");
                        Console.WriteLine();
                        loggedIn = true;

                        // Write Greeting
                        Console.WriteLine("Welcome " + name + "!");

                        // Show remaining balance
                        double balance = 0;
                        for (int i = 0; i < users.Count; i++)
                        {
                            User user = users[i];
                            if (user.Username == name && user.Password == password)
                            {
                                balance = user.Balance;
                                Console.WriteLine("Your balance is " + user.Balance);
                                Console.WriteLine();
                            }
                        }

                        // Show product list
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("What would you like to buy?");
                            for (int i = 0; i < products.Count; i++)
                            {
                                Product product = products[i];
                                Console.WriteLine(i + 1 + ": " + product.Name + " (" + product.Price + ")");
                            }
                            Console.WriteLine(products.Count + ": Exit");

                            Console.WriteLine("Enter a number:");
                            string answer = Console.ReadLine();
                            int number = Convert.ToInt32(answer);
                            number = number - 1;

                            if (number == products.Count - 1)
                            {
                                // User selected Exit

                                // Write out new balance
                                foreach (var user in users)
                                {
                                    if (user.Username == name && user.Password == password)
                                    {
                                        user.Balance = balance;
                                    }
                                }
                                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                                File.WriteAllText(@"..\..\Data\Users.json", json);

                                // Write out new quantities
                                string json2 = JsonConvert.SerializeObject(products, Formatting.Indented);
                                File.WriteAllText(@"..\..\Data\Products.json", json);
                                

                                // Prevent console from closing
                                Console.WriteLine("Press any key to exit");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("You want to buy: " + products[number]);
                                Console.WriteLine("Your balance is " + balance);

                                // Check if user has enough balance
                                if (balance - products[number].Price < 0)
                                {
                                    Console.WriteLine("You do not have enough money to buy that.");
                                    continue;
                                }

                                // Check if product has any remaining quantity
                                if (products[number].Quantity <= 0)
                                {
                                    Console.WriteLine("Sorry, " + products[number]  + " is out of stock");
                                    continue;
                                }

                                Console.WriteLine("Enter Yes to purchase");
                                answer = Console.ReadLine();

                                if (answer == "Yes")
                                {
                                    balance = balance - products[number].Price;
                                    products[number].Quantity--;

                                    Console.WriteLine("You bought " + products[number].Name);
                                    Console.WriteLine("Your new balance is " + balance);
                                }
                                else 
                                {
                                    Console.WriteLine("Purchase cancelled");
                                }
                            }

                        }
                    }
                    else
                    {
                        // TODO: fix so we don't have to restart app on failed login
                        Console.WriteLine("You entered an invalid password. Please restart TUSC to try again.");

                        // Prevent console from closing
                        Console.WriteLine("Press any key to exit");
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    // TODO: fix so we don't have to restart app on failed login
                    Console.WriteLine("You entered an unknown user. Please restart TUSC to try again.");

                    // Prevent console from closing
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    return;
                }

            }

            // Prevent console from closing
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

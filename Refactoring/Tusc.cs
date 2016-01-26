using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Tusc
    {
        public static void Start(List<User> users, List<Product> products)
        {
            // Show app info
            Console.WriteLine("Welcome to TUSC");
            Console.WriteLine("---------------");

            // Login
            Login:
            bool loggedIn = false;
            Console.WriteLine();
            Console.WriteLine("Enter Username:");
            string name = Console.ReadLine();

            // Validate Username
            bool validUser = false;
            if (!string.IsNullOrEmpty(name))
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
                        loggedIn = true;

                        // Show welcome message
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine("Login successful! Welcome " + name + "!");
                        Console.ResetColor();
                        
                        // Show remaining balance
                        double balance = 0;
                        for (int i = 0; i < users.Count; i++)
                        {
                            User user = users[i];
                            if (user.Username == name && user.Password == password)
                            {
                                balance = user.Balance;
                                Console.WriteLine();
                                Console.WriteLine("Your balance is " + user.Balance.ToString("C"));
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
                                Console.WriteLine(i + 1 + ": " + product.Name + " (" + product.Price.ToString("C") + ")");
                            }
                            Console.WriteLine(products.Count + 1 + ": Exit");

                            Console.WriteLine("Enter a number:");
                            string answer = Console.ReadLine();
                            int number = Convert.ToInt32(answer);
                            number = number - 1;

                            if (number == products.Count)
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
                                File.WriteAllText(@"Data\Users.json", json);

                                // Write out new quantities
                                string json2 = JsonConvert.SerializeObject(products, Formatting.Indented);
                                File.WriteAllText(@"Data\Products.json", json2);


                                // Prevent console from closing
                                Console.WriteLine();
                                Console.WriteLine("Press Enter key to exit");
                                Console.ReadLine();
                                return;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("You want to buy: " + products[number].Name);
                                Console.WriteLine("Your balance is " + balance.ToString("C"));

                                // Prompt for quantity
                                Console.WriteLine("Enter amount to purchase:");
                                answer = Console.ReadLine();
                                int quantity = Convert.ToInt32(answer);

                                // Check if quantity is less than 0
                                if (balance - products[number].Price * quantity < 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine();
                                    Console.WriteLine("You do not have enough money to buy that.");
                                    Console.ResetColor();
                                    continue;
                                }

                                // Check if user has enough balance
                                if (products[number].Quantity <= quantity)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine();
                                    Console.WriteLine("Sorry, " + products[number].Name + " is out of stock");
                                    Console.ResetColor();
                                    continue;
                                }

                                // If quantity is greater than zero
                                if (quantity > 0)
                                {
                                    balance = balance - products[number].Price * quantity;
                                    products[number].Quantity = products[number].Quantity - quantity;

                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("You bought " + quantity + " " + products[number].Name);
                                    Console.WriteLine("Your new balance is " + balance.ToString("C"));
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine();
                                    Console.WriteLine("Purchase cancelled");
                                    Console.ResetColor();
                                }
                            }
                        }
                    }
                    else
                    {
                        // Invalid Password
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("You entered an invalid password.");
                        Console.ResetColor();

                        goto Login;
                    }
                }
                else
                {
                    // Invalid User
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("You entered an invalid user.");
                    Console.ResetColor();

                    goto Login;
                }
            }

            // Prevent console from closing
            Console.WriteLine();
            Console.WriteLine("Press Enter key to exit");
            Console.ReadLine();
        }
    }
}

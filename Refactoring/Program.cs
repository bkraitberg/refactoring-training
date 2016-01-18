using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] validUsers = { "Jason", "David", "Glen", "Rod" };
            string[] passwords = { "sfa", "upmc", "hitech", "optx" };
            double[] balances = { -7.50, 1.25, 12.75, 38.50 };

            string[] products = { "Chips", "Chocolate Bars", "Candy", "Gum", "Pop" };
            double[] prices = { 1.49, 1.25, 1.00, 0.80, 0.75 };

            // Show app info
            Console.WriteLine("Welcome to TUSC");

            // Login
            bool loggedIn = false;
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();
            if (name.Length > 0)
            {
                if (validUsers.Contains(name)) 
                {
                    // Valid user!
                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();
                    
                    // Find user index
                    for (int i = 0; i < validUsers.Length; i++)
                    {
                        if (validUsers[i] == name)
                        {
                            if (passwords[i] == password)
                            {
                                Console.WriteLine("Login successful!");
                                loggedIn = true;

                                // Greeting
                                Console.WriteLine("Welcome " + name + "!");

                                // Show remaining balance
                                double balance = 0;

                                // Find user index
                                for (int j = 0; j < validUsers.Length; j++)
                                {
                                    if (validUsers[j] == name)
                                    {
                                        balance = balances[j];
                                        Console.WriteLine("Your balance is " + balance);
                                        break;
                                    }
                                }

                                // Show product list
                                while (true)
                                {
                                    Console.WriteLine("What would you like to buy?");
                                    for (int k = 0; k < products.Length; k++)
                                    {
                                        // Get price
                                        double price = 0.0;
                                        for (int l = 0; l < prices.Length; l++)
                                        {
                                            price = prices[k];
                                        }

                                        Console.WriteLine(k + 1 + ": " + products[k] + " (" + price +  ")");
                                    }
                                    Console.WriteLine("Enter \"Exit\" to exit");

                                    Console.WriteLine("Enter a number:");
                                    string answer = Console.ReadLine();
                                    if (answer == "Exit")
                                    {
                                        break;
                                    }

                                    int number = Convert.ToInt32(answer);
                                    number = number - 1;

                                    Console.WriteLine("You want to buy: " + products[number]);
                                    Console.WriteLine("Your balance is " + balance);
                                    Console.WriteLine("Enter Yes to purchase");
                                    answer = Console.ReadLine();

                                    if (answer == "Yes")
                                    {
                                        balance = balance - prices[number];
                                        Console.WriteLine("You bought " + products[number]);
                                        Console.WriteLine("Your new balance is " + balance);
                                    }

                                    if (answer == "Exit")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (loggedIn == false)
                    {
                        Console.WriteLine("Login failed. Please restart TUSC to try again.");
                        Console.WriteLine("Press enter to exit");
                        Console.ReadLine();
                        return; 
                    }

                }
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}

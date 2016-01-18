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

            // Greeting
            Console.WriteLine("Welcome " + name + "!");
            
            // Show remaining balance
            double balance = 0;

            // Find user index
            for (int i = 0; i < validUsers.Length; i++)
            {
                if (validUsers[i] == name)
                {
                    balance = balances[i];
                    Console.WriteLine("Your balance is " + balance);
                    break;
                }
            }


            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}

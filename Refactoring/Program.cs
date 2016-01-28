using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Refactoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load users from data file
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data/Users.json"));

            // Load products from data file
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data/Products.json"));

            Tusc.Start(users, products);
        }
    }
}

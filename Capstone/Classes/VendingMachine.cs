using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public List<Food> foodItems = new List<Food>();
        public VendingMachine()
        {

            string directory = Environment.CurrentDirectory;
            string fileName = @"vendingmachine.csv";
            string fullPath = Path.Combine(directory, fileName);
            
            //read file and fill foodItems list with the food
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        //reading each line of the csv file
                        string line = sr.ReadLine();

                        //separating the info from each line
                        string[] splitLine = line.Split("|");

                        //retrieving the classname, location, name, price

                        // format of CSV file is: location | itemName | price | class (gum candy etc)
                        string className = splitLine[splitLine.Length - 1];
                        string location = splitLine[0];
                        string itemName = splitLine[1];
                        // parse decimal from file / string array
                        decimal price = decimal.Parse(splitLine[2]);
                        
                        
                        // adding each class of food to foodItems list
                        if (className == "Candy")
                        {
                            Candy item = new Candy(location, itemName, price);
                            foodItems.Add(item);
                        }
                        if (className == "Chip")
                        {
                            Chip item = new Chip(location, itemName, price);
                            foodItems.Add(item);
                        }
                        if (className == "Drink")
                        {
                            Drink item  = new Drink(location, itemName, price);
                            foodItems.Add(item);
                        }
                        if (className == "Gum")
                        {
                            Gum item  = new Gum(location, itemName, price);
                            foodItems.Add(item);
                        }                       
                    }
                }
            }
            // if someone deleted the CSV file, wouldnt be able to find the information and would return as follows.
            catch (IOException)
            {
                Console.WriteLine("The vending machine file was not found");
            }
            // any other exception that falls outside of our parameters.
            catch (Exception)
            {
                Console.WriteLine("There was a mistake inside the vending machine file");
            }
        }

        public void PurchaseFood(LogSheet logsheet)
        {
            this.DisplayItems();
            // displays current balance 
            logsheet.AdjustBalance(0);

            Console.WriteLine("Please enter item key:");//updates inventory and logsheet
            string order = Console.ReadLine();
            Console.Clear();

            foreach (Food item in foodItems)
            {
                if (order.ToUpper() == item.Location)
                {
                    if (item.SnacksLeft == 0)
                    {
                        Console.WriteLine("SOLD OUT :_(");
                    }
                    else
                    {
                        if (logsheet.AdjustBalance(item)) //calling adjustbalance...this is returning a bool AND adjusting our balance
                        {
                            item.PrintMessage();//dispense food - print message
                            item.SnacksLeft--; // track inventory
                        }
                        
                    }
                }
            }
        }


        public void DisplayItems()
        {
            foreach (Food item in foodItems)
            {
                Console.WriteLine($"{item.Location} | {item.Name} | ${item.Cost} | Remaining: {item.SnacksLeft}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    // abstract class because we dont just creat basic foods. only artistic abstract foods.
    public abstract class Food
    {
        public int startingSnacks = 5;
        public string Name { get; }
        public decimal Cost { get; set; }
        public string Location { get; }
        public int snacksLeft;
        public int SnacksLeft { get; set; }
        public Food(string location, string name, decimal cost)
        {
            this.Location = location;
            this.Name = name;
            this.Cost = cost;
            this.SnacksLeft = startingSnacks;
        }
        public abstract void PrintMessage();

    }
}

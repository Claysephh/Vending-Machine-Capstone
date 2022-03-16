using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Gum : Food
    {
        public override void PrintMessage()
        {
            string title = @"

     Here's your gum!
        
       .-_---------------. ""Chew            
      / /_  | | |\  /|  /|     Chew,
     /  \_/ \_/ | \/ | / |       Yum!""
    :.________________/ /
    | .--. .--. .--.  |/
    '-----------------'
                                   ";

            Console.WriteLine(title);
        }
        public Gum(string location, string name, decimal cost) : base(location, name, cost) { }
    }
}

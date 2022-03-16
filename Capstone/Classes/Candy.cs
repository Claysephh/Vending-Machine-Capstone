using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy : Food
    {
        public override void PrintMessage()
        {
            string title = @"
   Here's your Candy!
    ___  ___  ___  ___  ___.---------------.
  .'\__\'\__\'\__\'\__\'\__,`   .  ____ ___ \
  |\/ __\/ __\/ __\/ __\/ _:\   |`.  \  \___ \    ""Munch
   \\'\__\'\__\'\__\'\__\'\_`.__|""""`. \  \___ \      Munch,
    \\/ __\/ __\/ __\/ __\/ __:                \          Yum!""
     \\'\__\'\__\'\__\ \__\'\_;-----------------` 
      \\/   \/   \/   \/   \/ :               hh|
       \|______________________;________________| ";

            Console.WriteLine(title);
        }
        public Candy(string location, string name, decimal cost) : base(location, name, cost) { }
    }
}

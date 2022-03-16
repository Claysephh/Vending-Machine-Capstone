using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chip : Food
    {
        public override void PrintMessage()
        {

            string title = @"

Here's your chips!
                               _
                        ____,-' `-.
""Crunch              .' .--.\.    \
     Crunch,      _.' `-.`--'  `.   \
       Yum!""    /       \ `.-' /`-. `-.
               /`-.      `-.` /.-' `.  \
              _\   \        `-. \.-' \.'
       ____ .' \,-. `.         `.\.-'
      /  .-`--./   \  \     ___.'      
    ,-`-/  ___/__._'   /_.-'
   (  .-`-(__ /  `.`-''
    `/   |/  `|    |
     `--' \   \    |
           `--'`._.'
";
            Console.WriteLine(title);
        }
        public Chip(string location, string name, decimal cost) : base(location, name, cost) { }
        
    }
}

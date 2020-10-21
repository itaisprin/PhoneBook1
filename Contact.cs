using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace PhoneBook1
{
    class Contact //contact profile
    {

        public string name; //name of caller
        public string mail; //email of caller
        public string number; //the phone number itself
        public bool numberIntact; //if the number is intact (true/false)

        public List<DateTime> callHistories = new List<DateTime>(); //list of call histories

        public bool integer(string a)
        {
            if(a == "")
            {
                return false;
            }
            foreach(char c in a)
            {
                if(char.IsDigit(c) == false)
                {
                    return false;
                }
            }
            return true;
        }//makes sure a string is made only out of numbers


        
        




    }
}

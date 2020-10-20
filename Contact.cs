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

        public class phoneNumber //phone number class
        {
            public string number; //the phone number itself
            public bool numberIntact; //if the number is intact (true/false)
        }

        public phoneNumber newPhoneNumber = new phoneNumber(); //phone number of caller

        public class callHistory //info about one call
        {
            public DateTime time; //time they were called
        }

        public List<callHistory> callHistories = new List<callHistory>(); //list of call histories

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

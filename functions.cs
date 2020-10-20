using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhoneBook1
{
    class functions
    {
        public Contact addNewContact(string name, string mail, string number, List<Contact.callHistory> calls)
        {
            Contact newContact = new Contact();
            newContact.name = name;
            newContact.mail = mail;
            newContact.newPhoneNumber.number = number;
            newContact.callHistories = calls;
            newContact.newPhoneNumber.numberIntact = numIntact(number);
            return newContact;
        }

        public bool numIntact(string number)
        {
            char[] array = number.ToCharArray();
            if(array[1] == '5' && array[0] == '0')
            {
                return true;
            }
            return false;
        }
        public class activeContact //this class actively makes sure a contact is added correctly by the user. 
        {
            public bool integer(string a)
            {
                if (a == "")
                {
                    return false;
                }
                foreach (char c in a)
                {
                    if (char.IsDigit(c) == false)
                    {
                        return false;
                    }
                }
                return true;
            }//makes sure a string is made only out of numbers
            public string fixPhone(string contactNum) //makes sure only a proper phone number is entered
            {
                string num = contactNum;
                bool isANum = true;
                foreach (char c in num)
                {
                    if (char.IsDigit(c) == false)
                    {
                        isANum = false;
                        break;
                    }
                }
                while (num.Length != 10 || isANum == false)
                {
                    Console.WriteLine("Your number is unavailable, please enter a different one.");
                    num = Console.ReadLine();
                    int zero = 0;
                    foreach (char c in num)
                    {
                        if (char.IsDigit(c) == false)
                        {
                            isANum = false;
                            zero++;
                            break;
                        }
                    }
                    if (zero == 0)
                    {
                        isANum = true;
                    }
                }
                contactNum = num;
                return contactNum;
            }

            public string fixName(string contactName)
            {
                bool badName = true;
                string finalName = "";
                string[] name = contactName.Split(' ');
                while (badName)
                {
                    int OK = 0;
                    if (name.Length != 2)
                    {
                        OK++;
                    }
                    foreach (string s in name)
                    {
                        foreach (char c in s)
                        {
                            if (char.IsLetter(c) != true)
                            {
                                OK++;
                                break;
                            }
                        }
                    }
                    if (OK == 0)
                    {
                        badName = false;
                    }
                    else
                    {
                        Console.WriteLine("Name is unavailable to use, please enter a proper name:");
                        name = Console.ReadLine().Split(' ');
                    }
                }

                foreach (string s in name)
                {
                    char[] a = s.ToLower().ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    finalName += new string(a) + " ";
                }
                return finalName;
            } //makes sure only a proper name is entered
            public DateTime fixTime()  //make sure the date is entered properly
            {
                string year = "0001";
                string month = "01";
                string day = "01";
                string hour = "00";
                string minute = "00";
                do
                {
                    Console.WriteLine("Enter Year");
                    year = Console.ReadLine();
                    if (integer(year) == false)
                    {
                        year = "-1";
                    }
                } while ((Convert.ToInt32(year)) < 0 || (Convert.ToInt32(year)) > 9999);
                do
                {
                    Console.WriteLine("Enter Month");
                    month = Console.ReadLine();
                    if (integer(month) == false)
                    {
                        month = "-1";
                    }
                } while ((Convert.ToInt32(month)) < 01 || (Convert.ToInt32(month)) > 12);
                do
                {
                    Console.WriteLine("Enter Day");
                    day = Console.ReadLine();
                    if (integer(day) == false)
                    {
                        day = "-1";
                    }
                } while ((Convert.ToInt32(day) < 01) || (Convert.ToInt32(day)) > 31);
                do
                {
                    Console.WriteLine("Enter Hour");
                    hour = Console.ReadLine();
                    if (integer(hour) == false)
                    {
                        if (hour == "")
                        {
                            hour = "00";
                            break;
                        }
                        hour = "-1";
                    }
                } while ((Convert.ToInt32(hour)) < 00 || (Convert.ToInt32(hour)) > 23);
                do
                {
                    Console.WriteLine("Enter Minute");
                    minute = Console.ReadLine();
                    if (integer(minute) == false)
                    {
                        if (minute == "")
                        {
                            minute = "00";
                            break;
                        }
                        minute = "-1";
                    }
                } while ((Convert.ToInt32(minute)) < 00 || (Convert.ToInt32(minute)) > 60);
                DateTime time = new DateTime((Convert.ToInt32(year)), (Convert.ToInt32(month)), (Convert.ToInt32(day)),
                    (Convert.ToInt32(hour)), Convert.ToInt32(minute), 00);
                return time;
            }

            public Contact newContact() //add new contact
            {
                functions functions = new functions();
                Data data = new Data();
                Console.WriteLine("What is the contact's name?");
                string name = fixName(Console.ReadLine());
                Console.WriteLine("What is the contact's mail?");
                string mail = Console.ReadLine();
                Console.WriteLine("What is the contact's phone number?");
                string phoneNum = Console.ReadLine();
                string number = fixPhone(phoneNum);
                List<Contact.callHistory> callHistories = new List<Contact.callHistory>();
                while (true)
                {
                    Console.WriteLine("Do you have any calls to add to your call history? To continue type anything, If not press enter.");
                    string temp1 = Console.ReadLine();
                    if (temp1 == "")
                    {
                        break;
                    }
                    Contact.callHistory history = new Contact.callHistory();
                    Console.WriteLine("When did you call this contact?");
                    DateTime a = new DateTime();
                    a = fixTime();
                    history.time = a;
                    callHistories.Add(history);

                }
                Contact newContact = functions.addNewContact(name, mail, number, callHistories);
                if (data.duplicate(newContact) == true)
                {
                    Console.WriteLine("This contact has an identical name or number to an exsiting contact");
                    return null;
                }
                return newContact;

            }
        }
        public activeContact active = new activeContact();

        public void sortDates(List<Contact.callHistory> callHistories) //sorts call histories by date
        {
            callHistories.Sort((a, b) => DateTime.Compare(b.time, a.time));
        }
        public void printContact(Contact contact) //prints the contact out
        {
            Console.WriteLine("Name:" + contact.name);
            Console.WriteLine("Email Address:" + contact.mail);
            Console.WriteLine("Phone Number:" + contact.newPhoneNumber.number);
            Console.WriteLine("Is Phone Number Active:" + contact.newPhoneNumber.numberIntact);
            printHistory(contact.callHistories);
            Console.WriteLine("");
        }
        public void printHistory(List<Contact.callHistory> callHistories) //print the call history logs for one contact
        {
            sortDates(callHistories);
            foreach (var history in callHistories)
            {
                Console.WriteLine(history.time);
            }
        }

        public void call(Contact contact) //option to call a contact
        {
            Contact.callHistory call = new Contact.callHistory();
            call.time = DateTime.Now;
            contact.callHistories.Add(call);
        }

        public void editContact(Contact contact) //edit a contact from the contact book
        {
            activeContact activeContact = new activeContact();
            Console.WriteLine("What would like to change the name to? If you don't want to change the name press enter.");
            string temp = Console.ReadLine();
            if (temp != "")
            {
                contact.name = activeContact.fixName(temp);
            }
            Console.WriteLine("What would like to change the mail to? If you don't want to change the mail press enter.");
            temp = Console.ReadLine();
            if (temp != "")
            {
                contact.mail = temp;
            }
            Console.WriteLine("What would like to change the number to? If you don't want to change the number press enter.");
            temp = Console.ReadLine();
            if (temp != "")
            {
                contact.newPhoneNumber.number = activeContact.fixPhone(temp);
                contact.newPhoneNumber.numberIntact = numIntact(temp);
            }


        }

        public string isValidName(string contactName) //make sure only a valid name is entered
        {
            bool badName = true;
            string finalName = "";
            string[] name = contactName.Split(' ');
            while (badName)
            {
                int OK = 0;
                if (name.Length != 2)
                {
                    OK++;
                }
                foreach (string s in name)
                {
                    foreach (char c in s)
                    {
                        if (char.IsLetter(c) != true)
                        {
                            OK++;
                            break;
                        }
                    }
                }
                if (OK == 0)
                {
                    badName = false;
                }
                else
                {
                    return null;
                }
            }

            foreach (string s in name)
            {
                char[] a = s.ToLower().ToCharArray();
                a[0] = char.ToUpper(a[0]);
                finalName += new string(a) + " ";
            }
            return finalName;
        }

        public string isValidNumber(string contactNum) //makes sure only a proper phone number is entered
        {
            string num = contactNum;
            bool isANum = true;
            foreach (char c in num)
            {
                if (char.IsDigit(c) == false)
                {
                    isANum = false;
                    break;
                }
            }
            if(num.Length != 10)
            {
                isANum = false;
            }
            if(isANum == false)
            {
                return null;
            }
            return num;
        }
        public Contact addFromFile(string filePath)
        {
            string[] contactDetails = File.ReadAllLines(filePath);
            if (contactDetails.Length == 0)
            {
                return null;
            }
            Contact newContact = new Contact();
            for(int i = 0; i<4; i++)
            {
                string temp;
                switch (i)
                {
                    case 0:
                        temp = isValidName(contactDetails[i]);
                        if(temp == null)
                        {
                            return null;
                        }
                        newContact.name = temp;
                        break;
                    case 1:
                        newContact.mail = contactDetails[i];
                        break;
                    case 2:
                        temp = isValidNumber(contactDetails[i]);
                        if (temp == null)
                        {
                            return null;
                        }
                        newContact.newPhoneNumber.number = temp;
                        newContact.newPhoneNumber.numberIntact = numIntact(temp);
                        break;
                }
            }
            return newContact;
        }


    }
}

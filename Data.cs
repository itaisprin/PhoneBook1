using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhoneBook1
{
    class Data
    {
        functions function = new functions();

        public List<Contact> contactList = new List<Contact>(); //the database for contacts

        public Contact search(string nameOrNumber) //search for contact
        {
            if(nameOrNumber == "")
            {
                return null;
            }
            Char[] charry = nameOrNumber.ToCharArray();
            if (char.IsDigit(charry[0]) == true)
            {
                foreach (var contact in contactList)
                {
                    if (nameOrNumber == Convert.ToString(contact.newPhoneNumber.number))
                    {
                        return contact;
                    }
                }
            }
            if (char.IsLetter(charry[0]) == true)
            {
                foreach (var contact in contactList)
                {
                    string tempName = "";
                    string[] name = nameOrNumber.Split(' ');
                    foreach (string s in name)
                    {
                        char[] a = s.ToLower().ToCharArray();
                        a[0] = char.ToUpper(a[0]);
                        tempName += new string(a) + " ";
                    }
                    if (tempName == Convert.ToString(contact.name))
                    {
                        return contact;
                    }
                }
            }
            return null;
        }

        public bool duplicate(Contact contact) //checks for duplicate names
        {
            foreach(Contact c in contactList)
            {
                if(c.name == contact.name || c.newPhoneNumber.number == contact.newPhoneNumber.number)
                {
                    return true;
                }
            }
            return false;
        }

        public void deleteContact(string nameOrNumber) //delete contacts
        {
            bool deleted = false;
            foreach (Contact contact in contactList)
            {
                if (search(nameOrNumber) == contact)
                {
                    contactList.Remove(contact);
                    deleted = true;
                    break;
                }
            }
            if (deleted == false)
            {
                Console.WriteLine("The contact you looked for was not found.");
            }
        }

        public void sortContacts() //sorts contacts by name
        {
            contactList.Sort((a, b) => string.Compare(a.name, b.name));
        }
        public void printAllContacts() //prints the whole contact book
        {
            sortContacts();
            foreach (Contact contact in contactList)
            {
                function.printContact(contact);
            }
        } 

        class hist
        {
            public DateTime time;
            public string name;
        }
        public void printAllCallHistory() //prints all call log from the book in order by date
        {
            List<hist> newList = new List<hist>();
            foreach(Contact contact in contactList)
            {
                foreach(Contact.callHistory callHistory in contact.callHistories)
                {
                    hist call = new hist();
                    call.time = callHistory.time;
                    call.name = contact.name;
                    newList.Add(call);
                }
            }
            newList.Sort((a, b) => DateTime.Compare(b.time, a.time));
            foreach(hist hist in newList)
            {
                if(newList.ToArray().Length == 0)
                {
                    Console.WriteLine("You have no call history.");
                    break;
                }
                Console.WriteLine(hist.time + "  " + hist.name);
            }
        }

        public void addToList(Contact contact) //makes sure only valid contacts are added to contact book
        {
            while (true)
            {
                if(contact == null)
                {
                    break;
                }
                if(duplicate(contact) == true)
                {
                    break;
                }
                contactList.Add(contact);
                break;
            }
        }

        public void exportContacts(string path) //exports the whole contact book to as csv file
        {
            sortContacts();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine("name" + "," + "mail" + "," + "number" + "," +
                        "numberIntact" + "," + "call history");
                foreach (Contact contact in contactList)
                {
                    string callH = "";
                    foreach (Contact.callHistory call in contact.callHistories)
                    {
                        callH = callH + call.time + " " ;
                    }
                    file.WriteLine(contact.name + "," + contact.mail + "," + contact.newPhoneNumber.number + "," +
                        contact.newPhoneNumber.numberIntact + "," + callH);
                }
                file.Close();
                file.Dispose();
            }
        }
    }
}

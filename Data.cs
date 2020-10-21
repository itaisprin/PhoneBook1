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
                foreach (Contact contact in contactList)
                {
                    if (nameOrNumber == contact.number || nameOrNumber == contact.name)
                    {
                        return contact;
                    }
                    else { return null; }
                }
            }
            string[] name = nameOrNumber.Split(' ');
            string finalName="";
            foreach (string s in name)
            {
                char[] a = s.ToLower().ToCharArray();
                foreach (char c in a)
                {
                    if (char.IsLetter(c) != true)
                    {
                        continue;
                    }
                    Char.ToLower(c);
                }
                if(a.Length != 0)
                {
                    if (Char.IsLetter(a[0]) == true)
                    {
                        a[0] = char.ToUpper(a[0]);
                    }
                }
                finalName += new string(a) + " ";
            }
            foreach (Contact contact in contactList)
            {
                if(nameOrNumber == contact.number || finalName == contact.name)
                {
                    return contact;
                }
            }
            return null;
        }

        public bool duplicate(Contact contact) //checks for duplicate names
        {
            foreach(Contact c in contactList)
            {
                if(c.name == contact.name || c.number == contact.number)
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
                foreach(DateTime callHistory in contact.callHistories)
                {
                    hist call = new hist();
                    call.time = callHistory;
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
                    foreach (DateTime call in contact.callHistories)
                    {
                        callH = callH + call + " " ;
                    }
                    file.WriteLine(contact.name + "," + contact.mail + "," + contact.number + "," +
                        contact.numberIntact + "," + callH);
                }
                file.Close();
                file.Dispose();
            }
        }
    }
}

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

        public Contact duplicate(Contact contact, bool boolean) //checks for duplicate names and returns the existing contact
        {
            foreach (Contact c in contactList)
            {
                if (c.name == contact.name || c.number == contact.number)
                {
                    return contact;
                }
            }
            return null;
        }
        public Contact duplicate(Contact contact, bool boolean, bool boolean2)
        {
            List<Contact> dupList = contactList;
            dupList.Remove(duplicate(contact, true));
            {
                foreach (Contact c in dupList)
                {
                    if (c.name == contact.name || c.number == contact.number)
                    {
                        return c;
                    }
                }
                return null;
            }
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
                    Console.WriteLine("You tried to enter the following contact:\n");
                    function.printContact(contact);
                    Contact contact1 = duplicate(contact, true);
                    Console.WriteLine("The following contact already exists:\n");
                    function.printContact(contact1);
                    if (duplicate(contact, true, true) != null)
                    {
                        Console.WriteLine("The following contact also already exists:\n");
                        function.printContact(duplicate(contact, true, true));
                        Console.WriteLine("Unfortunately, we could not add your contact the the book.");
                        break;
                    }
                    Console.WriteLine("What would you like to do?\n1.Replace old contact with new one.\n2.Keep old contact and discard new one.");
                    string temp = Console.ReadLine();
                    switch (temp)
                    {
                        case "1":

                            contactList.Add(contact);
                            contactList.Remove(contact1);
                            break;
                        case "2":
                            break;
                        default:
                            break;

                    }
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

        public void dynamicSearch(string nameOrNumber)
        {
            sortContacts();
            List<Contact> dynamicList = new List<Contact>();
            nameOrNumber.ToLower();
            foreach(Contact contact in contactList)
            {
                if (contact.name.ToLower().Contains(nameOrNumber) == true || contact.number.Contains(nameOrNumber) == true)
                    dynamicList.Add(contact);
            }
            while (dynamicList.ToArray().Length != 0)
            {
                foreach(Contact c in dynamicList)
                {
                    function.printContact(c);
                }
                break;
            }
            if(dynamicList.ToArray().Length == 0)
                Console.WriteLine("Contact couldn't be found");
        }
    }
}

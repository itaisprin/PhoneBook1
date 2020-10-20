using System;
using System.IO;
using System.Threading;

namespace PhoneBook1
{
    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            bool program = true;
            functions function = new functions();
            while (program) // the program
            {
                Console.WriteLine("Welcome to your contact manager! Pick one of the following options:");
                Console.WriteLine("1. Add Contact \n2. Edit Contact \n3. Delete Contact \n4. Call Contact " +
                    "\n5. Browse Contacts \n6. Search Contact \n7. Import Contact \n8. Export Contact " +
                    "\n9. Call History \n10. Exit");
                string i = Console.ReadLine();
                switch (i) 
                {
                    case "1":
                        Contact contact = function.active.newContact();
                        data.addToList(contact);
                        break;
                    case "2":
                        Console.WriteLine("Write name or number of contact you would lke to edit");
                        string temp = Console.ReadLine();
                        Contact contact1 = data.search(temp);
                        if(contact1 == null)
                        {
                            Console.WriteLine("The contact you looked for does not exist, or you had a typo.");
                            break;
                        }
                        function.editContact(contact1);
                        break;
                    case "3":
                        Console.WriteLine("Write name or number of contact you would lke to delete");
                        string temp1 = Console.ReadLine();
                        data.deleteContact(temp1);
                        break;
                    case "4":
                        Console.WriteLine("Which contact would you like to call?");
                        string temp2 = Console.ReadLine();
                        Contact contact2 = data.search(temp2);
                        if (contact2 == null)
                        {
                            Console.WriteLine("The contact you looked for does not exist, or you had a typo.");
                            break;
                        }
                        function.call(contact2);
                        break;
                    case "5":
                        data.printAllContacts();
                        break;
                    case "6":
                        Console.WriteLine("Which contact would you like to look for?Enter name or number.");
                        Contact contact3 = data.search(Console.ReadLine());
                        if (contact3 == null)
                        {
                            Console.WriteLine("The contact you looked for does not exist, or you had a typo.");
                            break;
                        }
                        function.printContact(contact3);
                        break;
                    case "7":
                        Console.WriteLine("Type location of the file you woud like to import a contact from. The format should be - @C:\\ab\\bc\\file.txt and so on");
                        string temp3 = Console.ReadLine();
                        if (System.IO.File.Exists(temp3) == false || temp3.EndsWith(".txt") == false)
                        {
                            Console.WriteLine("The file you typed in could not be found or used");
                            break;
                        }
                        Contact contact4 = function.addFromFile(temp3);
                        if (contact4 == null)
                        {
                            Console.WriteLine("The contact you looked for does not exist, or you had a typo.");
                            break;
                        }
                        data.addToList(contact4);
                        break;
                    case "8":
                        string csvFile = @"C:\Users\itie\source\repos\PhoneBook1\export.csv";
                        //string csvFile = Console.ReadLine();
                        if (System.IO.File.Exists(csvFile) == false || csvFile.EndsWith(".csv") == false)
                        {
                            Console.WriteLine("The file you typed in could not be found or used");
                            break;
                        }
                        data.exportContacts(csvFile);
                        break;
                    case "9":
                        data.printAllCallHistory();
                        break;
                    case "10":
                        program = false;
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}

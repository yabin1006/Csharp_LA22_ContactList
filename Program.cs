using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LA22_ContactList_Robin
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactOperation cop = new ContactOperation();
            Console.WriteLine("Phone Contact List");
            bool conti = true;
            do
            {
                Console.WriteLine("\nContact List Menu:\n1. List all contact\n2. Add name and phone number to contact" +
                    "\n3. Update phone number for existed contact\n4. Update contact name\n5. Delete a phone number from a contact" +
                    "\n6. Delete a whole contact by name\n7. Search a phone number by name\n8. Load cotact from text file\n9. Exit menu");
                //ConsoleKeyInfo cki = Console.ReadKey();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D9:
                        conti = false;
                        break;
                    case ConsoleKey.D1:
                        cop.ShowContact();
                        break;
                    case ConsoleKey.D2:
                        Console.Write("\nEnter contact full name (first name and last name): ");
                        string fullname;
                        while((fullname = Console.ReadLine()) == "")
                            Console.Write("Try to enter full name: ");
                        Console.Write("Enter phone number (10 digital number): ");
                        ulong phonenum;
                        while (!ulong.TryParse(Console.ReadLine(), out phonenum) || phonenum<1.0e9 || phonenum>=1.0e10)
                            Console.Write("Phone number is NOT valid, try again: ");
                        char pnum_type;
                        Console.Write("Select the type (H/W/C/O) of the phone number (For Home/Work/Cell/Other): ");
                        while ((pnum_type =char.ToUpper(Convert.ToChar(Console.ReadLine()))) != 'H' && pnum_type != 'W' && pnum_type != 'C' && pnum_type != 'O')
                            Console.Write("The selection is NOT valid, try again (H/W/C/O): ");
                        string type = pnum_type.ToString();
                        cop.AddNum(fullname, phonenum, type);
                        break;
                    case ConsoleKey.D3:
                        Console.Write("\nEnter contact full name (first name and last name): ");
                        while ((fullname = Console.ReadLine()) == "")
                            Console.Write("Try to enter full name: ");
                        Console.Write("Enter old phone number (10 digital number): ");
                        ulong oldphonenum, newphonenum;
                        while (!ulong.TryParse(Console.ReadLine(), out oldphonenum) || oldphonenum < 1.0e9 || oldphonenum >= 1.0e10)
                            Console.Write("Old phone number is NOT valid, try again: ");
                        Console.Write("Enter new phone number (10 digital number): ");
                        while (!ulong.TryParse(Console.ReadLine(), out newphonenum) || newphonenum < 1.0e9 || newphonenum >= 1.0e10)
                            Console.Write("New phone number is NOT valid, try again: ");
                        Console.Write("Select the type (H/W/C/O) of the phone number (For Home/Work/Cell/Other): ");
                        while ((pnum_type = char.ToUpper(Convert.ToChar(Console.ReadLine()))) != 'H' && pnum_type != 'W' && pnum_type != 'C' && pnum_type != 'O')
                            Console.Write("The selection is NOT valid, try again (H/W/C/O): ");
                        type = pnum_type.ToString();
                        cop.UpdateNumByName(fullname, oldphonenum, newphonenum, type);
                        break;
                    case ConsoleKey.D4:
                        Console.Write("\nEnter old contact full name (first name and last name): ");
                        string oldname, newname;
                        while ((oldname = Console.ReadLine()) == "")
                            Console.Write("Try to enter old full name: ");
                        Console.Write("Enter new contact full name (first name and last name): ");
                        while ((newname = Console.ReadLine()) == "")
                            Console.Write("Try to enter new full name: ");
                        cop.UpdateName(oldname, newname);
                        break;
                    case ConsoleKey.D5:
                        Console.Write("\nEnter contact full name (first name and last name): ");
                        while ((fullname = Console.ReadLine()) == "")
                            Console.Write("Try to enter full name: ");
                        Console.Write("Enter phone number (10 digital number): ");
                        while (!ulong.TryParse(Console.ReadLine(), out phonenum) || phonenum < 1.0e9 || phonenum >= 1.0e10)
                            Console.Write("Phone number is NOT valid, try again: ");
                        cop.DeleteNum(fullname, phonenum);
                        break;
                    case ConsoleKey.D6:
                        Console.Write("\nEnter contact full name (first name and last name): ");
                        while ((fullname = Console.ReadLine()) == "")
                            Console.Write("Try to enter full name: ");
                        cop.DeleteNum(fullname, 9999);
                        break;
                    case ConsoleKey.D7:
                        Console.Write("\nEnter contact name (full, first ,or last name): ");
                        string name;
                        while ((name = Console.ReadLine()) == "")
                            Console.Write("Try to enter full name: ");
                        cop.SearchByName(name);
                        break;
                    case ConsoleKey.D8:
                        cop.LoadContactFile();
                        break;
                }

            } while (conti);
        }
    }
}

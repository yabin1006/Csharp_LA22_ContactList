using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LA22_ContactList_Robin
{
    class ContactOperation
    {
        public List<Contact> contactList = new List<Contact>();

        public ContactOperation() { }

        public bool CheckNameinList(string fname)
        {
            fname = fname.Trim();
            while (fname.Contains("  "))
                fname = fname.Replace("  ", " ");
            foreach (var item in contactList)
                if (item.Fullname.ToLower() == fname.ToLower())
                {
                    Console.WriteLine($"{fname} is existed in the contact");
                    foreach (var num in item.phoneNumList)
                        Console.WriteLine(num);
                    return true;
                }
            Console.WriteLine($"{fname} is NOT in contact.");
            return false;
        }

        public bool AddNum(string fname, ulong phonenum, string type)
        {
            fname = fname.Trim();
            while (fname.Contains("  "))
                fname = fname.Replace("  ", " ");
            foreach (var item in contactList)
                if(item.Fullname.ToLower() == fname.ToLower())
                {
                    Regex rege = new Regex("\\D+");  //Regex regular expression
                    foreach(string num in item.phoneNumList)
                        //if(ulong.Parse((num.Replace("-","").Split(' '))[0]) == phonenum)
                        if(ulong.Parse(rege.Replace(num, "")) == phonenum)
                        {
                            Console.WriteLine($"{phonenum} is existed. Overrite (Y/N)?");
                            ConsoleKeyInfo cki;
                            while((cki = Console.ReadKey()).Key != ConsoleKey.Y && cki.Key != ConsoleKey.N)
                                Console.Write("Enter Y/N to overwrite?");
                            if (cki.Key == ConsoleKey.Y)
                            {
                                item.phoneNumList.Remove(num);
                                item.phoneNumList.Add(string.Format("{0:###-###-####}", phonenum) + " " + type.ToUpper()); //Overwrite exist number from exist contact
                                Console.WriteLine($"{string.Format("{0:###-###-####}", phonenum)} is successfully added to contact {fname}!");
                                return true;
                            }
                            else return false;
                        }
                    item.phoneNumList.Add(string.Format("{0:###-###-####}", phonenum) + " " + type.ToUpper()); //Add new number to exist contact
                    Console.WriteLine($"{string.Format("{0:###-###-####}", phonenum)} is successfully added to contact {fname}!");
                    return true;
                }
            string firname = fname.Split()[0].Trim().ToLower(); 
            firname = char.ToUpper(firname[0]) + firname.Substring(1);
            string lasname = fname.Split()[1].Trim().ToLower();
            lasname = char.ToUpper(lasname[0]) + lasname.Substring(1);
            /*contactList.Add(new Contact() {Firstname = firname, Lastname = lasname, 
                phoneNumList = new List<string>() { string.Format("{0:###-###-####}", phonenum) + " " + type.ToUpper() }
            });*/        
            contactList.Add(new Contact()  //Add new number to new contact. initialize List!!
            {
                Firstname = firname,
                Lastname = lasname,
                phoneNumList = new List<string>() {string.Format("{0:###-###-####}", phonenum) + " " + type.ToUpper()}  //initialize nested List phoneNumList in the first call !!
            });
            Console.WriteLine($"Contact {fname} and phone number {string.Format("{0:###-###-####}", phonenum)} are successfully added!");  
            return true;
        }

        public bool UpdateNumByName(string fname, ulong oldphonenum, ulong newphonenum, string type)
        {
            fname = fname.Trim();
            while (fname.Contains("  "))
                fname = fname.Replace("  ", " ");
            foreach (var item in contactList)
                if (item.Fullname.ToLower() == fname.ToLower())
                    foreach (string num in item.phoneNumList)
                        if (ulong.Parse((num.Replace("-", "").Split(' '))[0]) == oldphonenum)
                        {
                            item.phoneNumList.Remove(num);
                            item.phoneNumList.Add(string.Format("{0:###-###-####}", newphonenum) + " " + type.ToUpper());
                            Console.WriteLine($"{string.Format("{0:###-###-####}", newphonenum)} is successfully updated to contact {fname}!");
                            return true;
                        }
            Console.WriteLine("Phone number update failed, try again.");
            return false;
        }

        public bool UpdateName(string oldname, string newname)
        {
            oldname = oldname.Trim(); newname = newname.Trim();
            while (oldname.Contains("  "))
                oldname = oldname.Replace("  ", " ");
            while (newname.Contains("  "))
                newname = newname.Replace("  ", " ");
            foreach (var item in contactList)
                if (item.Fullname.ToLower() == oldname.ToLower())
                {
                    string oldfirstname = item.Firstname;
                    string oldlastname = item.Lastname;
                    string newfirstname = newname.Split(' ')[0].Trim().ToLower();
                    newfirstname = char.ToUpper(newfirstname[0]) + newfirstname.Substring(1); //Captital word
                    string newlastname = newname.Split(' ')[1].Trim().ToLower();
                    newlastname = char.ToUpper(newlastname[0]) + newlastname.Substring(1);
                    item.Firstname = newfirstname; item.Lastname = newlastname;
                    Console.WriteLine($"Old name {oldfirstname} {oldlastname} has been successfully updated by new name {newfirstname} {newlastname}!");
                    return true;
                }
            Console.WriteLine("Contact name update failed, try again.");
            return false;
        }

        public bool DeleteNum(string fname, ulong oldphonenum)
        {
            fname = fname.Trim(); 
            while (fname.Contains("  "))
                fname = fname.Replace("  ", " ");
            foreach (var item in contactList)
                if (item.Fullname.ToLower() == fname.ToLower())
                    if (oldphonenum == 9999)
                    {
                        contactList.Remove(item);
                        Console.WriteLine($"The contact {fname} is successfully deleted!");
                        return true;
                    }else
                    {
                        foreach(var num in item.phoneNumList)
                            if (ulong.Parse((num.Replace("-", "").Split(' '))[0]) == oldphonenum)
                            {
                                item.phoneNumList.Remove(num);
                                Console.WriteLine($"The phone number {num} from {fname} is sucessfully deleted!");
                                return true;
                            }  
                    }
            Console.WriteLine("Delete failed, try again.");
            return false;
        }

        public int SearchByName(string name)
        {
            name = name.Trim();
            while (name.Contains("  "))
                name = name.Replace("  ", " ");
            int count = 0;
            foreach (var item in contactList)
                if (item.Fullname.ToLower().Contains(name.ToLower()))
                {
                    Console.WriteLine($"{item.Fullname} is found in the contact, matching the search: ");
                    foreach (var num in item.phoneNumList)
                        Console.WriteLine(num);
                    count++;
                }
            if(count == 0)
                Console.WriteLine($"{name} is NOT found in the contact");
            return count;
        }

        public void ShowContact()
        {
            for(int i=0; i < contactList.Count; i++)
            {
                Console.WriteLine($"Contact {contactList[i].Fullname} and phone number:");
                for(int j=0;j<contactList[i].phoneNumList.Count;j++)
                    Console.WriteLine($"\t{j+1}. {contactList[i].phoneNumList[j]}");
            }
        }

        public void LoadContactFile()
        {
            string[] lines = File.ReadAllLines(@"../../../LA22_ContactList_Robin/contact_list.txt");
            string fname, phonetype; ulong phonenum;
            for(int i = 0; i < lines.Length; i++)
            {
                fname = lines[i].Split(',')[0].Trim();
                phonenum =ulong.Parse(lines[i].Split(',')[1]);
                phonetype = lines[i].Split(',')[2].Trim();
                phonetype = (char.ToUpper(phonetype[0])).ToString();
                AddNum(fname, phonenum, phonetype);
            }
            Console.WriteLine("Contact list: ");
            foreach(var item in contactList)
            {
                Console.WriteLine($"{item.Fullname}:");
                foreach(var num in item.phoneNumList)
                    Console.WriteLine($"\t{num}");
            }
                
        }
    }
}

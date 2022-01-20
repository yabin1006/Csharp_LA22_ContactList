using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LA22_ContactList_Robin
{
    class Contact
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname
        {
            get=> $"{Firstname} {Lastname}";
        }
        //public List<string> phoneNumList = new List<string>(); //this and next ways, both work, Why??? NO double declare??
        public List<string> phoneNumList { get; set; }
    }
}

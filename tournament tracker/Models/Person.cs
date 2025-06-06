using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tournament_tracker.Models
{
    public  class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string CellphoneNumber { get; set; }
        public int ID { get; set; }

        public string FullName => FirstName + " " + LastName;

        public Person(int id,string firstName, string lastName, string emailAdress, string cellphoneNumber)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAdress = emailAdress;
            CellphoneNumber = cellphoneNumber;
        }

        public Person( string firstName, string lastName, string emailAdress, string cellphoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAdress = emailAdress;
            CellphoneNumber = cellphoneNumber;
        }
        public string getFullNameString()
        {
            return FirstName + " " + LastName;
        }
    }
}

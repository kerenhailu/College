using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Models
{
    public class Student
    {
        public string firstName;
        public string lastName;
        public string dateBorn;
        public string email;
        public int schoolYear;

        public Student(string firstName, string lastName, string dateBorn, string email, int schoolYear)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateBorn = dateBorn;
            this.email = email;
            this.schoolYear = schoolYear;
        }
    }
}
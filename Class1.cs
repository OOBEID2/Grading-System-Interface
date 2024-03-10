using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Student
    {
        private string firstName;
        private string lastName;
        private int id;
        private string major;
        private string email;
        private int assessments;
        private int midterm;
        private int final;
        private int finalGrade;

        public Student()
        {
            firstName = " ";
            lastName = " ";
            id = 0;
            major = " ";
            email = " ";
            assessments = 0;
            midterm = 0;
            final = 0;
            finalGrade = 0;
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Major
        {
            get { return major; }
            set { major = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Assessments
        {
            get { return assessments; }
            set { assessments = value; }
        }

        public int Midterm
        {
            get { return midterm; }
            set { midterm = value; }
        }

        public int Final
        {
            get { return final; }
            set { final = value; }
        }

        public int Grade
        {
            get { return finalGrade; }
            set { finalGrade = value; }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Cw3.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string StudiesName { get; set; }
        
        public string StudiesMode { get; set; }
        
        public string Email { get; set; }

        public string FathersName { get; set; }
        
        public string MothersName { get; set; }

        public static Student MapCsvColumns(string csvLine)
        {
            var columns = csvLine.Split(",");
            return new Student()
            {
                FirstName = columns[0],
                LastName = columns[1],
                IndexNumber = columns[2],
                DateOfBirth = DateTime.Parse(columns[3]),
                StudiesName = columns[4],
                StudiesMode = columns[5],
                Email = columns[6],
                FathersName = columns[7],
                MothersName = columns[8]
            };
        }

        public override string ToString()
        {
            return $"{FirstName},{LastName},{IndexNumber},{DateOfBirth},{StudiesName},{StudiesMode},{Email},{FathersName},{MothersName}";
        }

        public static List<Student> GenerateStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    FirstName = "Jan", LastName = "Kowalski", IndexNumber = "s1234",
                    DateOfBirth = DateTime.Parse("12/1/1990"), StudiesName = "IT", StudiesMode = "Dzienne",
                    Email = "jk@jk.pl", FathersName = "Adam", MothersName = "Anna"
                },
                new Student
                {
                    FirstName = "Agnieszka", LastName = "Kowalska", IndexNumber = "s1235",
                    DateOfBirth = DateTime.Parse("12/1/1993"), StudiesName = "IT", StudiesMode = "Zaoczne",
                    Email = "ak@ak.pl", FathersName = "Jan", MothersName = "Agnieszka"
                },
                new Student
                {
                    FirstName = "Adam", LastName = "Nowak", IndexNumber = "s1236",
                    DateOfBirth = DateTime.Parse("12/1/1996"), StudiesName = "IT", StudiesMode = "Internetowe",
                    Email = "an@an.pl", FathersName = "Krzysztof", MothersName = "Marta"
                },
            };
        }
    }
}

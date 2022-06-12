using Cw3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cw3.Services
{
    public class FileDbService : IFileDbService
    {

        private static List<Student> _students;

        static FileDbService()
        {
            _students = Student.GenerateStudents();
            SaveAsCsv(_students);
        }

        public Student GetStudent(string indexNumber)
        {
            return _students.Find(e => e.IndexNumber == indexNumber);
        }

        public IEnumerable<Student> GetStudents()
        {
            return LoadFromCsv();
        }

        public bool AddStudent(Student student)
        {
            if (_students.Find(s => s.IndexNumber.Equals(student.IndexNumber)) is not null) { return false; }
            student.IndexNumber = new Random().Next(1, 20000).ToString();
            _students.Add(student);
            SaveAsCsv(_students);
            return true;
        }

        public bool RemoveStudent(string indexNumber)
        {
            var removed =_students.Remove(_students.Find(s => s.IndexNumber.Equals(indexNumber)));
            if (removed) { SaveAsCsv(_students); }
            return removed;
        }

        public bool UpdateStudent(string indexNumber, Student student)
        {
            var studentToUpdateIndex = _students.FindIndex(s => s.IndexNumber.Equals(indexNumber));
            if (studentToUpdateIndex is -1) { return false; }
            _students[studentToUpdateIndex] = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                StudiesName = student.StudiesName,
                StudiesMode = student.StudiesMode,
                Email = student.Email,
                FathersName = student.FathersName,
                MothersName = student.MothersName
            };
            SaveAsCsv(_students);
            return true;
        }
        
        private static void SaveAsCsv(List<Student> students)
        {
            Console.WriteLine("Adding to file");
            File.WriteAllLines("students.csv", students.Select(s => string.Join(",", s.ToString())));
        }

        private static List<Student> LoadFromCsv()
        {
            Console.WriteLine("Reading from file");
            return File.ReadAllLines("students.csv")
                .Select(s => Student.MapCsvColumns(s))
                .ToList();

        }
    }
}

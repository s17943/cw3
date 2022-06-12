using System.Collections.Generic;
using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IFileDbService _fileDbService;
        
        public StudentsController(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = new List<Student>(_fileDbService.GetStudents());
            return Ok(students);
        }
        
        [HttpGet]
        [Route("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            var student = _fileDbService.GetStudent(indexNumber);
            if (student is null) { return BadRequest(); }
            return Ok(student);
        }

        [HttpPut]
        [Route("{indexNumber}")]
        public IActionResult UpdateStudent(string indexNumber, Student student)
        {
            if (!_fileDbService.UpdateStudent(indexNumber, student)) { return BadRequest(); }
            return Ok(_fileDbService.GetStudent(student.IndexNumber));
        }
        
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (student is null || HasNulls(student)) return BadRequest();
            if (!_fileDbService.AddStudent(student)) { return BadRequest(); }
            return CreatedAtRoute("Student created", student);
        }

        [HttpDelete]
        [Route("{indexNumber}")]
        public IActionResult RemoveStudent(string indexNumber)
        {
            if (!_fileDbService.RemoveStudent(indexNumber)) { return BadRequest(); }
            return Ok($"Student {indexNumber} removed");
        }

        private bool HasNulls(Student student)
        {
            var fields = student.GetType().GetProperties();
            foreach (var field in fields)
            {
                if (field.GetValue(student) is null && field.Name is not "IndexNumber")
                {
                    return true;
                }
            }
            return false;
        }
    }
}

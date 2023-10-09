using System;
using System.Threading.Tasks;
using CRUD.Interface;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;


        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            try
            {
                var students = await _studentRepository.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("Invalid data");
                }

               await _studentRepository.AddStudentAsync(student);

                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student updatedStudent)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.NameOfTheFaculty = updatedStudent.NameOfTheFaculty;
            existingStudent.StudentNumber = updatedStudent.StudentNumber;

            _studentRepository.UpdateStudentAsync(existingStudent);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            try
            {
               await _studentRepository.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

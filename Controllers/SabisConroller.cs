using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SabisTest.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SabisTest.Entities;
using SabisTest.Models;

namespace SabisTest.Controllers
{
  public class SabisConroller:Controller
  {
    private readonly ISabisRepository _repository;
    public SabisConroller(ISabisRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    [Route("/api/sabis/students")]
    public async Task<IActionResult> GetStudents()
    {
      var students =await _repository.GetStudents();
      if (students != null) return Ok(students);
      return NotFound();
    }


    [HttpGet]
    [Route("/api/sabis/courses")]
    public async Task<IActionResult> GetCourses()
    {
      var courses = await _repository.GetCourses();
      if (courses != null) return Ok(courses);
      return NotFound();
    }

    [HttpGet]
    [Route("/api/sabis/course/id")]
    public async Task<IActionResult> GetCourse(int id)
    {
      var course = await _repository.GetCourseById(id);
      if (course != null) return Ok(course);
      return NotFound();
    }

    [HttpGet]
    [Route("/api/sabis/student/{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
      var student = await _repository.GetStudentById(id);
      if (student != null) return Ok(student);
      return NotFound();
    }

    [HttpPost]
    [Route("/api/sabis/student/")]
    public async Task<IActionResult> SaveStudent([FromBody]StudentViewModel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          Student student = model.ToStudent();

        await  _repository.SaveStudent(student);

        }
        else
        {
          return BadRequest(ModelState);
        }
      }
      catch (Exception ex)
      {
        ///Should add a logger
        //TODO: Add logger here, efffft
      }

      return BadRequest("Failed to save new order");
    }


    [HttpGet]
    [Route("/api/sabis/enrollement/id")]
    public async Task<IActionResult> GetEnrollment(int id)
    {
      var enrollment = await _repository.GetEnrollmentById(id);
      if (enrollment != null) return Ok(enrollment);
      return NotFound();
    }

    [HttpGet]
    [Route("/api/sabis/semester/id")]
    public async Task<IActionResult> GetSemester(int id)
    {
      var semester = await _repository.GetSemesterById(id);
      if (semester != null) return Ok(semester);
      return NotFound();
    }

    [HttpGet]
    [Route("/api/sabis/semesters")]
    public async Task<IActionResult> GetSemesters()
    {
      var semesters = await _repository.GetSemesters();
      if (semesters != null) return Ok(semesters);
      return NotFound();
    }
  }//end controller class

}

using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.ScholarshipDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScholarshipController : ControllerBase
    {
        private readonly IScholarshipService _scholarshipService;
        private readonly ILoggingService _loggingService;

        public ScholarshipController(IScholarshipService scholarshipService, ILoggingService loggingService)
        {
            _scholarshipService = scholarshipService ?? throw new ArgumentNullException(nameof(scholarshipService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ScholarshipDto>>> GetAllScholarships()
        {
            try
            {
                var scholarships = await _scholarshipService.GetAllScholarshipAsync();
                return Ok(scholarships);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllScholarships method: {ex.Message}");
                return BadRequest("Something went wrong while fetching scholarships.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ScholarshipDto>> GetScholarshipById(int id)
        {
            try
            {
                var scholarship = await _scholarshipService.GetScholarshipByIdAsync(id);

                if (scholarship == null)
                {
                    return NotFound("Scholarship not found.");
                }

                return Ok(scholarship);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetScholarshipById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the scholarship.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ScholarshipDto>> AddScholarship(AddScholarshipDto newScholarship)
        {
            try
            {
                var addedScholarship =  _scholarshipService.AddScholarshipAsync(newScholarship);
                _loggingService.LogInfo("New scholarship added successfully.");
                return CreatedAtAction(nameof(GetScholarshipById), new { id = addedScholarship.Id }, addedScholarship);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddScholarship method: {ex.Message}");
                return BadRequest("Something went wrong while adding the scholarship.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateScholarship(int id, ScholarshipDto scholarshipDto)
        {
            try
            {
                var existingScholarship = await _scholarshipService.GetScholarshipByIdAsync(id);

                if (existingScholarship == null)
                {
                    return NotFound("Scholarship not found.");
                }

                if (id != scholarshipDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _scholarshipService.UpdateScholarshipAsync(scholarshipDto);
                _loggingService.LogInfo($"Scholarship with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateScholarship method: {ex.Message}");
                return BadRequest("Something went wrong while updating the scholarship.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteScholarship(int id)
        {
            try
            {
                var existingScholarship = await _scholarshipService.GetScholarshipByIdAsync(id);

                if (existingScholarship == null)
                {
                    return NotFound("Scholarship not found.");
                }

                await _scholarshipService.DeleteScholarshipAsync(id);
                _loggingService.LogInfo($"Scholarship with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteScholarship method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the scholarship.");
            }
        }
    }
}

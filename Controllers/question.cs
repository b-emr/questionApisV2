using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testfinal.Models;

namespace testfinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class question : ControllerBase
    {
        private readonly AppDbContext _context;

        public question(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestSectionElement>>> GetTestSectionElements()
        {
            return await _context.TestSectionElements.ToListAsync();
        }

        // GET: api/question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestSectionElement>> GetTestSectionElement(long id)
        {
            var testSectionElement = await _context.TestSectionElements.FindAsync(id);

            if (testSectionElement == null)
            {
                return NotFound();
            }

            return testSectionElement;
        }

        // PUT: api/question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestSectionElement(long id, TestSectionElement testSectionElement)
        {
            if (id != testSectionElement.Id)
            {
                return BadRequest();
            }

            _context.Entry(testSectionElement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestSectionElementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestSectionElement>> PostTestSectionElement(TestSectionElement testSectionElement)
        {
            _context.TestSectionElements.Add(testSectionElement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestSectionElement", new { id = testSectionElement.Id }, testSectionElement);
        }

        // DELETE: api/question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestSectionElement(long id)
        {
            var testSectionElement = await _context.TestSectionElements.FindAsync(id);
            if (testSectionElement == null)
            {
                return NotFound();
            }

            _context.TestSectionElements.Remove(testSectionElement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestSectionElementExists(long id)
        {
            return _context.TestSectionElements.Any(e => e.Id == id);
        }
    }
}

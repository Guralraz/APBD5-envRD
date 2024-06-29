namespace APBD5_envRD;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly DataContext _context;

    public AnimalsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/animals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals([FromQuery] string orderBy = "name")
    {
        var animals = _context.Animals.AsQueryable();

        switch (orderBy)
        {
            case "name":
                animals = animals.OrderBy(a => a.Name);
                break;
            case "description":
                animals = animals.OrderBy(a => a.Description);
                break;
            case "category":
                animals = animals.OrderBy(a => a.Category);
                break;
            case "area":
                animals = animals.OrderBy(a => a.Area);
                break;
        }

        return await animals.ToListAsync();
    }
    
    // GET: api/animals/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    // POST: api/animals
    [HttpPost]
    public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
    }

    // PUT: api/animals/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest();
        }

        _context.Entry(animal).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Animals.Any(e => e.Id == id))
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

    // DELETE: api/animals/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

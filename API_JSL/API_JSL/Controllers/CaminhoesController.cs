using API_JSL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaminhoesController : ControllerBase
    {
        private readonly JSL_Context _context;

        public CaminhoesController(JSL_Context context) => _context = context;

        // GET: api/Caminhoes
        [HttpGet]
        public IEnumerable<Caminhao> GetCaminhao() => _context.Caminhao;

        // GET: api/Caminhoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaminhao([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var caminhao = await _context.Caminhao.FindAsync(id);

            if (caminhao == null)
                return NotFound();

            return Ok(caminhao);
        }

        // PUT: api/Caminhoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaminhao([FromRoute] int id, [FromBody] Caminhao caminhao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != caminhao.IdCaminhao)
                return BadRequest();

            _context.Entry(caminhao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaminhaoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Caminhoes
        [HttpPost]
        public async Task<IActionResult> PostCaminhao([FromBody] Caminhao caminhao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Caminhao.Add(caminhao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaminhao", new { id = caminhao.IdCaminhao }, caminhao);
        }

        // DELETE: api/Caminhoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaminhao([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var caminhao = await _context.Caminhao.FindAsync(id);
            if (caminhao == null)
                return NotFound();

            _context.Caminhao.Remove(caminhao);
            await _context.SaveChangesAsync();

            return Ok(caminhao);
        }

        private bool CaminhaoExists(int id) => _context.Caminhao.Any(e => e.IdCaminhao == id);
    }
}
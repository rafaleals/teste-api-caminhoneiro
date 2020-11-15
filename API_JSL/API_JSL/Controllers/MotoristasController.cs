using API_JSL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly JSL_Context _context;

        public MotoristasController(JSL_Context context) => _context = context;

        [HttpGet]
        public IEnumerable<Motorista> GetMotorista() => _context.Motorista;

        /// <summary>
        /// Lista os motoristas conforme parâmetro passado
        /// </summary>
        /// <param name="tipo">nome / sobrenome</param>
        /// <returns></returns>
        [Route("Listar/{tipo}")]
        [HttpGet("{tipo}")]
        public IEnumerable<Motorista> Listar(string tipo) => _context.Motorista.OrderBy(m => (tipo == "nome") ? m.Nome : m.Sobrenome);

        [Route("Detalhes/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorista([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var motorista = await _context.Motorista
                .Include(e => e.Endereco)
                .Include(c => c.Caminhao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motorista == null)
                return NotFound();

            return Ok(motorista);
        }

        [Route("Edit/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorista([FromRoute] int id, [FromBody] Motorista motorista)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != motorista.Id)
                return BadRequest();

            _context.Update(motorista);
            motorista.Endereco.Coordenadas = Util.Services.GetGeolocalization(motorista.Endereco);

            _context.Update(motorista.Endereco);
            _context.Update(motorista.Caminhao);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoristaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> PostMotorista([FromBody] Motorista motorista)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            motorista.Endereco.Coordenadas = Util.Services.GetGeolocalization(motorista.Endereco);

            _context.Add(motorista.Endereco);
            _context.Add(motorista.Caminhao);
            _context.SaveChanges();
            _context.Add(motorista);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotorista", new { id = motorista.Id }, motorista);
        }

        /// <summary>
        /// REsultado
        /// </summary>
        /// <returns></returns>
        /*
         [
            {
                "id": 2,
                "nome": "João",
                "sobrenome": "José",
                "idCaminhao": 2,
                "idEndereco": 2,
                "caminhao": null,
                "endereco": null
            },
            {
                "id": 3,
                "nome": "Diego",
                "sobrenome": "Severino",
                "idCaminhao": 3,
                "idEndereco": 3,
                "caminhao": null,
                "endereco": null
            },
            {
                "id": 4,
                "nome": "Sandra",
                "sobrenome": "Pereira",
                "idCaminhao": 4,
                "idEndereco": 4,
                "caminhao": null,
                "endereco": null
            }
        ]
    */

        [Route("Delete/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorista([FromRoute] int id)
        {
            try
            {
                var motorista = await _context.Motorista
                    .Include(c => c.Caminhao)
                    .Include(e => e.Endereco)
                    .FirstOrDefaultAsync(m => m.Id == id);
                _context.Motorista.Remove(motorista);
                _context.Caminhao.Remove(motorista.Caminhao);
                _context.Endereco.Remove(motorista.Endereco);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        private bool MotoristaExists(int id) => _context.Motorista.Any(e => e.Id == id);
    }
}
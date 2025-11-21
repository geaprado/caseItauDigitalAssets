using CaseItauDigitalAssetsBank.Application.DTOs;
using CaseItauDigitalAssetsBank.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseItauDigitalAssetsBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;
        public ClientesController(ClienteService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _service.GetByIdAsync(id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ClienteCreateDto dto)
        {
            var c = await _service.CreateAsync(dto.Nome, dto.Email);
            return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteCreateDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto.Nome, dto.Email);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost("{id:int}/depositar")]
        [Authorize]
        public async Task<IActionResult> Depositar(int id, [FromBody] OperacaoDto dto)
        {
            var ok = await _service.DepositarAsync(id, dto.Valor);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost("{id:int}/sacar")]
        [Authorize]
        public async Task<IActionResult> Sacar(int id, [FromBody] OperacaoDto dto)
        {
            var ok = await _service.SacarAsync(id, dto.Valor);
            return ok ? NoContent() : BadRequest("Saldo insuficiente ou cliente inexistente");
        }
    }
}

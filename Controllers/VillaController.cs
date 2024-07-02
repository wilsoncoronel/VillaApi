﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Datos;
using VillaAPI.Modelos;
using VillaAPI.Modelos.Dto;

namespace VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas() {
            _logger.LogInformation("Obtener las villas");
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id) {
            if (id == 0) {
                _logger.LogError("Error al traer la villa con el id "+ id);
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v=> v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa== null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Villas.FirstOrDefaultAsync(v=>v.Nombre.ToLower() == createDto.Nombre.ToLower())!=null) {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya esta registrada");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            Villa model = _mapper.Map<Villa>(createDto);
            
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id= model.Id}, model);
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id) {
            if (id == 0) { 
                return BadRequest();
                
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(v=>v.Id == id);
            if (villa == null) { 
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) {
            if (updateDto == null || id != updateDto.Id) {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(updateDto);
            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatepartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v=>v.Id == id);
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
            if(villa == null) return BadRequest();
            patchDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Villa modelo = _mapper.Map<Villa>(villaDto);
            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

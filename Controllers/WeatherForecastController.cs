using magic_villa.Models.Dto;
using magic_villa.Data;
using Microsoft.AspNetCore.Mvc;
using magic_villa.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace magic_villa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VillaAPIController : ControllerBase
{

    [HttpGet(Name = "GetVillas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("id:int", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }

    [HttpPost(Name = "create-villa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
    {
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa Already Exists!");
            return BadRequest(ModelState);
        }
        if (villaDto == null)
        {
            return BadRequest(villaDto);
        }
        if (villaDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDto);
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
    }



    [HttpDelete("id:int", Name = "Detete-villa")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        VillaStore.villaList.Remove(villa);
        return NoContent();
    }

    [HttpPatch("id:int", Name = "Update-partial-villa")]
    public IActionResult updatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        patchDTO.ApplyTo(villa, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}

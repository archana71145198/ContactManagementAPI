using ContactManage.Services.Interface;
using ContactManagment.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManage.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDto contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.AddContact(contact);
            return Ok(created);
        }
        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _service.GetAllContacts();
            return Ok(contacts);
        }
        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactDto contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != contact.Id)
                return BadRequest("Id mismatch");

            var updated = await _service.UpdateContact(contact);
            if (updated == null)
                return NotFound($"Contact with Id {id} not found");

            return Ok(updated);
        }
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _service.DeleteContact(id);
            if (!deleted)
                return NotFound($"Contact with Id {id} not found");

            return NoContent();
        }
       

    }
}

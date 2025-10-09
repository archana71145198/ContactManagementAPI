using ContactManage.Services.Interface;
using ContactManagment.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private string GetLoggedUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private string GetLoggedUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email).Value;
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDto contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = GetLoggedUserId();
            var userEmail = GetLoggedUserEmail();


            var created = await _service.AddContact(contact, userId, userEmail);
            return Ok(created);
        }
        [HttpGet("GetPagedContacts")]
        public async Task<IActionResult> GetPagedContacts(int page = 1, int pageSize = 10)
        {
            var result = await _service.GetPagedContactsAsync(page, pageSize);
            return Ok(result);
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
            var userId = GetLoggedUserId();
            var userEmail = GetLoggedUserEmail();

            var updated = await _service.UpdateContact(contact, userId, userEmail);
            if (updated == null)
                return NotFound($"Contact with Id {id} not found");

            return Ok(updated);
        }
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var userId = GetLoggedUserId();
            var userEmail = GetLoggedUserEmail();
            var deleted = await _service.DeleteContact(id, userId, userEmail);
            if (!deleted)
                return NotFound($"Contact with Id {id} not found");

            return Ok($" Contact with ID {id} deleted successfully.");
        }


        [HttpGet("GetLogInfo")]
        public async Task<IActionResult> GetAllLogs()
        {
            var logs = await _service.GetAllLogsAsync();
            return Ok(logs);
        }
    }
}

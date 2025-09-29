using AutoMapper;
using ContactManage.Repository;
using ContactManage.Repository.Models;
using ContactManagment.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManage.Services
{
    public class ContactService:IContactService
    {
        private readonly ContactRepository _repository;
        private readonly IMapper _mapper;


        public ContactService(ContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<ContactDto> AddContact(ContactDto contact)
        {
            var request = _mapper.Map<Contact>(contact);

            var reesult= await _repository.AddContact(request);
            /* var response = new ContactDto {
                 Id = contact.Id,
             Name = contact.Name,
             Email = contact.Email,
             Phone = contact.Phone,
             };
             return response;*/
            return _mapper.Map<ContactDto>(reesult);

        }
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            var contacts = await _repository.GetAllContacts();

            /*  return contacts.Select(c => new ContactDto
              {
                  Id = c.Id,
                  Name = c.Name,
                  Email = c.Email,
                  Phone = c.Phone
              });*/
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);

        }
        public async Task<ContactDto?> UpdateContact(ContactDto contact)
        {

            /*  var entity = new Contact
              {
                  Id = contact.Id,
                  Name = contact.Name,
                  Email = contact.Email,
                  Phone = contact.Phone
              };
  */
            var entity = _mapper.Map<Contact>(contact);

            var updated = await _repository.UpdateContact(entity);
            /*  if (updated == null) return null;

              return new ContactDto
              {
                  Id = updated.Id,
                  Name = updated.Name,
                  Email = updated.Email,
                  Phone = updated.Phone
              };*/
            return updated == null ? null : _mapper.Map<ContactDto>(updated);

        }
        public async Task<bool> DeleteContact(int id)
        {
            return await _repository.DeleteContact(id);
        }

    }
}

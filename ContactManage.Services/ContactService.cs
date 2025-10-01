using AutoMapper;
using ContactManage.Repository;
using ContactManage.Repository.Models;
using ContactManage.Services.Interface;
using ContactManagment.Dto;

namespace ContactManage.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;


        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<CreateContactDto> AddContact(CreateContactDto contact)
        {
            var request = _mapper.Map<Contact>(contact);

            var reesult = await _repository.AddContact(request);
            return _mapper.Map<CreateContactDto>(reesult);

        }
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            var contacts = await _repository.GetAllContacts();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);

        }
        public async Task<ContactDto?> UpdateContact(ContactDto contact)
        {
            var entity = _mapper.Map<Contact>(contact);

            var updated = await _repository.UpdateContact(entity);
           
            return updated == null ? null : _mapper.Map<ContactDto>(updated);

        }
        public async Task<bool> DeleteContact(int id)
        {
            return await _repository.DeleteContact(id);
        }

    }
}

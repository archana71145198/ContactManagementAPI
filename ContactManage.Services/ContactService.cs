using AutoMapper;
using ContactManage.Repository;
using ContactManage.Repository.Enums;
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

        public async Task<CreateContactDto> AddContact(CreateContactDto contact, string userId, string userName)
        {
            var request = _mapper.Map<Contact>(contact);

            var reesult = await _repository.AddContact(request);
            await _repository.LogAction(reesult.Id, ActionType.Create, userId, userName);

            return _mapper.Map<CreateContactDto>(reesult);

        }
        public async Task<IEnumerable<ContactDto>> GetAllContacts()
        {
            var contacts = await _repository.GetAllContacts();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);

        }
        public async Task<PagedResultDto<ContactDto>> GetPagedContactsAsync(int page, int pageSize)
        {
            var pagedContacts = await _repository.GetPagedContactsAsync(page, pageSize);

            return new PagedResultDto<ContactDto>
            {
                Items = _mapper.Map<IEnumerable<ContactDto>>(pagedContacts.Items),
                TotalCount = pagedContacts.TotalCount,
                Page = pagedContacts.Page,
                PageSize = pagedContacts.PageSize
            };
        }
        public async Task<ContactDto?> UpdateContact(ContactDto contact, string userId, string userName)
        {
            var entity = _mapper.Map<Contact>(contact);

            var updated = await _repository.UpdateContact(entity);
            if (updated != null)
                await _repository.LogAction(updated.Id, ActionType.Update, userId, userName);

            return updated == null ? null : _mapper.Map<ContactDto>(updated);

        }
        public async Task<bool> DeleteContact(int id,string userId, string userName)
        {
            var deleted = await _repository.DeleteContact(id);
            if (deleted)
                await _repository.LogAction(id, ActionType.Delete, userId, userName);

            return deleted;
        }
        public async Task<IEnumerable<LogInfo>> GetAllLogsAsync()
        {
            return await _repository.GetAllLogsAsync();
        }

    }
}

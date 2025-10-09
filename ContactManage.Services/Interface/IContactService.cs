using ContactManage.Repository.Models;
using ContactManagment.Dto;

namespace ContactManage.Services.Interface
{
    public interface IContactService
    {
        Task<CreateContactDto> AddContact(CreateContactDto contact, string userId, string userName);
        Task<IEnumerable<ContactDto>> GetAllContacts();
        Task<PagedResultDto<ContactDto>> GetPagedContactsAsync(int page, int pageSize);

        Task<ContactDto?> UpdateContact(ContactDto contact, string userId, string userName);

        Task<bool> DeleteContact(int id, string userId, string userName);
        Task<IEnumerable<LogInfo>> GetAllLogsAsync();

    }
}

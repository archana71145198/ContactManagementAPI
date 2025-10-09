using ContactManage.Repository.Enums;
using ContactManage.Repository.Models;
using ContactManagment.Dto;

namespace ContactManage.Repository
{
    public interface IContactRepository
    {
        Task<Contact> AddContact(Contact contact);
        Task<bool> DeleteContact(int id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> UpdateContact(Contact contact);
        Task<PagedResultDto<Contact>> GetPagedContactsAsync(int page, int pageSize);
        Task LogAction(int recordId, ActionType action, string userId, string userName);
        Task<IEnumerable<LogInfo>> GetAllLogsAsync();


    }
}
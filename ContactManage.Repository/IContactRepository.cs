using ContactManage.Repository.Models;

namespace ContactManage.Repository
{
    public interface IContactRepository
    {
        Task<Contact> AddContact(Contact contact);
        Task<bool> DeleteContact(int id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> UpdateContact(Contact contact);
    }
}
using ContactManagment.Dto;

namespace ContactManage.Services.Interface
{
    public interface IContactService
    {
        Task<CreateContactDto> AddContact(CreateContactDto contact);
        Task<IEnumerable<ContactDto>> GetAllContacts();
        Task<ContactDto?> UpdateContact(ContactDto contact);

        Task<bool> DeleteContact(int id);

    }
}

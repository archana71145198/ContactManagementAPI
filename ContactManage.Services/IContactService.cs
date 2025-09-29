using ContactManage.Repository.Models;
using ContactManagment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManage.Services
{
    public interface IContactService
    {
        Task<ContactDto> AddContact(ContactDto contact);
        Task<IEnumerable<ContactDto>> GetAllContacts();
        Task<ContactDto?> UpdateContact(ContactDto contact);

        Task<bool> DeleteContact(int id);

    }
}

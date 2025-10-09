using ContactManage.Repository.Enums;
using ContactManage.Repository.Models;
using ContactManagment.Dto;
using Microsoft.EntityFrameworkCore;

namespace ContactManage.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }
        public async Task<PagedResultDto<Contact>> GetPagedContactsAsync(int page, int pageSize)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Contacts
                .Where(c => !c.IsDeleted)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<Contact>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contacts.Where(c => !c.IsDeleted)
.ToListAsync();
        }
        public async Task<Contact?> UpdateContact(Contact contact)
        {
            var existing = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contact.Id);
            if (existing == null) return null;

            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;

            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> DeleteContact(int id)
        {
            var existing = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task LogAction(int recordId, ActionType action, string userId, string userName )
        {
            var log = new LogInfo
            {
                RecordId = recordId,
                Action = action,
                UserId = userId,
                UserName = userName, 
                AppName = "Contact API",
                Timestamp = DateTime.Now
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<LogInfo>> GetAllLogsAsync()
        {
            return await _context.Logs
                .OrderByDescending(l => l.Timestamp) 
                .ToListAsync();
        }


    }
}

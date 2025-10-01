using ContactManage.Repository.Models;
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
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
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

            _context.Contacts.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

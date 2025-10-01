using AutoMapper;
using ContactManage.Repository.Models;
using ContactManagment.Dto;

namespace ContactManage.Services
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
            CreateMap<CreateContactDto, Contact>();
            CreateMap<Contact, CreateContactDto>();

        }
    }
}

using AutoMapper;
using ContactManage.Repository.Models;
using ContactManagment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManage.Services
{
    public class ContactProfile:Profile
    {
        public ContactProfile()
        {
            // Entity → DTO
            CreateMap<Contact, ContactDto>();

            // DTO → Entity
            CreateMap<ContactDto, Contact>();
        }
    }
}

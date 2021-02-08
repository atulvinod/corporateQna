using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<CorporateQnA.Models.UserDetails, Models.UserDetails>();

            CreateMap<Models.UserDetails, CorporateQnA.Models.UserDetails>();
        }
    }
}

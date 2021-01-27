using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            //data to core
            CreateMap<Services.Models.Category, CorporateQnA.Models.Category>();

            //core to data
            CreateMap<CorporateQnA.Models.Category, Models.Category>();
              
        }
    }
}

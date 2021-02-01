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
            CreateMap<Models.Category, CorporateQnA.Models.Category>();

            CreateMap<CorporateQnA.Models.Category, Models.Category>();

            CreateMap<CorporateQnA.Models.CategoryDetails, Models.CategoryDetails>();

            CreateMap<Models.CategoryDetails, CorporateQnA.Models.CategoryDetails>();

        }
    }
}

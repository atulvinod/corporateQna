using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps
{
    public class AnswerMap : Profile
    {
        public AnswerMap()
        {
            //data to core
            CreateMap<Models.Answer, CorporateQnA.Models.Answer>();

            //core to data
            CreateMap<CorporateQnA.Models.Answer, Models.Answer>();

            //data to core
            CreateMap<Models.AnswerDetails, CorporateQnA.Models.AnswerDetails>()
               .ForMember(x=>x.IsBestSolution, o=>o.MapFrom(z=> z.IsBestSolution > 0))
               .ForMember(x => x.LikedByUser, o => o.MapFrom(z => z.LikedByUser > 0))
               .ForMember(x => x.DislikedByUser, o => o.MapFrom(z => z.DislikedByUser > 0));

        }
    }
}

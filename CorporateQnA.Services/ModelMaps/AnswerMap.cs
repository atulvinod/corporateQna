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
            CreateMap<CorporateQnA.Services.Models.Answer, CorporateQnA.Models.Answer>();

            //core to data
            CreateMap<CorporateQnA.Models.Answer, CorporateQnA.Services.Models.Answer>()
            .ForMember(m => m.CategoryId, opt => opt.MapFrom(x => int.Parse(x.CategoryId)))
            .ForMember(m => m.LikeCount, opt => opt.MapFrom(x => int.Parse(x.LikeCount)))
            .ForMember(m => m.DislikeCount, opt => opt.MapFrom(x => int.Parse(x.DislikeCount)));
        }
    }
}

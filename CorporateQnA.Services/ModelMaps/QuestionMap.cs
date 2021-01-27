using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps
{
    public class QuestionMap : Profile
    {
        public QuestionMap()
        {
            //data to core
            CreateMap<CorporateQnA.Services.Models.Question, CorporateQnA.Models.Question>();

            //TODO: is resolved
            //core to data
            CreateMap<CorporateQnA.Models.Question, CorporateQnA.Services.Models.Question>()
                .ForMember(m => m.CategoryId, opt => opt.MapFrom(x => int.Parse(x.CategoryId)))
                .ForMember(m => m.ViewCount, opt => opt.MapFrom(x => int.Parse(x.ViewCount)))
                .ForMember(m => m.UpvoteCount, opt => opt.MapFrom(x => int.Parse(x.UpvoteCount)));
        }
    }
}

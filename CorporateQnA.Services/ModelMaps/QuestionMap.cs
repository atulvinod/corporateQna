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
            CreateMap<Models.Question, CorporateQnA.Models.Question>();

            CreateMap<CorporateQnA.Models.Question, Models.Question>();

            CreateMap<Models.QuestionDetails, CorporateQnA.Models.QuestionDetails>()
                .ForMember(x=>x.Resolved ,o=>o.MapFrom(x=>x.Resolved > 0));

        }
    }
}

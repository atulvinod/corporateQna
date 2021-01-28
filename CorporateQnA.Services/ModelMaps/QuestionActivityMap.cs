using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateQnA.Models.Enums;

namespace CorporateQnA.Services.ModelMaps
{
    public class QuestionActivityMap : Profile
    {
        public QuestionActivityMap()
        {

            CreateMap<Models.QuestionActivity, CorporateQnA.Models.QuestionActivity>()
                    .ForMember(x => x.ActivityType, opt => opt.MapFrom(z => (short)z.ActivityType));

            CreateMap<CorporateQnA.Models.QuestionActivity, Models.QuestionActivity>()
                .ForMember(x => x.ActivityType, opt => opt.MapFrom(z => (QuestionActivityType)(z.ActivityType)));

        }

    }
}

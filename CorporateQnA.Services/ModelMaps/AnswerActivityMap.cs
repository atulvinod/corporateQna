using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateQnA.Models.Enums;

namespace CorporateQnA.Services.ModelMaps
{
    public class AnswerActivityMap : Profile
    {
        public AnswerActivityMap()
        {
            //data to core
            CreateMap<Models.AnswerActivity, CorporateQnA.Models.AnswerActivity>()
                .ForMember(x => x.ActivityType, opt => opt.MapFrom(z => (short)z.ActivityType));

            CreateMap<CorporateQnA.Models.AnswerActivity, Models.AnswerActivity>()
                .ForMember(x => x.ActivityType, opt => opt.MapFrom(z => (AnswerActivityType)(z.ActivityType)));
        }
    }
}

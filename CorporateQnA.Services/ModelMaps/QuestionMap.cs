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

            //core to data
            CreateMap<CorporateQnA.Models.Question, CorporateQnA.Services.Models.Question>();

            //data to core
            CreateMap<CorporateQnA.Services.Models.QuestionDetails, CorporateQnA.Models.QuestionDetails>();

            //core to data
            CreateMap<CorporateQnA.Models.QuestionDetails, CorporateQnA.Services.Models.QuestionDetails>();

        }
    }
}

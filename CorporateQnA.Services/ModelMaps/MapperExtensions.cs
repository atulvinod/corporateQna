using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps
{
    public static class MapperExtensions
    {
        public static IMapper GetMapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(typeof(AnswerMap));
                config.AddProfile(typeof(AnswerActivityMap));
                config.AddProfile(typeof(CategoryMap));
                config.AddProfile(typeof(QuestionActivityMap));
                config.AddProfile(typeof(UserMap));
                config.AddProfile(typeof(QuestionMap));

            }).CreateMapper();
        }

        public static T Map<T>(this object src)
        {
            IMapper mapper = GetMapperConfiguration();
            return mapper.Map<T>(src);
        }

        public static IEnumerable<T> MapCollection<T>(this IEnumerable<object> src)
        {
            IMapper mapper = GetMapperConfiguration();
            return mapper.Map<IEnumerable<T>>(src);
        }
    }
}

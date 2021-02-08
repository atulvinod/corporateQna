using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.ModelMaps.Extensions
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

        public static T MapTo<T>(this object src)
        {
            //To prevent null exception when using with FirstOrDefault()
            return src != null ? GetMapperConfiguration().Map<T>(src) : default;
        }

        public static IEnumerable<T> MapCollectionTo<T>(this IEnumerable<object> src)
        {
            return GetMapperConfiguration().Map<IEnumerable<T>>(src);
        }
    }
}

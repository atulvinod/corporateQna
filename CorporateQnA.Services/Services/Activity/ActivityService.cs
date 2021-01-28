using AutoMapper;
using CorporateQnA.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public class ActivityService : IActivityService
    {
        private readonly PetaPoco.Database database;
        private readonly IMapper mapper;

        public ActivityService(IConfiguration configuration, IMapper mapper)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
            this.mapper = mapper;
        }

        public int CreateAnswerActivity(AnswerActivity answerActivity)
        {
            try
            {
                var data = this.mapper.Map<CorporateQnA.Services.Models.AnswerActivity>(answerActivity);
                data.CreatedAt = DateTime.Now;
                var id = this.database.Insert(data);
                return (int)id;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateQuestionActivity(QuestionActivity questionActivity)
        {
            try
            {
                var data = this.mapper.Map<CorporateQnA.Services.Models.QuestionActivity>(questionActivity);
                data.CreatedAt = DateTime.Now;
                var id = this.database.Insert(data);
                return (int)id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

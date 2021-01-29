using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Models.Enums;
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


                var d = this.mapper.Map<CorporateQnA.Services.Models.AnswerActivity>(answerActivity);
                var check = this.database.Query<CorporateQnA.Services.Models.AnswerActivity>("SELECT * FROM AnswerActivity WHERE UserId = @0 AND AnswerId = @1", d.UserId, d.AnswerId).FirstOrDefault<CorporateQnA.Services.Models.AnswerActivity>();

              

                if(check == null)
                {
                    d.ActivityType = (short)answerActivity.ActivityType;
                    d.CreatedAt = DateTime.Now;
                    this.database.Insert(d);
                    return 1;
                }

                //both activity are the same
                if (check.ActivityType == (short)answerActivity.ActivityType)
                {
                    return 0;
                }

                //user has liked before, next state is dislike
                if (check.ActivityType == (short)AnswerActivityType.Like && answerActivity.ActivityType == AnswerActivityType.Dislike)
                {
                    check.ActivityType = (short)AnswerActivityType.Dislike;
                    check.CreatedAt = DateTime.Now;
                    this.database.Update(check);
                    return 2;
                }

                //user has disliked before, next state is like
                if (check.ActivityType == (short)AnswerActivityType.Dislike && answerActivity.ActivityType == AnswerActivityType.Like)
                {
                    check.ActivityType = (short)AnswerActivityType.Like;
                    check.CreatedAt = DateTime.Now;
                    this.database.Update(check);
                    return 3;
                }



            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }

        public int CreateQuestionActivity(QuestionActivity questionActivity)
        {
            try
            {
                var d = this.mapper.Map<CorporateQnA.Services.Models.QuestionActivity>(questionActivity);
                var check = this.database.ExecuteScalar<long>("SELECT COUNT(*) FROM QuestionActivity WHERE UserId = @0 AND QuestionId = @1 AND ActivityType = @2", d.UserId, d.QuestionId, d.ActivityType);
                if (check == 0)
                {
                    d.CreatedAt = DateTime.Now;
                    var id = this.database.Insert(d);
                    return (int)id;
                }
                return 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

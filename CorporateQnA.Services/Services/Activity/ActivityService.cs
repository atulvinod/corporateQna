using CorporateQnA.Models;
using CorporateQnA.Models.Enums;
using CorporateQnA.Services.ModelMaps.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace CorporateQnA.Services
{
    public class ActivityService : IActivityService
    {
        private readonly PetaPoco.Database database;

        public ActivityService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int CreateAnswerActivity(AnswerActivity answerActivity)
        {
            try
            {

                var d = answerActivity.MapTo<Models.AnswerActivity>();
                var check = this.database.FirstOrDefault<Models.AnswerActivity>("WHERE UserId = @0 AND AnswerId = @1", d.UserId, d.AnswerId);

                if (check == null)
                {
                    d.ActivityType = (short)answerActivity.ActivityType;
                    d.CreatedAt = DateTime.Now;
                    this.database.Insert(d);
                    return 1;
                }

                //both activity are the same, then remove the activity to set the state to neutral
                if (check.ActivityType == (short)answerActivity.ActivityType)
                {
                    this.database.Delete(check);
                    return 0;
                }

                //user has liked before, next state is dislike
                if (check.ActivityType == (short)ActivityTypes.Like && answerActivity.ActivityType == ActivityTypes.Dislike)
                {
                    check.ActivityType = (short)ActivityTypes.Dislike;
                    check.CreatedAt = DateTime.Now;
                    this.database.Update(check);
                    return 2;
                }

                //user has disliked before, next state is like
                if (check.ActivityType == (short)ActivityTypes.Dislike && answerActivity.ActivityType == ActivityTypes.Like)
                {
                    check.ActivityType = (short)ActivityTypes.Like;
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
                var dataModel = questionActivity.MapTo<Models.QuestionActivity>();
                var activity = this.database.FirstOrDefault<Models.QuestionActivity>("WHERE UserId = @0 AND QuestionId = @1 AND ActivityType = @2", dataModel.UserId, dataModel.QuestionId, dataModel.ActivityType);

                if (questionActivity.ActivityType == ActivityTypes.Resolved)
                {
                    if (activity == null)
                    {
                        dataModel.CreatedAt = DateTime.Now;
                        this.database.Insert(dataModel);
                        this.database.Update<Answer>("SET IsBestSolution = 1 WHERE Id = @0", questionActivity.AnswerId);
                    }
                    else
                    {
                        this.database.Delete(activity);
                        this.database.Update<Answer>("SET IsBestSolution = 0 WHERE Id = @0", questionActivity.AnswerId);
                    }

                    return 1;
                }
                else
                {
                    if (activity == null)
                    {
                        dataModel.CreatedAt = DateTime.Now;
                        this.database.Insert(dataModel);
                        return 1;

                    }
                    return 0;
                }

            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

using CorporateQnA.Models;
using CorporateQnA.Models.Enums;
using CorporateQnA.Services.ModelMaps.Extensions;
using CorporateQnA.Services.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace CorporateQnA.Services
{
    public class ActivityService : BaseService, IActivityService
    {

        /// <summary>
        /// Initializes an instance of Activity service
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public ActivityService(IConfiguration configuration) : base(configuration){}

        /// <summary>
        /// Creates answer activity
        /// </summary>
        /// <param name="answerActivity">The answer activity</param>
        /// <returns>The identifier</returns>
        public int CreateAnswerActivity(AnswerActivity answerActivity)
        {
            try
            {

                var answerActivityModel = answerActivity.MapTo<Models.AnswerActivity>();
                var existingAnswerActivity = this.database.FirstOrDefault<Models.AnswerActivity>("WHERE UserId = @0 AND AnswerId = @1", answerActivityModel.UserId, answerActivityModel.AnswerId);

                if (existingAnswerActivity == null)
                {
                    answerActivityModel.ActivityType = (short)answerActivity.ActivityType;
                    answerActivityModel.CreatedAt = DateTime.Now;
                    this.database.Insert(answerActivityModel);
                    return 1;
                }

                //both activity are the same, then remove the activity to set the state to neutral
                if (existingAnswerActivity.ActivityType == (short)answerActivity.ActivityType)
                {
                    this.database.Delete(existingAnswerActivity);
                    return 0;
                }

                //user has liked before, next state is dislike
                if (existingAnswerActivity.ActivityType == (short)ActivityTypes.Like && answerActivity.ActivityType == ActivityTypes.Dislike)
                {
                    existingAnswerActivity.ActivityType = (short)ActivityTypes.Dislike;
                    existingAnswerActivity.CreatedAt = DateTime.Now;
                    this.database.Update(existingAnswerActivity);
                    return 2;
                }

                //user has disliked before, next state is like
                if (existingAnswerActivity.ActivityType == (short)ActivityTypes.Dislike && answerActivity.ActivityType == ActivityTypes.Like)
                {
                    existingAnswerActivity.ActivityType = (short)ActivityTypes.Like;
                    existingAnswerActivity.CreatedAt = DateTime.Now;
                    this.database.Update(existingAnswerActivity);
                    return 3;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }

        /// <summary>
        /// Creates question activity
        /// </summary>
        /// <param name="questionActivity">The question activity</param>
        /// <returns>The identifier</returns>
        public int CreateQuestionActivity(QuestionActivity questionActivity)
        {
            try
            {
                var dataModel = questionActivity.MapTo<Models.QuestionActivity>();
                var existingActivity = this.database.FirstOrDefault<Models.QuestionActivity>("WHERE UserId = @0 AND QuestionId = @1 AND ActivityType = @2", dataModel.UserId, dataModel.QuestionId, dataModel.ActivityType);

                if (questionActivity.ActivityType == ActivityTypes.Resolved)
                {
                    if (existingActivity == null)
                    {
                        dataModel.CreatedAt = DateTime.Now;
                        this.database.Insert(dataModel);
                        this.database.Update<Answer>("SET IsBestSolution = 1 WHERE Id = @0", questionActivity.AnswerId);
                    }
                    else
                    {
                        this.database.Delete(existingActivity);
                        this.database.Update<Answer>("SET IsBestSolution = 0 WHERE Id = @0", questionActivity.AnswerId);
                    }

                    return 1;
                }
                else
                {
                    if (existingActivity == null)
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

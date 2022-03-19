using CorporateQnA.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using CorporateQnA.Services.ModelMaps.Extensions;
using CorporateQnA.Models.ViewModels;
using CorporateQnA.Services.Services;

namespace CorporateQnA.Services
{
    public class AnswerService : BaseService, IAnswerService
    {
        /// <summary>
        /// Initializes an instance of AnswerService
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public AnswerService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates an answer to a specific question
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public int CreateAnswer(Answer answer)
        {
            try
            {
                var data = answer.MapTo<Models.Answer>();
                data.AnsweredOn = DateTime.Now;
                return (int)this.database.Insert(data); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the answers for a question
        /// </summary>
        /// <param name="getAnswer">The get answer view model</param>
        /// <returns>The answer details</returns>
        public IEnumerable<AnswerDetails> GetAnswersForQuestion(GetAnswerForQuestion getAnswer)
        {
            return this.database.FetchProc<Models.AnswerDetails>("GetAnswers", new { questionId = getAnswer.QuestionId, userId = getAnswer.UserId }).MapCollectionTo<AnswerDetails>();
        }

        /// <summary>
        /// Sets the answer of a question as solution
        /// </summary>
        /// <param name="state">The solution state</param>
        /// <returns>identifier</returns>
        public int SetAnswerAsSolution(AnswerAsSolution state)
        {
            return (int)this.database.Update<Answer>("SET IsBestSolution =@0 WHERE Id = @1", state.IsBestSolution, state.AnswerId);
        }
    }
}

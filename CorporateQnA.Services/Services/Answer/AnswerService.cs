using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Models.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Services.ModelMaps.Extensions;

namespace CorporateQnA.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly PetaPoco.Database database;

        public AnswerService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Answer answer)
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

        public IEnumerable<AnswerDetails> GetAnswersForQuestion(GetAnswer getAnswer)
        {
            return this.database.FetchProc<Models.AnswerDetails>("GetAnswers", new { questionId = getAnswer.QuestionId, userId = getAnswer.UserId }).MapCollectionTo<AnswerDetails>();
        }

        public int SetAnswerAsSolution(AnswerAsSolution state)
        {
            return (int)this.database.Update<Answer>("SET IsBestSolution =@0 WHERE Id = @1", state.IsBestSolution, state.AnswerId);
        }
    }
}

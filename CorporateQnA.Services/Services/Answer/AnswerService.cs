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
    public class AnswerService : IAnswerService
    {
        private readonly PetaPoco.Database database;
        private readonly IMapper mapper;

        public AnswerService(IConfiguration configuration, AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Answer answer)
        {
            try
            {
                var data = this.mapper.Map<CorporateQnA.Services.Models.Answer>(answer);
                data.AnsweredOn = DateTime.Now;
                var id = this.database.Insert(data);
                return (int)id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AnswerDetails> GetAnswersForQues(GetAnswer getAnswer)
        {
            var answers = this.database.FetchProc<AnswerDetails>("[master].dbo.GetAnswers", new { questionId = getAnswer.QuestionId, userId = getAnswer.UserId });
            return answers;
        }

        public int SetAnswerState(AnswerState state)
        {
            var i = this.database.Execute("UPDATE Answer SET IsBestSolution =@0 WHERE Id = @1", state.IsBestSolution, state.AnswerId);
            return (int)i;
        }
    }
}

using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using static CorporateQnA.Models.Enums.SearchFilterTypes;
using CorporateQnA.Models.Enums;
using CorporateQnA.Services.ModelMaps.Extensions;

namespace CorporateQnA.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly PetaPoco.Database database;
        public QuestionService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Question question)
        {
            try
            {
                var data = question.MapTo<Models.Question>();
                data.AskedOn = DateTime.Now;
                return (int)this.database.Insert(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<QuestionDetails> GetQuestions()
        {
            return this.database.Fetch<Models.QuestionDetails>("ORDER BY LikeCount DESC").MapCollectionTo<QuestionDetails>();
        }

        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter search)
        {
            //check if searchInput is not null
            search.searchInput = search.searchInput ?? "";

            //query database for show values
            return this.database.FetchProc<Models.QuestionDetails>("SearchQuestion", new { userId = search.userId, keyword = search.searchInput, categoryId = search.categoryId, sortBy = (short)search.SortBy, show = (short)search.Show }).MapCollectionTo<QuestionDetails>();
        }

        public IEnumerable<QuestionDetails> QuestionsByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE AskedBy = @0", userId).MapCollectionTo<QuestionDetails>();
        }

        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE EXISTS(SELECT * FROM Answer a WHERE a.AnsweredBy = @0  AND a.QuestionId = QuestionDetails.QuestionId);", userId).MapCollectionTo<QuestionDetails>();
        }
    }
}

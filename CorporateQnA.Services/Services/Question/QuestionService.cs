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

namespace CorporateQnA.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AutoMapper.IMapper mapper;
        private readonly PetaPoco.Database database;
        public QuestionService(IConfiguration configuration, AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Question question)
        {
            try
            {
                var data = this.mapper.Map<Models.Question>(question);
                data.AskedOn = DateTime.Now;
                return (int)this.database.Insert(data); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<QuestionDetails> GetQuestions()
        {
            try
            {
                return this.database.Fetch<Models.QuestionDetails>(" ORDER BY LikeCount DESC").Select(s => this.mapper.Map<CorporateQnA.Models.QuestionDetails>(s));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter search)
        {
            List<Models.QuestionDetails> fetch;
            //check if searchInput is not null
            search.searchInput = search.searchInput ?? "";

            //query database for show values
            fetch = this.database.FetchProc<CorporateQnA.Services.Models.QuestionDetails>("SearchQuestion", new { userId = search.userId, keyword = search.searchInput, categoryId = search.categoryId, sortBy = (short)search.SortBy, show = (short)search.Show });
            return fetch.Select(x => this.mapper.Map<QuestionDetails>(x));
        }

        public IEnumerable<QuestionDetails> QuestionsByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE AskedBy = @0", userId).Select(s => this.mapper.Map<QuestionDetails>(s));
        }

        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE EXISTS(SELECT * FROM Answer a WHERE a.AnsweredBy = @0  AND a.QuestionId = QuestionId);", userId).Select(s => this.mapper.Map<QuestionDetails>(s));
        }
    }
}

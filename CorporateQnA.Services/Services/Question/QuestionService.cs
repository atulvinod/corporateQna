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
using CorporateQnA.Services.Services;

namespace CorporateQnA.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        /// <summary>
        /// Initializes an instance of QuestionService
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public QuestionService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new question
        /// </summary>
        /// <param name="question">The question model</param>
        /// <returns>The created question identifier</returns>
        public int CreateQuestion(Question question)
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

        /// <summary>
        /// Gets all the questions available
        /// </summary>
        /// <returns>The question list</returns>
        public IEnumerable<QuestionDetails> GetAllQuestions()
        {
            return this.database.Fetch<Models.QuestionDetails>("ORDER BY LikeCount DESC").MapCollectionTo<QuestionDetails>();
        }

        /// <summary>
        /// Searches a question
        /// </summary>
        /// <param name="searchFilter">The search filter</param>
        /// <returns>The found questions</returns>
        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter searchFilter)
        {
            //check if searchInput is not null
            searchFilter.searchInput = searchFilter.searchInput ?? "";

            //query database for show values
            return this.database.FetchProc<Models.QuestionDetails>("SearchQuestion", new { userId = searchFilter.userId, keyword = searchFilter.searchInput, categoryId = searchFilter.categoryId, sortBy = (short)searchFilter.SortBy, show = (short)searchFilter.Show }).MapCollectionTo<QuestionDetails>();
        }

        /// <summary>
        /// Gets the questions found by user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The found questions</returns>
        public IEnumerable<QuestionDetails> FindQuestionsByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE AskedBy = @0", userId).MapCollectionTo<QuestionDetails>();
        }

        /// <summary>
        /// The questions answered by the user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The questions</returns>
        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId)
        {
            return this.database.Fetch<Models.QuestionDetails>("WHERE EXISTS(SELECT * FROM Answer a WHERE a.AnsweredBy = @0  AND a.QuestionId = QuestionDetails.QuestionId);", userId).MapCollectionTo<QuestionDetails>();
        }
    }
}

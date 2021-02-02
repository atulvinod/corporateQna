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
                var id = this.database.Insert(data);
                return (int)id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int questionId)
        {
            try
            {
                this.database.Delete(questionId);

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
                return this.database.Query<Models.QuestionDetails>(" SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] qd ORDER BY qd.LikeCount DESC").Select(s => this.mapper.Map<CorporateQnA.Models.QuestionDetails>(s));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Question question)
        {
            try
            {
                this.database.Update(this.mapper.Map<Models.Question>(question));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter search)
        {
            IEnumerable<Models.QuestionDetails> fetch;
            //check if searchInput is not null
            search.searchInput = search.searchInput ?? "";
            //query database for show values
            switch (search.Show)
            {
                case Show.MyQuestions:
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle) LIKE '%{search.searchInput.ToLower()}%' AND q.AskedBy = {search.userId}");
                    break;
                case Show.MyParticipation:
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle) LIKE '%{search.searchInput.ToLower()}%' AND EXISTS(SELECT 1 FROM Answer a WHERE a.QuestionId = q.QuestionId AND a.AnsweredBy = {search.userId})");
                    break;
                case Show.Solved:
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle)  LIKE '%{search.searchInput.ToLower()}%' AND q.Resolved > 0;");
                    break;
                case Show.Unsolved: 
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle)  LIKE '%{search.searchInput.ToLower()}%' AND q.Resolved = 0;");
                    break;
                case Show.Hot:
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle)  LIKE '%{search.searchInput.ToLower()}%' ORDER BY q.AskedOn DESC");
                    break;

                //all is default
                default:
                    fetch = this.database.Query<Models.QuestionDetails>($"SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE LOWER(q.QuestionTitle)  LIKE '%{search.searchInput.ToLower()}%'");
                    break;
            }

            return fetch.Where(x =>
            {
                bool select = true;
                //based on category id
                if (search.categoryId != 0)
                {
                    select = x.CategoryId == search.categoryId;
                }


                //sort by
                switch (search.SortBy)
                {
                    case SortBy.Last10Days:
                        select = x.AskedOn >= DateTime.Now.AddDays(-10) && select;
                        break;
                    case SortBy.Last30Days:
                        select = x.AskedOn >= DateTime.Now.AddDays(-30) && select;
                        break;
                    case SortBy.Recent:
                        select = x.AskedOn >= DateTime.Now.AddDays(-7) && select;
                        break;
                    default:
                        break;
                }

                return select;
            }).Select(x => this.mapper.Map<QuestionDetails>(x));
        }

        public IEnumerable<QuestionDetails> QuestionsByUser(int userId)
        {
            return this.database.Query<CorporateQnA.Models.QuestionDetails>("SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE q.AskedBy = @0", userId).Select(s => this.mapper.Map<CorporateQnA.Models.QuestionDetails>(s));
        }

        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId)
        {
            return this.database.Query<CorporateQnA.Models.QuestionDetails>("SELECT * FROM [CorporateQ&A].[dbo].[QuestionDetails] q WHERE EXISTS(SELECT * FROM Answer a WHERE a.AnsweredBy = @0  AND a.QuestionId = q.QuestionId);", userId).Select(s => this.mapper.Map<QuestionDetails>(s));
        }

        public void SetQuestionSolution(QuestionSolution solution)
        {
            var activity = this.database.Query<Models.QuestionActivity>("SELECT * FROM [CorporateQ&A].[dbo].[QuestionActivity] q WHERE q.QuestionId = @0 AND q.UserId = @1 AND q.ActivityType = 2",solution.QuestionId,solution.ResolvedBy).FirstOrDefault();

            if(activity == null)
            {
                var newActivity = new QuestionActivity
                {
                    ActivityType = QuestionActivityType.Resolved,
                    UserId = solution.ResolvedBy,
                    QuestionId = solution.QuestionId,
                    CreatedAt = DateTime.Now
                };
                this.database.Insert(this.mapper.Map<CorporateQnA.Services.Models.QuestionActivity>(newActivity));
                this.database.Execute("UPDATE Answer SET IsBestSolution = 1 WHERE Id = @0", solution.AnswerId);
            }
            else
            {
                this.database.Delete(activity);
                this.database.Execute("UPDATE Answer SET IsBestSolution = 0 WHERE Id = @0", solution.AnswerId);
            }
        }


    }
}

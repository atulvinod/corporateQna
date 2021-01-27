using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using Microsoft.Extensions.Configuration;
using AutoMapper;

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

        public IEnumerable<Question> GetQuestions()
        {
            throw new NotImplementedException();
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
    }
}

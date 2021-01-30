﻿using AutoMapper;
using CorporateQnA.Models;
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

        public IEnumerable<AnswerDetails> GetAnswersForQues(int questionId)
        {
            var answers = this.database.Query<CorporateQnA.Services.Models.AnswerDetails>("SELECT * FROM [AnswerDetails] WHERE QuestionId = @0", questionId).Select(s=>this.mapper.Map<CorporateQnA.Models.AnswerDetails>(s));
            return answers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Models;

namespace CorporateQnA.Services
{
    public interface IActivityService
    {
        public int CreateQuestionActivity(QuestionActivity question);

        public int CreateAnswerActivity(AnswerActivity answerActivity);

    }
}

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
        /// <summary>
        /// Creates question activity
        /// </summary>
        /// <param name="question">The question activity state</param>
        /// <returns>The identifier</returns>
        public int CreateQuestionActivity(QuestionActivity question);

        /// <summary>
        /// Creates answer activity
        /// </summary>
        /// <param name="answerActivity">The answer activity</param>
        /// <returns>The identifier</returns>
        public int CreateAnswerActivity(AnswerActivity answerActivity);

    }
}

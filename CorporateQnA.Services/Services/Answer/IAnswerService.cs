using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Models;
using CorporateQnA.Models.ViewModels;

namespace CorporateQnA.Services
{
    public interface IAnswerService
    {
        /// <summary>
        /// Creates an answer to a question
        /// </summary>
        /// <param name="answer">The answer model</param>
        /// <returns>The created answer identifier</returns>
        public int CreateAnswer(Answer answer);

        /// <summary>
        /// Gets the answers for a question
        /// </summary>
        /// <param name="getAnswer">The get answer view model</param>
        /// <returns>The answer details</returns>
        public IEnumerable<AnswerDetails> GetAnswersForQuestion(GetAnswerForQuestion getAnswer);

        /// <summary>
        /// Sets the answer as a solution for a question
        /// </summary>
        /// <param name="state">The answer state</param>
        /// <returns>The identifier</returns>
        public int SetAnswerAsSolution(AnswerAsSolution state);

    }
}

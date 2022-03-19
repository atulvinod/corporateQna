using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public interface IQuestionService
    {
        /// <summary>
        /// Creates a new question
        /// </summary>
        /// <param name="question">The question model</param>
        /// <returns>The question identifier</returns>
        public int CreateQuestion(Question question);

        /// <summary>
        /// Gets all the questions
        /// </summary>
        /// <returns>
        /// The question list
        /// </returns>
        public IEnumerable<QuestionDetails> GetAllQuestions();

        /// <summary>
        /// Searches question
        /// </summary>
        /// <param name="searchFilter">The applied filters</param>
        /// <returns>The questions found</returns>
        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter searchFilter);

        /// <summary>
        /// Gets the questions for a user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The question details</returns>
        public IEnumerable<QuestionDetails> FindQuestionsByUser(int userId);

        /// <summary>
        /// Gets the questions answered by user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The question details list</returns>
        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId);
    }
}

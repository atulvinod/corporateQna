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
        public int Create(Question question);

        public void Update(Question question);

        public void Delete(int questionId);

        public IEnumerable<QuestionDetails> GetQuestions();

        public IEnumerable<QuestionDetails> SearchQuestion(SearchFilter searchFilter);

        public IEnumerable<QuestionDetails> QuestionsByUser(int userId);

        public IEnumerable<QuestionDetails> QuestionsAnsweredByUser(int userId);

        public void SetQuestionSolution(QuestionSolution solution);
    }
}

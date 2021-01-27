using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Models;

namespace CorporateQnA.Services
{
    public interface IAnswerService
    {
        public int Create(Answer answer);

        public IEnumerable<Answer> GetAnswersForQues(string questionId);

    }
}

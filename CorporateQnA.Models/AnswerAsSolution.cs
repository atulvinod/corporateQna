using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class AnswerAsSolution
    {
        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public bool IsBestSolution { get; set; }
    }
}

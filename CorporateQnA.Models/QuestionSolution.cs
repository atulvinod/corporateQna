using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class QuestionSolution
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public int ResolvedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int AnsweredBy { get; set; }

        public int CategoryId { get; set; }

        public string Content { get; set; }

        public bool IsBestSolution { get; set; }

        public DateTime AnsweredOn { get; set; }
    }
}

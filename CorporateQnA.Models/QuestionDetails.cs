using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class QuestionDetails
    {
        public int QuestionId { get; set; }

        public string UserName { get; set; }

        public string QuestionTitle { get; set; }

        public string Content { get; set; }

        public int AskedBy { get; set; }

        public DateTime AskedOn { get; set; }

        public int CategoryId { get; set; }

        public int LikeCount { get; set; }

        public int ViewCount { get; set; }

        public int Resolved { get; set; }

        public int AnswerCount { get; set; }
    }
}

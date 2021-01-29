using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class AnswerDetails
    {
        public int AnswerId { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public string Content { get; set; }

        public int AnsweredBy { get; set; }

        public  DateTime AnsweredOn { get; set; }

        public int QuestionId { get; set; }

        public string UserName { get; set; }
    }
}

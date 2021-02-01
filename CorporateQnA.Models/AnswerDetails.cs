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
        
        public bool IsBestSolution { get; set; }

        public int QuestionId { get; set; }

        public string AnsweredByName { get; set; }

        public int AskedBy { get; set; }

        public bool LikedByUser { get; set; }

        public bool DislikedByUser { get; set; }
    }
}

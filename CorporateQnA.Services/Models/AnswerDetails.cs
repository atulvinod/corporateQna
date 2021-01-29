using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("AnswerDetails")]
    public class AnswerDetails
    {
        [Column]
        public int AnswerId { get; set; }

        [Column]
        public int LikeCount { get; set; }

        [Column]
        public int DislikeCount { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public int AnsweredBy { get; set; }

        [Column]
        public DateTime AnsweredOn { get; set; }

        [Column]
        public int QuestionId { get; set; }

        [Column]
        public string UserName { get; set; }
    }


}

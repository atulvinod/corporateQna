using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("QuestionDetails")]
    public class QuestionDetails
    {
        [Column]
        public int QuestionId { get; set; }

        [Column]
        public string UserName { get; set; }

        [Column]
        public string QuestionTitle { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public int AskedBy { get; set; }

        [Column]
        public DateTime AskedOn { get; set; }

        [Column]
        public int CategoryId { get; set; }

        [Column]
        public int LikeCount { get; set; }

        [Column]
        public int ViewCount { get; set; }
        
        [Column]
        public int AnswerCount { get; set; }


        [Column]
        public int Resolved { get; set; }
    }
}

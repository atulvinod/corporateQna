using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("Answer")]
    [PrimaryKey("Id")]
    public class Answer
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int AnsweredBy { get; set; }

        [Column]
        public int QuestionId { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public bool IsBestSolution { get; set; }

        [Column]
        public DateTime AnsweredOn { get; set; }
    }
}

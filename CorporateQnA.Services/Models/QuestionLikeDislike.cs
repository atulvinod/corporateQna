using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("QuestionLikeDislike")]
    [PrimaryKey("Id")]
    public class QuestionLikeDislike
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string UserId { get; set; }

        [Column]
        public string AnswerId { get; set; }

        [Column]
        public bool Value { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

        [Column]
        public DateTime ModifiedAt { get; set; }
    }
}

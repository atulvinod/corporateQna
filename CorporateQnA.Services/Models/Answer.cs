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
        public string UserId { get; set; }

        [Column]
        public int CategoryId { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public bool IsBestSolution { get; set; }

        [Column]
        public int LikeCount { get; set; }

        [Column]
        public int DislikeCount { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

        [Column]
        public DateTime ModifiedAt { get; set; }
    }
}

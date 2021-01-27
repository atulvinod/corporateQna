using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("Question")]
    [PrimaryKey("Id")]
    public class Question
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int CategoryId { get; set; }

        [Column]
        public string UserId { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public int ViewCount { get; set; }

        [Column]
        public int UpvoteCount { get; set; }

        [Column]
        public bool IsResolved { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

        [Column]
        public DateTime ModifiedAt { get; set; }
    }
}

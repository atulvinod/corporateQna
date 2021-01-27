using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Answer
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public string Content { get; set; }

        public string IsBestSolution { get; set; }

        public string LikeCount { get; set; }

        public string DislikeCount { get; set; }

        public string CreatedAt { get; set; }
    }
}

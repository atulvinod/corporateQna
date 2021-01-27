using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Question
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ViewCount { get; set; }

        public string TotalAnswerCount { get; set; }

        public string UpvoteCount { get; set; }

        public string IsResolved { get; set; }

        public string CreatedAt { get; set; }

    }
}

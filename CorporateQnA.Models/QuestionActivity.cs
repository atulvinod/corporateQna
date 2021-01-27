using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Models
{
    public class QuestionActivity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public int ActivityType{ get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

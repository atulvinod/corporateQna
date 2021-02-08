using CorporateQnA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{

    public class AnswerActivity
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public int AnswerId { get; set; }

        public ActivityTypes ActivityType { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class UserDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public int TotalLikes { get; set; }

        public int TotalDislikes { get; set; }

        public int QuestionAsked { get; set; }

        public int QuestionAnswered { get; set; }

        public int QuestionResolved { get; set; }
    }
}

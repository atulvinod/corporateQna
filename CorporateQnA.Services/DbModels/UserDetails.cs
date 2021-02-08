using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("UserDetails")]
    [PrimaryKey("Id")]
    public class UserDetails
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Department { get; set; }

        [Column]
        public string Location { get; set; }

        [Column]
        public string Position { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public int TotalLikes { get; set; }

        [Column]
        public int TotalDislikes { get; set; }

        [Column]
        public int QuestionAsked { get; set; }

        [Column]
        public int QuestionAnswered { get; set; }

        [Column]
        public int QuestionResolved { get; set; }
    }
}

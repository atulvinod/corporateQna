using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("QuestionActivity")]
    [PrimaryKey("Id")]
    public class QuestionActivity
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public int UserId { get; set; }

        [Column]
        public int QuestionId { get; set; }

        [Column]
        public short ActivityType { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }
    }
}

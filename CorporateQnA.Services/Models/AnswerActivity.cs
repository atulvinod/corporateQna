using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("AnswerActivity")]
    [PrimaryKey("Id")]
    public class AnswerActivity
    {

        [Column]
        public int Id { get; set; }

        [Column]
        public int UserId { get; set; }

        [Column]
        public int AnswerId { get; set; }

        [Column]
        public short ActivityType { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

    }
}

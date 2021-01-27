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
        public int AskedBy { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Content { get; set; }

        [Column]
        public DateTime AskedOn{ get; set; }
    }
}

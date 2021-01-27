using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int AskedBy { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime AskedOn { get; set; }

    }
}

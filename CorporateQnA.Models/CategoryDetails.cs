using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class CategoryDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ThisWeek { get; set; }

        public int ThisMonth { get; set; }

        public int Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TagsThisWeek { get; set; }

        public string TagsThisMonth { get; set; }

        public string TotalTags { get; set; }

        public string CreatedAt { get; set; }

    }
}

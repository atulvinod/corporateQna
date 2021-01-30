using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CorporateQnA.Models.Enums.SearchFilterTypes;

namespace CorporateQnA.Models
{
    public class SearchFilter
    {
        public string searchInput { get; set; }

        public int categoryId { get; set; }

        public Show  Show { get; set; }

        public SortBy SortBy { get; set; }

        public int userId { get; set; }
    }
}

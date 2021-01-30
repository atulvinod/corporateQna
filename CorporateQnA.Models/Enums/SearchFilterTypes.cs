using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models.Enums
{
    public class SearchFilterTypes
    {
        public enum Show
        {
            All,
            MyQuestions,
            MyParticipation,
            Hot,
            Solved,
            Unsolved
        }

        public enum SortBy
        {
            All,
            Recent,
            Last10Days,
            Last30Days,
        }
    }
}

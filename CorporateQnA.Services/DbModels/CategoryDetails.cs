using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("CategoryDetails")]
    [PrimaryKey("Id")]
    public class CategoryDetails
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }
        
        [Column]
        public int ThisWeek { get; set; }

        [Column]
        public int ThisMonth { get; set; }

        [Column]
        public int Total { get; set; }
    }
}

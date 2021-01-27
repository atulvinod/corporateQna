using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Services.Models
{
    [TableName("Category")]
    [PrimaryKey("id")]
    public class Category
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public int CreatedBy{ get; set; }

        [Column]
        public DateTime CreatedOn { get; set; }
    }
}

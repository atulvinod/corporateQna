using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace CorporateQnA.Models
{
    
    [TableName("Users")]
    [PrimaryKey("Id")]
    public class AppUser
    {
        [Column]
        public int Id { get; set; }
        
        [Column]
        public string Name { get; set; }
        
        [Column]
        public string Email { get; set; }
        
        [Column]
        public string Location { get; set; }

        [Column]
        public string Position { get; set; }

        [Column]
        public string Department { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monager.Database.Entities
{
    public class User
    {
        public User()
        {
            Entries = new HashSet<Entry>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LoginKey { get; set; }

        public ICollection<Entry> Entries { get; set; }
    }
}

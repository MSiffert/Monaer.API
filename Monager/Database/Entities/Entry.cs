using System;
using System.ComponentModel.DataAnnotations;

namespace Monager.Database.Entities
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

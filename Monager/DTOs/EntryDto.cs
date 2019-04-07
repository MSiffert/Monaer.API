using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Monager.Database;
using Monager.Database.Entities;

namespace Monager.DTOs
{
    public class EntryDto
    {
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

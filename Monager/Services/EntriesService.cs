using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Monager.Database;
using Monager.Database.Entities;
using Monager.DTOs;

namespace Monager.Services
{
    public class EntriesService : IEntriesService
    {
        private readonly MonagerDbContext _dbContext;

        public EntriesService(MonagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        public async Task<List<EntryDto>> GetEntries()
        {
            return await _dbContext.Entries.Select(e => new EntryDto
            {
                Category = e.Category,
                Id = e.Id,
                Price = e.Price,
                Timestamp = e.Timestamp,
                UserId = e.User.Id
            }).ToListAsync();
        }

        public async Task<EntryDto> CreateEntry(EntryDto entryDto)
        {
            var entry = new Entry
            {
                Category = entryDto.Category,
                Price = entryDto.Price,
                Timestamp = entryDto.Timestamp,
                UserId = entryDto.UserId
            };

            _dbContext.Add(entry);
            await _dbContext.SaveChangesAsync();

            entryDto.Id = entry.Id;
            return entryDto;
        }

        public async Task<EntryDto> UpdateEntry(EntryDto entryDto)
        {
            var entry = await _dbContext.Entries.FirstOrDefaultAsync(e => e.Id == entryDto.Id);
            entry.Category = entryDto.Category;
            entry.Price = entryDto.Price;
            entry.Timestamp = entryDto.Timestamp;

            await _dbContext.SaveChangesAsync();
            return entryDto;
        }

        public async Task DeleteEntry(int id)
        {
            var entry = await _dbContext.Entries.FirstOrDefaultAsync(e => e.Id == id);
            _dbContext.Remove(entry);
            await _dbContext.SaveChangesAsync();
        }
    }

    public interface IEntriesService
    {
        Task<List<EntryDto>> GetEntries();
        Task<EntryDto> CreateEntry(EntryDto entry);
        Task<EntryDto> UpdateEntry(EntryDto entry);
        Task DeleteEntry(int id);
    }
}

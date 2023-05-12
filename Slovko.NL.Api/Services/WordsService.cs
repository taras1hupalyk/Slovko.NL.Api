using Dapper;
using Microsoft.EntityFrameworkCore;
using Slovko.NL.Api.DataAccess;
using Slovko.NL.Api.Models;

namespace Slovko.NL.Api.Services
{
    public class WordsService
    {
        private readonly ApplicationDbContext _db;
        private readonly DapperContext _context;

        public WordsService(ApplicationDbContext db, DapperContext context)
        {
            _db = db;
            _context = context;
        }

        public async Task<IEnumerable<Word>> GetWords()
        {
            return await _context.Connection.QueryAsync<Word>("SELECT * FROM fiveletterwords");            
        }

        public async Task<Word> AddWord(Word word)
        {
            _db.FiveLetterWords.Add(word);
            await _db.SaveChangesAsync();
            return word;
        }

        public async Task<IEnumerable<Word>> ApplyFilter(LetterGroup[] lettersStates)
        {
            var filter = FilterGenerator.GenerateFilter(lettersStates);

            //order by entropy


            var result = await _context.Connection
                .QueryAsync<Word>(@$"SELECT * FROM fiveletterwords 
                                     WHERE value ~ '{filter.Item1}'
                                     ORDER BY entropy DESC");

            return result.Where(x => filter.Item2.All(y => x.Value.Contains(y)));
        }
    }
}

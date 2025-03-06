using Dapper;
using Slovko.NL.Api.DataAccess;
using Slovko.NL.Api.Models;

namespace Slovko.NL.Api.Services
{
    public class WordsService
    {
        private readonly DapperContext _context;

        public WordsService(DapperContext context)
        {
            _context = context;
            
        }
        

        public  IEnumerable<FiveLetterWord> GetWords()
        {
            _context.Connection.Open();
            var result = _context.Connection.Query<FiveLetterWord>("SELECT Id, Word, CAST(Entropy AS REAL) AS Entropy FROM FiveLetterWords");
                       
            return result;
        }


        public async Task<IEnumerable<FiveLetterWord>> ApplyFilter(LetterGroup[] lettersStates)
        {
            var filter = FilterGenerator.GenerateFilter(lettersStates);

            //order by entropy
           

            var result = await _context.Connection
                .QueryAsync<FiveLetterWord>(@$"SELECT Id, Word, CAST(Entropy AS REAL) AS Entropy FROM FiveLetterWords 
                                     WHERE Word REGEXP '{filter.Item1}'
                                     ORDER BY entropy DESC");

            return result.Where(x => filter.Item2.All(y => x.Word.Contains(y)));
        }
    }
}

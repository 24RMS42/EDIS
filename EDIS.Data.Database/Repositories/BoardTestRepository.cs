using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Boards;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class BoardTestRepository : Repository<BoardTest>, IBoardTestRepository
    {
        public async Task<BoardTest> GetBoard(string boardId, string certId)
        {
            var board = await Connection.Table<BoardTest>().Where(x => x.BoardId == boardId && x.CertId == certId).FirstOrDefaultAsync();
            if (board != null)
                return await Connection.GetWithChildrenAsync<BoardTest>(board.BoardTestId);
            return null;
        }

        public async Task UpdateBoard(BoardTest board)
        {
            var b = await Connection.Table<BoardTest>().Where(x => x.BoardId == board.BoardId && x.CertId == board.CertId).FirstOrDefaultAsync();
            if (b != null)
                await Connection.InsertOrReplaceWithChildrenAsync(board, true);
        }

        public async Task DeleteBoard(string boardId, string certId)
        {
            var board = await Connection.Table<BoardTest>().Where(x => x.BoardId == boardId && x.CertId == certId).FirstOrDefaultAsync();
            if (board != null)
            {
                var toDelete = await Connection.GetWithChildrenAsync<BoardTest>(board.BoardTestId);
                if (toDelete != null)
                    await Connection.DeleteAsync(toDelete, true);
            }
        }
    }
}
using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Boards;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        public async Task<Board> GetBoard(string boardId)
        {
            var cert = await Connection.Table<Board>().Where(x => x.BoardId == boardId).FirstOrDefaultAsync();
            if (cert != null)
                return await Connection.GetWithChildrenAsync<Board>(boardId);
            return null;
        }

        public async Task UpdateBoard(Board board)
        {
            var b = await Connection.Table<Board>().Where(x => x.BoardId == board.BoardId).FirstOrDefaultAsync();
            if (b != null)
                await Connection.InsertOrReplaceWithChildrenAsync(board, true);
        }

        public async Task DeleteBoard(string boardId)
        {
            var cert = await Connection.Table<Board>().Where(x => x.BoardId == boardId).FirstOrDefaultAsync();
            if (cert != null)
            {
                var toDelete = await Connection.GetWithChildrenAsync<Board>(boardId);
                if (toDelete != null)
                    await Connection.DeleteAsync(toDelete, true);
            }
        }
    }
}
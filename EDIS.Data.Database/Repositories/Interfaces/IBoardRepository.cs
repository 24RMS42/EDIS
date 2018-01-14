using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Boards;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface IBoardRepository : IRepository<Board>
    {
        Task<Board> GetBoard(string boardId);
        Task UpdateBoard(Board certificate);
        Task DeleteBoard(string boardId);
    }
}
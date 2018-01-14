using System.Threading.Tasks;
using EDIS.Data.Database.Repositories.Interfaces.Base;
using EDIS.Domain.Boards;

namespace EDIS.Data.Database.Repositories.Interfaces
{
    public interface IBoardTestRepository : IRepository<BoardTest>
    {
        Task<BoardTest> GetBoard(string boardId, string certId);
        Task UpdateBoard(BoardTest board);
    }
}
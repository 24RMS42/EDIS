using System.Collections.Generic;
using System.Threading.Tasks;
using EDIS.Domain;
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;

namespace EDIS.Service.Interfaces
{
    public interface IBoardsService
    {
        Task<ServiceResult> GetAllBoards(BoardsFilters filters, string buildingId, bool forceCloud = false);
        Task<ServiceResult> GetAllDownloadedBoards(string buildingId);
        Task<ServiceResult> GetBoardDetails(string boardIds, string buildingId, bool forceCloud = false);
        Task<ServiceResult> GetBoardTestDetails(string boardId, string certId, bool forceCloud = false);
        Task<ServiceResult> GetTestedCircuits(string certId, string boardId);
        Task<ServiceResult> GetTestedCircuit(string circuitTestId);
        Task<ServiceResult> UploadBoard(string boardId);
        Task<ServiceResult> GetTestedBoards(string certId);
        Task<ServiceResult> AssociateBoardWithCertificate(string boardId, string buildingId, string certId);
        Task<ServiceResult> RemoveTestedBoard(string boardId, string certId);
        Task<ServiceResult> AddNewCircuitForTest(string circuitId, string boardId, string certId);
        Task<ServiceResult> RemoveTestedCircuit(string circuitId, string boardId, string certId);
        Task<ServiceResult> GetCircuitPoints(string circuitId);
        Task<ServiceResult> SaveCircuitPoint(CircuitPointsRcdTest circuitPointsRcdTest, string circuitTestId, string estateId, bool isBiggerZs);
        Task<ServiceResult> DeleteCircuitPoint(string endPoint);
        Task<ServiceResult> SaveBoard(Board board);
        Task<ServiceResult> SaveBoardTest(BoardTest board);
        Task<ServiceResult> SaveCircuit(Circuit circuit);
        Task<ServiceResult> SaveCircuitTestWithoutObservation(CircuitTest circuit);
        Task<ServiceResult> SaveCircuitTest(CircuitTest circuit, string estateId, bool isBiggerZs);
        Task<ServiceResult> GetCertificateInspectionObservation(string circuitPointRcdTest);
    }
}
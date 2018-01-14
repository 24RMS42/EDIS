using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain;
using EDIS.Domain.Boards;
using EDIS.Domain.Certificates;
using EDIS.Domain.Circuits;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;
using EDIS.Shared.Models;

namespace EDIS.Service.Implementation
{
    public class BoardsService : BaseService, IBoardsService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IEstatesLookupsService _estatesLookupsService;

        public async Task<ServiceResult> GetAllBoards(BoardsFilters filters, string buildingId, bool forceCloud = false)
        {
            if (!forceCloud)
            {
                var boardsDb = await DbManager.BoardRowRepository.GetAll();
                if (boardsDb.Any())
                    return new SuccessServiceResult { ResultObject = new Tuple<IEnumerable<BoardRow>, int>(boardsDb, boardsDb.Count) };
            }

            var url = GlobalSettings.BaseURL + "/boards";

            var response = await _requestProvider.PostAsync<BoardsRequest, BoardsResponse>(url,
                new BoardsRequest
                {
                    Token = Settings.AccessToken,
                    BuildingId = buildingId,
                    Limit = new List<int>() { 0, 60 },
                    Filters = filters
                });


            if (response != null && response.Boards.Any())
            {
                if (forceCloud)
                    await DbManager.BoardRowRepository.DeleteAll();
                await DbManager.BoardRowRepository.AddMany(response.Boards);

                return new SuccessServiceResult { ResultObject = new Tuple<IEnumerable<BoardRow>, int, int>(response.Boards, response.BoardFound, response.BoardReturned) };
            }

            return new FalseServiceResult("Response is null");
        }

        public async Task<ServiceResult> GetAllDownloadedBoards(string buildingId)
        {
            var boards = await DbManager.BoardRepository.GetAll(x => x.BuildingId == buildingId);
            return new SuccessServiceResult { ResultObject = boards };
        }

        public async Task<ServiceResult> GetTestedCircuits(string certId, string boardId)
        {
            var circuitTest = await DbManager.CircuitTestRepository.GetAll(x => x.CertId == certId && x.BoardId == boardId);
            return new SuccessServiceResult { ResultObject = circuitTest };
        }

        public async Task<ServiceResult> GetTestedCircuit(string circuitTestId)
        {
            var circuit = await DbManager.CircuitTestRepository.GetCircuitTest(circuitTestId);
            return new SuccessServiceResult{ResultObject = circuit};
        }

        public async Task<ServiceResult> GetTestedBoards(string certId)
        {
            var boards = await DbManager.BoardTestRepository.GetAll(x => x.CertId == certId);
            return new SuccessServiceResult { ResultObject = boards };
        }

        public async Task<ServiceResult> AssociateBoardWithCertificate(string boardId, string buildingId, string certId)
        {
            var boardDetails = await GetBoardDetails(boardId, buildingId);

            var boardTest = AutoMapper.Mapper.Map<BoardTest>(boardDetails.ResultObject as Board);
            boardTest.CertId = certId;
            boardTest.BoardId = boardId;
            boardTest.BoardTestId = Guid.NewGuid().ToString();

            await DbManager.BoardTestRepository.Add(boardTest);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> RemoveTestedBoard(string boardId, string certId)
        {
            var board = await DbManager.BoardTestRepository.FindByQuery(x => x.BoardId == boardId && x.CertId == certId);
            await DbManager.BoardTestRepository.Delete(board);
            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> GetBoardDetails(string boardId, string buildingId, bool forceCloud = false)
        {
            var board = await DbManager.BoardRepository.GetBoard(boardId);
            if (board != null)
                return new SuccessServiceResult { ResultObject = board };

            var url = GlobalSettings.BaseURL + "/boards/detail";

            var response = await _requestProvider.PostAsync<BoardRequest, BoardResponse>(url,
                new BoardRequest
                {
                    Token = Settings.AccessToken,
                    BuildingId = buildingId,
                    BoardIds = new List<string> { boardId }
                });

            if (response.Boards.Any())
            {
                await DbManager.BoardRepository.AddMany(response.Boards);
                board = response.Boards.FirstOrDefault();
            }

            if (response.Circuits.Any())
            {
                await DbManager.CircuitRepository.AddMany(response.Circuits);
                if (board != null) board.Circuits = response.Circuits.ToList();
            }

            if (response.CircuitsPointsRcd.Any())
            {
                await DbManager.CircuitPointsRcdRepository.AddMany(response.CircuitsPointsRcd);
                if (board != null) board.CircuitsPointsRcd = response.CircuitsPointsRcd.ToList();
            }

            return new SuccessServiceResult { ResultObject = board };
        }

        public async Task<ServiceResult> GetBoardTestDetails(string boardId, string certId, bool forceCloud = false)
        {
            var board = await DbManager.BoardTestRepository.GetBoard(boardId, certId);
            if (board != null)
                return new SuccessServiceResult { ResultObject = board };

            return new FalseServiceResult("Error occured");
        }

        public async Task<ServiceResult> UploadBoard(string boardId)
        {
            var board = await DbManager.BoardRepository.GetBoard(boardId);
            if (board == null)
                return new FalseServiceResult("Error occured, Board doesn't exist!");

            var url = GlobalSettings.BaseURL + "/boards/upload";

            var response = await _requestProvider.PostAsync<UploadBoardRequest, UploadBoardResponse>(url,
                new UploadBoardRequest
                {
                    Token = Settings.AccessToken,
                    BuildingId = board.BuildingId,
                    Boards = new List<Board> { board },
                    Circuits = board.Circuits,
                    CircuitsPointsRcd = board.CircuitsPointsRcd
                });

            if (response != null)
            {
                return new SuccessServiceResult { ResultObject = response.Message };
            }

            return new FalseServiceResult("Error occured, response is null");
        }

        public async Task<ServiceResult> AddNewCircuitForTest(string circuitId, string boardId, string certId)
        {
            var circuit = await DbManager.CircuitRepository.FindByQuery(x => x.CircuitId == circuitId);

            var circuitTest = AutoMapper.Mapper.Map<CircuitTest>(circuit);
            circuitTest.CertId = certId;
            circuitTest.BoardId = boardId;
            circuitTest.CircuitId = circuitId;
            circuitTest.CircuitTestId = Guid.NewGuid().ToString();

            await DbManager.CircuitTestRepository.Add(circuitTest);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> RemoveTestedCircuit(string circuitId, string boardId, string certId)
        {
            var circuit = await DbManager.CircuitTestRepository.FindByQuery(x => x.CircuitId == circuitId && x.BoardId == boardId && x.CertId == certId);
            await DbManager.CircuitTestRepository.Delete(circuit);
            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> GetCircuitPoints(string circuitId)
        {
            var circuits = await DbManager.CircuitPointsRcdTestRepository.GetCircuitTestedEndPoints(circuitId);

            foreach (var circuit in circuits)
            {
                var obs = await GetCertificateInspectionObservation(circuit.CprTestId);
                var inspObs = obs?.ResultObject as CertificateInspectionObservations;
                if (inspObs == null) continue;
                circuit.ObsFromId = inspObs.ObsFromId;
                circuit.DbcctClassificationCode = inspObs.CertInspectionObsItemStatus;
                circuit.DbcctObservation = inspObs.CertInspectionObsItemObservation;
            }

            return new SuccessServiceResult { ResultObject = circuits };
        }

        public async Task<ServiceResult> GetCertificateInspectionObservation(string circuitPointRcdTest)
        {
            var certInspObs = await DbManager.CertificateInspectionObservationsRepository.FindByQuery(x => x.CprTestId == circuitPointRcdTest);
            return new SuccessServiceResult{ResultObject = certInspObs};
        }

        public async Task<ServiceResult> DeleteCircuitPoint(string endPoint)
        {
            await DbManager.CircuitPointsRcdTestRepository.DeleteCircuitTestedEndPoint(endPoint);
            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveBoard(Board board)
        {
            var b = await DbManager.BoardRepository.GetBoard(board.BoardId);

            await DbManager.BoardRepository.UpdateBoard(board);

            b = await DbManager.BoardRepository.GetBoard(board.BoardId);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveBoardTest(BoardTest board)
        {
            var b = await DbManager.BoardTestRepository.GetBoard(board.BoardId, board.CertId);

            await DbManager.BoardTestRepository.UpdateBoard(board);

            b = await DbManager.BoardTestRepository.GetBoard(board.BoardId, board.CertId);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveCircuit(Circuit circuit)
        {
            var b = await DbManager.CircuitRepository.GetCircuit(circuit.CircuitId);

            await DbManager.CircuitRepository.UpdateCircuit(circuit);

            b = await DbManager.CircuitRepository.GetCircuit(circuit.CircuitId);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveCircuitPoint(CircuitPointsRcdTest circuitPointsRcdTest, string circuitTestId, string estateId, bool isBiggerZs)
        {
            var test = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CprTestId == circuitPointsRcdTest.CprTestId);

            var certInspObs = await DbManager.CertificateInspectionObservationsRepository.FindByQuery(x => x.CprTestId == circuitPointsRcdTest.CprTestId);
            var all = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CertId == circuitPointsRcdTest.CertId);
            var itemNo = all.Select(x => x.CertInspectionObsItemNo).OrderByDescending(x => x).ToList().FirstOrDefault();

            var obs = await _estatesLookupsService.GetObservationFromLookups(estateId);
            var defaultObs = obs.FirstOrDefault(x => x.DefaultObservation == 1);
            if (defaultObs == null) return new FalseServiceResult("Default observation doesn't exist!");

            if (certInspObs == null)
            {
                certInspObs = new CertificateInspectionObservations();
                certInspObs.CertInspectionObsId = Guid.NewGuid().ToString();
                certInspObs.CertId = circuitPointsRcdTest.CertId;
                certInspObs.BoardId = circuitPointsRcdTest.BoardId;
                certInspObs.CircuitTestId = circuitTestId;
                certInspObs.CprTestId = circuitPointsRcdTest.CprTestId;
                certInspObs.CertInspectionObsItemNo = ++itemNo;
            }

            certInspObs.ObservationType = 4;
            certInspObs.ItemClosed = "0";
            certInspObs.ItemDate = DateTime.UtcNow.ToString();

            if (isBiggerZs)
            {
                certInspObs.ObsFromId = defaultObs.ObsFromId;
                certInspObs.CertInspectionObsItemStatus = defaultObs.ObsCatCode;
                certInspObs.CertInspectionObsItemObservation = defaultObs.ObsFromTitle;
                certInspObs.ItemStatus = defaultObs.ObsFromStatus;
            }
            else
            {
                certInspObs.ObsFromId = circuitPointsRcdTest.ObsFromId;
                certInspObs.CertInspectionObsItemStatus = circuitPointsRcdTest.DbcctClassificationCode;
                certInspObs.CertInspectionObsItemObservation = circuitPointsRcdTest.DbcctObservation;
            }

            if (!string.IsNullOrEmpty(certInspObs.ObsFromId) || !string.IsNullOrEmpty(certInspObs.CertInspectionObsItemStatus))
                await DbManager.CertificateInspectionObservationsRepository.UpdateCertificateInspectionObservations(certInspObs);

            await DbManager.CircuitPointsRcdTestRepository.UpdateCircuitTestedEndPoint(circuitPointsRcdTest);

            test = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CprTestId == circuitPointsRcdTest.CprTestId);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveCircuitTestWithoutObservation(CircuitTest circuit)
        {
            await DbManager.CircuitTestRepository.UpdateCircuit(circuit);
            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveCircuitTest(CircuitTest circuit, string estateId, bool isBiggerZs)
        {
            var test = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CircuitTestId == circuit.CircuitTestId);

            var certInspObs = await DbManager.CertificateInspectionObservationsRepository.FindByQuery(x => x.CircuitTestId == circuit.CircuitTestId);
            var all = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CertId == circuit.CertId);
            var itemNo = all.Select(x => x.CertInspectionObsItemNo).OrderByDescending(x => x).ToList().FirstOrDefault();

            var obs = await _estatesLookupsService.GetObservationFromLookups(estateId);
            var defaultObs = obs.FirstOrDefault(x => x.DefaultObservation == 1);
            if (defaultObs == null) return new FalseServiceResult("Default observation doesn't exist!");

            if (certInspObs == null)
            {
                certInspObs = new CertificateInspectionObservations();
                certInspObs.CertInspectionObsId = Guid.NewGuid().ToString();
                certInspObs.CertId = circuit.CertId;
                certInspObs.BoardId = circuit.BoardId;
                certInspObs.CircuitTestId = circuit.CircuitTestId;
                certInspObs.CertInspectionObsItemNo = ++itemNo;
            }
            
            certInspObs.ObservationType = 2;
            certInspObs.ItemClosed = "0";
            certInspObs.ItemDate = DateTime.UtcNow.ToString();

            if (isBiggerZs)
            {
                certInspObs.ObsFromId = defaultObs.ObsFromId;
                certInspObs.CertInspectionObsItemStatus = defaultObs.ObsCatCode;
                certInspObs.CertInspectionObsItemObservation = defaultObs.ObsFromTitle;
                certInspObs.ItemStatus = defaultObs.ObsFromStatus;
            }
            else
            {
                certInspObs.ObsFromId = circuit.ObsFromId;
                certInspObs.CertInspectionObsItemStatus = circuit.DbcctClassificationCode;
                certInspObs.CertInspectionObsItemObservation = circuit.DbcctObservation;
            }
            
            await DbManager.CertificateInspectionObservationsRepository.UpdateCertificateInspectionObservations(certInspObs);

            test = await DbManager.CertificateInspectionObservationsRepository.GetAll(x => x.CircuitTestId == circuit.CircuitTestId);

            await DbManager.CircuitTestRepository.UpdateCircuit(circuit);

            return new SuccessServiceResult();
        }

        public BoardsService(IRequestProvider requestProvider, IEstatesLookupsService estatesLookupsService)
        {
            _requestProvider = requestProvider;
            _estatesLookupsService = estatesLookupsService;
        }
    }
}
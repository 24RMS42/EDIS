using EDIS.Data.Database.DatabaseManager;
using EDIS.Domain.Boards;
using EDIS.Service.Mapper;

namespace EDIS.Service.Base
{
    public class BaseService
    {
        protected IDatabaseManager DbManager;

        public BaseService()
        {
            DbManager = new DatabaseManager();

            var boardProfile = new BoardProfile();
            boardProfile.RegisterMappings();
        }
    }
}
using EDIS.Domain.Boards;
using EDIS.Domain.Circuits;

namespace EDIS.Service.Mapper
{
    public class BoardProfile
    {
        public void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Board, BoardTest>();

                cfg.CreateMap<BoardTest, Board>();

                cfg.CreateMap<Circuit, CircuitTest>();

                cfg.CreateMap<CircuitTest, Circuit>();
            });
        }
    }
}
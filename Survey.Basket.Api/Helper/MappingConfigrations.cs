using Mapster;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Helper
{
    public class MappingConfigrations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollDto>()
                 .Map(dest => dest.Summary, src => src.Summary)
                 .Map(dest => dest.Titel, src => src.Titel)
                 .TwoWays();
        }
    }
}

using AutoMapper;
using ProductivityTracker.Client.Models;
using ProductivityTracker.Common;

namespace ProductivityTracker.Client
{
    public class ObjectMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<ClientDto, ClientModel>();
            Mapper.CreateMap<IndustryDto, IndustryModel>();
            Mapper.CreateMap<PositionDto, PositionModel>();
        }
    }
}
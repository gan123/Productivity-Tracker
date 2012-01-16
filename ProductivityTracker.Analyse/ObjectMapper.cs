using AutoMapper;
using ProductivityTracker.Analyse.Models;
using ProductivityTracker.Common;

namespace ProductivityTracker.Analyse
{
    public class ObjectMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<ProductivityDto, ProductivityModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(p => new StatusModel {Id = p.StatusId, Name = p.StatusName}));
            Mapper.CreateMap<StatusDto, StatusModel>();
            Mapper.CreateMap<CommentDto, CommentModel>();
        }
    }
}

using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Productivity.Data.Models;

namespace ProductivityTracker.Productivity.Data
{
    public class ObjectMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<StatusDto, StatusModel>();
            Mapper.CreateMap<RecruiterDto, RecruiterModel>();
        }
    }
}
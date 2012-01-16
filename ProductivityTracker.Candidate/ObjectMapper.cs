using AutoMapper;
using ProductivityTracker.Candidate.Models;
using ProductivityTracker.Common;

namespace ProductivityTracker.Candidate
{
    public class ObjectMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<CandidateDto, CandidateModel>();
        }
    }
}
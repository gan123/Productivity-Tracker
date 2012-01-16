using System;
using System.Globalization;
using AutoMapper;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.Mappings
{
    public class ObjectMapper
    {
        public static void Initalise()
        {
            Mapper.CreateMap<Domain.Model.EntityBase, EntityDto>();

            Mapper.CreateMap<Domain.Model.EntityBase, EntityDto>()
                .Include<Domain.Model.User, UserDto>()
                .Include<Domain.Model.Client, ClientDto>()
                .Include<Domain.Model.Position, PositionDto>()
                .Include<Domain.Model.Industry, IndustryDto>()
                .Include<Domain.Model.PositionCovered, PositionCoveredDto>()
                .Include<Domain.Model.Productivity, ClientProductivitySummaryDto>()
                .Include<Domain.Model.Productivity, ProductivityDto>()
                .Include<Domain.Model.Candidate, CandidateDto>()
                .Include<Domain.Model.Status, StatusDto>();
            Mapper.CreateMap<Domain.Model.User, UserDto>();

            Mapper.CreateMap<Domain.Model.User, UserDto>()
                .Include<Domain.Model.Recruiter, RecruiterDto>();

            Mapper.CreateMap<Domain.Model.Recruiter, RecruiterDto>();
            Mapper.CreateMap<Domain.Model.Position, PositionDto>();
            Mapper.CreateMap<Domain.Model.Client, ClientDto>();
            Mapper.CreateMap<Domain.Model.Industry, IndustryDto>();
            Mapper.CreateMap<Domain.Model.PositionCovered, PositionCoveredDto>();
            Mapper.CreateMap<Domain.Model.Productivity, ClientProductivitySummaryDto>();
            Mapper.CreateMap<Domain.Model.Candidate, CandidateDto>();
            Mapper.CreateMap<Domain.Model.Status, StatusDto>();
            Mapper.CreateMap<Domain.Model.Comment, CommentDto>();
            Mapper.CreateMap<Domain.Model.Productivity, ProductivityDto>()
                .ForMember(dest => dest.Month, p => p.MapFrom(opt => GetMonth(opt.DateSent)))
                .ForMember(dest => dest.Week, p => p.MapFrom(opt => GetWeekOfMonth(opt.DateSent)));
        }

        private static string GetMonth(DateTime? date)
        {
            return date.Value.ToString("MMM-yy");
        }

        public static string GetWeekOfMonth(DateTime? date)
        {
            var day = date.Value.Day;
            const string format = "Week {0}";
            if (day >= 1 && day <= 7) return string.Format(format, 1);
            if (day >= 8 && day <= 14) return string.Format(format, 2);
            if (day >= 15 && day <= 28) return string.Format(format, 3);
            return string.Format(format, 4);
        }
    }
}

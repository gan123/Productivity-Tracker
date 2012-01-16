using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using ProductivityTracker.Common;
using ProductivityTracker.Recruiter.Models;

namespace ProductivityTracker.Recruiter
{
    public class ObjectMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<RecruiterDto, RecruiterModel>().ConstructUsing(r => new RecruiterModel(ServiceLocator.Current.GetInstance<IAsyncRequestDispatcherFactory>()));
        }
    }
}
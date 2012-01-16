using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Industry;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class RemoveIndustryHandler : RavenRequestHandler<RemoveIndustryRequest, RemoveIndustryResponse>
    {
        private readonly IIndustryService _industryService;

        public RemoveIndustryHandler(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        public override Response Handle(RemoveIndustryRequest request)
        {
            _industryService.Remove(request.Id);
            return CreateDefaultResponse();
        }
    }
}
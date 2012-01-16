using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Industry;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddIndustryHandler : RavenRequestHandler<AddIndustryRequest, AddIndustryResponse>
    {
        private readonly IIndustryService _industryService;

        public AddIndustryHandler(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        public override Response Handle(AddIndustryRequest request)
        {
            _industryService.Add(request.Name);
            return CreateDefaultResponse();
        }
    }
}
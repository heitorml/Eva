using Eva.Infrastructure.InternalSerices.Eligibility.Models;

namespace Eva.Infrastructure.InternalSerices.Eligibility
{
    public interface IEligibilityService
    {
        public Task<HttpResponseMessage> GetEligibilityAsync(EligibilityRequest request);
    }
}

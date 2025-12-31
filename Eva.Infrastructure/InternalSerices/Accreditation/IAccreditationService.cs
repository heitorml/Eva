using Eva.Infrastructure.InternalSerices.Accreditation.Models;

namespace Eva.Infrastructure.InternalSerices.Accreditation
{
    public interface IAccreditationService
    {
        public Task<HttpResponseMessage> Request(RequestAccreditation request);
    }
}

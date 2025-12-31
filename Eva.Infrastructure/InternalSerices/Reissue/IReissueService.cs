using Eva.Infrastructure.InternalSerices.Reissue.Models;

namespace Eva.Infrastructure.InternalSerices.Reissue
{
    public interface IReissueService
    {
        Task<string> Confirm(string id);
         Task<string> Request(RequestReissue solicitacao);
    }
}

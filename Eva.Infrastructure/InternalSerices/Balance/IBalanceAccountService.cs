using Eva.Infrastructure.InternalSerices.Balance.Models;

namespace Eva.Infrastructure.InternalSerices.Balance
{
    public interface IBalanceAccountService
    {
        public Task<ResponseBalance> GetSaldo(string cardNumber);
    }
}

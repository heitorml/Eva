using Eva.Infrastructure.InternalSerices.SearchCard.Models;

namespace Eva.Infrastructure.InternalSerices.SearchCard
{
    public interface ISearchCardsService
    {
        Task<ResponseSearchCard> SearchCardsAsync(RequestSearchCard request);
    }
}

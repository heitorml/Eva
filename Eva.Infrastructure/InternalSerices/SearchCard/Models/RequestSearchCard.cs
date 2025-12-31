namespace Eva.Infrastructure.InternalSerices.SearchCard.Models
{
    public class RequestSearchCard
    {
        public string cpf { get; set; }
        public string dataNascimento { get; set; }
        public string produto { get; set; }
        public string origemSolicitacao { get; set; }
        public string idUnicoSolicitacao { get; set; }
    }
}

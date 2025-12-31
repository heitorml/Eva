namespace Eva.Infrastructure.InternalSerices.Reissue.Models
{
    public class RequestReissue
    {
        public string? Cpf { get; set; }
        public string? Matricula { get; set; }
        public string? Nome { get; set; }
        public string? DataNascimento { get; set; }
        public string? Motivo { get; set; }
        public string? NumeroCartao { get; set; }
        public string Produto { get; set; }
    }

}

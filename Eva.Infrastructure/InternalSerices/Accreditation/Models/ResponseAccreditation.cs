namespace Eva.Infrastructure.InternalSerices.Accreditation.Models
{
    public class ResponseAccreditation
    {
        public string id { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class ResponseErrorsAccreditation
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string detail { get; set; }
        public string instance { get; set; }
        public string additionalProp1 { get; set; }
        public string additionalProp2 { get; set; }
        public string additionalProp3 { get; set; }
    }
}

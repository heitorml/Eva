namespace Eva.Infrastructure.InternalSerices.Accreditation.Models
{
    public class RequestAccreditation
    {
        public string documentNumber { get; set; }
        public string uuid { get; set; }
        public bool serparPromotional { get; set; }
        public Contact contact { get; set; }
        public Bankdata bankData { get; set; }
        public Affiliation[] affiliations { get; set; }
    }
    public class Contact
    {
        public string documentNumber { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class Bankdata
    {
        public string name { get; set; }
        public string account { get; set; }
        public string agency { get; set; }
        public string code { get; set; }
    }

    public class Affiliation
    {
        public string acquirerCode { get; set; }
        public string pv { get; set; }
        public string ecVan { get; set; }
    }
}

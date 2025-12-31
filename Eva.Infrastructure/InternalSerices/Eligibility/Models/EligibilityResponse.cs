namespace Eva.Infrastructure.InternalSerices.Eligibility.Models
{
    public class EligibilityResponse
    {
        public string id { get; set; }
        public string documentNumber { get; set; }
        public Status[] status { get; set; }
        public Eligibleproduct[] eligibleProducts { get; set; }
        public Registeredproduct[] registeredProducts { get; set; }
        public string companyName { get; set; }
    }
    public class Status
    {
        public DateTime dateRegister { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public bool current { get; set; }
    }
    public class Eligibleproduct
    {
        public string acronyms { get; set; }
        public string code { get; set; }
        public Businesscondition businessCondition { get; set; }
    }
    public class Registeredproduct
    {
        public string acronyms { get; set; }
        public string code { get; set; }
        public Businesscondition businessCondition { get; set; }
    }
    public class Businesscondition
    {
        public float administrationFee { get; set; }
        public float paymentManagementFee { get; set; }
        public float transactionFee { get; set; }
        public int joinFee { get; set; }
        public bool annualFee { get; set; }
        public int closeDay { get; set; }
        public int refundPeriod { get; set; }
    }
    public class Errors
    {
        public string application { get; set; }
        public string[] messages { get; set; }
    }
}

namespace Eva.Infrastructure.InternalSerices.Reissue.Models
{

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ResponseReissue
    {
        public List<Link> Links { get; set; }
    }

}

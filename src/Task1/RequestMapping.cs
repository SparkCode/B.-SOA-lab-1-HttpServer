namespace Serialization
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class RequestMapping : System.Attribute
    {
        public string Method { set; get; }

        public RequestMapping()
        {
            Method = "GET";
        }
    }
}

namespace Serialization
{
    public class HttpQuery
    {
        private readonly string _resource;
        private readonly string _method;

        public HttpQuery(string resource, string method)
        {
            _resource = resource;
            _method = method;
        }
        
        private bool Equals(HttpQuery other) => string.Equals(_resource, other._resource) && string.Equals(_method, other._method);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HttpQuery) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_resource?.GetHashCode() ?? 0)*397) ^ (_method?.GetHashCode() ?? 0);
            }
        }
    }
}

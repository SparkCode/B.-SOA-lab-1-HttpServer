using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serialization
{
    public static class RequestMappingAttributeProcessor
    {
        public static IDictionary<HttpQuery, MethodInfo> GetRequestMap(object obj)
        {
            var requestMap = new Dictionary<HttpQuery, MethodInfo>();
            var methods = obj.GetType().GetMethods();
            foreach (var method in methods) 
            {
                var attribute = method.GetCustomAttributes(true).OfType<RequestMapping>().FirstOrDefault();
                if (attribute == null) continue;
                var name = method.Name;
                var httpMethod = attribute.Method;
                var httpQuery = new HttpQuery(name, httpMethod);
                requestMap.Add(httpQuery, method);
            }
            return requestMap;
        }
    }
}

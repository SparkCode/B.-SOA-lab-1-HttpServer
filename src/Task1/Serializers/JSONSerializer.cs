using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Serialization
{
    class JsonSerializer<T> : IStringSerializer<T>
    {
        public string Serialize(T obj)
        {
            var serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(obj);

            return result;
        }

        public T Deserialize(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
    }
}

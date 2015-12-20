using System;

namespace Serialization
{
    interface IStringSerializer<T> : ISerializer<T,String>, IDeserializer<String, T>
    {
    }
}

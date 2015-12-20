namespace Serialization
{
    interface IDeserializer<T, R>
    {
        R Deserialize(T obj);
    }
}

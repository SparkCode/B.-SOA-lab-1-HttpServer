namespace Serialization
{
    interface ISerializer<T, R>
    {
        R Serialize(T obj);
    }
}

namespace Serialization
{
    public class StorageService<T>
    {
        public T Data { set; get; }

        public void Save(T data)
        {
            Data = data;
        }
    }
}

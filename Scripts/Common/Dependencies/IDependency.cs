namespace Racing
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }
}
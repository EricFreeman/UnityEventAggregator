namespace UnityEventAggregator
{
    public interface IListener<T> where T : struct
    {
        void Handle(T message);
    }
}
namespace JainEventAggregator
{
    public interface IListener<T>
    {

        void Handle(T message);
    }
}

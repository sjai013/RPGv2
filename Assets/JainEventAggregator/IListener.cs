namespace JainEventAggregator
{
    public interface IListener<in T>
    {

        void Handle(T message);
    }
}

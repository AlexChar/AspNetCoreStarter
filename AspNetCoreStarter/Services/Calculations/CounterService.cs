namespace AspNetCoreStarter.Services.Calculations
{
    public interface ICounterService<T>
    {
        void Increment();
        T GetValue();
    }

    public class CounterService : ICounterService<int>
    {
        private readonly ICounterStore<int> _counterStore;

        public CounterService(ICounterStore<int> counterStore)
        {
            _counterStore = counterStore;
        }

        public void Increment()
        {
            _counterStore.Value++;
        }

        public int GetValue() => _counterStore.Value;
    }

    public interface ICounterStore<T>
    {
        T Value { get; set; }
    }

    public class CounterStore : ICounterStore<int>
    {
        public int Value { get; set; }
    }

    
}

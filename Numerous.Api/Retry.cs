namespace Numerous.Api
{
    public struct Retry
    {
        public int Count;

        public static readonly Retry Infinite = new Retry {Count = int.MaxValue};
        public static readonly Retry Never = new Retry {Count = 0};

        public static implicit operator int(Retry value)
        {
            return value.Count;
        }

        public static implicit operator Retry(int value)
        {
            return new Retry { Count = value };
        }
    }
}
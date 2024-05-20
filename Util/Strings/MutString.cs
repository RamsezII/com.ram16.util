namespace _UTIL_
{
    public class MutString : ThreadSafe<object>
    {
        public override string ToString() => Value.ToString();
    }
}